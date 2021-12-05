using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class cmdStarter: MonoBehaviour
{
	/*
	Activates command prompt.
	TODO: Create similar solution for Mac and configure to automatically select for Mac/Windows. Could be as simple as knowing what filename to use instead of cmd.exe
	*/
    // Start is called before the first frame update
    void Start()
    {
		// example call below
        // winCMDStart("E:/anaconda3/Scripts/activate.bat E:/anaconda3/envs/180DP & python PyOut/wrapperTest.py", noWindow: true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void winCMDStart(string commands, bool autoClose = true, bool startMinimized = true, bool noWindow = false)
	{
		/*
		Starts cmd.exe in the base folder of the Unity Project.
		See read_hands_start.cs for example on how to reach a particular Python virtual environment via cmd.exe arguments.
		The user will have to input their virtual environment location and anaconda activation batch file into the game.
		
		commands: argument to pass to cmd.exe. Type similarly to how a command would be entered by hand, but replace backslashes (\) in filepaths by double backslashes (\\).
			Or could use forward slashes for filepaths, at least on Win10.
			Commands on consecutive lines can be separated by "&" or "&&" in the argument.
		autoClose: if True, the terminal window will close whenever its task finishes. This coincides with the end of your Python codeif you are running a single .py through this.
		startMinimized: if True, terminal window starts minimized.
		noWindow: if True, terminal window will not appear at all, but the arguments will be executed.
		useShell: if True, output will go to default shell rather than Unity std output stream.
		
		TODO: need to find a way to do the same on Mac. This will not work on Mac.
		*/
		using(var process = new Process())
		{
			process.StartInfo.FileName = "cmd.exe"; // could make this generic later
			
			if(autoClose)
			{
				process.StartInfo.Arguments = "/C " + commands;
			}
			else
			{
				process.StartInfo.Arguments = "/K " + commands;
			}
			if(startMinimized && !noWindow)
			{
				process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
			}
			if(noWindow)
			{
				process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			}
			process.Start();
		}
		
	}
}
