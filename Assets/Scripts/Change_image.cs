using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change_image : MonoBehaviour {

	public RawImage img;

	public InputField path;
	public string pathS;
	public WWW www;

	void Awake()
	{
		img = this.gameObject.GetComponent<RawImage> ();
	}

	// Use this for initialization
	IEnumerator image ( string ruta, WWW www) {
		www = new WWW ("file:///C:/Users/DS/Desktop/"+ruta+".png");
		yield return www ;
		img.texture = www.texture;
		img.SetNativeSize ();
	}

	// Update is called once per frame
	void Update () {
		pathS = path.text;
	}


	public void cargarImagen(){

			StartCoroutine (image(pathS, www));
	
	}
}