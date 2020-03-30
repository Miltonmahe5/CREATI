using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.CrossPlatformInput
{

    /// <summary>
    /// hola amigos
    /// </summary>

    public class Edit_Transform_Objects : MonoBehaviour
    {
        int carta = 0;
        int tag_gameObject = 0;
        int seleccion;
        GameObject marcador;
        Transform objeto;
        int boton_presionado = 20;
        bool joysMov = false;
        public GameObject ejes;
        public GameObject contenedor_ejes;
        public GameObject posXY;
        public GameObject posZ;



        public Text angX;
        public Text angY;
        public Text angZ;

        public Text scale;



        int temp = 0;
        Vector3[] guardar_pos_ejes = new Vector3[24];

        float threshold = 0.5f;


        int posss;
        Vector3 a;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {




            // Para PC
            if (Input.GetAxis("Vertical") > threshold)
            {

                arriba();
            }

            if (Input.GetAxis("Vertical") < -threshold)
            {

                abajo();
            }


            if (Input.GetAxis("Horizontal") > threshold)
            {

                derecha();
            }

            if (Input.GetAxis("Horizontal") < -threshold)
            {

                izquierda();
            }

            //Para mobil android y UWP


            if (posXY.GetComponent<RectTransform>().localPosition.y > posXY.GetComponent<Joystick>().MovementRange / 2
                || posXY.GetComponent<RectTransform>().localPosition.y < -posXY.GetComponent<Joystick>().MovementRange / 2
                || posXY.GetComponent<RectTransform>().localPosition.x > posXY.GetComponent<Joystick>().MovementRange / 2
                || posXY.GetComponent<RectTransform>().localPosition.x < -posXY.GetComponent<Joystick>().MovementRange / 2
                || posZ.GetComponent<RectTransform>().localPosition.y > posZ.GetComponent<Joystick>().MovementRange / 2
                || posZ.GetComponent<RectTransform>().localPosition.y < -posZ.GetComponent<Joystick>().MovementRange / 2)
            {

                joysMov = true;

            }
            else
            {
                joysMov = false;
            }

            if (posXY.GetComponent<RectTransform>().localPosition.y > posXY.GetComponent<Joystick>().MovementRange / 2)
            {

                arriba();


            }



            if (posXY.GetComponent<RectTransform>().localPosition.y < -posXY.GetComponent<Joystick>().MovementRange / 2)
            {

                abajo();


            }



            if (posXY.GetComponent<RectTransform>().localPosition.x > posXY.GetComponent<Joystick>().MovementRange / 2)
            {

                derecha();


            }


            if (posXY.GetComponent<RectTransform>().localPosition.x < -posXY.GetComponent<Joystick>().MovementRange / 2)
            {

                izquierda();


            }




            if (posZ.GetComponent<RectTransform>().localPosition.y > posZ.GetComponent<Joystick>().MovementRange / 2)
            {

                atras();


            }


            if (posZ.GetComponent<RectTransform>().localPosition.y < -posZ.GetComponent<Joystick>().MovementRange / 2)
            {

                adelante();

            }



            //Seleccion de objeto a modificar
            carta = ManagerdeCartas.marcador;
            marcador = GameObject.Find("ImageTarget" + (carta + 1));
            seleccion = marcador.GetComponent<Cambiar_Enlazado>().sele;




            if (marcador.transform.childCount >= 1)
            {
                Transform c = marcador.transform.GetChild(seleccion);
                c.parent = c;
                objeto = c;
            }



            switch (boton_presionado)
            {
                case 1:
                    rotarX();
                    break;
                case 2:
                    rotarY();
                    break;
                case 3:
                    rotarZ();
                    break;
                case 4:
                    arriba();
                    break;
                case 5:
                    adelante();
                    break;
                case 6:
                    izquierda();
                    break;
                case -1:
                    rotarmenosX();
                    break;
                case -2:
                    rotarmenosY();
                    break;
                case -3:
                    rotarmenosZ();
                    break;
                case -4:
                    abajo();
                    break;
                case -5:
                    atras();
                    break;
                case -6:
                    derecha();
                    break;
                case 7:
                    agrandar();
                    break;
                case -7:
                    encoger();
                    break;
                case 20:
                    x();
                    break;

            }



            Quaternion pos_target = marcador.transform.localRotation;
            Quaternion pos = objeto.localRotation;
            ejes.transform.localRotation = pos;
            contenedor_ejes.transform.localRotation = pos_target;
            float xx = marcador.transform.localPosition.x;
            float yy = marcador.transform.localPosition.y;
            float zz = marcador.transform.localPosition.z;


            if (objeto.tag == "1")
            {

                contenedor_ejes.transform.localPosition = new Vector3(xx, yy, zz);

            }
            else if (objeto.tag == "2")
            {

                contenedor_ejes.transform.localPosition = new Vector3(xx, yy, zz);
            }
            else if (objeto.tag == "3")
            {

                contenedor_ejes.transform.localPosition = new Vector3(xx, yy, zz);
            }
            else
            {

            }

            if (carta == 0)
            {
                ejes.transform.localPosition = guardar_pos_ejes[0];
            }
            if (carta == 1)
            {
                ejes.transform.localPosition = guardar_pos_ejes[1];
            }
            if (carta == 2)
            {
                ejes.transform.localPosition = guardar_pos_ejes[2];
            }
            if (carta == 3)
            {
                ejes.transform.localPosition = guardar_pos_ejes[3];
            }
            if (carta == 4)
            {
                ejes.transform.localPosition = guardar_pos_ejes[4];
            }
            if (carta == 5)
            {
                ejes.transform.localPosition = guardar_pos_ejes[5];
            }
            if (carta == 6)
            {
                ejes.transform.localPosition = guardar_pos_ejes[6];
            }
            if (carta == 7)
            {
                ejes.transform.localPosition = guardar_pos_ejes[7];
            }
            if (carta == 8)
            {
                ejes.transform.localPosition = guardar_pos_ejes[8];
            }
            if (carta == 9)
            {
                ejes.transform.localPosition = guardar_pos_ejes[9];
            }
            if (carta == 10)
            {
                ejes.transform.localPosition = guardar_pos_ejes[10];
            }
            if (carta == 11)
            {
                ejes.transform.localPosition = guardar_pos_ejes[11];
            }
            if (carta == 12)
            {
                ejes.transform.localPosition = guardar_pos_ejes[12];
            }
            if (carta == 13)
            {
                ejes.transform.localPosition = guardar_pos_ejes[13];
            }
            if (carta == 14)
            {
                ejes.transform.localPosition = guardar_pos_ejes[14];
            }
            if (carta == 15)
            {
                ejes.transform.localPosition = guardar_pos_ejes[15];
            }
            if (carta == 16)
            {
                ejes.transform.localPosition = guardar_pos_ejes[16];
            }
            if (carta == 17)
            {
                ejes.transform.localPosition = guardar_pos_ejes[17];
            }
            if (carta == 18)
            {
                ejes.transform.localPosition = guardar_pos_ejes[18];
            }
            if (carta == 19)
            {
                ejes.transform.localPosition = guardar_pos_ejes[19];
            }
            if (carta == 20)
            {
                ejes.transform.localPosition = guardar_pos_ejes[20];
            }
            if (carta == 21)
            {
                ejes.transform.localPosition = guardar_pos_ejes[21];
            }
            if (carta == 22)
            {
                ejes.transform.localPosition = guardar_pos_ejes[22];
            }
            if (carta == 23)
            {
                ejes.transform.localPosition = guardar_pos_ejes[23];
            }


            //angX.text = (objeto.GetComponent<Transform>().localEulerAngles.x).ToString("#.0");
            //angY.text = (objeto.GetComponent<Transform>().localEulerAngles.y).ToString("#.0");
            //angZ.text = (objeto.GetComponent<Transform>().localEulerAngles.z).ToString("#.0");
            scale.text = (objeto.GetComponent<Transform>().localScale.x * 10).ToString("#0.0");



        }
        public void rotarX()
        {
            boton_presionado = 1;
            Quaternion pos = objeto.localRotation;
            objeto.localRotation = pos * Quaternion.Euler(1, 0, 0);
            contenedor_ejes.SetActive(true);
            //Debug.Log ("rot x");

        }
        public void rotarY()
        {
            boton_presionado = 2;
            Quaternion pos = objeto.localRotation;
            objeto.localRotation = pos * Quaternion.Euler(0, 1, 0);
            contenedor_ejes.SetActive(true);

        }
        public void rotarZ()
        {
            boton_presionado = 3;
            Quaternion pos = objeto.localRotation;
            objeto.localRotation = pos * Quaternion.Euler(0, 0, 1);
            contenedor_ejes.SetActive(true);

        }
        public void rotarmenosX()
        {
            boton_presionado = -1;
            Quaternion pos = objeto.localRotation;
            objeto.localRotation = pos * Quaternion.Euler(-1, 0, 0);
            contenedor_ejes.SetActive(true);


        }
        public void rotarmenosY()
        {
            boton_presionado = -2;
            Quaternion pos = objeto.localRotation;
            objeto.localRotation = pos * Quaternion.Euler(0, -1, 0);
            contenedor_ejes.SetActive(true);

        }
        public void rotarmenosZ()
        {
            boton_presionado = -3;
            Quaternion pos = objeto.localRotation;
            objeto.localRotation = pos * Quaternion.Euler(0, 0, -1);
            contenedor_ejes.SetActive(true);

        }


        public void arriba()
        {
            //boton_presionado = 4;
            objeto.Translate(Vector3.up * 0.005f);
            ejes.transform.Translate(Vector3.up * 0.005f);
            guardar_pos_ejes[carta] = ejes.transform.localPosition;
            contenedor_ejes.SetActive(true);

        }
        public void abajo()
        {
            //boton_presionado = -4;
            objeto.Translate(Vector3.down * 0.005f);
            ejes.transform.Translate(Vector3.down * 0.005f);
            guardar_pos_ejes[carta] = ejes.transform.localPosition;
            contenedor_ejes.SetActive(true);
        }
        public void adelante()
        {
            //boton_presionado = 5;
            objeto.Translate(Vector3.forward * 0.005f);
            ejes.transform.Translate(Vector3.forward * 0.005f);
            guardar_pos_ejes[carta] = ejes.transform.localPosition;
            contenedor_ejes.SetActive(true);
        }
        public void atras()
        {
            //boton_presionado = -5;
            objeto.Translate(Vector3.back * 0.005f);
            ejes.transform.Translate(Vector3.back * 0.005f);
            guardar_pos_ejes[carta] = ejes.transform.localPosition;
            contenedor_ejes.SetActive(true);
        }
        public void izquierda()
        {
            //boton_presionado = 6;
            objeto.Translate(Vector3.left * 0.005f);
            ejes.transform.Translate(Vector3.left * 0.005f);
            guardar_pos_ejes[carta] = ejes.transform.localPosition;
            contenedor_ejes.SetActive(true);
        }
        public void derecha()
        {
            //boton_presionado = -6;
            objeto.Translate(Vector3.right * 0.005f);
            ejes.transform.Translate(Vector3.right * 0.005f);
            guardar_pos_ejes[carta] = ejes.transform.localPosition;
            contenedor_ejes.SetActive(true);
        }

        public void agrandar()
        {
            if (seleccion == 0)
            {

                boton_presionado = 7;
                objeto.localScale += new Vector3(0.001f, 0.001f, 0.001f);
                contenedor_ejes.SetActive(true);
            }
            else
            {
                boton_presionado = 7;
                objeto.localScale += new Vector3(0.005f, 0.005f, 0.005f);
                contenedor_ejes.SetActive(true);

            }

        }
        public void encoger()
        {
            if (objeto.localScale.x > 0)
            {
                if (seleccion == 0)
                {

                    boton_presionado = -7;
                    objeto.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
                    contenedor_ejes.SetActive(true);
                }
                else
                {
                    boton_presionado = -7;
                    objeto.localScale -= new Vector3(0.005f, 0.005f, 0.005f);
                    contenedor_ejes.SetActive(true);

                }
            }

        }
        public void x()
        {

            boton_presionado = 20;
            if (joysMov)
            {
                contenedor_ejes.SetActive(true);
            }
            else
            { contenedor_ejes.SetActive(false); }

        }
        public void reset_transform()
        {
            if (seleccion == 0)
            {
                objeto.localPosition = new Vector3(0, 0, 0);
                objeto.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                objeto.localRotation = Quaternion.Euler(90, 90, 0);
                Quaternion pos_target = marcador.transform.localRotation;
                Quaternion pos = objeto.localRotation;
                ejes.transform.localRotation = pos;
                contenedor_ejes.transform.localRotation = pos_target;
                ejes.transform.localPosition = new Vector3(0, 0, 0);
                guardar_pos_ejes[carta] = new Vector3(0, 0, 0);

            }
            else if (seleccion == 1)
            {
                objeto.localPosition = new Vector3(0, 0, 0);
                objeto.localScale = new Vector3(1, 1, 1);
                objeto.localRotation = Quaternion.Euler(90, 0, -90);
                Quaternion pos_target = marcador.transform.localRotation;
                Quaternion pos = objeto.localRotation;
                ejes.transform.localRotation = pos;
                contenedor_ejes.transform.localRotation = pos_target;
                ejes.transform.localPosition = new Vector3(0, 0, 0);
                guardar_pos_ejes[carta] = new Vector3(0, 0, 0);


            }
            else if (seleccion == 2)
            {
                objeto.localPosition = new Vector3(0, 0, 0);
                objeto.localScale = new Vector3(1, 1, 1);
                objeto.localRotation = Quaternion.Euler(90, 90, 0);
                Quaternion pos_target = marcador.transform.localRotation;
                Quaternion pos = objeto.localRotation;
                ejes.transform.localRotation = pos;
                contenedor_ejes.transform.localRotation = pos_target;
                ejes.transform.localPosition = new Vector3(0, 0, 0);
                guardar_pos_ejes[carta] = new Vector3(0, 0, 0);

            }
            else
            {
            }
        }

    }
}