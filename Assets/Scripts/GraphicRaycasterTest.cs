//Attach this script to your Canvas GameObject.
//Also attach a GraphicsRaycaster component to your canvas by clicking the Add Component button in the Inspector window.
//Also make sure you have an EventSystem in your hierarchy.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;



public class GraphicRaycasterTest : MonoBehaviour
{
    public static bool isOnlyTouchBackground;
	public Text texto;

    void Start()
    {
        isOnlyTouchBackground = true;
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (EventSystem.current.IsPointerOverGameObject())
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current)
                {
                    pointerId = -1,
                };

                pointerData.position = Input.mousePosition;

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);
				Debug.Log ("tocados por el raycast "+ results.Count);
				//texto.text = results.Count.ToString();
                if (results.Count == 1){
                    isOnlyTouchBackground = true;                    
                }
                else
                {   isOnlyTouchBackground = false;
                }
            }
        }
    }
}

