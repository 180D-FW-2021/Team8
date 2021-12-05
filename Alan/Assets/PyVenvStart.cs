using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyVenvStart : MonoBehaviour
{
	bool onWin = Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor; // 1 if using Windows, 0 if using Mac
						// assuming no other platforms at this time
	string separator = " ; "; // ';' separates commands in bash, '&' separates commands in cmd
	GameObject startObj;
	cmdStarter startScript;
  // Start is called before the first frame update
  void Start()
  {
	  // testing
		// pyStart("E:/anaconda3/Scripts/activate.bat", "E:/anaconda3/envs/180DP", "PyOut/wrapperTest.py");
  }

  // Update is called once per frame
  void Update()
  {
        
  }
	
	public void pyStart(string activateLoc, string venvLoc, string pyPath, bool autoClose = true, bool startMinimized = true, bool noWindow = false)
	{
		// Uses the .bat or .sh script specified by activateLoc to activate the virtual environment specified by venvLoc and run the .py specified by pyPath.
		// last 3 arguments are just passed on to CMDStart.
		string pyStart = "";
		string procArgs = "";
		// assemble argument to CMDStarter
    if(onWin)
		{
			separator = " & ";
		}
		pyStart = separator + " python " + pyPath;
		procArgs = activateLoc + " " + venvLoc + " " + pyStart;
		
		// locate and call CMDStarter
		startObj = GameObject.Find("ProcessStartObject");
		startScript = startObj.GetComponent<cmdStarter>();
		startScript.CMDStart(procArgs, autoClose, startMinimized, noWindow);
	}
}
