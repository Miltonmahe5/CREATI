using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TriLib;
using TriLib.Samples;
using TriLib.Extras;
using System.IO;
using GracesGames.SimpleFileBrowser.Scripts;
using System.Linq;
using UnityStandardAssets.CrossPlatformInput;



    public class Cambiar_Enlazado : MonoBehaviour
{

    /// <summary>
    /// Variables que contienen el sprite donde carga la imagen y el videoplayer del video cargado
    /// </summary>
	public SpriteRenderer spritin;
    public GameObject plane;
    public VideoPlayer movie;
    public int sele = 0;

    /// <summary>
    /// Variable indica si la carta esta siendo seleccionada en el menu
    /// </summary>
    public bool Active = false;


    /// <summary>
    ///  Variables encargadas de tomar la ruta de ubicación del archivo y de identificar el tipo de extensión
    /// </summary>
	public static GameObject objeto;
    public static string pathS;
    public static bool cambioP = false;
    public string extension;
    public string fileLocal;
    public InputField archivo;

    /// <summary>
    /// Coordenadas de posición de los elementos enlazados 
    /// </summary>
    public float posX;
    public float posY;
    public float posZ;


    // Variable de control del cambio en la posición, escala y rotación
    float ratePos = 10.0f;

    /// <summary>
    /// Variables de control de la rotación en los ejes x,y, y z. Además de la escala
    /// </summary>
	public float angX;
    public float angY;
    public float angZ;
    public float scale;


    //panel de selección de marcador y de carga de contenido
    public GameObject panelCarga;


    //funciones para actualizar datos UI

    public delegate void mydelegate();
    public static mydelegate upContent;


    void Start()
    {
        upContent += cargarContenido;
        //GameObject.FindGameObjectWithTag("agregar").GetComponent<Animator>().Play("Normal");
    }

    /// <summary>
    ///  Subir video
    /// </summary>
    /// <param name="ruta"></param>
    /// <returns>Video y audio en el componente Video Player</returns>
    IEnumerator video(string ruta)
    {
        WWW www = new WWW("file:///" + ruta);
        yield return www;
        Debug.Log(www.url);

        movie = plane.GetComponent<VideoPlayer>();
        movie.url = (www.url).Replace("file:///", "");
        movie.audioOutputMode = VideoAudioOutputMode.AudioSource;
        movie.controlledAudioTrackCount = 1;
        movie.EnableAudioTrack(0, true);
        movie.SetTargetAudioSource(0, plane.GetComponent<AudioSource>());
        movie.Play();
        //movie.Pause();
        // Destruye cualquier elemento hijo del contenedor de modelos 3d
        if (objeto.transform.childCount >= 1)
        {
            Transform c = objeto.transform.GetChild(0);
            c.parent = c;
            Destroy(c.gameObject);
        }
        plane.SetActive(true);
        plane.GetComponent<MeshRenderer>().enabled = true;
        plane.transform.localPosition = new Vector3(0, 0, 0);
        plane.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
        plane.transform.localRotation = Quaternion.Euler(90, -90, 90);



    }

    /// <summary>
    ///  Subir imagen
    /// </summary>
    /// <param name="ruta"></param>
    /// <returns>Imagen cargada en el componente sprite</returns>
    IEnumerator image(string ruta)
    {
        WWW www = new WWW("file:///" + ruta);
        yield return www;
        // Destruye cualquier elemento hijo del contenedor de modelos 3d
        if (objeto.transform.childCount >= 1)
        {
            Transform c = objeto.transform.GetChild(0);
            c.parent = c;
            Destroy(c.gameObject);
        }

        spritin.sprite = ConvertToSprite(www.texture, spritin);
        spritin.transform.localPosition = new Vector3(0, 0, 0);
        spritin.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        spritin.transform.localRotation = Quaternion.Euler(90, 0, 0);
    }

    /// <summary>
    ///  Subir obj
    /// </summary>
    /// <param name="ruta"></param>
    /// <returns>Carga un OBJ en la escena</returns>
    IEnumerator obj(string ruta)
    {
        WWW www = new WWW("file:///" + ruta);
        yield return www;

        if (ruta != null)
        {
            // Destruye cualquier elemento hijo del contenedor de modelos 3d
            if (objeto.transform.childCount >= 1)
            {
                Transform c = objeto.transform.GetChild(0);
                c.parent = c;
                Destroy(c.gameObject);
            }
            // Función encargada decargar el OBJ
            OBJLoader.LoadOBJFile(ruta);
            var rendererComponents = objeto.GetComponentsInChildren<Renderer>(true);
            foreach (var component in rendererComponents)
                component.enabled = false;
            Cambiar_Enlazado.objeto.transform.localPosition = new Vector3(0, 0, 0);
            Cambiar_Enlazado.objeto.transform.localScale = new Vector3(1, 1, 1);
            Cambiar_Enlazado.objeto.transform.localRotation = Quaternion.Euler(90, 0, 0);


        }

    }

