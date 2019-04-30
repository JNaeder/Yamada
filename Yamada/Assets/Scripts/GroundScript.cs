using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    PolygonCollider2D pollyColl;

    Vector3 startPos, endPos;





    private void OnDrawGizmos()
    {
        pollyColl = GetComponent<PolygonCollider2D>();

        startPos = new Vector3(pollyColl.bounds.center.x - (pollyColl.bounds.size.x /2) ,pollyColl.bounds.center.y, 0);
        endPos = new Vector3(pollyColl.bounds.center.x + (pollyColl.bounds.size.x / 2), pollyColl.bounds.center.y, 0);


        Gizmos.color = Color.blue;
        Gizmos.DrawLine(startPos, endPos);
    }


}
