using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class csvReaderStart : MonoBehaviour
{
	// attach to object in starting screen. Starts and closes the Python hand reader
	string filepath = "wrist_single.csv";
	string exitFlag = "exit.txt";
	Process process = new Process();
	private static bool opened = false;
	
	public static csvReaderStart instance; // used for keeping the object around across scenes
	
    // Start is called before the first frame update
    void Start() //maybe move startup to game startup time?
	{
		if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if(this != instance) {
            Destroy(this.gameObject);
        }
		if(opened)
		{
			UnityEngine.Debug.Log("Hand reader already started!");
			return;
		}
		UnityEngine.Debug.Log("hands read script start");
		process.StartInfo.Arguments = ""; // might be used later to feed frame delay to python
		process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
		try
		{
			if(Application.platform == RuntimePlatform.WindowsPlayer)
			{
				// works for Win10 and Win11
				process.StartInfo.FileName = Directory.GetCurrentDirectory() + @"\.." + @"\csvHandsWin10\wrapperTest";
				UnityEngine.Debug.Log("Premade Win executable");
			}
			else if(Application.platform == RuntimePlatform.OSXPlayer)
			{
				process.StartInfo.FileName = Directory.GetCurrentDirectory() + @"\.." + @"\csvHandsMac\wrapperTest";
				//user differentiates between M1 and x86 mac
				UnityEngine.Debug.Log("Premade Mac executable");
			}
			else if(Application.platform == RuntimePlatform.OSXEditor)
			{
				// editor path is different
				process.StartInfo.FileName = Directory.GetCurrentDirectory() + @"\csvHandsMac\wrapperTest";
				UnityEngine.Debug.Log("Mac editor");
			}
			else if(Application.platform == RuntimePlatform.WindowsEditor)
			{
				// editor path is different
				process.StartInfo.FileName = Directory.GetCurrentDirectory() + @"\csvHandsWin10\wrapperTest";
				UnityEngine.Debug.Log("Win editor");
			}
		}
		catch(System.ComponentModel.Win32Exception e) // I don't think this handler even works
		{
			process.StartInfo.FileName = Directory.GetCurrentDirectory() + @"\.." + @"\csvHandsUser\wrapperTest";
			// filepath = @"csvHandsUser\" + filepath; // probably unnecessary unless unity base filepath changes across os
			UnityEngine.Debug.Log("User-generated executable");
		}
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.CreateNoWindow = false;
		process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
		process.Start();
		opened = true;
	}

	// this calls right before game quit.
    void OnApplicationQuit()
	{
		// write to file to tell python to close
		// python will rewrite it on startup.
		using(FileStream exitwrite = new FileStream(
				exitFlag, FileMode.OpenOrCreate,
				FileAccess.ReadWrite, FileShare.ReadWrite))
		{
			exitwrite.WriteByte((byte) 'f');
		}
	}
}
