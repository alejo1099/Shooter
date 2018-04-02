using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorUpdates : MonoBehaviour
{

    //Este codigo le permite a cualquier script agregar una funcion a la ejecucion en el Update
    //y se hace a traves de un delegate
    public static ControladorUpdates controladorUpdates;

    public delegate void funcionesUpdates();
    public event funcionesUpdates funcionesUpdatesActuales;

    void Awake()
    {
        //Singleton de si mismo
        if (!controladorUpdates)
        {
            controladorUpdates = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Update()
    {
        //Ejecucion de la referencia delegate
        if (funcionesUpdatesActuales != null)
        {
            funcionesUpdatesActuales();
        }
    }
}