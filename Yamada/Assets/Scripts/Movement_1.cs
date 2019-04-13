using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_1 : MonoBehaviour
{
    Rigidbody2D rB;
    SpriteRenderer sP;
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
    public PlantGuy healableObject;
    public ParticleSystem[] healingPS;

    int startExtraJumpNum;

    public Transform groundCheck;
    public LayerMask NonPlayerLayer;

    public Transform healingHandPos;
    

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        sP = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();


        startExtraJumpNum = extraJumpNum;
        foreach (ParticleSystem pS in healingPS)
        {
            pS.enableEmission = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        Jumping();
        CheckGround();
        Healing();
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
                //sP.flipX = false;
                Vector3 newScale = transform.localScale;
                newScale.x = 1;
                transform.localScale = newScale;
            }
            else if (h < 0)
            {
                // sP.flipX = true;
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
        
            if (Input.GetButton("Fire1"))
            {
                anim.SetBool("isHealing", true);
            foreach (ParticleSystem pS in healingPS) {
                if (healableObject != null)
                {
                    pS.enableEmission = true;
                }
            }

                
            if (healableObject != null)
            {
                float distBetweenHandAndPlant = Vector3.Distance(healingHandPos.position, healableObject.transform.position);

                foreach(ParticleSystem pS in healingPS) {
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
                healableObject.health -= Time.deltaTime * 20;
                
            }
            }
            else {
                anim.SetBool("isHealing", false);
            foreach (ParticleSystem pS in healingPS)
            {
                pS.enableEmission = false;
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Healable")
        {
            inHealableArea = true;
            healableObject = collision.gameObject.GetComponent<PlantGuy>();
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
