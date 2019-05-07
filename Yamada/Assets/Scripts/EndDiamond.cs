using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDiamond : MonoBehaviour
{
    GameManager gM;
    public GameObject hitExplosion;


    Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        gM = FindObjectOfType<GameManager>();
    }


    private void Update()
    {


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("End Level!");
            anim.Play("EndDiamond_Got");

        }
    }


    public IEnumerator PlayParticleAndLoadScene()
    {
        Instantiate(hitExplosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        gM.LoadScene(0);
    }
}
