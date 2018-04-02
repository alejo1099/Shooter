using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCEnemigo : NPC
{
    //Script padre de los enemigos con comportamiento establecido de este cuando se acerque a un ciudadano
    public float rangoDeAtaque;
    public float rangoDepersecusion;
    public float velocidadActualNPC;
    private float tiempoActivarAgente;

    protected bool atacando;
    private bool obstaculoActivado;

    //Metodo para actualizar el destino del NPC
    public void ActualizacionObjetivo()
    {
        if (agenteNPC.enabled)
        {
            ActualizarDestino(objetivoActual.position);
        }
    }

    //Metodo para verificar si el enemigo esta en posicion de ataque, y asi mismo activa un NavMeshObstacle para evitar errores de agentes que buscan el mismo objetivo
    public void VerificarAtaque()
    {
        if (atacando && AlLlegarAlDestino(transformNPC.position, objetivoActual.position, rangoDeAtaque))
        {
            Atacar();
        }
        else if (obstaculoActivado && atacando && !AlLlegarAlDestino(transformNPC.position, objetivoActual.position, rangoDeAtaque))
        {
            DesactivarObstaculo();
        }
        else if (obstaculoActivado && !atacando && !agenteNPC.enabled)
        {
            objetivoActual = ControladorPlayer.controladorPlayer.transform;
            DesactivarObstaculo();
        }
        Perseguir();
    }

    //Metodo que los hijos personalizaran para definir su propia version de ataque
    public virtual void Atacar()
    {
        if (!obstaculoActivado)
        {
            agenteNPC.enabled = false;
            obstaculoActivado = true;
            obstaculoNPC.enabled = true;
        }
    }

    //activa el NavMeshagent para seguir el objetivo si se mueve
    private void Perseguir()
    {
        if (!obstaculoActivado && !agenteNPC.enabled && Time.time > tiempoActivarAgente + 0.1f)
        {
            agenteNPC.enabled = true;
        }
    }

    //Desactiva el NavMeshObstacle para evitar errores con el NavMeshAgent
    private void DesactivarObstaculo()
    {
        tiempoActivarAgente = Time.time;
        obstaculoNPC.enabled = false;
		obstaculoActivado = false;
    }

    //Metodo que buscar con raycast elementos
    public bool BuscarRaycast(Vector3 origen, Vector3 direccion, ref Transform referenciaGolpe, float tamañoRayo, LayerMask capasVerificar)
    {
        RaycastHit informacionGolpe;
        if (Physics.Raycast(origen, direccion, out informacionGolpe, tamañoRayo, capasVerificar, QueryTriggerInteraction.Ignore))
        {
            referenciaGolpe = informacionGolpe.transform;
            return true;
        }
        return false;
    }
}