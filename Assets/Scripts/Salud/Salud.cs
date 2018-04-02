using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salud : MonoBehaviour
{

	//Metodo encargado de la salud de todos los NPC
    protected GameObject esteGameObject;

    protected int cantidadSalud;

	public int CantidadSalud
	{
		get
		{
			return cantidadSalud;
		}
		set
		{
			cantidadSalud = value;
		}
	}

	//Metodo que desactiva el objeto si este ha agotado su cantidad de vida
    public void VerificarSalud()
    {
        if (cantidadSalud <= 0)
            esteGameObject.SetActive(false);
    }
}