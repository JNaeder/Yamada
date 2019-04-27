using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealableObject))]
public class SwitchScript : MonoBehaviour

{
    public GameObject interactKeyImage;
    public DoorScript doorToOpen;
    public Color[] colorStages;
    public ParticleSystem pS;

    Animator anim;
    HealableObject hO;
    SpriteRenderer[] sP;

    bool isInteractable = false;
    bool hasInteracted = false;

    float startHealth;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        hO = GetComponent<HealableObject>();

        sP = GetComponentsInChildren<SpriteRenderer>();
        startHealth = hO.health;
        SetColor();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteractable && !hasInteracted) {
            if (Input.GetButtonDown("Interact")) {

                doorToOpen.MoveDoor();
                hasInteracted = true;
                interactKeyImage.SetActive(false);
                isInteractable = false;
                anim.SetBool("isOn", true);
            }


        }


        SetColor();

        


        
    }


    void SetColor() {
        if (hO.health > (startHealth * 0.5f))
        {
            ChangeColor(colorStages[0]);

        }
        else if (hO.health < (startHealth * 0.5f) && hO.health > 0)
        {
            ChangeColor(colorStages[1]);

        }
        else if (hO.health <= 0) {

            ChangeColor(colorStages[2]);
            pS.gameObject.SetActive(false);
        }


    }


    void ChangeColor(Color newColor) {
        foreach (SpriteRenderer s in sP) {
            s.color = newColor;

        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            if (!hasInteracted && !hO.isHealable)
            {
                    interactKeyImage.SetActive(true);
                    isInteractable = true;
                
            }
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!hasInteracted && !hO.isHealable)
            {
                interactKeyImage.SetActive(false);
                isInteractable = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!hasInteracted && !hO.isHealable)
            {
                interactKeyImage.SetActive(true);
                isInteractable = true;
            }
        }
    }
}
