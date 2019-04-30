using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealableObject))]
public class ShootingGuy : MonoBehaviour
{
    Transform playerTrans;
    Animator anim;
    HealableObject hO;
    SpriteRenderer[] sP;

    public GameObject bulletPrefab;
    public Transform aimer;
    public Transform muzzle;
    public ParticleSystem pS;
    public float repeatTime = 10f;
    public Color[] colorStages;


    float startTime = 0;
    float startHealth;
    bool hasBeenHeald = false;
    bool playerIsInTrigger = false;
 
    // Start is called before the first frame update
    void Start()
    {
        playerTrans = FindObjectOfType<Movement_1>().transform;
        anim = GetComponent<Animator>();
        hO = GetComponent<HealableObject>();
        sP = GetComponentsInChildren<SpriteRenderer>();
        startHealth = hO.health;


        SetColor();
    }

    // Update is called once per frame
    void Update()
    {


        if (hO.isHealable && !hasBeenHeald && playerIsInTrigger)
        {
            LookAtPlayer();
            LoopShooting();
        }
        else if (!hO.isHealable && !hasBeenHeald) {

            BecomeHealed();

        }
        SetColor();
    }


    void LoopShooting() {
        if (Time.time > startTime + repeatTime) {
            Debug.Log("Shoot!");
            BeginShooting();
            startTime = Time.time;
        }


    }



    void LookAtPlayer() {
        Vector3 dir = transform.position - playerTrans.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        aimer.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);


    }


    void BeginShooting() {
        anim.SetBool("isShooting", true);


    }


    public void ShootAtPlayer() {
        anim.SetBool("isShooting", false);
        GameObject newBullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity) as GameObject;
        BulletScript bulletScript = newBullet.gameObject.GetComponent<BulletScript>();
        bulletScript.SetTargetPosition(playerTrans.position);

    }

    public void BecomeHealed() {
        hasBeenHeald = true;



    }


    public void TriggerShooting() {
        playerIsInTrigger = true;

    }
    public void UnTriggerShooting() {
        playerIsInTrigger = false;
     
       }

    void SetColor()
    {
        if (hO.health > (startHealth * 0.5f))
        {
            ChangeColor(colorStages[0]);

        }
        else if (hO.health < (startHealth * 0.5f) && hO.health > 0)
        {
            ChangeColor(colorStages[1]);

        }
        else if (hO.health <= 0)
        {

            ChangeColor(colorStages[2]);
            pS.gameObject.SetActive(false);
        }


    }


    void ChangeColor(Color newColor)
    {
        foreach (SpriteRenderer s in sP)
        {
            s.color = newColor;

        }


    }


    private void OnDrawGizmos()
    {
        Collider2D coll = GetComponent<Collider2D>();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + new Vector3(coll.offset.x, coll.offset.y, 0), new Vector2(coll.bounds.size.x + 1, coll.bounds.size.y));
        Color newColor = Color.yellow;
        newColor.a = 0.25f;
        Gizmos.color = newColor;
        Gizmos.DrawCube(transform.position + new Vector3(coll.offset.x, coll.offset.y, 0), new Vector2(coll.bounds.size.x + 1, coll.bounds.size.y));


    }
}
