using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteCardImage : MonoBehaviour
{
    Image spriteCardShow;
    Manager mann;
    public string numImage;

    void Awake()
    {
        mann = FindObjectOfType<Manager>();
        spriteCardShow = GetComponent<Image>();
    }

    public void ImagenToShow(int index)
    {
        spriteCardShow.sprite = mann.imageCard[index];
        Debug.Log("index" + index);
        numImage = (index).ToString();
    }
}
