using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGuyTrigger : MonoBehaviour
{

    ShootingGuy shootingGuy;
    // Start is called before the first frame update
    void Start()
    {
        shootingGuy = GetComponentInParent<ShootingGuy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shootingGuy.TriggerShooting();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shootingGuy.UnTriggerShooting();
        }
    }



    private void OnDrawGizmos()
    {
        Collider2D coll = GetComponent<Collider2D>();

        Color newColor = Color.red;
        newColor.a = 0.15f;
        Gizmos.color = newColor;

        Gizmos.DrawCube(new Vector2(transform.position.x + coll.offset.x,transform.position.y + coll.offset.y), coll.bounds.size);
    }
}
