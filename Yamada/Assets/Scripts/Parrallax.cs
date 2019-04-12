using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrallax : MonoBehaviour
{

    public Transform[] backgrounds;
    float[] parrallaxScale;
    public float smoothing = 1;

    Transform cam;
    Vector3 previousCamPos;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;

        parrallaxScale = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++) {
            parrallaxScale[i] = backgrounds[i].position.z;
          }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++) {
            float parrallax = (previousCamPos.x - cam.position.x) * parrallaxScale[i];

            float backgroundTargetPosX = backgrounds[i].position.x + parrallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

            previousCamPos = cam.position;
          }

    }
}
