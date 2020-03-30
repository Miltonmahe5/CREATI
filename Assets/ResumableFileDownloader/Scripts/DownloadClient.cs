using System.Net;
using System;
using System.IO;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO.Compression;

namespace ribit
{
    namespace Utils
    {
        //Creating an enum for different Dowmload Modes.
        public enum DownloadMode
        {
            Resumable, NonResumable,
        }
        //Creating an enum for different Connection Types.
        public enum ConnectionType
        {
            WIFI, MOBILENET, NOTREACHABLE
        }
        //This is the Download CLient Class Used for Downloading Files.
        public class DownloadClient
        {
            //Defining Different Variables for using it in the DownloadClient Class.
            private long BytesDownloaded;
            private long TotalBytesToDownload;
            private long TotalBytesThisSession;
            int BytesRead;
            private Uri _url;
            private string _downloadLocation;
            private DownloadMode _mode;
            private byte[] _Buffer;
            bool _Cancelled;
            bool _isbusy = false;
            public bool Paused = false;
            public bool _DownloadAnyway;
            //Creating Constructor for DownloadCLient Class.
            public DownloadClient(string url, string DownloadLocation, DownloadMode mode,bool DownloadAnyway = true)
            {
                _url = new Uri(url);
                _downloadLocation = DownloadLocation;
                _mode = mode;
                _Buffer = new byte[1024 * 4];
                _DownloadAnyway = DownloadAnyway;
            }
            public DownloadClient(string url, string DownloadLocation, DownloadMode mode, byte[] Buffer, bool DownloadAnyway = true)
            {
                _url = new Uri(url);
                _downloadLocation = DownloadLocation;
                _mode = mode;
                _Buffer = Buffer;
                _DownloadAnyway = DownloadAnyway;
            }
            //Defining the Thread Here.
            Thread AsyncThread;
            //Creating few Delegate Callbacks and event to report progress etc.
            public delegate void DownloadProgressChanged(OnProgressChangedEvent e);
            public delegate void DownloadCompleted(OnDownloadCompletedEvent e);
            public event DownloadProgressChanged ProgressChangedEvent;
            public event DownloadCompleted DownloadCompletedEvent;
            //Start Download if the giving Client is not busy.
            public void StartDownloadAsync()
            {
                //checking if we are not busy and then starting file download.
				if (!_isbusy) {
					AsyncThread = new Thread (new ThreadStart (StartAsyncDownloadThread));
					AsyncThread.Start ();
				}
				UnityEngine.Debug.Log (_isbusy);
            }
            //Cancels the given download.
            public void CancelAsync()
            {
                _Cancelled = true;
            }
            //Pause/Resume functionality for the download
            public void SwitchPause()
            {
                if (Paused)
                {
                    Paused = false;
                }
                else
                {
                    Paused = true;
                }
            }
            //Starts the background thread for file download and reports the update to the delegates created above.
            private void StartAsyncDownloadThread()
            {
                //trying and catching exception for creating download request.
                try
                {
                    //creating a webrequest and defining its method as GET to download file.
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
                    request.KeepAlive = true;
                    request.Timeout = 30000;
                    request.ReadWriteTimeout = 30000;
                    FileStream DownloadFile;
                    request.Method = "GET";
                    //Getting Total Size of the file to download
                    TotalBytesToDownload = request.GetResponse().ContentLength;
                    FileInfo FileToDownload = new FileInfo(_downloadLocation);

                    //Switching cases for resumable and non resumable as per the value passed in the constructor.
                    if (_mode == DownloadMode.NonResumable)
                    {
                        if (File.Exists(_downloadLocation))
                        {
                            if (FileToDownload.Length > 0 && FileToDownload.Length < TotalBytesToDownload)
                            {
                                if (_DownloadAnyway)
                                {
                                    FileToDownload.Delete();
                                }
                                else
                                {
                                    UnityEngine.Debug.Log("File Already Exsists");
                                    AsyncThread.Join();
                                    return;
                                    
                                }
                                
                            }
                        }
                    }
                    else if (_mode == DownloadMode.Resumable)
                    {

                        if (File.Exists(_downloadLocation))
                        {
                            if (_DownloadAnyway && FileToDownload.Length < TotalBytesToDownload)
                            {
                                request = (HttpWebRequest)WebRequest.Create(_url);
                                request.Method = "GET";
                                request.AddRange((int)FileToDownload.Length);
                                UnityEngine.Debug.Log("Request Headers: " + request.Headers.ToString());
                            }
                            else
                            {
                                UnityEngine.Debug.Log("File Already Exsists");
                                if (DownloadCompletedEvent != null)
									DownloadCompletedEvent(new OnDownloadCompletedEvent(null, false));
                                AsyncThread.Join();
                                return;
                            }
                        }

                    }
                    ///Getting response from the server.
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    //checking if the file exists and it exists and server supports partial content the append the incomming data of else create new file of 0B.
                    if (File.Exists(_downloadLocation))
                    {
                        if (response.StatusCode == HttpStatusCode.PartialContent)
                        {
                            DownloadFile = new FileStream(_downloadLocation, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                        }
                        else
                        {
                            DownloadFile = new FileStream(_downloadLocation, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                            DownloadFile.SetLength(0);
                        }
                    }
                    else {

                        DownloadFile = new FileStream(_downloadLocation, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                    }
                    BytesDownloaded = DownloadFile.Length;

                    TotalBytesThisSession = 0;

                    UnityEngine.Debug.Log("Response Headers: " + response.Headers);
                    UnityEngine.Debug.Log("Response Status: " + response.StatusDescription);
                    Stream ResponseStream = response.GetResponseStream();
                    //Writing Bytes to files
                    BytesRead = ResponseStream.Read(_Buffer, 0, _Buffer.Length);
                    while (BytesRead > 0 && !_Cancelled)
                    {
                        if (!Paused)
                        {
                            _isbusy = true;
                            DownloadFile.Write(_Buffer, 0, BytesRead);
                            BytesDownloaded += BytesRead;
                            TotalBytesThisSession += BytesRead;
                            BytesRead = ResponseStream.Read(_Buffer, 0, _Buffer.Length);

                            //Reporting progress if the given event is registered.
                            if (ProgressChangedEvent != null)
                            {
                                ProgressChangedEvent(new OnProgressChangedEvent(BytesDownloaded, TotalBytesToDownload, TotalBytesThisSession, Paused));
                            }

                        }




                    }
                    //Report the download completion at the end of while loop.
                    if (DownloadCompletedEvent != null)
                    {
                        if (!_Cancelled)
                        {
                            DownloadCompletedEvent(new OnDownloadCompletedEvent(null, false));
                        }
                        else
                        {
                            DownloadCompletedEvent(new OnDownloadCompletedEvent(null, true));
                        }
                    }
                    DownloadFile.Flush();
                    DownloadFile.Close();
                    response.Close();
					AsyncThread.Abort();
                    AsyncThread.Join();
                    _isbusy = false;
                    


                }
				catch (Exception ex)
                {

                    _isbusy = false;
                    if (DownloadCompletedEvent != null)
                    {
                        DownloadCompletedEvent(new OnDownloadCompletedEvent(ex, false));
                    }
                }
            }
        }
        //event class created for reporting progress.
        public class OnProgressChangedEvent
        {

            public readonly long BytesReceived;
            public readonly long TotalBytesToReceive;
            public readonly long TotalBytesThisSession;
            public readonly int Progress;
            public readonly bool Paused;
            public OnProgressChangedEvent(long bytesReceived, long totalBytesToReceive, long totalBytesThisSession, bool paused)
            {
                BytesReceived = bytesReceived;
                TotalBytesToReceive = totalBytesToReceive;
                TotalBytesThisSession = totalBytesThisSession;
                Paused = paused;
                if (Progress <= 100)
                {
                    Progress = (int)((float)bytesReceived / totalBytesToReceive * 100);
                }
                else
                {
                    Progress = 100;
                }
            }


        }
        public class OnDownloadCompletedEvent
        {
            public readonly Exception error;
            public readonly bool Cancelled;
            public OnDownloadCompletedEvent(Exception Error, bool cancelled)
            {
                error = Error;
                Cancelled = cancelled;
            }

        }
        //This class is used for checking internet connection and conection type.
        public class ConnectionChecker
        {
            /// <summary>
            /// This does not check connection exactly this is just to determine connection type.
            /// </summary>
            /// <returns></returns>
            public static ConnectionType GetConnectionType()
            {
                if (UnityEngine.Application.internetReachability == UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork)
                {
                    return ConnectionType.WIFI;
                }
                else if (UnityEngine.Application.internetReachability == UnityEngine.NetworkReachability.ReachableViaCarrierDataNetwork)
                {
                    return ConnectionType.MOBILENET;
                }
                else
                {
                    return ConnectionType.NOTREACHABLE;
                }
            }
            /// <summary>
            /// This can Check Internet Availability.
            /// </summary>
            /// <returns></returns>
            public static bool GetConnectionStatus()
            {
                try
                {
                    WebClient client = new WebClient();
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; });
                    client.OpenRead("https://www.google.com");
                    return true;
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.Log(ex);
                    return false;
                }
            }

        }
    }
}

