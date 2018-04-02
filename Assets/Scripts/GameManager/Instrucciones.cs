using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instrucciones : MonoBehaviour {

	//Esto se encarga de salir de la escena de instrucciones
	public void CargarMain()
	{
		SceneManager.LoadScene("Escena Principal");
	}
}