    /// <summary>
    ///  Subir fbx
    /// </summary>
    /// <param name="ruta"></param>
    /// <returns>Carga un FBX en la escena</returns>
    IEnumerator fbx(string ruta)
    {
        WWW www = new WWW("file:///" + ruta);
        yield return www;
        if (ruta != null)
        {
            // Destruye cualquier elemento hijo del contenedor de modelos 3d
            if (objeto.transform.childCount >= 1)
            {
                Transform c = objeto.transform.GetChild(0);
                c.parent = c;
                Destroy(c.gameObject);
            }
            GameObject myGameObject;

            using (var assetLoader = new AssetLoader())
            {
                //In case you don't have a valid filename, set this to the file extension
                //to help TriLib assigining a file loader to this file
                //example value: ".FBX"
                var filename = ruta;
                var fileData = File.ReadAllBytes(filename);
                myGameObject = assetLoader.LoadFromMemory(fileData, filename);

                myGameObject.transform.parent = Cambiar_Enlazado.objeto.transform;
                myGameObject.transform.localPosition = new Vector3(0, 0, 0);
                myGameObject.transform.localScale = new Vector3(0.006f, 0.006f, 0.006f);
                myGameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                var rendererComponents = objeto.GetComponentsInChildren<Renderer>(true);
                foreach (var component in rendererComponents)
                    component.enabled = true;
                Cambiar_Enlazado.objeto.transform.localPosition = new Vector3(0, 0, 0);
                Cambiar_Enlazado.objeto.transform.localScale = new Vector3(1, 1, 1);
                Cambiar_Enlazado.objeto.transform.localRotation = Quaternion.Euler(90, 0, 0);

            }
        }
    }




    void Update()
    {      


    }


    public void cargarContenido()
    {
        if (Active && this.GetComponent<DefaultTrackableEventHandler>().stateCard)
        {

            string extension = pathS.Split('.')[pathS.Split('.').Length - 1];
            fileLocal = pathS.Split(char.Parse("/"))[pathS.Split(char.Parse("/")).Length - 1];



			if (extension == "png" || extension == "jpg" || extension == "jpeg")
            {
                objeto = GameObject.Find("contenedor" + TouchLoadContent.marcador);
                this.transform.Find("Sprite").gameObject.SetActive(true);
                plane.GetComponent<VideoPlayer>().enabled = false;
                plane.GetComponent<MeshRenderer>().enabled = false;
                plane.SetActive(false);
                sele = 0;
                StartCoroutine(image(pathS));

            }

            if (extension == "wmv" || extension == "mp3" || extension == "mpeg" || extension == "mpg"
                || extension == "mov" || extension == "mp4")
            {
                objeto = GameObject.Find("contenedor" + TouchLoadContent.marcador);
                plane.GetComponent<VideoPlayer>().enabled = true;
                plane.SetActive(true);
                sele = 1;
                this.transform.Find("Sprite").gameObject.SetActive(false);
                StartCoroutine(video(pathS));

            }


            if (extension == "obj")
            {
                objeto = GameObject.Find("contenedor" + TouchLoadContent.marcador);
                plane.SetActive(false);
                plane.GetComponent<VideoPlayer>().enabled = false;
                plane.GetComponent<MeshRenderer>().enabled = false;
                sele = 2;
                this.transform.Find("Sprite").gameObject.SetActive(false);
                Debug.Log(objeto.name);
                StartCoroutine(obj(pathS));

            }

            if (extension == "fbx")
            {
                objeto = GameObject.Find("contenedor" + TouchLoadContent.marcador);
                plane.SetActive(false);
                plane.GetComponent<VideoPlayer>().enabled = false;
                plane.GetComponent<MeshRenderer>().enabled = false;
                sele = 2;
                this.transform.Find("Sprite").gameObject.SetActive(false);
                StartCoroutine(fbx(pathS));

            }
            //GameObject.FindGameObjectWithTag("agregar").GetComponent<Animator>().Play("Normal");

        }
        else {

            Debug.Log("No Valido");
        }
        }

  

    public static Sprite ConvertToSprite(Texture2D texture, SpriteRenderer spriton)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), (spriton.sprite.textureRect.width) / (spriton.sprite.bounds.size.x));

    }


    public void changeStatus()
    {
        cambioP = false;
    }

    //--------------funciones del desplazamiento-------------------//

    public void adelante()
    {
        if (Active)
        {
            this.transform.forward = transform.forward * ratePos;
        }
    }



}   