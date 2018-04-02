using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaPrincipal : MonoBehaviour {

	//Este codigo tiene metodos publicos que seran activados desde botones,y estos metodos se encargarn de cargar
	//diferentes escenas

	//Funcion que carga la escena del juego
	public void CargarEscenaPrincipal()
	{
		SceneManager.LoadScene("Escena Uno");
	}

	//Funcion que carga la primera escena
	public void CargarInstrcuciones()
	{
		SceneManager.LoadScene("Instrucciones");
	}

	//Funcion que quita el juego
	public void Salir()
	{
		Application.Quit();
	}
}
