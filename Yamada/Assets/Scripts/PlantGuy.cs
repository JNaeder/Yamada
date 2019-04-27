using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealableObject))]
public class PlantGuy : MonoBehaviour
{
    
    public float animSpeed = 1;
    
    Animator anim;
    HealableObject hO;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        hO = GetComponent<HealableObject>();
    }

    // Update is called once per frame
    void Update()
    {


        anim.SetFloat("health", hO.health);
        anim.speed = animSpeed;


        if (hO.health <= 0) {
            hO.isHealable = false;
        }

    }


    private void OnDrawGizmos()
    {
        Collider2D coll = GetComponent<Collider2D>();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, coll.bounds.size);


    }


}
