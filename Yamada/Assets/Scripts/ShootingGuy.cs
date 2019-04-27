using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGuy : MonoBehaviour
{
    Transform playerTrans;
    Animator anim;

    public GameObject bulletPrefab;
    public Transform aimer;
    public Transform muzzle;
    public float repeatTime = 10f;
 
    // Start is called before the first frame update
    void Start()
    {
        playerTrans = FindObjectOfType<Movement_1>().transform;
        anim = GetComponent<Animator>();
        InvokeRepeating("BeginShooting", 0.01f, repeatTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
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
}
