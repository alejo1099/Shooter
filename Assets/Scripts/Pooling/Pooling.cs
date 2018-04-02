using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{

    //Codigo padre de aquellos scripts que requieran instanciar objetos o hacer Pooling con ellos(Activarlos en una posicion, y desactivarlos cuando no son necesarios, y volverlos a activar)

    //Metodo encargado de posicionar y activar objetos requeridos si estan desactivados en la jerarquia(Pooling)
    public void PoolingObjetos(GameObject[] arrayDeObjetos, Vector3 posicionPooling)
    {
        for (int i = 0; i < arrayDeObjetos.Length; i++)
        {
            if (!arrayDeObjetos[i].activeInHierarchy)
            {
                arrayDeObjetos[i].transform.position = posicionPooling;
                arrayDeObjetos[i].SetActive(true);
                break;
            }
        }
    }

    //metodo que instacia objetos,los desactiva, los almacena en un array y les coloca un padre para no desorganizar la ventana de jerarquia
    public void InstanciarObjeto(int cantidadInstanciar, GameObject prefabObjeto, Transform padreInstancia,ref GameObject[] arrayAlmacenar)
    {
		arrayAlmacenar = new GameObject[cantidadInstanciar];
        for (int i = 0; i < cantidadInstanciar; i++)
        {
            GameObject objeto = Instantiate(prefabObjeto, Vector3.zero, Quaternion.identity);
            objeto.SetActive(false);
            objeto.transform.SetParent(padreInstancia);
			arrayAlmacenar[i] = objeto;
        }
    }
}
