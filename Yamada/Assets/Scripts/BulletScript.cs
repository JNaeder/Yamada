using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Vector3 targetPos;
    Vector3 dir;
    public float bulletSpeed = 3f;
    public float bulletDamage = 50f;
    public GameObject pSPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir.normalized * Time.deltaTime * bulletSpeed ;



    }



    public void SetTargetPosition(Vector3 playerPos) {
        targetPos = playerPos;
        dir = targetPos - transform.position;
    }


    void DestroyBullet() {
        GameObject newPs = Instantiate(pSPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(newPs, 5);
        Destroy(gameObject);


    }
    



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Movement_1 player = collision.gameObject.GetComponent<Movement_1>();
            player.radLevel += bulletDamage;
            DestroyBullet();

        }
        else {
            DestroyBullet();
        }
    }




}
