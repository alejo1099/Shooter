using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PoolingCiudadanos : Pooling {

	//Script encargado de instanciar cierta cantidad de ciudadanos y activarlos mientras pasa el tiempo
	public GameObject prefabCiudadano;
	private GameObject[] arrayCiudadano;

	//Se instancian los prefabs, y se invokan los metodos de Pooling de ciudadanos cada cierto tiempo,y se colocan algunos ciudadanos iniciales
	void Start()
	{
		InstanciarObjeto(100,prefabCiudadano,transform.GetChild(0),ref arrayCiudadano);
		InvokeRepeating("CiudadanoPooling",60,45);
		Invoke("CiudadanosIniciales",2);
	}

	//Se instacnian algunos ciudadanos al iniciar el juego
	private void CiudadanosIniciales()
	{
		for (int i = 0; i < 20; i++)
		{
			CiudadanoPooling();
		}
	}

	//Se instacnian algunos ciudadanos mientras pasa el juego
	private void CiudadanoPooling()
	{
		PoolingObjetos(arrayCiudadano,GeneradorDeWayPoints.generadorDeWayPoints.wayPoints[Random.Range(0,GeneradorDeWayPoints.generadorDeWayPoints.wayPoints.Length)].position);
	}	
}
