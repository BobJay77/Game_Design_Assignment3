using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{

    [SerializeField]
    private FruitFactory factory;

    // Start is called before the first frame update
    void Start()
    {
        var instance = factory.GetAInstance();
        instance.transform.position = new Vector3(transform.position.x + 0.25f, transform.position.y, -1);

    }

   
}
