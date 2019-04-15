using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneChanger : MonoBehaviour
{
    public int sceneNumber;



    void Start()
    {
        SceneManager.LoadScene(sceneNumber);


    }

    
}
