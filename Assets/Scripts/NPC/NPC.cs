using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(Rigidbody))]

public class NPC : MonoBehaviour
{

    //Scripts padre de los ciudadanos y los enemigos, que posee funciones utiles para ambos NPC, como distancia al objetivo,actualizar destino,etc

    protected NavMeshAgent agenteNPC;
    protected NavMeshObstacle obstaculoNPC;

    protected Transform transformNPC;
    protected Transform objetivoActual;

    public void ActualizarDestino(Vector3 destinoAgente)
    {
        if (agenteNPC.enabled)
        {
            agenteNPC.destination = destinoAgente;
        }
    }

    public void ActualizarDestino(Vector3 destinoAgente, float velocidad, float distanciaDetenerse)
    {
        if (agenteNPC.enabled)
        {
            agenteNPC.destination = destinoAgente;
            agenteNPC.speed = velocidad;
            agenteNPC.stoppingDistance = distanciaDetenerse;
        }
    }

    public bool AlLlegarAlDestino()
    {
        if (agenteNPC.enabled)
        {
            if (!agenteNPC.pathPending && agenteNPC.remainingDistance <= agenteNPC.stoppingDistance)
            {
                return true;
            }
        }
        return false;
    }

    public bool AlLlegarAlDestino(Vector3 posicionNPC, Vector3 posicionObjetivo, float distanciaMinima)
    {
        if (Vector3.Distance(posicionNPC, posicionObjetivo) <= distanciaMinima)
        {
            return true;
        }
        return false;
    }
}