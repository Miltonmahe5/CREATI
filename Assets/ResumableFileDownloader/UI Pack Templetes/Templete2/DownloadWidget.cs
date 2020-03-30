using UnityEngine;
using System.ComponentModel;
using UnityEngine.UI;
public class DownloadWidget : MonoBehaviour
{
    
    

    [Header("UI")]
    public Slider ProgressBar;
    public Text DownloadSpeedText;
    public Text LogMessageText;
    public Text DownloadProgressText;
    public Text EstimatedTimeText;
    public Text PercentageText;
    public Button CancelButton;
    public ToggleSprite PauseButton;
    DownloadManager Manager = new DownloadManager();
    private bool DestroyCalled = false;
    // Use this for initialization
    void Start()
    {
        //setting Default values and Adding Listner to the Cancel button.
        ProgressBar.minValue = 0;
        ProgressBar.maxValue = 100;
        CancelButton.onClick.AddListener(() => Manager.CancleDownload());
        PauseButton.Onclick(() => Manager.SwitchPause());
    }
    void Update()
    {
        //updating the values according to the data received
        ProgressBar.value = Mathf.Lerp(ProgressBar.value, Manager.GetCurrentProgress(), Time.deltaTime * 10);
        DownloadSpeedText.text = Manager.GetDownloadSpeedStringFormated();
        EstimatedTimeText.text = Manager.GetRemainingTimeFormatedString();
        PercentageText.text = Manager.GetCurrentProgress().ToString("F0") + "%";
        DownloadProgressText.text = Manager.GetFormatedDownloadProgress();
        LogMessageText.text = Manager.GetLogMessages();
        //destroys this instance when Download is Complete
        if(Manager.GetDownloadCompletionsStatus() && !DestroyCalled || Manager.GetCancellationStatus() == true && !DestroyCalled)
        {
            Destroy();
        }
    }
    //Method that calls Download
    public void StartDownload(string Url,string DownloadLocation)
    {


		Manager.DownloadFileAsync(Url,Application.dataPath + "/" + DownloadLocation,ribit.Utils.DownloadMode.NonResumable);

    }
    //Method that Destroys this instance after 3sec.
    void Destroy()
    {
        DestroyCalled = true;
        Destroy(gameObject, 3);
    }
}
