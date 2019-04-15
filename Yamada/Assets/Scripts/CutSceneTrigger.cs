using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneTrigger : MonoBehaviour
{

    PlayableDirector director;

    public bool onlyPlayOnce = true;

    bool hasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Play CutScene!");
        if (!hasPlayed) {
            director.Play();
        }
        if (onlyPlayOnce) {
            hasPlayed = true;
        }
        
    }
}
