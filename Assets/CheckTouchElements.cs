using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CheckTouchElements : MonoBehaviour
{
  
	public static bool isOnlyTouchBackgroundAndroid;
	//public Text texto;

	void Start()
	{
		isOnlyTouchBackgroundAndroid = true;
	}

	void FixedUpdate()
	{
		

			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
			{
				PointerEventData pointerData = new PointerEventData(EventSystem.current)
				{
					pointerId = -1,
				};

				pointerData.position = Input.GetTouch(0).position;

				List<RaycastResult> results = new List<RaycastResult>();
				EventSystem.current.RaycastAll(pointerData, results);
				Debug.Log ("tocados por el raycast "+ results.Count);
				//texto.text = results.Count.ToString();
				if (results.Count <= 2){
					isOnlyTouchBackgroundAndroid = true;                    
				}
				else
				{   isOnlyTouchBackgroundAndroid = false;
				}
			}
		}

}