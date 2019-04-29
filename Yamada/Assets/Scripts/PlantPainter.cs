using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPainter : MonoBehaviour
{
    public GameObject[] prefab;
    public float scaleRangeMin, scaleRangeMax;
    public Transform plantFolder;

    

    public void SpawnItem(Vector2 pos) {
        int randNum = Random.Range(0, prefab.Length);

        GameObject newPlant = Instantiate(prefab[randNum], pos, Quaternion.identity) as GameObject;
        float newFloat = Random.Range(scaleRangeMin, scaleRangeMax);
        int reverseFloat = Random.Range(-1, 2);
       

        Vector3 newScale = new Vector3(newFloat, newFloat, 1);
        if(reverseFloat != 0) {
            newScale.x = newScale.x * reverseFloat;
         
           }


        newPlant.transform.localScale = newScale;
        newPlant.transform.parent = plantFolder;
        SpriteRenderer sP = newPlant.GetComponent<SpriteRenderer>();
        float newY = sP.bounds.size.y * 0.4f;
        newPlant.transform.position = new Vector2(newPlant.transform.position.x, newPlant.transform.position.y + newY);


    }



    public void DeleteAllPlants() {
        Transform[] plants = plantFolder.GetComponentsInChildren<Transform>();
        for(int i = 1; i < plants.Length; i++) {
            DestroyImmediate(plants[i].gameObject);
         
           }


    }
}
