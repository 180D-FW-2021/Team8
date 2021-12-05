using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class process_init_test : MonoBehaviour
{
	string filename = "cmd.exe";
	string procArgs = "/K echo Hello World";
    // Start is called before the first frame update
    void Start()
    {
        using(var process = new Process())
		{
			process.StartInfo.FileName = filename;
			process.StartInfo.Arguments = procArgs;
			process.Start();
			UnityEngine.Debug.Log("After process");
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
