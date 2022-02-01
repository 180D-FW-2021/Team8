using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class cmdStarter: MonoBehaviour
{
	/*
	Activates command processor for Mac/Windows.
	*/
	
    // Start is called before the first frame update
    void Start()
    {	
		
		// testing
		/*
		if(onWin)
		{
			CMDStart("echo hello world & echo Win", autoClose: false, startMinimized: false);
			CMDStart("E:/anaconda3/Scripts/activate.bat E:/anaconda3/envs/180DP & python PyOut/wrapperTest.py");
		}
		else
		{
			CMDStart("echo hello world ; echo Mac", autoClose: false, startMinimized: false);
		}
		*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void CMDStart(string commands, bool autoClose = true, bool startMinimized = true, bool noWindow = false)
	{
		/*
		Starts terminal (either cmd or bash) in the base folder of the Unity Project.
		See read_hands_start.cs for example on how to reach a particular Python virtual environment via cmd.exe arguments.
		The user will have to input their virtual environment location and anaconda activation batch file into the game.
		
		commands: argument to pass to terminal. Type similarly to how a command would be entered by hand, but replace backslashes (\) in filepaths by double backslashes (\\).
			Or could use forward slashes for filepaths, at least on Win10.
			Commands on consecutive lines can be separated by "&" or "&&" in the argument on windows or by ";" on mac.
		autoClose: if True, the terminal window will close whenever its task finishes. This coincides with the end of your Python code if you are running a single .py through this.
		startMinimized: if True, terminal window starts minimized.
		noWindow: if True, terminal window will not appear at all, but the arguments will be executed.
		
		*/
		using(var process = new Process())
		{
			bool onWin = Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor; // 1 if using Windows, 0 if using Mac
						// assuming no other platforms at this time
			string separator = " ; "; // ';' separates commands in bash, '&' separates commands in cmd
			process.StartInfo.Arguments = "";
			if(onWin)
			{
				process.StartInfo.FileName = "cmd.exe";
				process.StartInfo.Arguments = "/K "; // must prepend args with this for cmd to actually execute
				separator = " & ";
			}
			else
			{
				process.StartInfo.FileName = "/System/Applications/Utilities/Terminal.app";
			}
			process.StartInfo.Arguments += commands;
			if(autoClose)
			{
				process.StartInfo.Arguments += separator + "exit"; // exit terminal after completing
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
