using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;



public class Edit_Transform_Objects : MonoBehaviour
{
    /// <summary>
    /// Llamado de las variables relacionadas con la tarjeta y el contenido enlazado a esta.
    /// </summary>
    public int carta = 0; // número de la carta seleccionada a modificar
    public int seleccion; // tipo de contenido caragado 0: imagen, 1:video y 2: modelos 3d (obj, fbx)
    public GameObject marcador; // Gameobject del contenido enlazado a la carta
    Transform objeto; //  transform del contenido cargado


    GameObject[] imageTargets;
    public bool targetSeleccioando  = false;


    // Llamado del modelo 3d de los ejes que representa las transformaciones 
    public GameObject ejes;
    public GameObject contenedor_ejes;

    //  Llamado de los elementos del UI Joystick posición
    public GameObject posXY;
    public GameObject posZ;
    // Llamado de los elementos del UI Joystick rotación
    public GameObject rotXY;
    public GameObject rotZ;
    // Llamado de los elementos del UI Joystick escala
    public GameObject scaleX;


    // Joystick temporales del UI
    GameObject joyXY;
    GameObject joyZ;


    // Variables generales
    public int transMode = 0; // posicion 1, // rotacion 2, // escala 3
    float thersholdJoysXY;
    float thersholdJoysZ;



    //Arreglo con info de la posición de cada elemento
    public Vector3[] guardar_pos_ejes = new Vector3[24];


    //Delegate para saber la carta 
    public delegate void Delegate();
    public static Delegate updateCard;




    // Use this for initialization
    void Start()
    {
        
        ///transformPlusY(transMode);
        updateCard += selectContent;
        imageTargets = GameObject.FindGameObjectsWithTag("carta");
        setJoysticks(1);
        // variable de ajuste del joystick
        thersholdJoysXY = (-(0.0005f) * (Screen.width) * (joyXY.GetComponent<Joystick>().MovementRange / 2) + (joyXY.GetComponent<Joystick>().MovementRange)) /2;
        thersholdJoysZ = (-(0.0005f) * (Screen.width) * (joyZ.GetComponent<Joystick>().MovementRange / 2) + (joyZ.GetComponent<Joystick>().MovementRange))/2;

        //Inicializa en la carta 1
        marcador = GameObject.Find("ImageTarget1"); //Busca el ImageTarget con el index de la carta

    }



    

    public void selectContent() {

        //Actualiza info de la carta seleccionada           
        carta = TouchLoadContent.marcador;
        marcador = GameObject.Find("ImageTarget" + (carta + 1)); //Busca el ImageTarget con el index de la carta
        /*Verifica que tipo de contenido esta enlazado 0: imagen, 1:video y 2: modelos 3d (obj, fbx) */
        seleccion = marcador.GetComponent<Cambiar_Enlazado>().sele;     

        //Actualiza info del objeto a modificar
        if (marcador.transform.childCount >= 1)
        {
            objeto = marcador.transform.GetChild(seleccion);
        }

        //Ubica el modelo 3D del contenido en posicion y rotación inicialmente
        contenedor_ejes.transform.localRotation = marcador.transform.localRotation;
        ejes.transform.localRotation = objeto.localRotation;     
        
        targetSelected();




    }

    // Variable controla el tipo de transformación ejecutada
    public void transformMode(int val) {
        transMode = val; // posicion 1, // rotacion 2, // escala 3
                         //Fija los joysticks que se usaran en la transformación
        setJoysticks(transMode);
        


    }
    public void setJoysticks(int modeTrans) {
        switch (modeTrans)
        {
            case 1:
                joyXY = posXY;
                joyZ = posZ;
                break;
            case 2:
                joyXY = rotXY;
                joyZ = rotZ;
                break;
            case 3:
                joyXY = scaleX;
                break;
        }

    }


    // Revisa si se ha seleccionado un marcador
    public void targetSelected()
    {

        foreach (var imgTar in imageTargets)
        {           
            if (imgTar.GetComponent<Cambiar_Enlazado>().Active)
            {
            
                targetSeleccioando = true;
                break;
            }
            targetSeleccioando = false;
        }
           

    }

