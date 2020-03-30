using UnityEngine;
using System.ComponentModel;
using UnityEngine.UI;

public class Templete4 : MonoBehaviour {
    [Header("Essentials")]
    public string Url;
    public string DownloadLocation;
    [Header("UI")]
    public Slider ProgressBar;
    public Slider GlintHandeler;
    public Text DownloadSpeedText;
    public Text LogMessageText;
    public Text DownloadProgressText;
    public Text EstimatedTimeText;
    public Text PercentageText;
    public Button CancelButton;
    public Button DownloadButton;
    public Button CancelButton_2;
    public Button Back_Cancel;
    public GameObject WhileDownloadingScreen;
    public GameObject BeforeDownloadingScreen;
    public GameObject CancelScreen;
    private Color GlintAlphaC;
    public ToggleSprite Toggle;
    DownloadManager Manager = new DownloadManager();
    // Use this for initialization
    void Start () {
        //Setting Default values
        ProgressBar.minValue = 0;
        ProgressBar.maxValue = 100;
        GlintHandeler.minValue = 0;
        GlintHandeler.maxValue = 100;
        GlintAlphaC = GlintHandeler.handleRect.GetComponent<Image>().color;
        //Adding Listner to Cancel button
        CancelButton.onClick.RemoveAllListeners();
        CancelButton_2.onClick.RemoveAllListeners();
        CancelButton.onClick.AddListener(() => OnCancelButton());
        CancelButton_2.onClick.AddListener(() => OnCancelButton_2());
        Toggle.Onclick(() => Manager.SwitchPause());
    }
    void Update()
    {
        //Updating the values
        ProgressBar.value = Mathf.Lerp(ProgressBar.value, Manager.GetCurrentProgress(), Time.deltaTime*10);
        DownloadSpeedText.text = Manager.GetDownloadSpeedStringFormated();
        EstimatedTimeText.text = Manager.GetRemainingTimeFormatedString(DownloadManager.RemainingTimeFormat.Format3);
        PercentageText.text = Manager.GetCurrentProgress().ToString("F0") + "%";
        DownloadProgressText.text = Manager.GetFormatedDownloadProgress();
        LogMessageText.text = Manager.GetLogMessages();
        //handling the Glint Effect
        if (Manager.GetCancellationStatus() == false && Manager.GetDownloadCompletionsStatus() == false)
        {
            GlintHandeler.handleRect.GetComponent<Image>().color = GlintAlphaC;
            if (GlintHandeler.value < (ProgressBar.value - 0.1))
            {
                GlintHandeler.value = Mathf.Lerp(GlintHandeler.value, ProgressBar.value, Time.deltaTime * 5);
                if (GlintHandeler.value < (ProgressBar.value * 0.9))
                {
                    GlintAlphaC.a = Mathf.Lerp(GlintAlphaC.a, 1, Time.deltaTime);
                }
                else
                {
                    GlintAlphaC.a = Mathf.Lerp(GlintAlphaC.a, 0, Time.deltaTime);
                }
            }
            else if ((GlintHandeler.value) >= (ProgressBar.value - 0.1))
            {
                GlintHandeler.value = 0;
            }
        }
        else
        {
            GlintHandeler.value = ProgressBar.value;
            GlintAlphaC.a = 0;
        }
        if(Manager.GetDownloadCompletionsStatus() == true)
        {
            CancelButton.gameObject.SetActive(false);
        }
        
    }
    //This function Starts the Download
	public void StartDownload()
    {
        

        Manager.DownloadFileAsync(Url, Application.persistentDataPath,ribit.Utils.DownloadMode.Resumable,"",true);
        CancelButton.GetComponentInChildren<Text>().text = "Cancel";

    }
    //This is used to change the screen to Download Screen.
   public void ChangeScreen()
    {
        BeforeDownloadingScreen.SetActive(false);
        CancelButton.gameObject.SetActive(true);
    }
    public void OnCancelButton()
    {
        if (Manager.GetCancellationStatus() == false && Manager.GetDownloadCompletionsStatus() == false)
        {
            BeforeDownloadingScreen.SetActive(false);
            CancelButton.gameObject.SetActive(false);
            CancelScreen.SetActive(true);
        }
        else
        {
            StartDownload();  
        }
    }
    public void back()
    {
        BeforeDownloadingScreen.SetActive(false);
        CancelButton.gameObject.SetActive(true);
        CancelScreen.SetActive(false);
    }
    public void OnCancelButton_2()
    {
		Manager.CancleDownload ();
        BeforeDownloadingScreen.SetActive(false);
        CancelButton.gameObject.SetActive(true);
        CancelScreen.SetActive(false);
        CancelButton.GetComponentInChildren<Text>().text = "Resume";
    }
}
