using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealableObject))]
public class MovingPlatform : MonoBehaviour
{
    public Transform endPos, platform;
    public float speed = 3f;
    public bool isMovable;
    public Color[] colorStages;

    bool movingForward;
    float startHealth;

    Vector3 startPos;
    HealableObject hO;
    ParticleSystem pS;
    SpriteRenderer sP;


    // Start is called before the first frame update
    void Start()
    {
        hO = GetComponent<HealableObject>();
        startPos = platform.position;
        pS = GetComponentInChildren<ParticleSystem>();
        sP = GetComponentInChildren<SpriteRenderer>();

        startHealth = hO.health;

    }

    // Update is called once per frame
    void Update()
    {
        if (hO.health <= 0)
        {
            MovePlatform();
        }
        SetColor();
    }


    void MovePlatform() {

        if (movingForward)
        {
            platform.position = Vector2.MoveTowards(platform.position, endPos.position, Time.deltaTime * speed);
        }
        else {
            platform.position = Vector2.MoveTowards(platform.position, startPos, Time.deltaTime * speed);
        }

        Vector2 newPos = new Vector2(platform.position.x, platform.position.y);
        Vector2 newStartPos = new Vector2(startPos.x, startPos.y);
        Vector2 newEndPos = new Vector2(endPos.position.x, endPos.position.y);

        if (newPos == newEndPos)
        {
            movingForward = false;

        }
        else if (newPos == newStartPos) {
            movingForward = true;

        }

    }

    void SetColor()
    {
        if (hO.health > (startHealth * 0.5f))
        {
            ChangeColor(colorStages[0]);

        }
        else if (hO.health < (startHealth * 0.5f) && hO.health > 0)
        {
            ChangeColor(colorStages[1]);

        }
        else if (hO.health <= 0)
        {

            ChangeColor(colorStages[2]);
            pS.gameObject.SetActive(false);
        }


    }


    void ChangeColor(Color newColor)
    {
        sP.color = newColor;


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(platform.position, endPos.position);

        Gizmos.DrawCube(endPos.position, new Vector3(platform.localScale.x, platform.localScale.y, platform.localScale.z));

        
            Collider2D coll = GetComponent<Collider2D>();
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position + new Vector3(coll.offset.x, coll.offset.y, 0), new Vector2(coll.bounds.size.x + 1, coll.bounds.size.y));
            Color newColor = Color.yellow;
            newColor.a = 0.25f;
            Gizmos.color = newColor;
            Gizmos.DrawCube(transform.position + new Vector3(coll.offset.x, coll.offset.y, 0), new Vector2(coll.bounds.size.x + 1, coll.bounds.size.y));

        
    }

    
}