    void Update(){
      

        if (Input.GetMouseButton(0) && marcador.GetComponent<DefaultTrackableEventHandler>().stateCard && marcador.GetComponent<Cambiar_Enlazado>().Active ){  //y verficar que se enlazo contenido              

                if (joyXY.GetComponent<RectTransform>().localPosition.y > thersholdJoysXY)
            {
                    transformPlusY(transMode);
                }
                if (joyXY.GetComponent<RectTransform>().localPosition.y < -thersholdJoysXY)
            {
                    transformMinusY(transMode);
                }
                if (joyXY.GetComponent<RectTransform>().localPosition.x > thersholdJoysXY)
            {
                    transformPlusX(transMode);
                }
                if (joyXY.GetComponent<RectTransform>().localPosition.x < -thersholdJoysXY)
            {
                    transformMinusX(transMode);
                }
                if (joyZ.GetComponent<RectTransform>().localPosition.y > thersholdJoysZ)
            {
                    transformPlusZ(transMode);
                }
                if (joyZ.GetComponent<RectTransform>().localPosition.y < -thersholdJoysZ)
            {
                    transformMinusZ(transMode);
                }
               
            }

       
        if (joyXY.GetComponent<RectTransform>().localPosition.y > thersholdJoysXY ||
            joyXY.GetComponent<RectTransform>().localPosition.y < -thersholdJoysXY ||
            joyXY.GetComponent<RectTransform>().localPosition.x > thersholdJoysXY ||
            joyXY.GetComponent<RectTransform>().localPosition.x < -thersholdJoysXY ||
            joyZ.GetComponent<RectTransform>().localPosition.y > thersholdJoysZ ||
            joyZ.GetComponent<RectTransform>().localPosition.y < -thersholdJoysZ
            )
        {
            contenedor_ejes.SetActive(true);
            contenedor_ejes.transform.localPosition = marcador.transform.localPosition;
            Quaternion pos_target = marcador.transform.localRotation;         
            contenedor_ejes.transform.localRotation = pos_target;

        }
        
        else
        {
            contenedor_ejes.SetActive(false);
        }


    }


    public void transformPlusX(int transMode){
            switch (transMode)
            {
                case 1:
                    derecha();
                    break;
                case 2:
                    rotarX();
                    break;
                case 3:
                    agrandar();                   
                    break;
            }
        }
        public void transformMinusX(int transMode){
            switch (transMode)
            {
                case 1:
                    izquierda();
                    break;
                case 2:
                    rotarmenosX();
                    break;
                case 3:
                    encoger();
                    break;
            }
        }
        public void transformPlusY(int transMode){
            switch (transMode)
            {
                case 1:
                    arriba();
                    break;
                case 2:
                    rotarY();
                    break;
                case 3:
                    agrandar();
                    break;
            }
        }
        public void transformMinusY(int transMode){
            switch (transMode)
            {
                case 1:
                    abajo();
                    break;
                case 2:
                    rotarmenosY();
                    break;
                case 3:
                    encoger();
                    break;
            }
        }

        public void transformPlusZ(int transMode){
            switch (transMode)
            {
                case 1:
                    atras();
                    break;
                case 2:
                    rotarmenosZ();
                    break;
                case 3:
                    break;
            }
        }
        public void transformMinusZ(int transMode){
            switch (transMode)
            {
                case 1:                   
                    adelante();
                    break;
                case 2:
                    rotarZ();               
                    break;
                case 3:
                    break;
            }
        }

