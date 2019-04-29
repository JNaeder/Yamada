using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPainter : MonoBehaviour
{
    public GameObject[] prefab;
    public float scaleRangeMin, scaleRangeMax;
    public Transform plantFolder;
    public SortingLayer sortLay;

    

    public void SpawnItem(Vector2 pos) {
        int randNum = Random.Range(0, prefab.Length);

        GameObject newPlant = Instantiate(prefab[randNum], pos, Quaternion.identity) as GameObject;
        float newFloat = Random.Range(scaleRangeMin, scaleRangeMax);
        Vector3 newScale = new Vector3(newFloat, newFloat, 1);
        newPlant.transform.localScale = newScale;
        newPlant.transform.parent = plantFolder;
        SpriteRenderer sP = newPlant.GetComponent<SpriteRenderer>();
        sP.sortingLayerName = sortLay.name;


    }


    public void TestMethod() {
        Debug.Log("Testing!");

    }
}
