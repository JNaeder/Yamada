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

    public Transform groundCheck;
    public LayerMask NonPlayerLayer;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        sP = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jumping();
        CheckGround();
    }


    void Movement() {
        float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        isWalking = Mathf.Abs(h) > 0.1f;
        anim.SetBool("isWalking", isWalking);
;

        transform.position += new Vector3(h, 0, 0) * Time.deltaTime * speed;


        if (h > 0)
        {
            sP.flipX = false;
        }
        else if (h < 0) {
            sP.flipX = true;
        }
    }


    void Jumping() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            rB.AddForce(new Vector2(0, 100) * jumpForce);
        }


        if (rB.velocity.y < 0) {
            //make player fall faster
            rB.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime * fallMult;
        } else if (rB.velocity.y > 0 && !Input.GetButton("Jump")) {
            rB.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime * jumpDamp;
        }
    }


    void CheckGround() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, NonPlayerLayer);
        anim.SetBool("isGrounded", isGrounded);
        //Debug.Log(isGrounded);

    }
}
