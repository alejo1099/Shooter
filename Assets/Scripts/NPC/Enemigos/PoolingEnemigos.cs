using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PoolingEnemigos : Pooling
{

    //Scripts encargado de instanciar y activar los enemigos atraves de tiempo
    //ya sean zombies o vigilantes
    public static PoolingEnemigos poolingEnemigos;

    public GameObject[] zombies;
	GameObject[] vigilantes;
    public GameObject prefabZombie;
	public GameObject prefabVigilante;

    //Referencia de si mismo
    void Awake()
    {
        poolingEnemigos = this;
    }

    //instancia de los tipos de enemigos, y tambien la activacion de estos a traves del tiempo
    void Start()
    {
        InstanciarObjeto(50, prefabZombie, transform.GetChild(0), ref zombies);
		InstanciarObjeto(50, prefabVigilante, transform.GetChild(0), ref vigilantes);
        InvokeRepeating("SpawnZombies", 60, 15);
		InvokeRepeating("SpawnVigilantes", 60, 60);
    }

    //Activacion de Zombies
    private void SpawnZombies()
    {
        PoolingObjetos(zombies, GeneradorDeWayPoints.generadorDeWayPoints.wayPoints[Random.Range(0, GeneradorDeWayPoints.generadorDeWayPoints.wayPoints.Length)].position);
    }

    //Activacion de vigilantes
	private void SpawnVigilantes()
	{
		PoolingObjetos(vigilantes, GeneradorDeWayPoints.generadorDeWayPoints.wayPoints[Random.Range(0, GeneradorDeWayPoints.generadorDeWayPoints.wayPoints.Length)].position);
	}
}
