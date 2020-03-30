using UnityEngine;
using System.ComponentModel;
using UnityEngine.UI;

public class Tempalate1 : MonoBehaviour {
    [Header("Essentials")]
    public string Url;
    public string DownloadLocation;
    [Header("UI")]
    public Slider ProgressBar;
    public Text DownloadSpeedText;
    public Text LogMessageText;
    public Text DownloadProgressText;
    public Text EstimatedTimeText;
    public Text PercentageText;
    public Button CancelButton;
    public Button DownloadButton;
    public GameObject WhileDownloadingScreen;
    public GameObject BeforeDownloadingScreen;
    DownloadManager Manager = new DownloadManager();
    // Use this for initialization
    void Start () {
        //Setting Default values and Adding Listner.
        BeforeDownloadingScreen.SetActive(true);
        WhileDownloadingScreen.SetActive(false);
        ProgressBar.minValue = 0;
        ProgressBar.maxValue = 100;
        CancelButton.onClick.AddListener(() => Manager.CancleDownload());
	}
    
  
    void Update()
    {
        //Update Values
        ProgressBar.value = Mathf.Lerp(ProgressBar.value, Manager.GetCurrentProgress(), Time.deltaTime*10);
        DownloadSpeedText.text = Manager.GetDownloadSpeedStringFormated();
        EstimatedTimeText.text = Manager.GetRemainingTimeFormatedString();
        PercentageText.text = Manager.GetCurrentProgress().ToString("F0") + "%";
        DownloadProgressText.text = Manager.GetFormatedDownloadProgress();
        LogMessageText.text = Manager.GetLogMessages();
    }
    //This function Starts a Non Resumable Download.
	public void StartDownload()
    {
        

        Manager.DownloadFileAsync(Url, DownloadLocation,ribit.Utils.DownloadMode.NonResumable);
        print(Manager.GetDownloadFileName());
            
    }
	//This method Changes the Screen to Download Screen.
   public void ChangeScreen()
    {
        WhileDownloadingScreen.SetActive(true);
        BeforeDownloadingScreen.SetActive(false);
    }
}
