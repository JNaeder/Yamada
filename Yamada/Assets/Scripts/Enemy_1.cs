using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{

    public Transform startPos, endPos;
    public float movementSpeed = 1;
    public float buffer = 0.5f;

    bool isGoingForward = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGoingForward)
        {
            GoToNextPosition(startPos.position);
        }
        else {
            GoToNextPosition(endPos.position);
        }

        CheckPos();
    }


    void GoToNextPosition(Vector3 nextPos) {
        Vector3 diff = nextPos - transform.position;
        transform.position += diff * Time.deltaTime * movementSpeed;

    }

    void CheckPos() {
        if (transform.position.x > endPos.position.x - buffer)
        {
            isGoingForward = true;
            Debug.Log("Shit!");
        }
        else if (transform.position.x < startPos.position.x + buffer) {
            isGoingForward = false;
            Debug.Log("Fufkc");
        }


        Debug.Log(isGoingForward + " " + transform.position.x + ": start: " + startPos.position.x + " end: " + endPos.position.x);
    }
}
