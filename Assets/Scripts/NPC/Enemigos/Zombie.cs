using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public sealed class Zombie : NPCEnemigo
{

    //Script hijo de NPCEnemigo encargado del tipo de ataque que hara el zombie, ademas de seguir atacando al Player si el enemigo actual ya esta muerto
	private CiudadanoControlador ciudadanoControlador;

    private Transform manoDerecha;
    private Transform manoIzquierda;
    private Transform contenedorManoDerecha;
    private Transform contenedorManoIzquierda;

	private LineRenderer lineaDeAtaqueDerecha;
    private LineRenderer lineaDeAtaqueIzquierda;

    private LayerMask capasNPC;

    public float tasaDeAtaque;
    private float tiempoAcumuladoAtaque;

    //Obtencion de referencias y establecimiento de valores iniciales
    void Awake()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
        agenteNPC = GetComponent<NavMeshAgent>();
        agenteNPC.enabled = true;
        obstaculoNPC = GetComponent<NavMeshObstacle>();
        transformNPC = transform;
        manoDerecha = transform.GetChild(0);
        manoIzquierda = transform.GetChild(1);
		lineaDeAtaqueDerecha = manoDerecha.GetComponent<LineRenderer>();
		lineaDeAtaqueIzquierda = manoIzquierda.GetComponent<LineRenderer>();
		rangoDeAtaque = 3;
		rangoDepersecusion = 5;
		velocidadActualNPC = 3;
		tasaDeAtaque = 0.75f;
    }

    //Referencias al delegate y valores para iniciar la AI del NPC
    void Start()
    {
        capasNPC = InformacionUtil.informacionUtil.capasUtiles[0];
        objetivoActual = ControladorPlayer.controladorPlayer.transform;
        ActualizarDestino(objetivoActual.position, 3.5f, rangoDeAtaque);
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales += VerificarAtaque;
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales += ActualizacionObjetivo;
		ControladorUpdates.controladorUpdates.funcionesUpdatesActuales += VerificarSaludEnemigo;
    }

    //Version de zombie de atacar
    public override void Atacar()
    {
        base.Atacar();
		transformNPC.LookAt(objetivoActual);
        if (Time.time > tiempoAcumuladoAtaque + tasaDeAtaque)
        {
            AtaqueZombie(manoDerecha, lineaDeAtaqueDerecha, ref contenedorManoDerecha);
            AtaqueZombie(manoIzquierda, lineaDeAtaqueIzquierda, ref contenedorManoIzquierda);
        }
    }

    //Lanza dos LineRenderer al objetivo y si son golpeados rebajan su vida
    private void AtaqueZombie(Transform mano, LineRenderer lineaMano, ref Transform contenedor)
    {
        StartCoroutine(TiempoAtaque(lineaMano));
        lineaMano.SetPosition(0, mano.position);
        tiempoAcumuladoAtaque = Time.time;
        if (BuscarRaycast(mano.position, mano.forward, ref contenedor, rangoDeAtaque, capasNPC))
        {
            lineaMano.SetPosition(1, contenedor.position);
			if(!ciudadanoControlador)
			{
				ciudadanoControlador = contenedor.GetComponent<CiudadanoControlador>();
			}
			ciudadanoControlador.CantidadSalud -= 5;
        }
        else
        {
            lineaMano.SetPosition(1, mano.position + mano.forward * rangoDeAtaque);
        }
    }

    //Corrutina que activa el linerenderer mientras se ataca al objetivo
    private IEnumerator TiempoAtaque(LineRenderer linea)
    {
        linea.enabled = true;
        yield return new WaitForSeconds(tasaDeAtaque * 0.5f);
        linea.enabled = false;
    }

    //verificador de salud del objetivo, para cambiar de objetivo cuando éste este muerto
	private void VerificarSaludEnemigo()
	{
		if(ciudadanoControlador && ciudadanoControlador.CantidadSalud <= 0)
		{
			atacando = false;
			ciudadanoControlador = null;
		}
	}

    //Trigger para verificar si un ciudadano entro en el rango de persecusion, si no es asi lo ignora
    void OnTriggerStay(Collider other)
    {
        if (!atacando && Vector3.Distance(transformNPC.position, other.transform.position) <= rangoDepersecusion)
        {
            atacando = true;
            objetivoActual = other.transform;
            ActualizarDestino(objetivoActual.position, velocidadActualNPC, rangoDeAtaque);
        }
    }

    //Eliminacion de referencias y destruccion de este codigo
	void OnDisable()
	{
        agenteNPC.enabled = false;
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales -= ActualizacionObjetivo;
		ControladorUpdates.controladorUpdates.funcionesUpdatesActuales -= VerificarAtaque;
		ControladorUpdates.controladorUpdates.funcionesUpdatesActuales -= VerificarSaludEnemigo;
		Destroy(this);
	}
}