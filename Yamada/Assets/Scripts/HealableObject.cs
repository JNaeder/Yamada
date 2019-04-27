using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealableObject : MonoBehaviour
{
    public float health = 100;
    public bool isHealable = true;



    // Update is called once per frame
    void Update()
    {
        if (health < 0) {
            isHealable = false;

        }

        
    }
}
