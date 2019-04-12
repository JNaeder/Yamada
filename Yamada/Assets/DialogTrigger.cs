using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogTrigger : MonoBehaviour
{
    public GameObject instructionBubble;

    PlayableDirector dir;

    // Start is called before the first frame update
    void Start()
    {
        dir = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") {
            dir.Play();
         
           }
    }


}
