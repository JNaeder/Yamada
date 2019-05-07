using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{

    public GameObject interactKeyImage;
    public GameObject mapMenuUI;


    Movement_1 player;


    private void Start()
    {
        player = FindObjectOfType<Movement_1>();
    }

    private void Update()
    {
        InputControl();
    }


    void InputControl()
    {
        if (Input.GetButtonDown("Interact"))
        {
            ShowMap();
            interactKeyImage.SetActive(false);

        }

    }

    void ShowMap()
    {
        Debug.Log("Show Map!");
        mapMenuUI.SetActive(true);
        player.isMoveable = false;
    }  

    public void TurnOffMap()
    {
        mapMenuUI.SetActive(false);
        player.isMoveable = true;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactKeyImage.SetActive(true);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactKeyImage.SetActive(false);
        }
    }


    private void OnDrawGizmos()
    {
        Collider2D coll = GetComponent<Collider2D>();
        Color newColor = Color.yellow;
        Gizmos.DrawWireCube(transform.position + new Vector3(coll.offset.x, coll.offset.y, 0), new Vector2(coll.bounds.size.x + 1, coll.bounds.size.y));
        newColor.a = 0.15f;
        Gizmos.color = newColor;
        Gizmos.DrawCube(transform.position + new Vector3(coll.offset.x, coll.offset.y, 0), new Vector2(coll.bounds.size.x + 1, coll.bounds.size.y));


    }
}
