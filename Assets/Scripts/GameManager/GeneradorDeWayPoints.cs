using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDeWayPoints : MonoBehaviour {

	//Este codigo instanciara wayPoints que los ciudadanos utilizaran para seguir, ademas tambien verifica que este dentro del NavMesh para
	//que no se presenten errores
	public static GeneradorDeWayPoints generadorDeWayPoints;
	public LayerMask capasEdificios;

	private GameObject WayPoint;

	private Collider[] informacionOverlap;
	public Transform[] wayPoints;

	private Transform esteTransform;

	public int cantidadWayPoints;

	private bool colocacionExitosa;

	//Singleton de si mismo y obtencion de referencias
	void Awake()
	{
		if(!generadorDeWayPoints)
		{
			generadorDeWayPoints = this;
		}else
		{
			Destroy(this);
		}
		esteTransform = transform.GetChild(0);
		wayPoints = new Transform[cantidadWayPoints];
	}

	//Llamada del metodo
	void Start () {
		CrearWayPoints();
	}
	
	//Funcion que instancia los wayPoints, y lo almacena en un array donde se accederan por los ciudadanos
	private void CrearWayPoints()
	{
		for (int i = 0; i < cantidadWayPoints; i++)
		{
			WayPoint = new GameObject("WayPoint" + i);
			while (!colocacionExitosa)
			{
				WayPoint.transform.position = new Vector3(Random.Range(-198f,198f),0,Random.Range(-198f,198f));
				informacionOverlap = Physics.OverlapBox(WayPoint.transform.position,new Vector3(2,0,2),Quaternion.identity,capasEdificios,QueryTriggerInteraction.Ignore);
				if(informacionOverlap.Length == 0)
				{
					colocacionExitosa = true;
				}
			}
			wayPoints[i] = WayPoint.transform;
			WayPoint.transform.SetParent(esteTransform);
			colocacionExitosa = false;
		}
	}
}