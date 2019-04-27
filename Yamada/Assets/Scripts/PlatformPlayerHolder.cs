using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayerHolder : MonoBehaviour
{
    

    Transform playerTrans;
    MovingPlatform mP;

    bool isOnPlatform;

    Vector2 offset;


    // Start is called before the first frame update
    void Start()
    {
        mP = GetComponentInParent<MovingPlatform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnPlatform) {
            Vector2 newPos = new Vector2(0, 0);

            //playerTrans.position = newPos;
                }



    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           playerTrans = collision.gameObject.transform;
            playerTrans.position += transform.position;
            Debug.Log(offset);
            isOnPlatform = true;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isOnPlatform = false;

        }
    }
}
