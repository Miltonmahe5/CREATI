using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TriLib;
using TriLib.Samples;
using TriLib.Extras;
using System;
using System.Collections.Generic;
using System.IO;
using GracesGames.SimpleFileBrowser.Scripts;



public class Change_Sprite : MonoBehaviour {

	public SpriteRenderer spritin;
	public static GameObject objeto;
	public static string pathS;
	public WWW www;
	public string extension;

	public InputField archivo;


	void Awake()
	{
		objeto =	GameObject.Find ("contenedor");
	}

	// Use this for initialization
	IEnumerator image ( string ruta, WWW www) {
		www = new WWW ("file:///"+ruta);
		yield return www ;
		spritin.sprite= ConvertToSprite(www.texture, spritin);
	}



	IEnumerator video ( string ruta, WWW www) {
		www = new WWW ("file:///"+ruta);
		yield return www ;
		spritin.sprite= ConvertToSprite(www.texture, spritin);
	}

	IEnumerator obj ( string ruta, WWW www) {
		www = new WWW ("file:///"+ruta);
		yield return www ;
		if(ruta !=null){
			if(objeto.transform.childCount>=1){
				Transform c = objeto.transform.GetChild(0);
				c.parent = c;
				Destroy(c.gameObject);
			}
			//Debug.Log (ruta);
			OBJLoader.LoadOBJFile (ruta);

		}

	}

	IEnumerator fbx ( string ruta, WWW www) {
		www = new WWW ("file:///"+ruta);
		yield return www ;
		if (ruta != null) {
			if (objeto.transform.childCount >= 1) {
				Transform c = objeto.transform.GetChild (0);
				c.parent = c;
				Destroy (c.gameObject);
			}
			GameObject myGameObject;

			using (var assetLoader = new AssetLoader ()) {
				//In case you don't have a valid filename, set this to the file extension
				//to help TriLib assigining a file loader to this file
				//example value: ".FBX"
				var filename = ruta;
				var fileData = File.ReadAllBytes (filename);
				myGameObject = assetLoader.LoadFromMemory (fileData, filename);

				myGameObject.transform.parent = Change_Sprite.objeto.transform;
				myGameObject.transform.localPosition = new Vector3 (0, 0, 0);
				myGameObject.transform.localScale = new Vector3 (0.01f, 0.01f, 0.01f);
				myGameObject.transform.localRotation = Quaternion.Euler (0, 0, 0);


			} 
		}
	}




	// Update is called once per frame
	void Update () {
		archivo.text = pathS;

	}



	public static Sprite ConvertToSprite(Texture2D texture,  SpriteRenderer spriton)
	{
		return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), (spriton.sprite.textureRect.width) /(spriton.sprite.bounds.size.x));
	}



	public void cargarImage(){

		string []ext = pathS.Split ('.');
		extension = ext [1];
		if (extension  == "png" || extension  == "jpg") {
			StartCoroutine (image(pathS, www));

		}

		if (extension  == "wmv" || extension  == "mp3" || extension  == "mpeg" || extension  == "mpg" || extension  == "avi"
			|| extension  == "mov" || extension  == "mp4") {
			StartCoroutine (video(pathS, www));

		}
		if (extension  == "obj") {
			StartCoroutine (obj(pathS, www));

		}

		if (extension  == "fbx") {
			StartCoroutine (fbx(pathS, www));

		}


	}
}