using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement_1 : MonoBehaviour
{
    Rigidbody2D rB;
    Animator anim;


    public float speed = 2;
    public float jumpForce;
    public float fallMult = 1;
    public float jumpDamp = 1;
    public bool isGrounded = false;
    public bool isWalking = false;
    public bool isMoveable = true;
    public bool inHealableArea = false;
    public bool isHealing = false;
    public float effectRot;
    public int extraJumpNum = 1;
    public float radLevel = 0;
    public float maxRadLevel = 500;
    public Image radLevelImg;
    public HealableObject healableObject;
    public ParticleSystem[] healingPS;
    public float recoveringSpeed = 2;

    int startExtraJumpNum;

    public Transform groundCheck;
    public LayerMask NonPlayerLayer;

    public Transform healingHandPos;
    

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


        startExtraJumpNum = extraJumpNum;
        foreach (ParticleSystem pS in healingPS)
        {
            //pS.enableEmission = false;
            ParticleSystem.EmissionModule emis = pS.emission;
            emis.enabled = false;

            pS.transform.parent = null;
            pS.transform.position = Vector3.zero;
            
        }




    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        Jumping();
        CheckGround();
        Healing();
        SetRadLevel();
    }


    void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        if (isMoveable)
        {
            isWalking = Mathf.Abs(h) > 0.1f;
            anim.SetBool("isWalking", isWalking);
            transform.position += new Vector3(h, 0, 0) * Time.deltaTime * speed;


            if (h > 0)
            {
                
                Vector3 newScale = transform.localScale;
                newScale.x = 1;
                transform.localScale = newScale;
            }
            else if (h < 0)
            {
                
                Vector3 newScale = transform.localScale;
                newScale.x = -1;
                transform.localScale = newScale;
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
;





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
        if (Input.GetButtonDown("Jump") && extraJumpNum != 0)
        {
            extraJumpNum--;
            rB.AddForce(new Vector2(0, 100) * jumpForce);
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
    }


    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, NonPlayerLayer);
        anim.SetBool("isGrounded", isGrounded);
        //Debug.Log(isGrounded);
        if (isGrounded)
        {
            extraJumpNum = startExtraJumpNum;
        }
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Healable")
        {
            inHealableArea = true;
            healableObject = collision.gameObject.GetComponent<HealableObject>();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Healable")
        {
            inHealableArea = false;
            healableObject = null;
        }
    }
}
