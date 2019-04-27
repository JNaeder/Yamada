using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayerHolder : MonoBehaviour
{
    

    Transform playerTrans;
    Vector3 startScale;
    

   


    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
    }

    


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerTrans = collision.transform;
            playerTrans.parent = transform;
            Vector3 newScale = new Vector3(1 / startScale.x, 1 / startScale.y, 1);
            playerTrans.localScale = newScale;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerTrans.parent = null;

        }
    }
}
