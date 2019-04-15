using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGuy : MonoBehaviour
{
    public float health = 100;
    public float animSpeed = 1;
    public bool isHealable = true;


    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("health", health);
        anim.speed = animSpeed;


        if (health <= 0) {
            isHealable = false;
        }

    }

   
}
