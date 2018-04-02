using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CiudadanoControlador : Salud
{
    //scripts encargado de la salud del ciudadano, ademas encargado de agregar el scripts de ciudadano al activarse
    private bool objetoReactivado;

    //Obtencion de referencias
    void Awake()
    {
        esteGameObject = gameObject;
        cantidadSalud = 500;
    }

    //Si el objeto ya habia sido activado entonces se agrega el ciudadano, y se da la salud y se hace la refrencia al delegate
    void OnEnable()
    {
        if (objetoReactivado)
        {
            if (gameObject.tag == "NPC/Ciudadano")
            {
                esteGameObject.AddComponent<Ciudadano>();
				cantidadSalud = 500;
            }
            ControladorUpdates.controladorUpdates.funcionesUpdatesActuales += VerificarSalud;
        }
    }

    //Si el objeto se activo por primera vez se da la salud y la referencia al delegate
    void Start()
    {
        if (gameObject.tag == "NPC/Ciudadano")
        {
            esteGameObject.AddComponent<Ciudadano>();
        }else
		{
			CantidadSalud = 1000;
		}
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales += VerificarSalud;
    }

    //Se eliminan las referencias y se indica que el objeto ya se ha deasctivado
    void OnDisable()
    {
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales -= VerificarSalud;
        objetoReactivado = true;
    }
}