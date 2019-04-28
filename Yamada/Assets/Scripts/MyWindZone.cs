using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyWindZone : MonoBehaviour
{

    public Collider2D windZoneBox;
    ParticleSystem[] pS;

    float windZoneHeight, windZoneWidth;
    Vector3 windZonePos;

    // Start is called before the first frame update
    void Start()
    {
        windZoneBox = GetComponentInChildren<Collider2D>();
        windZoneHeight = windZoneBox.bounds.size.y;
        windZoneWidth = windZoneBox.bounds.size.x;
        windZonePos = windZoneBox.offset;


        pS = GetComponentsInChildren<ParticleSystem>();


       


    }


    float CalculatePSLifeTime(float wZHeight) {
        float pSLifeTime = windZoneHeight * 0.11f;


        return pSLifeTime;
       }


    void AutoSetPSSize() {
        for (int i = 0; i < pS.Length; i++)
        {
            ParticleSystem.MainModule emis = pS[i].main;
            emis.startLifetimeMultiplier = CalculatePSLifeTime(windZoneHeight);

            ParticleSystem.ShapeModule pSShape = pS[i].shape;
            Vector3 pSScale = pSShape.scale;
            pSScale.x = windZoneWidth * 0.7f;
            pSShape.scale = pSScale;

            pSShape.position = windZonePos;


        }


    }

    private void OnDrawGizmos()
    {
        Color newColor = Color.blue;
        newColor.a = 0.05f;
        Gizmos.color = newColor;

        Vector2 newPos = new Vector2((windZoneBox.transform.position.x + windZoneBox.offset.x), (windZoneBox.transform.position.y + windZoneBox.offset.y));
        Gizmos.DrawCube(newPos, windZoneBox.bounds.size);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(newPos, windZoneBox.bounds.size);
    }


}
