using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class ControladorPlayer : MonoBehaviour
{
    //Script encargado de la UI, ademas del tipo de disparo
    public static ControladorPlayer controladorPlayer;
    private MovimientoRotacion moverRotar;
    private CiudadanoControlador ciudadanoControlador;

    private Vector3 inicioRayo;

    public LayerMask capasDeseadas;
    public Text textoVida;
    public Text textoAyuda;
    public GameObject canvas;

    private LineRenderer lineRendererPlayer;

    private Camera ReferenciaCamara;

    private Transform transformPlayer;
    private Transform transformCamara;
    public Transform posicionDisparo;

    public float velocidadMovimiento;
    public float velocidadRotacion;
    public float tiempoLineRenderer;

    private int textoRandom;

    //Referencias iniciales
    void Awake()
    {
        controladorPlayer = this;
        ReferenciaCamara = GetComponentInChildren<Camera>();
        transformCamara = ReferenciaCamara.transform;
        transformPlayer = transform;
        lineRendererPlayer = GetComponent<LineRenderer>();
        ciudadanoControlador = GetComponent<CiudadanoControlador>();
    }

    //Referencias a los delegates e invocacion repetida de un metodo
    void Start()
    {
        moverRotar = new MovimientoRotacion(velocidadMovimiento, velocidadRotacion, transformPlayer, transformCamara);
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales += Disparar;
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales += moverRotar.MovimientoRigidbody;
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales += moverRotar.RotacionRigidbody;
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales += ActualizarTexto;
        InvokeRepeating("RecuperarVida", 10, 10);
    }

    //actualiza la cantidad de salud de la UI
    private void ActualizarTexto()
    {
        textoVida.text = "Vida: " + ciudadanoControlador.CantidadSalud;
    }

    //Tipo de ataque del jugador, tirando LineRenderer y si golpea un enemigo rebaja su vida
    private void Disparar()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(RenderizarLinea());

            inicioRayo = ReferenciaCamara.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            lineRendererPlayer.SetPosition(0, posicionDisparo.position);
            RaycastHit informacionGolpe;
            if (Physics.Raycast(inicioRayo, transformCamara.forward, out informacionGolpe, 50, capasDeseadas, QueryTriggerInteraction.Ignore))
            {
                lineRendererPlayer.SetPosition(1, informacionGolpe.point);
                informacionGolpe.transform.GetComponent<ControladorEnemigos>().CantidadSalud -= 50;
            }
            else
            {
                lineRendererPlayer.SetPosition(1, inicioRayo + transformCamara.forward * 50);
            }
        }
    }

    //Corutina que activa el LineRenderer

    private IEnumerator RenderizarLinea()
    {
        lineRendererPlayer.enabled = true;
        yield return new WaitForSeconds(tiempoLineRenderer);
        lineRendererPlayer.enabled = false;
    }

    //Metodo que incrementa la vida mientras pasa el tiempo
    private void RecuperarVida()
    {
        GetComponent<CiudadanoControlador>().CantidadSalud += 10;
    }

    //Trigger para verificar si hay un ciudadano y poder mostrar el texto de ayuda
    void OnTriggerStay(Collider other)
    {
        if (!textoAyuda.enabled)
        {
            canvas.SetActive(true);
            textoAyuda.enabled = true;
            textoRandom = Random.Range(0, 3);
            switch (textoRandom)
            {
                case 0:
                    textoAyuda.text = "Los personajes verdes son rapidos\ny te bajaran la vida \n considerablemente";
                    break;
                case 1:
                    textoAyuda.text = "Los personajes rojos son lentos\n pero si te alcanzan sera tu fin";
                    break;
                case 2:
                    textoAyuda.text = "Los personajes azules son amigos\n no los lastimes";
                    break;
            }
        }
        canvas.transform.position = other.transform.position;
        canvas.transform.LookAt(transformPlayer);
    }

    //Desactivacion del texto
    void OnTriggerExit(Collider other)
    {
        textoAyuda.enabled = false;
        canvas.SetActive(false);
    }

    //Eliminacion de referencias al delegate, y cargar la escena inicial
    void OnDisable()
    {
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales -= Disparar;
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales -= moverRotar.MovimientoRigidbody;
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales -= moverRotar.RotacionRigidbody;
        ControladorUpdates.controladorUpdates.funcionesUpdatesActuales -= ActualizarTexto;
        SceneManager.LoadScene("Escena Principal");
    }
}