using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;




namespace Doozy.Engine.UI
{


    public class ManagerTutorial : MonoBehaviour
    {
        public GameObject[] subPanelTuto;
        public GameObject[] subTransJoystick;
        public static bool tuto = false;      
        public static UnityEvent checkCardStep = new UnityEvent();
        public static int tutoState;

        void Start()
        {
            checkCardStep.AddListener(showPanel);           
        }

        public void tutoOn()
        {           
            tuto = true;
            tutoState = 0;
        }

        public void showPanel()
        {

            switch (tutoState)
            {
                //Despliega panel de ubicar cargador Paso 1
                case 1:
                    activateDeactivate();
                    StartCoroutine(transPerfectPanel(2));

                    break;
                //Despliega panel de darle click a la carta Paso 3
                case 3:
                    activateDeactivate();
                    StartCoroutine(transPerfectPanel(4));                   
                    break;
                // Despliega manito de carga Paso 5
                case 5:
                    deactivateAll();
                    activateDeactivate();
                    tutoState++;
                    break;
                

            }
        }


        public void endTuto()
        {
            if (tutoState == 6 && tuto)
            {
                tuto = false;
                deactivateAll();

            }
        }

            // Panel  activa manitos de cada panel de transformación
            public void joystAvaliable()
        {
            if (tutoState == 4 && tuto)
            {               
                tutoState++; 
                deactivateAll(); //Hide todos los UIView
                for (int i = 0; i < subTransJoystick.Length; i++)
                {
                   subTransJoystick[i].SetActive(true); 
                   
                }

            }
        }
        //Corutina para la transición de los paneles de perfecto
        IEnumerator transPerfectPanel(int step)
        {
            yield return new WaitForSeconds(2f);
            tutoState = step;
            activateDeactivate();
         }

        public void activateDeactivate()
        {
            for (int i = 0; i < subPanelTuto.Length; i++)
            {
                if (tutoState == i)
                {
                    subPanelTuto[i].GetComponent<UIView>().Show();
                }
                else
                {
                    subPanelTuto[i].GetComponent<UIView>().Hide();
                }
            }
        }

        public void  deactivateAll()
        {

            for (int i = 0; i < subPanelTuto.Length; i++)
            {
                subPanelTuto[i].GetComponent<UIView>().Hide();
            }
            for (int i = 0; i < subTransJoystick.Length; i++)
            {
                subTransJoystick[i].SetActive(false);
            }

        }

        public void Update()
        {
            { Debug.Log(tutoState); }
        }




    }
}