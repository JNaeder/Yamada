using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcController : MonoBehaviour
{

    public PostProcessVolume pPVolume;

    Grain grainLayer;
    ColorGrading colorGradeLayer;
    Vignette vignetteLayer;
    ChromaticAberration chromeLayer;


    Movement_1 player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Movement_1>();

        pPVolume.profile.TryGetSettings(out grainLayer);
        pPVolume.profile.TryGetSettings(out colorGradeLayer);
        pPVolume.profile.TryGetSettings(out vignetteLayer);
        pPVolume.profile.TryGetSettings(out chromeLayer);



    }

    // Update is called once per frame
    void Update()
    {
        float radPerc = player.radLevel / player.maxRadLevel;
        vignetteLayer.intensity.value = Remap(radPerc, 0,0.18f,1,0.48f);
        colorGradeLayer.saturation.value = Remap(radPerc, 0f, -12f, 1f, -60f);
        grainLayer.intensity.value = Remap(radPerc, 0f, 0.216f, 1f, 1f);
        // Debug.Log("Orginal: " + radPerc + " New Value: " + Remap(radPerc, 0, 0.18f, 1, .48f));

        if (player.isSpecialMove)
        {
            chromeLayer.enabled.value = true;
           // colorGradeLayer.saturation.value = -100f;

        }
        else {

            chromeLayer.enabled.value = false;
            colorGradeLayer.saturation.value = -12f; ;
        }

        
    }



    float Remap(float thisNum, float inputA, float inputB, float outputA, float outputB) {


        return (thisNum - inputA) / (outputA - inputA) * (outputB - inputB) + inputB;
    }
}
