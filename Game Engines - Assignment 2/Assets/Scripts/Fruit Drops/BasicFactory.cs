using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFactory<T> : MonoBehaviour where T : MonoBehaviour 
{
    [SerializeField]
    private GameObject prefab;

    public GameObject GetAInstance()
    {
        return Instantiate(prefab);
    }
         
}
