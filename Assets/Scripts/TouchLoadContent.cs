using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Doozy.Engine.UI;
using UnityEngine.EventSystems;




public class TouchLoadContent : MonoBehaviour
{

        //public GameObject cartas;
        public static int marcador;
        public GameObject[] marcadores;
      

    // Start is called before the first frame update



    void Start()
        {
         
            marcadores = GameObject.FindGameObjectsWithTag("carta");
            for (int i = 0; i < marcadores.Length; i++)
            {
                marcadores[i] = GameObject.Find("ImageTarget" + (i + 1));
            }
        }

    // Update is called once per frame 
    void Update()
        {
    #if UNITY_ANDROID
		if (GameObject.Find("FileBrowserUI") == null && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && CheckTouchElements.isOnlyTouchBackgroundAndroid)
			
    #endif
    #if UNITY_EDITOR || UNITY_STANDALONE
          //  if (Input.GetMouseButtonDown(0) && GameObject.Find("FileBrowserUI") == null  && GraphicRaycasterTest.isOnlyTouchBackground)
            #endif
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name==this.gameObject.name)
                {

                    Debug.Log(hit.collider.gameObject.name);
                    for (int i = 0; i < marcadores.Length; i++)
                    {

                        if (marcadores[i] == hit.collider.gameObject)
                        {                            
                            marcador = i;
                            marcadores[i].GetComponent<Cambiar_Enlazado>().Active = true;
                            Manager.updateGreenLabel();
                            // Indica si selección la carta
                            if (marcador == 1 && ManagerTutorial.tutoState == 2 && ManagerTutorial.tuto)
                            {
                                ManagerTutorial.tutoState++;
                                ManagerTutorial.checkCardStep.Invoke();
                            }


                    }
                        else
                        {
                            marcadores[i].GetComponent<Cambiar_Enlazado>().Active = false;
                        }
                    }

                    Edit_Transform_Objects.updateCard();

                }
            }
        }

  
}
