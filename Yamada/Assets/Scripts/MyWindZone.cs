using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyWindZone : MonoBehaviour
{

    Collider2D windZoneBox;
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


        for(int i = 0; i < pS.Length; i++) {
            ParticleSystem.MainModule emis = pS[i].main;
            emis.startLifetimeMultiplier = CalculatePSLifeTime(windZoneHeight);

            ParticleSystem.ShapeModule pSShape = pS[i].shape;
            Vector3 pSScale = pSShape.scale;
            pSScale.x = windZoneWidth * 0.7f;
            pSShape.scale = pSScale;

            pSShape.position = windZonePos;

         
           }


    }


    float CalculatePSLifeTime(float wZHeight) {
        float pSLifeTime = windZoneHeight * 0.11f;


        return pSLifeTime;
       }


}
