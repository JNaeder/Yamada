using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealableObject))]
public class PlantGuy : MonoBehaviour
{
    
    public float animSpeed = 1;
    
    Animator anim;
    HealableObject hO;
    Collider2D coll;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        hO = GetComponent<HealableObject>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {


        anim.SetFloat("health", hO.health);
        anim.speed = animSpeed;


        if (hO.health <= 0) {
            hO.isHealable = false;
            coll.enabled = false;
        }

    }


    private void OnDrawGizmos()
    {
        Collider2D coll = GetComponent<Collider2D>();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + new Vector3(coll.offset.x,coll.offset.y,0), new Vector2(coll.bounds.size.x + 1, coll.bounds.size.y));
        Color newColor = Color.yellow;
        newColor.a = 0.25f;
        Gizmos.color = newColor;
        Gizmos.DrawCube(transform.position + new Vector3(coll.offset.x, coll.offset.y, 0), new Vector2(coll.bounds.size.x + 1, coll.bounds.size.y));
        //

    }


}
