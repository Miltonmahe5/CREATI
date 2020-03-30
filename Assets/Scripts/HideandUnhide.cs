using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideandUnhide : MonoBehaviour
{

    public GameObject panel;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void HideUnhide()
    {

        if (panel.activeInHierarchy)
        {
            panel.SetActive(false);

        }
        else
        {
            panel.SetActive(true);


        }
    }
}