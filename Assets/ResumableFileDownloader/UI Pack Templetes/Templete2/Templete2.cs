using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Templete2 : MonoBehaviour {
    public InputField Searchtfield;
    public InputField DestinationField;
    public Button DownloadButton;
    public GameObject DownloadWidget;
    public GameObject ContentHolder;
	// Use this for initialization
	void Start () {
        //Adding listner.
        DownloadButton.onClick.RemoveAllListeners();
        DownloadButton.onClick.AddListener(() => AddDownload());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //Adds a new Instance of DownloadWidget.
    void AddDownload()
    {
       var DownloadWid = Instantiate(DownloadWidget, ContentHolder.transform)as GameObject;
        DownloadWid.GetComponent<DownloadWidget>().StartDownload(Searchtfield.text, DestinationField.text);
    }
}
