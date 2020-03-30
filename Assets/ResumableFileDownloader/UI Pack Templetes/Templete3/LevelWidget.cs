using UnityEngine;
using System.ComponentModel;
using UnityEngine.UI;
public class LevelWidget : MonoBehaviour {
    /// <summary>
    /// Download Url
    /// </summary>
    [Tooltip("Download URL")]
    public string DownloadUrl;
    /// <summary>
    /// Location to store Downloaded file
    /// </summary>
    [Tooltip("Location to store Downloaded file")]
    public string DownloadLocation;
    /// <summary>
    /// Security validation of ssl for some sites that might ask.
    /// </summary>
    [Tooltip("Set this to secured if you have no idea about this")]

    [Header("UI")]
    public Slider ProgressBar;
    public Text PercentageText;
    public Button DownloadButton;
    public Text DownloadButtonText;
    DownloadManager Manager;
    private bool DownloadCompletedCalled = false;
    // Use this for initialization
    void Start()
    {
        Manager = new DownloadManager();
        //setting the value of progress bar.
        ProgressBar.minValue = 0;
        ProgressBar.maxValue = 100;
        //adding listeners to the button
        DownloadButton.onClick.RemoveAllListeners();
        DownloadButton.onClick.AddListener(() => StartDownload(DownloadUrl,DownloadLocation));
        
        //triggering the button and the progressbar because in the starting we need download button.
        DownloadButton.gameObject.SetActive(true);
        ProgressBar.gameObject.SetActive(false);
    }
  
    void Update()
    {
        //updating values
        ProgressBar.value = Mathf.Lerp(ProgressBar.value, Manager.GetCurrentProgress(), Time.deltaTime * 10);
        
        PercentageText.text = "Downloading " + Manager.GetCurrentProgress().ToString("F0") + "%";
        if (!Manager.GetDownloadCompletionsStatus())
        {
            DownloadButtonText.text = "Download Now";
        }
        else
        {
            DownloadButtonText.text = "Play";
        }
        if(DownloadCompletedCalled == false && Manager.GetDownloadCompletionsStatus() == true)
        {
            ChangeButtons();
        }
    }
    //Download Method Created with url and download Location
    public void StartDownload(string Url, string DownloadLocation)
    {
    //Calling download file from DownloadManager
    Manager.DownloadFileAsync(Url, DownloadLocation,ribit.Utils.DownloadMode.Resumable);
        //Switching button and Progress bar.
        DownloadButton.gameObject.SetActive(false);
        ProgressBar.gameObject.SetActive(true);
    }
    
    void OnDownloadButtonClickedAfterDownload()
    {
        //Add your action here
    }
    void ChangeButtons()
    {
        DownloadButton.gameObject.SetActive(true);
        ProgressBar.gameObject.SetActive(false);
        DownloadButton.onClick.RemoveAllListeners();
        DownloadButton.onClick.AddListener(() => OnDownloadButtonClickedAfterDownload());
        DownloadCompletedCalled = true;
    }
}

