using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{

    //public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //public float panSpeed = 1000f;

    float smooth = 1f;
    float tiltAngle = 50f;

    // Update is called once per frame
    void Update()
    {
        //Vector3 rot = transform.rotation;
        //float rot = Input.GetAxis("Horizontal") * panSpeed;
        //rot *= Time.deltaTime;
        //transform.Rotate(0, rot, 0);

        float yAxis = transform.eulerAngles.y;
        //Debug.Log(yAxis);
        
        if(yAxis < 309){
            transform.Rotate(new Vector3(0, 1f, 0)* Time.deltaTime);
        }
        else if(yAxis > 354){
            transform.Rotate(new Vector3(0, -1f, 0)* Time.deltaTime);
        }
        //160.43

        //float tiltAroundy = Input.GetAxis("Vertical") * tiltAngle;

        // Rotate the cube by converting the angles into a quaternion.
        //Quaternion target = Quaternion.Euler(0, tiltAroundy, 0);

        // Dampen towards the target rotation
        //transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
    }
}
