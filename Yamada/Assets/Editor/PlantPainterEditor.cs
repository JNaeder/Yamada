using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlantPainter))]
public class PlantPainterEditor : Editor
{
    bool paintPlants = false;
    PlantPainter myScript;
    Vector3 mousePos;

    Vector2 leftPoint, rightPoint;

    private void OnSceneGUI()
    {
        ///base.DrawDefaultInspector();

        if (myScript == null) {
            myScript = (PlantPainter)target;
        }




        mousePos = new Vector3(Event.current.mousePosition.x, Event.current.mousePosition.y, 0);
        mousePos.y = SceneView.currentDrawingSceneView.camera.pixelHeight - mousePos.y;
        mousePos = SceneView.currentDrawingSceneView.camera.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;


        if (paintPlants) {
            Event e = Event.current;
            if (e.isMouse) {

                if (e.type == EventType.MouseDown) {

                    if (e.button == 0 && e.alt == true) {

                        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
                        if (hit.collider != null && hit.collider.gameObject.tag == "Ground")
                        {

                            myScript.SpawnItem(FindMiddlePoint(hit));
                           //Debug.Log(FindMiddlePoint(hit));



                        } else {
                            Debug.LogWarning("No Collidor to spawn items");
                          }

                    }
                    }

                }


            }
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Paint Plants is " + paintPlants)){
            paintPlants = !paintPlants;

        }

        if(GUILayout.Button("Delete All Plants")) {
            myScript.DeleteAllPlants();
         
           }
    }




    Vector2 FindMiddlePoint(RaycastHit2D newHit) {

        PolygonCollider2D polyColl = newHit.collider.gameObject.GetComponent<PolygonCollider2D>();
        Vector2 hitPoint = newHit.point;



        leftPoint = Vector2.one * -1000;
        rightPoint = Vector2.one * 1000;
        float midY = polyColl.bounds.center.y;

       
        //Find LeftX
        foreach (Vector2 p in polyColl.GetPath(0)) {
            Vector2 newP = new Vector2(((p.x * polyColl.transform.localScale.x) + polyColl.transform.position.x), ((p.y * polyColl.transform.localScale.y) + polyColl.transform.position.y));

                if (newP.x < hitPoint.x)
                {
                    if (newP.x > leftPoint.x)
                    {
                        leftPoint = newP;
                    }
                }
            
        }

        //Find RightX
        foreach (Vector2 p in polyColl.GetPath(0))
        {
            Vector2 newP = new Vector2(((p.x * polyColl.transform.localScale.x) + polyColl.transform.position.x), ((p.y * polyColl.transform.localScale.y) + polyColl.transform.position.y));

                if (newP.x > hitPoint.x)
                {
                    if (newP.x < rightPoint.x)
                    {
                        rightPoint = newP;
                    }
            }
            
        }
        float newY = Remap(hitPoint.x, leftPoint.x, leftPoint.y, rightPoint.x, rightPoint.y);

        Debug.Log("Hit poit is : " + hitPoint + " and Right Point is : " + rightPoint + " and left Point is : " + leftPoint);

        return new Vector2(hitPoint.x, newY);

    }





    float Remap(float thisNum, float inputA, float inputB, float outputA, float outputB)
    {


        return (thisNum - inputA) / (outputA - inputA) * (outputB - inputB) + inputB;
    }

    







}
