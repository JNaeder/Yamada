using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealingPool : MonoBehaviour
{

    public float recoverAmount = 200;
    public int numberOfKeys = 3;
    public bool canRecover;
    public bool keyPressGameOn;
    public GameObject recoverKeyImage;

    public Sprite[] comboKeysUp, comboKeysPressed;
    public SpriteRenderer[] comboKeysHolder;
    KeyCode[] comboKeyCodes;

    Movement_1 playerGirl;



    // Start is called before the first frame update
    void Start()
    {
        recoverKeyImage.SetActive(false);
        playerGirl = FindObjectOfType<Movement_1>();


        foreach(SpriteRenderer t in comboKeysHolder) {
            t.gameObject.SetActive(false);
          }

    }

    // Update is called once per frame
    void Update()
    {
        if (canRecover) {
           
            if (Input.GetButtonDown("Recover")) {
                //playerGirl.Recover(recoverAmount);
                GenerateComboKeys();
                keyPressGameOn = true;
                canRecover = false;
            }  

        }


        if (playerGirl.radLevel <= 0) {
            recoverKeyImage.SetActive(false);
        }



        if (keyPressGameOn) {
            int currentKeyComboNum = 0;
         
          
            }


    }




    void GenerateComboKeys() {
        playerGirl.isMoveable = false;


        recoverKeyImage.SetActive(false);
        comboKeysHolder[0].sprite = comboKeysUp[Random.Range(0, comboKeysUp.Length)];
        comboKeysHolder[1].sprite = comboKeysUp[Random.Range(0, comboKeysUp.Length)];
        comboKeysHolder[2].sprite = comboKeysUp[Random.Range(0, comboKeysUp.Length)];
        foreach(SpriteRenderer t in comboKeysHolder) {
            t.gameObject.SetActive(true);
         
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
            keyPressGameOn = false;
            recoverKeyImage.SetActive(false);
        }
    }
}
