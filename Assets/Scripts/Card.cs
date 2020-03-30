using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    int count = 0;
    Manager manag;
    DefaultTrackableEventHandler handler;

    void Start()
    {
        count = 0;
        manag = FindObjectOfType<Manager>();
        handler = GetComponent<DefaultTrackableEventHandler>();
        Perform();
    }

    private void Update()
    {
        Perform();
   
    }

    void Perform()
    {
        bool stateCard = handler.stateCard;
        string nameCard = handler.nameCard;
        string numerCard = handler.numercard;      

        if (stateCard)
        {
            if (count == 0)
            {
                count++;
                manag.ActivateCardUI(nameCard, numerCard);
                Manager.updateGreenLabel();
            }
        }
        else if (!stateCard)
        {
            count = 0;
            manag.DisactivateCardUI(nameCard);
        }
    }
}
