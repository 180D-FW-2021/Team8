using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class osCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		var os = Environment.OSVersion;
		Debug.Log("Current OS Information:\n");
		Debug.Log("Platform: " + os.Platform);
		Debug.Log("Version String: " +  os.VersionString);
		Debug.Log("Version Information:");
		Debug.Log("   Major: " + os.Version.Major);
		Debug.Log("   Minor: " + os.Version.Minor);
		Debug.Log("Service Pack: " + os.ServicePack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
