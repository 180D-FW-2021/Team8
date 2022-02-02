using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeScript : MonoBehaviour
{

    

    Rigidbody m1_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m1_Rigidbody = GetComponent<Rigidbody>();
        m1_Rigidbody.constraints = RigidbodyConstraints.None; //RigidbodyConstraints.FreezePositionZ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

