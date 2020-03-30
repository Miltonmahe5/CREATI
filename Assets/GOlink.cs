using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOlink : MonoBehaviour
{
    public string Link1;
    public string Link2;
    public string Link3;
    public string Link4;
    public string Link5;
    public string Link6;

    public void Golin1()
    {
        Application.OpenURL(Link1);
    }

    public void Golin2()
    {
        Application.OpenURL(Link2);
    }

    public void Golin3()
    {
        Application.OpenURL(Link3);
    }

    public void Golin4()
    {
        Application.OpenURL(Link4);
    }

    public void Golin5()
    {
        Application.OpenURL(Link5);
    }

    public void Golin6()
    {
        Application.OpenURL(Link6);
    }
}
