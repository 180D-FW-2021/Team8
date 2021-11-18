using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // The mesh goes red when the mouse is over it...
    void OnMouseEnter()
    {
        Color start = Color.black;
        rend.material.color = start;
        //Color.red;
    }

    // ...the red fades out to cyan as the mouse is held over...
    void OnMouseOver()
    {
        //rend.material.color -= new Color(0.1F, 0, 0) * Time.deltaTime;
        rend.material.color = Color.black;
    }

    // ...and the mesh finally turns white when the mouse moves away.
    void OnMouseExit()
    {
        rend.material.color = Color.white;
    }


    // Start is called before the first frame update
    /*void Start()
    {
        gameObject.GetComponent<Text>().color = Color.black;
        //gameObject.GetComponent<Renderer>().material.color = Color.black;
        //renderer.material.color = Color.black;
    }

    void OnMouseEnter()
    {
        gameObject.GetComponent<Text>().color = Color.red;
        //gameObject.GetComponent<Renderer>().material.color = Color.red;
        //GetComponent<Renderer>() = Color.red;
        //renderer.material.color = Color.red;
    }

    void OnMouseExit()
    {
        //gameObject.GetComponent<Text>().color = Color.black;
        //gameObject.GetComponent<Renderer>().material.color = Color.black;
        //GetComponent<Renderer>() = Color.black;
        //renderer.material.color = Color.black;
        //Create a new cube primitive to set the color on
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //Get the Renderer component from the new cube
        var cubeRenderer = cube.GetComponent<Renderer>();

        //Call SetColor using the shader property name "_Color" and setting the color to red
        cubeRenderer.material.SetColor("_Color", Color.red);
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
    */
}
