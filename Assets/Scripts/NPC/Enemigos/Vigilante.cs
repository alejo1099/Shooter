using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public sealed class Vigilante : NPCEnemigo
{
    //Script encargado del ataque del Vigilante
    private Transform ojos;

    private LayerMask capasNPC;

    private float tiempoLerpOjos;

    //Obtencion de refrencias iniciales
    void Awake()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        agenteNPC = GetComponent<NavMeshAgent>();
        agenteNPC.enabled = true;
        obstaculoNPC = GetComponent<NavMeshObstacle>();
		ojos = transform.GetChild(0);
		transformNPC = transform;
		rangoDeAtaque = 2;
		rangoDepersecusion = 20;
		velocidadActualNPC = 1;
        agenteNPC.speed = 1;
    }

    //Variables iniciales y referencias al delegate
    void Start()
    {
		objetivoActual = ControladorPlayer.controladorPlayer.transform;
        capasNPC = InformacionUtil.informacionUtil.capasUtiles[0];
		agenteNPC.stoppingDistance = rangoDeAtaque;
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales += VerificarAtaque;
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales += ActualizacionObjetivo;
		ControladorUpdates.controladorUpdates.funcionesUpdatesActuales += BuscarVictimas;
    }

    //version propia del ataque del vigilante que desactiva al objetivo si esta en el rango de ataque, y los convierte en zombies
    public override void Atacar()
    {
        base.Atacar();
		transformNPC.LookAt(objetivoActual);
        objetivoActual.gameObject.SetActive(false);
        PoolingEnemigos.poolingEnemigos.PoolingObjetos(PoolingEnemigos.poolingEnemigos.zombies, objetivoActual.position);
        atacando = false;
    }

    //Mediante un raycast busca ciudadanos que pueda convertir en zombies
    private void BuscarVictimas()
    {
        if (!atacando)
        {
            tiempoLerpOjos += Time.fixedDeltaTime * 0.5f;
            ojos.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 60, 0), Quaternion.Euler(0, -60, 0), Mathf.PingPong(tiempoLerpOjos, 1));
            if (BuscarRaycast(ojos.position, ojos.forward, ref objetivoActual, rangoDepersecusion, capasNPC))
            {
                atacando = true;
            }
        }
    }

    //eliminacion de refrencias y destruccion del codigo
    void OnDisable()
    {
        agenteNPC.enabled = false;
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales -= ActualizacionObjetivo;
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales -= VerificarAtaque;
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales -= BuscarVictimas;
        Destroy(this);
    }
}