using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformacionUtil : MonoBehaviour
{
    //Este scripts contiene referencias a las capas que los scripts puedan necesitar para utilizar los metodos de Raycast
    public static InformacionUtil informacionUtil;

	public LayerMask[] capasUtiles;
	
    void Awake()
    {
        if (!informacionUtil)
        {
            informacionUtil = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
