using System.Net;
using System.ComponentModel;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using ribit.Utils;

    public class DownloadManager
    {
        #region Private Variables
        DownloadClient Client;
        private float DownloadSpeed;
        private float DataDownloaded;
        private float FileSize;
        private float RemainingTime;
        Stopwatch SW = new Stopwatch();
        private string LogMessages;
        private float Progress;
        float LastBytesReceived;
        string FileS;
        private string DownloadFileName;
        private bool IsDownloadComplete = false;
        private bool IsDownloadCancelled = false;
        private bool Paused;
        #endregion
        /// <summary>
        /// By pass Ssl Certificate 
        /// </summary>
        public enum RemainingTimeFormat
        {
            Format1,Format2,Format3,
        }
    /// <summary>
    /// Start Downloading the file without disturbing the Ongoing Thread i.e asynchronously.
    /// </summary>
    /// <param name="URL">url of the file to be downloaded</param>
    /// <param name="FileLocation">Loaction where to store the file after Download</param>
    public void DownloadFileAsync(string URL, string FileLocation,DownloadMode Mode,string FileName = "",bool DownloadAnyway = true)
        {
        //creating a URI
            Uri url = new Uri(URL);
        //Getting DownloadFile name from the server.
            DownloadFileName = System.IO.Path.GetFileName(url.LocalPath);
        //Setting File Location and File name.
            if (FileName != "")
            {
            Client = new DownloadClient(URL, FileLocation+"/"+FileName, Mode,DownloadAnyway);
            }
            else
            {
            Client = new DownloadClient(URL, FileLocation + "/" + DownloadFileName, Mode,DownloadAnyway);
            }
            //Registering to the events on DownloadClient Class.
            Client.ProgressChangedEvent += OnProgressChanged;
            Client.DownloadCompletedEvent += OnDownloadCompleted;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; });
            DownloadSpeed = 0; DataDownloaded = 0; FileSize = 0;
            SW.Reset();
            SW.Start();
           
            Client.StartDownloadAsync();
           
            
        IsDownloadCancelled = false;
           
           
        }
        /// <summary>
        /// Called when there is change in Download Progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnProgressChanged(OnProgressChangedEvent e)
    {    
        if (IsDownloadComplete == false)
        {
            DataDownloaded = e.BytesReceived;
            FileSize = e.TotalBytesToReceive;
            DownloadSpeed = (int)(e.TotalBytesThisSession / SW.Elapsed.TotalSeconds);
            RemainingTime = (e.TotalBytesToReceive - e.BytesReceived) / DownloadSpeed;
            Progress = e.Progress;
            Paused = e.Paused;
            if (e.Progress >= 100)
            {
                IsDownloadComplete = true;

            }
            if (e.Paused)
            {
                LogMessages = "Download Paused";
            }
            else
            {
                LogMessages = "Downloading " + GetDownloadFileName();
            }
        }
        else
        {
            DataDownloaded = FileSize;
            DownloadSpeed = 0;
            RemainingTime = 0;
            Progress = 100;
        }
            
        }
        /// <summary>
        /// Called When Download Completes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnDownloadCompleted(OnDownloadCompletedEvent e)
        {
        if(e.error != null)
	    {
			if (e.error.GetType() != typeof(System.Threading.ThreadAbortException)) {

				UnityEngine.Debug.Log (e.error);
				LogMessages = e.error.ToString ();
			}
        }else if (e.Cancelled)
        {
           LogMessages = "Download was cancelled by user";
           IsDownloadCancelled = true;
        }
        else
        {
                LogMessages = "Download Complete";
                
        }
		Progress = 100;
        }
    //Cancel the download.
        public void CancleDownload()
        {

        Client.CancelAsync();
          
        }
    //Pause or Resume Download.
        public void SwitchPause()
        {
            Client.SwitchPause();
        }
        //Amount of files downloaded in various units.
        /// <summary>
        /// Amount of file downloaded in Bytes(B)
        /// </summary>
        /// <returns></returns>
        public float GetFileDownloadedInBytes()
        {
            return DataDownloaded;
        }
        /// <summary>
        /// Amount of file downloaded in KiloBytes(KB)
        /// </summary>
        /// <returns>Data Downloaded in KB</returns>
        public float GetFileDownloadedInKiloBytes()
        {
            return DataDownloaded / 1024;
        }
        /// <summary>
        /// Amount of file downloaded in MegaBytes(MB)
        /// </summary>
        /// <returns>Data Downloaded in MB</returns>
        public float GetFileDownloadedInMegaBytes()
        {
            return DataDownloaded / (1024 * 1024);
        }
        /// <summary>
        /// Amount of file downloaded in GigaBytes(GB)
        /// </summary>
        /// <returns>Data Downloaded in GB</returns>
        public float GetFileDownloadedInGigaBytes()
        {
            return DataDownloaded / (1024 * 1024 * 1024);
        }
        //File Size in Different Units
        /// <summary>
        /// Amount of File in Bytes(B)
        /// </summary>
        /// <returns></returns>
        public float GetFileSizeInKiloBytes()
        {
            return FileSize;
        }
        /// <summary>
        /// File Size in KiloBytes(KB)
        /// </summary>
        /// <returns></returns>
        public float GetFileSizeInBytes()
        {
            return FileSize / 1024;
        }
        /// <summary>
        /// File Size in MegaBytes(MB)
        /// </summary>
        /// <returns></returns>
        public float GetFileSizeInMegaBytes()
        {
            return FileSize / (1024 * 1024);
        }
        /// <summary>
        /// File Size in GigaBytes(GB)
        /// </summary>
        /// <returns></returns>
        public float GetFileSizeInGigaBytes()
        {
            return FileSize / (1024 * 1024 * 1024);
        }
        //Download Speed in Different Units
        /// <summary>
        /// Download Speed in B/s
        /// </summary>
        /// <returns></returns>
        public float GetDownloadSpeedInBytesPerSecond()
        {
            return DownloadSpeed;
        }
        /// <summary>
        /// DownloadSpeed in KB/s
        /// </summary>
        /// <returns></returns>
        public float GetDownloadSpeedInKiloBytesPerSecond()
        {
            return DownloadSpeed / 1024;
        }
        /// <summary>
        /// Download Speed in MB/s
        /// </summary>
        /// <returns></returns>
        public float GetDownloadSpeedInMegaBytesPerSecond()
        {
            return DownloadSpeed / (1024 * 1024);
        }
        //TimeRemaining
        /// <summary>
        /// Estimated Download Time in Seconds
        /// </summary>
        /// <returns></returns>
        public float GetEstimatedTimeInSceconds()
        {
            return RemainingTime;
        }
        /// <summary>
        /// Estimated Download Time in Minutes
        /// </summary>
        /// <returns></returns>
        public float GetEstimatedTimeInMunites()
        {
            return RemainingTime / 60;
        }
        /// <summary>
        /// Estimated Download Time in Hours
        /// </summary>
        /// <returns></returns>
        public float GetEstimatedTimeInHours()
        {
            return RemainingTime / 360;
        }
        /// <summary>
        /// Estimated Download Time in Days
        /// </summary>
        /// <returns></returns>
        public float GetEstimatedTimeInDays()
        {
            return RemainingTime / (360 * 24);
        }
        //Progress
        /// <summary>
        /// Current Download Progress
        /// </summary>
        /// <returns></returns>
        public float GetCurrentProgress()
        {
            return Progress;
        }
        public string GetLogMessages()
        {
            return LogMessages;
        }
        //Formated Strings
        /// <summary>
        /// returns a well formated string of download speed.
        /// </summary>
        /// <returns></returns>
        public string GetDownloadSpeedStringFormated()
        {

            if (DownloadSpeed > 1024 && DownloadSpeed <= (1024*1024))
            {
                return string.Format("{0}KB/s", GetDownloadSpeedInKiloBytesPerSecond().ToString("0.0"));
            }
            else if (DownloadSpeed > (1024 * 1024) )
            {
                return string.Format("{0}MB/s", GetDownloadSpeedInMegaBytesPerSecond().ToString("0.0"));
            }
            else
            {
                return string.Format("{0}B/s", GetDownloadSpeedInBytesPerSecond().ToString("0.0"));
            }
        }
        /// <summary>
        /// returns a well formated string of remaining download time.
        /// </summary>
        /// <returns></returns>
        public string GetRemainingTimeFormatedString(RemainingTimeFormat Format = RemainingTimeFormat.Format1)
        {
            int Seconds = ((int)GetEstimatedTimeInSceconds() % 60);
            int Minutes = ((int)GetEstimatedTimeInSceconds()/60 ) % 60;
            int Hours = ((int)GetEstimatedTimeInSceconds()/360) % 60;
            int Days = ((int)GetEstimatedTimeInSceconds()/360*24)% 24;
            if (Format == RemainingTimeFormat.Format1)
            {
                if (GetEstimatedTimeInSceconds() > (60 * 60 * 24))
                {
                    return string.Format("{0} Days {1}Hours {2}Minutes {3}Seconds Left", Days, Hours, Minutes, Seconds);
                }
                else if (GetEstimatedTimeInSceconds() > 360)
                {
                    return string.Format("{0}Hours {1}Minutes {2}Seconds Left", Hours, Minutes, Seconds);
                }
                else if (GetEstimatedTimeInSceconds() > 60)
                {
                    return string.Format("{0}Minutes {1}Seconds Left", Minutes, Seconds);
                }
                else
                {
                    return string.Format("{0}Seconds Left", Seconds);
                }
            }
            else if(Format == RemainingTimeFormat.Format2)
            {
                if (GetEstimatedTimeInSceconds() > (60 * 60 * 24))
                {
                    return string.Format("{0:0}:{1:00} Days Left", Days, Hours, Minutes, Seconds);
                }
                else if (GetEstimatedTimeInSceconds() > 360)
                {
                    return string.Format("{0:0}:{1:00} hours Left", Hours, Minutes, Seconds);
                }
                else if (GetEstimatedTimeInSceconds() > 60)
                {
                    return string.Format("{0:0}:{1:00} minutes Left", Minutes, Seconds);
                }
                else
                {
                    return string.Format("{0}Seconds Left", Seconds);
                }
            }
            else if (Format == RemainingTimeFormat.Format3)
            {

                if (GetEstimatedTimeInSceconds() > (60 * 60 * 24))
                {
                    return string.Format("{0} Days Left", Days);
                }
                else if (GetEstimatedTimeInSceconds() > 360)
                {
                    return string.Format("{0}Hours Left", Hours);
                }
                else if (GetEstimatedTimeInSceconds() > 60)
                {
                    return string.Format("{0}Minutes Left", Minutes);
                }
                else
                {
                    return string.Format("{0}Seconds Left", Seconds);
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// returns well formated downloadprogress string
        /// </summary>
        /// <returns></returns>
        public string GetFormatedDownloadProgress()
        {
           
            if (FileSize > 1024 && FileSize <= (1024*1024))
            {
                FileS = (FileSize / (1024)).ToString("0") + "KB";
            }
            else if (FileSize > (1024 * 1024) && FileSize <= (1024*1024*1024))
            {
                FileS = (FileSize / (1024 * 1024)).ToString("0") + "MB";
                
            }
            else if(FileSize > (1024 * 1024 * 1024))
            {
                FileS = (FileSize / (1024 * 1024 * 1024)).ToString("0") + "GB";
                
            }
            else
            {
                FileS = ((int)FileSize).ToString("0") + "B";
                
            }
            
            if (GetFileDownloadedInBytes() > 1024 && GetFileDownloadedInBytes() <= (1024 * 1024))
            {
                string s = GetFileDownloadedInKiloBytes().ToString("0") + "KB"  +"/" + FileS;
                return s;
            }
            else if (GetFileDownloadedInBytes() > (1024 * 1024) && GetFileDownloadedInBytes() <= (1024 * 1024 * 1024))
            {
                string s = GetFileDownloadedInMegaBytes().ToString("0")  + "MB" + "/" + FileS;
                return s;
            }
            else if (GetFileDownloadedInBytes() > (1024 * 1024 * 1024))
            {
                string s = GetFileDownloadedInGigaBytes().ToString("0")  + "GB" + "/" + FileS;
                return s;
            }
            else
            {
                string s = GetFileDownloadedInBytes().ToString("0")  + "B" + "/" + FileS;
                return s;
            }
            
        }
        /// <summary>
        /// Name of the file you are currently downloading
        /// </summary>
        /// <returns></returns>
        public string GetDownloadFileName()
        {
            return DownloadFileName;
        }
        /// <summary>
        /// returns true when download is complete
        /// </summary>
        /// <returns></returns>
        public bool GetDownloadCompletionsStatus()
        {
            return IsDownloadComplete;
        }
        /// <summary>
        /// returns true if user cancels the download.
        /// </summary>
        /// <returns></returns>
        public bool GetCancellationStatus()
        {
            return IsDownloadCancelled;
        }
    /// <summary>
    /// returns true is download is paused.
    /// </summary>
    /// <returns></returns>
        public bool GetpauseStatus()
        {
            return Paused;
        }
    }
    
    