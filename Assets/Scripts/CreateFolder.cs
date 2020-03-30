using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.ComponentModel;

public class CreateFolder : MonoBehaviour {

	//private TitlesList tl;
	private string pathStart;
	[Header("Essentials")]
	public string[] Url;
	public string DownloadLocation;
	DownloadManager Manager = new DownloadManager();
	void Start () {
		pathStart = "/storage/emulated/0/CREati";
		if (Directory.Exists (pathStart) == false) {
			System.IO.Directory.CreateDirectory (pathStart);
			StartDownload ();
			//Directory.CreateDirectory(pathStart);
		} 

		//tl = GameObject.Find ("MenuController").GetComponent<TitlesList> ();
		//Load ();
	}

	public void StartDownload()
	{

		for(int i=0;i<=Url.Length;i++){
		Manager.DownloadFileAsync(Url[i], DownloadLocation,ribit.Utils.DownloadMode.NonResumable);
		///print(Manager.GetDownloadFileName());
		}
	}
}
