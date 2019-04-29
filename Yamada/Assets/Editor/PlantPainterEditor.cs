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
    

    private void OnSceneGUI()
    {
        base.DrawDefaultInspector();

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
                    if (e.button == 0) {
                        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.up);
                        if (hit.collider != null)
                        {
                            PolygonCollider2D polyColl = hit.collider.gameObject.GetComponent<PolygonCollider2D>();

                            foreach (Vector2 point in polyColl.GetPath(0)) {
                                Debug.Log(point);

                            }

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
    }

    
}
