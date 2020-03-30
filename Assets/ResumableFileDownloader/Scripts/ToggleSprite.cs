using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class ToggleSprite : MonoBehaviour {
    public Sprite On;
    public Sprite Off;
    public bool IsOn;
	// Use this for initialization
	void Start () {
        transform.GetComponent<Button>().onClick.AddListener(() => Toggle());
        IsOn = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (IsOn)
        {
            transform.GetComponent<Image>().sprite = Off;
        }
        else
        {
            transform.GetComponent<Image>().sprite = On;
        }
    }
    void Toggle()
    {
        if (IsOn)
        {
            IsOn = false;
        }
        else
        {
            IsOn = true;
        }
    }
    public void Onclick(UnityEngine.Events.UnityAction Call)
    {
        transform.GetComponent<Button>().onClick.AddListener(Call);
    }
}
