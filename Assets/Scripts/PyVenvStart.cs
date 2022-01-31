using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyVenvStart : MonoBehaviour
{
	
	GameObject startObj;
	cmdStarter startScript;
    // Start is called before the first frame update
    void Start()
    {
		// testing
		
		pyStart("180DP", "PyOut/wrapperTest.py");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void pyStart(string envName, string pyPath, bool autoClose = true, bool startMinimized = true, bool noWindow = false)
	{
		// Activates the virtual environment specified by envName and runs the .py specified by pyPath.
		// last 3 arguments are just passed on to CMDStart.
		bool onWin = Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor; // 1 if using Windows, 0 if using Mac
						// assuming no other platforms at this time
		string separator = onWin ? " & " : " ; "; // ';' separates commands in bash, '&' separates commands in cmd
		string pyStart = "";
		string procArgs = "";
		// assemble argument to CMDStarter
		pyStart = separator + " python " + pyPath;
		procArgs = "conda activate " + envName + pyStart;
		// locate and call CMDStarter
		startObj = GameObject.Find("ProcessStartObject");
		startScript = startObj.GetComponent<cmdStarter>();
		startScript.CMDStart(procArgs, autoClose, startMinimized, noWindow);
	}
}
