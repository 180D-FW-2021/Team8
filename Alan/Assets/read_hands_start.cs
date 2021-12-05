using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class read_hands_start: MonoBehaviour
{
	/*
	Starts Python venv specified by the user elsewhere in-game and activates wrapperTest.py
	Assumes that the Python venv has all required dependencies.
	*/
	string filename = "cmd.exe";
	static string activateLoc =  "E:\\anaconda3\\Scripts\\activate.bat"; 
	static string venvLoc = "E:\\anaconda3\\envs\\180DP"; // TODO: link both loc lines with Unity user input
	static string pyPath = "PyOut/wrapperTest.py";
	static string pyStart = "& python " + pyPath;
	string procArgs = "/C " + activateLoc + " " + venvLoc + " " + pyStart; // /C will cause the cmd window to close upon completion
    // Start is called before the first frame update
    void Start()
    {
        using(var process = new Process())
		{
			process.StartInfo.FileName = filename;
			process.StartInfo.Arguments = procArgs;
			process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
			process.Start();
			UnityEngine.Debug.Log("After process");
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
