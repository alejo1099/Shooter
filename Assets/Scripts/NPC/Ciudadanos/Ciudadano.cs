using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public sealed class Ciudadano : NPC
{

    //Script del ciudadano que recorrera los wayPoints de la clase GeneradorDeWayPoints

    //Llamadp de referencias
    void Awake()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
        GetComponent<NavMeshObstacle>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        agenteNPC = GetComponent<NavMeshAgent>();
    }
    //Asignacion de variables
    void Start()
    {
        agenteNPC.destination = EleccionRandomDestino();
        agenteNPC.speed = 1.5f;
        agenteNPC.stoppingDistance = 3;
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales += RecorrerWayPints;
    }

    //Metodo que actualiza el destino del ciudadano mediante una eleccion Random de Waypoints
    private void RecorrerWayPints()
    {
        if (agenteNPC.enabled)
        {
            if (AlLlegarAlDestino())
            {
                ActualizarDestino(EleccionRandomDestino());
            }
        }
    }

    //Metodo de eleccion Random de WayPoints
    private Vector3 EleccionRandomDestino()
    {
        return GeneradorDeWayPoints.generadorDeWayPoints.wayPoints[Random.Range(0, GeneradorDeWayPoints.generadorDeWayPoints.wayPoints.Length)].position;
    }

    //Elimina el metodo refernciado en el delegate y destruye este script
    void OnDisable()
    {
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales -= RecorrerWayPints;
        Destroy(this);
    }
}
