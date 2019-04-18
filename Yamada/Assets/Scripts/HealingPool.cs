using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealingPool : MonoBehaviour
{

    public float recoverAmount = 200;
    public bool canRecover;
    public GameObject recoverKeyImage;

    float startPlayerRadLevel;

    Movement_1 playerGirl;



    // Start is called before the first frame update
    void Start()
    {
        recoverKeyImage.SetActive(false);
        playerGirl = FindObjectOfType<Movement_1>();

        startPlayerRadLevel = playerGirl.radLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (canRecover) {
           
            if (Input.GetButtonDown("Recover")) {
                playerGirl.Recover(recoverAmount);
            }  

        }


        if (playerGirl.radLevel <= 0) {
            recoverKeyImage.SetActive(false);
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerGirl.radLevel > 0)
        {
            canRecover = true;
            recoverKeyImage.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canRecover = false;
            recoverKeyImage.SetActive(false);
        }
    }
}
