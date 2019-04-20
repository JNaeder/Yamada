using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonChicken : MonoBehaviour
{


    public GameObject interactKeyImage;
    public bool isInteractable = false;
    public GameObject[] conversationTopics;
    public GameObject convoTopicHolder;
    public GameObject convoBubble;


    Animator anim;

    string[] interactClipName;
    bool hasInteracted = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        SetUpAnimationNames();


        interactKeyImage.SetActive(false);
        convoBubble.SetActive(false);


        foreach (GameObject t in conversationTopics)
        {
            t.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteractable) {
            if (hasInteracted)
            {
                interactKeyImage.SetActive(false);
                if (anim.GetCurrentAnimatorStateInfo(0).shortNameHash == Animator.StringToHash("Chicken_Idle"))
                {
                    // Debug.Log(anim.GetCurrentAnimatorStateInfo(0).normalizedTime);

                    convoBubble.SetActive(true);
                    convoTopicHolder.SetActive(true);

                }

            }



            if (Input.GetButtonDown("Interact")) {
               // Debug.Log("JINKO!");
                int randClipNum = Random.Range(0, interactClipName.Length);
                anim.Play(interactClipName[randClipNum]);
                hasInteracted = true;
                convoBubble.SetActive(false);
                convoTopicHolder.SetActive(false);
                ChooseConvoTopic(); 


            }
           


        }
        

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactKeyImage.SetActive(true);
        isInteractable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactKeyImage.SetActive(false);
        isInteractable = false;
        hasInteracted = false;
        convoBubble.SetActive(false);
        convoTopicHolder.SetActive(false);
    }



    void SetUpAnimationNames() {
        interactClipName = new string[3];

        interactClipName[0] = "Chicken_Interact";
        interactClipName[1] = "Chicken_Interact_2";
        interactClipName[2] = "Chicken_Interact_3";

    }

    void ChooseConvoTopic() {
        int randNum = Random.Range(0, conversationTopics.Length);
        foreach (GameObject t in conversationTopics) {
            t.SetActive(false);
        }
        conversationTopics[randNum].SetActive(true);


    }
}
