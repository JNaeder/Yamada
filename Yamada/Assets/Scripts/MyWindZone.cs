using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyWindZone : MonoBehaviour
{

    Collider2D windZoneBox;
    ParticleSystem[] pS;

    float windZoneHeight;

    // Start is called before the first frame update
    void Start()
    {
        windZoneBox = GetComponentInChildren<Collider2D>();
        windZoneHeight = windZoneBox.bounds.size.y;


        pS = GetComponentsInChildren<ParticleSystem>();


        for(int i = 0; i < pS.Length; i++) {
            pS[i].startLifetime = CalculatePSLifeTime(windZoneHeight);

         
           }


    }


    float CalculatePSLifeTime(float wZHeight) {
        float pSLifeTime = windZoneHeight * 0.11f;


        return pSLifeTime;
       }


}
