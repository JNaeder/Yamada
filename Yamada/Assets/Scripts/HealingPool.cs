using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealingPool : MonoBehaviour
{

    public float recoverAmount = 200;
    public int numberOfKeys = 3;
    public float timeToHitNextKeyLimit = 5.0f;
    public float currentTime;

    public bool canRecover;
    public bool keyPressGameOn;
    public GameObject recoverKeyImage;

    public Sprite[] comboKeysUp, comboKeysPressed;
    public SpriteRenderer[] comboKeysHolder;

    KeyCode[] KeysToHit;
    KeyCode[] comboKeyCodes = {KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.UpArrow};
    Sprite[] pressedSprites;

    Movement_1 playerGirl;

    int currentKeyComboNum = 0;


    // Start is called before the first frame update
    void Start()
    {

        KeysToHit = new KeyCode[3];
        pressedSprites = new Sprite[3];
        recoverKeyImage.SetActive(false);
        playerGirl = FindObjectOfType<Movement_1>();


        foreach(SpriteRenderer t in comboKeysHolder) {
            t.gameObject.SetActive(false);
          }

        currentTime = timeToHitNextKeyLimit;

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
            
            currentTime -= Time.deltaTime;

            if (currentTime < 0)
            {
                keyPressGameOn = false;
                canRecover = false;
                playerGirl.isMoveable = true;
                recoverKeyImage.SetActive(false);
                currentKeyComboNum = 0;
                currentTime = timeToHitNextKeyLimit;
                foreach (SpriteRenderer t in comboKeysHolder)
                {
                    t.gameObject.SetActive(false);
                }
            }

                //Debug.Log(currentTime);
                //Debug.Log(KeysToHit[currentKeyComboNum]);

                if (Input.GetKeyDown(KeysToHit[currentKeyComboNum])) {
               // Debug.Log("Yes! You have pressed " + KeysToHit[currentKeyComboNum]);
                comboKeysHolder[currentKeyComboNum].sprite = pressedSprites[currentKeyComboNum];
                currentKeyComboNum++;
                currentTime = timeToHitNextKeyLimit;

                


                }


                if (currentKeyComboNum == 3) {
                    keyPressGameOn = false;
                    playerGirl.Recover(recoverAmount);
                    playerGirl.isMoveable = true;
                    recoverKeyImage.SetActive(false);
                    currentKeyComboNum = 0;
                currentTime = timeToHitNextKeyLimit;
                    foreach (SpriteRenderer t in comboKeysHolder)
                    {
                        t.gameObject.SetActive(false);
                    }

                }
            
            
          
            }


    }




    void GenerateComboKeys() {
        playerGirl.isMoveable = false;


        recoverKeyImage.SetActive(false);

        int rand1 = Random.Range(0, comboKeysUp.Length);

        comboKeysHolder[0].sprite = comboKeysUp[rand1];
        KeysToHit[0] = comboKeyCodes[rand1];
        pressedSprites[0] = comboKeysPressed[rand1];

        int rand2 = Random.Range(0, comboKeysUp.Length);

        comboKeysHolder[1].sprite = comboKeysUp[rand2];
        KeysToHit[1] = comboKeyCodes[rand2];
        pressedSprites[1] = comboKeysPressed[rand2];

        int rand3 = Random.Range(0, comboKeysUp.Length);

        comboKeysHolder[2].sprite = comboKeysUp[rand3];
        KeysToHit[2] = comboKeyCodes[rand3];
        pressedSprites[2] = comboKeysPressed[rand3];


        foreach (SpriteRenderer t in comboKeysHolder) {
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
