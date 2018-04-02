using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ControladorEnemigos : Salud
{
    //Script encargado de controlar la salud de los enemigos,ademas de agregar el Script con el tipo de enemigo
    private bool objetoReactivado;

    //verificacion de tag para establecer la cantidad de vida
    void Awake()
    {
        esteGameObject = gameObject;
        if (esteGameObject.tag == "NPC/Enemigo/Zombie")
        {
            esteGameObject.AddComponent<Zombie>();
            cantidadSalud = 750;
        }
        else
        {
            esteGameObject.AddComponent<Vigilante>();
            cantidadSalud = 1200;
        }
    }

    //Si el objeto ya ha sido activado se vuelve a verificar el tag para la cantidad de vida
    void OnEnable()
    {
        if (objetoReactivado)
        {
            if (esteGameObject.tag == "NPC/Enemigo/Zombie")
            {
                esteGameObject.AddComponent<Zombie>();
                cantidadSalud = 750;
            }
            else
            {
                esteGameObject.AddComponent<Vigilante>();
                cantidadSalud = 1200;
            }
            ControladorUpdates.controladorUpdates.funcionesUpdatesActuales += VerificarSalud;
        }
    }

    //Se pone la referencia de verificar la salud en el delegate
    void Start()
    {
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales += VerificarSalud;
    }

    //Se quitan las refrencias
    void OnDisable()
    {
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales -= VerificarSalud;
        objetoReactivado = true;
    }
}