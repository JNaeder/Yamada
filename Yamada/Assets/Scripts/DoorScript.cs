using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject door;
    public Transform finalPos;
    public float moveSpeed = 3;

    bool doorIsMoving;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doorIsMoving) {
            door.transform.position = Vector2.MoveTowards(door.transform.position, finalPos.position, moveSpeed * Time.deltaTime);

            if (door.transform.position == finalPos.position) {
                doorIsMoving = false;


            }

        }


    }


    public void MoveDoor() {
        doorIsMoving = true;
    }
}