        public void rotarX(){           
            Quaternion pos = objeto.localRotation;
            objeto.localRotation = pos * Quaternion.Euler(1, 0, 0);
            ejes.transform.localRotation = pos * Quaternion.Euler(1, 0, 0);
            //contenedor_ejes.SetActive(true);      
    }
        public void rotarY(){          
            Quaternion pos = objeto.localRotation;
            objeto.localRotation = pos * Quaternion.Euler(0, 1, 0);
            ejes.transform.localRotation = pos * Quaternion.Euler(0, 1, 0);
            //contenedor_ejes.SetActive(true);
    }
        public void rotarZ(){            
            Quaternion pos = objeto.localRotation;
            objeto.localRotation = pos * Quaternion.Euler(0, 0, 1);
            ejes.transform.localRotation = pos * Quaternion.Euler(0, 0, 1);

        //contenedor_ejes.SetActive(true);
    }
        public void rotarmenosX(){
            Quaternion pos = objeto.localRotation;
            objeto.localRotation = pos * Quaternion.Euler(-1, 0, 0);
            ejes.transform.localRotation = pos * Quaternion.Euler(-1, 0, 0);

        //contenedor_ejes.SetActive(true);
    }
        public void rotarmenosY(){               
            Quaternion pos = objeto.localRotation;
            objeto.localRotation = pos * Quaternion.Euler(0, -1, 0);
             ejes.transform.localRotation = pos * Quaternion.Euler(0, -1, 0);
        //contenedor_ejes.SetActive(true);
    }
        public void rotarmenosZ(){            
            Quaternion pos = objeto.localRotation;
            objeto.localRotation = pos * Quaternion.Euler(0, 0, -1);
            ejes.transform.localRotation = pos * Quaternion.Euler(0, 0, -1);

        //contenedor_ejes.SetActive(true);
    }
        public void arriba(){
            objeto.Translate(Vector3.up * 0.005f);
            ejes.transform.Translate(Vector3.up * 0.005f);
            guardar_pos_ejes[carta] = ejes.transform.localPosition;
            //contenedor_ejes.SetActive(true);
        }
        public void abajo(){           
            objeto.Translate(Vector3.down * 0.005f);
            ejes.transform.Translate(Vector3.down * 0.005f);
            guardar_pos_ejes[carta] = ejes.transform.localPosition;
            //contenedor_ejes.SetActive(true);
        }
        public void adelante(){           
            objeto.Translate(Vector3.forward * 0.005f);
            ejes.transform.Translate(Vector3.forward * 0.005f);
            //guardar_pos_ejes[carta] = ejes.transform.localPosition;
            //contenedor_ejes.SetActive(true);
        }
        public void atras(){           
            objeto.Translate(Vector3.back * 0.005f);
            ejes.transform.Translate(Vector3.back * 0.005f);
            guardar_pos_ejes[carta] = ejes.transform.localPosition;
            //contenedor_ejes.SetActive(true);
        }
        public void izquierda(){           
            objeto.Translate(Vector3.left * 0.005f);
            ejes.transform.Translate(Vector3.left * 0.005f);
            guardar_pos_ejes[carta] = ejes.transform.localPosition;
            //contenedor_ejes.SetActive(true);
        }
        public void derecha(){      
            objeto.Translate(Vector3.right * 0.005f);
            ejes.transform.Translate(Vector3.right * 0.005f);
            guardar_pos_ejes[carta] = ejes.transform.localPosition;
            //contenedor_ejes.SetActive(true);
        }
        public void agrandar(){
            if (seleccion == 0){
                objeto.localScale += new Vector3(0.001f, 0.001f, 0.001f);
                ejes.transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
            //contenedor_ejes.SetActive(true);
        }
            else{               
                objeto.localScale += new Vector3(0.005f, 0.005f, 0.005f);
                ejes.transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
            //contenedor_ejes.SetActive(true);
        }
        }
        public void encoger(){
            if (objeto.localScale.x > 0){
                if (seleccion == 0){                    
                    objeto.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
                    ejes.transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
                //contenedor_ejes.SetActive(true);
            }
                else{                   
                    objeto.localScale -= new Vector3(0.005f, 0.005f, 0.005f);
                    ejes.transform.localScale -= new Vector3(0.005f, 0.005f, 0.005f);
                //contenedor_ejes.SetActive(true);
            }
            }

        }

      
        public void reset_transform()
        {
        if( marcador.GetComponent<DefaultTrackableEventHandler>().stateCard && marcador.GetComponent<Cambiar_Enlazado>().Active) { 
        if (seleccion == 0)//Imagen
            {
                switch (transMode)
                {
                    case 1:
                        objeto.localPosition = new Vector3(0, 0, 0);
                        guardar_pos_ejes[carta] = new Vector3(0, 0, 0);
                        ejes.transform.localPosition = new Vector3(0, 0, 0);
                        break;
                    case 2:
                        objeto.localRotation = Quaternion.Euler(90, 0, 0);
                        Quaternion pos_target = marcador.transform.localRotation;
                        Quaternion pos = objeto.localRotation;
                        ejes.transform.localRotation = pos;
                        contenedor_ejes.transform.localRotation = pos_target;
                        break;
                    case 3:                        
                        objeto.localScale = new Vector3(1f, 1f, 1f);
                        break;
                }    
                 
            }
            else if (seleccion == 1)//Video
            {
                switch (transMode)
                {
                    case 1:
                        objeto.localPosition = new Vector3(0, 0, 0);
                        guardar_pos_ejes[carta] = new Vector3(0, 0, 0);
                        ejes.transform.localPosition = new Vector3(0, 0, 0);
                        break;
                    case 2:
                        objeto.localRotation = Quaternion.Euler(90, 0, 0);
                        Quaternion pos_target = marcador.transform.localRotation;
                        Quaternion pos = objeto.localRotation;
                        ejes.transform.localRotation = pos;
                        contenedor_ejes.transform.localRotation = pos_target;
                        break;
                    case 3:
                    Debug.Log("REscalo");
                    objeto.localScale = new Vector3(1, 1, 1);
                        break;
                }          


            }
            else if (seleccion == 2)//Obj & FBX
            {
                switch (transMode)
                {
                    case 1:
                        objeto.localPosition = new Vector3(0, 0, 0);
                        guardar_pos_ejes[carta] = new Vector3(0, 0, 0);
                        ejes.transform.localPosition = new Vector3(0, 0, 0);
                        break;
                    case 2:
                        objeto.localRotation = Quaternion.Euler(90, 0, 0);
                        Quaternion pos_target = marcador.transform.localRotation;
                        Quaternion pos = objeto.localRotation;
                        ejes.transform.localRotation = pos;
                        contenedor_ejes.transform.localRotation = pos_target;
                        break;
                    case 3:
                        objeto.localScale = new Vector3(1, 1, 1);
                        Debug.Log("REscalo");
                        break;
                }             

            }
          
            }
        }

    }
    
