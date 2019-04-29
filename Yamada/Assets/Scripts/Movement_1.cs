﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement_1 : MonoBehaviour
{
    Rigidbody2D rB;
    Animator anim;
    SpriteRenderer sP;
    BoxCollider2D boxColl;
    PolygonCollider2D polyColl;


    public float speed = 2;
    public float specialSpeed = 100;
    public float maxSpecialSpeed = 7f;
    public float jumpForce;
    public float fallMult = 1;
    public float jumpDamp = 1;
    public float radLevel = 0;
    public float maxRadLevel = 500;
    public float effectRot;
    public float recoveringSpeed = 2;
    public float specialRadSpeed = 5f;
    

    public bool isGrounded = false;
    public bool isWalking = false;
    public bool isMoveable = true;
    public bool inHealableArea = false;
    public bool isHealing = false;
    public bool isSpecialMove = false;
    public bool isInWindZone = false;



    public Image radLevelImg, radLevelIcon;
    public HealableObject healableObject;
    public ParticleSystem[] healingPS;
    public ParticleSystem[] specialPS;
    public Transform groundCheck;
    public LayerMask NonPlayerLayer;
    public Transform healingHandPos;


    Vector2 startCollidorSize;

    

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sP = GetComponent<SpriteRenderer>();
        boxColl = GetComponent<BoxCollider2D>();
        polyColl = GetComponent<PolygonCollider2D>();


        boxColl.enabled = false;
        polyColl.enabled = true;
        
        foreach (ParticleSystem pS in healingPS)
        {
            //pS.enableEmission = false;
            ParticleSystem.EmissionModule emis = pS.emission;
            emis.enabled = false;

            pS.transform.parent = null;
            pS.transform.position = Vector3.zero;
            
        }
        foreach (ParticleSystem ps in specialPS)
        {
            ParticleSystem.EmissionModule specialEmis = ps.emission;
            specialEmis.enabled = false;



        }




    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpecialMove)
        {
            Jumping();
            CheckGround();
            Healing();
        }
        SetRadLevel();
        SpecialMove();
    }

    private void FixedUpdate()
    {
        if (isSpecialMove)
        {
            SpecialMovement();
        }
        else
        {
            Movement();
        }
    }


    void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        if (isMoveable)
        {
            isWalking = Mathf.Abs(h) > 0.1f;
            anim.SetBool("isWalking", isWalking);
            transform.position += new Vector3(h, 0, 0) * Time.deltaTime * speed;


            if (h > 0)
            {
                
                Vector3 newScale = transform.localScale;
                newScale.x = Mathf.Abs(newScale.x);
                transform.localScale = newScale;
            }
            else if (h < 0)
            {
                
                Vector3 newScale = transform.localScale;
                newScale.x =  -Mathf.Abs(newScale.x);
                transform.localScale = newScale;
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
;





    }
    

    void SpecialMovement() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        rB.velocity = new Vector2(h, v) * specialSpeed;

    }

    void Healing() {
        
            if (Input.GetButton("Healing"))
            {
                anim.SetBool("isHealing", true);


            

                
            if (healableObject != null && healableObject.isHealable)
            { 
                

                float distBetweenHandAndPlant = Vector3.Distance(healingHandPos.position, healableObject.transform.position);

                foreach(ParticleSystem pS in healingPS) {
                    ParticleSystem.EmissionModule emis = pS.emission;
                    emis.enabled = true;

                    UnityEngine.ParticleSystem.ShapeModule pSShape = pS.shape;
                    pSShape.radius = distBetweenHandAndPlant / 2;
                    Vector3 middlePoint = new Vector3((healingHandPos.position.x + healableObject.transform.position.x) / 2, (healingHandPos.position.y + healableObject.transform.position.y) / 2, 0);
                    pSShape.position = middlePoint;

                    Vector3 vectorToTarget = healingHandPos.position - healableObject.transform.position;
                    effectRot = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                    Vector3 newRot = new Vector3(0, 0, effectRot);
                    pSShape.rotation = newRot;
                }
            }



            if (inHealableArea)
            {
                if (healableObject.health > 0)
                {
                    healableObject.health -= Time.deltaTime * 20;
                    radLevel += Time.deltaTime * 20;
                    Vector3 iconRot = radLevelIcon.transform.localRotation.eulerAngles;
                    iconRot.z += Time.deltaTime * 50f;
                    radLevelIcon.transform.localRotation = Quaternion.Euler(iconRot);

                }
            }
            }


            else {
                anim.SetBool("isHealing", false);
            foreach (ParticleSystem pS in healingPS)
            {
                ParticleSystem.EmissionModule emis = pS.emission;
                emis.enabled = false;
            }
        }
        

    }


    void Jumping()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rB.AddForce(new Vector2(0, 100) * jumpForce);
            //Debug.Log("Jump!");
        }

        if (rB.velocity.y < 0)
        {
            //make player fall faster
            rB.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime * fallMult;
        }
        else if (rB.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rB.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime * jumpDamp;
        }
        else if (isInWindZone) {
            rB.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime * jumpDamp;
        }
    }


    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, NonPlayerLayer);
        anim.SetBool("isGrounded", isGrounded);
        
    }


    void SetRadLevel() {

        float radLevelPerc = radLevel / maxRadLevel;
        Vector3 radLevelScale = radLevelImg.transform.localScale;
        radLevelScale.x = radLevelPerc;
        radLevelImg.transform.localScale = radLevelScale;

        if (radLevel > (maxRadLevel / 2))
        {
            anim.SetBool("isMutated", true);

        }
        else {

            anim.SetBool("isMutated", false);
        }


        if (radLevel >= maxRadLevel) {
            Debug.Log("You Lose");
            radLevel = maxRadLevel;
        }



    }


    public void Recover(float recoverAmount) {
        if (radLevel > 0)
        {

            Debug.Log("Healing Yourself!");

            radLevel -= recoverAmount;



            if (radLevel < 0)
            {
                radLevel = 0;
            }
        }

    }


    void SpecialMove() {

        if (Input.GetButtonDown("Special")) {
            isSpecialMove = true;
            rB.gravityScale = 0;
            //rB.velocity = Vector2.zero;
            foreach (ParticleSystem ps in specialPS)
            {
                ParticleSystem.EmissionModule specialEmis = ps.emission;
                specialEmis.enabled = true;
            }
            sP.enabled = false;
        }


        if (Input.GetButton("Special"))
        {
            radLevel += Time.deltaTime * specialRadSpeed;
            Vector3 iconRot = radLevelIcon.transform.localRotation.eulerAngles;
            iconRot.z += Time.deltaTime * 50f;
            radLevelIcon.transform.localRotation = Quaternion.Euler(iconRot);
            rB.gravityScale = 0;
            boxColl.enabled = true;
            polyColl.enabled = false;
        }


        if (Input.GetButtonUp("Special")) {
            isSpecialMove = false;
            rB.gravityScale = 1;
            rB.velocity = Vector3.zero;
            boxColl.enabled = false;
            polyColl.enabled = true;
            // rB.simulated = true;
            sP.enabled = true;
            foreach (ParticleSystem ps in specialPS)
            {
                ParticleSystem.EmissionModule specialEmis = ps.emission;
                ParticleSystem.MainModule pSMain = ps.main;
                pSMain.simulationSpace = ParticleSystemSimulationSpace.World;
                specialEmis.enabled = false;
            }

            

        }
        
    }

    



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Healable")
        {
            inHealableArea = true;
            healableObject = collision.gameObject.GetComponent<HealableObject>();
        }

        if (collision.gameObject.tag == "Wind Zone") {
            isInWindZone = true;

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Healable")
        {
            inHealableArea = false;
            healableObject = null;
        }

        if (collision.gameObject.tag == "Wind Zone")
        {
            isInWindZone = false;

        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.5f);
    }
}
