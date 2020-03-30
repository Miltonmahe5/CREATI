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

        public static int tutoState = 0;

        void Start()
        {

            checkCardStep.AddListener(showPanel);
           
        }

        public void tutoOn()
        {
            tuto = true;
        }

        public void showPanel()
        {

            switch (tutoState)
            {
                case 1:
                    activateDeactivate();
                    StartCoroutine(transPerfectPanel(2));

                    break;
                case 3:
                    activateDeactivate();
                    StartCoroutine(transPerfectPanel(4));                   
                    break;
                case 5:                  
                    activateDeactivate();
                    tutoState++;
                    break;
            }
        }


        // Panel  activa tuto en joysticks//
        public void joystAvaliable()
        {
            if (tutoState == 4 && tuto)
            {               
                tutoState++;
                deactivateAll();
                for (int i = 0; i < subTransJoystick.Length; i++)
                {
                   subTransJoystick[i].SetActive(true);
                   
                }

            }
        }



        IEnumerator transPerfectPanel(int step)
        {
            yield return new WaitForSeconds(3f);
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
            Debug.Log(tutoState);
        }
        




    }
}