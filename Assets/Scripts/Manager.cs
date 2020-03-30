using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public int indexCard = 0;
    public Sprite[] imageCard = new Sprite[25];
    public string[] listNameCard = new string[5];
    public UIView[] arrayCardText = new UIView[5];


    //Delegate para saber la carta 
    public delegate void Delegate();
    public static Delegate updateGreenLabel;
    public GameObject[] marcadores;



    void Start()
    {
        updateGreenLabel += greenMark;
        marcadores = GameObject.FindGameObjectsWithTag("carta");
        for (int i = 0; i < marcadores.Length; i++)
        {
            marcadores[i] = GameObject.Find("ImageTarget" + (i+1));
            marcadores[i].transform.GetChild(3).gameObject.SetActive(false);
        }
               
    }

    public void ActivateCardUI(string CardName, string NumCard)
    {
        for (int i = 0; i < listNameCard.Length; i++)
        {
            if (listNameCard[i] != CardName && listNameCard[i] == "Default")
            {
                indexCard = i;
            }
        }

        switch (NumCard)
        {
            case "Cara1": NumCard = "19"; break;
            case "Cara2": NumCard = "20"; break;
            case "Cara3": NumCard = "21"; break;
            case "Cara4": NumCard = "22"; break;
            case "Cara5": NumCard = "23"; break;
            case "Cara6": NumCard = "24"; break;
            case "CartaPoder": NumCard = "25"; break;
            default: print("Nothing"); break;
        }

        listNameCard[indexCard] = CardName;
        arrayCardText[indexCard].GetComponentInChildren<Text>().text = CardName;
        arrayCardText[indexCard].GetComponentInChildren<SpriteCardImage>().ImagenToShow(int.Parse(NumCard) - 1);
        arrayCardText[indexCard].Show();
        if (NumCard == "2" && ManagerTutorial.tutoState == 0 && ManagerTutorial.tuto) {
            ManagerTutorial.tutoState ++;
            ManagerTutorial.checkCardStep.Invoke();
        }
        

        
    }

    public void DisactivateCardUI(string CardName)
    {
        for (int i = 0; i < listNameCard.Length; i++)
        {
            if (listNameCard[i] == CardName)
            {
                listNameCard[i] = "Default";
                arrayCardText[i].Hide();
            }
        }
    }
    public void DisactivateCardUI()
    {
        for (int i = 0; i < listNameCard.Length; i++)
        {
            listNameCard[i] = "Default";
            arrayCardText[i].Hide();
        }
    }


    public void greenMark() {


        string carta = (TouchLoadContent.marcador).ToString();
        Debug.Log("Info carta" + carta);

        for (int i = 0; i < arrayCardText.Length; i++)
        {
            string cardImage = arrayCardText[i].GetComponentInChildren<SpriteCardImage>().numImage;
            Debug.Log("Info Image" + cardImage + "Index" +i);
            
            if (cardImage.Equals(carta)) {
                 arrayCardText[i].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                arrayCardText[i].transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(true);
                for (int j = 0; j < marcadores.Length; j++)
                {
                    if (j == int.Parse(carta))
                        marcadores[j].transform.GetChild(3).gameObject.SetActive(true);
                    else
                        marcadores[j].transform.GetChild(3).gameObject.SetActive(false);
                }
               
                Debug.Log("Loppuse");
            }
            else {
                arrayCardText[i].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                arrayCardText[i].transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
               
                Debug.Log("NoLoppuse");
               


            }





        }

    }
}