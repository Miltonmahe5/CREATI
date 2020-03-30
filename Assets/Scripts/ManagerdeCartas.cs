using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerdeCartas : MonoBehaviour {

	public GameObject cartas;
	public static int marcador;
	public GameObject [] marcadores;

	// Use this for initialization
	void Start () {
		
		marcadores = GameObject.FindGameObjectsWithTag ("carta");
		for (int i = 0; i < marcadores.Length; i++) {
			marcadores[i] = GameObject.Find("ImageTarget"+(i+1));
		}

	}

	// Update is called once per frame
	void Update () {

		marcador = cartas.GetComponent<Dropdown>().value;

		for (int i = 0; i < marcadores.Length; i++) {

			if (i == marcador) {
				//marcadores [i].GetComponent<Cambiar_Enlazado> ().Active = true;

			} else {
				//marcadores [i].GetComponent<Cambiar_Enlazado> ().Active = false;
			}
		}

				
	}






	
}
