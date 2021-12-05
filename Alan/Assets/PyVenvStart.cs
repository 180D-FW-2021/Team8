using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyVenvStart : MonoBehaviour
{
	bool onWin = Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor; // 1 if using Windows, 0 if using Mac
						// assuming no other platforms at this time
	string separator = " ; "; // ';' separates commands in bash, '&' separates commands in cmd
	string activateLoc =  "E:/anaconda3/Scripts/activate.bat"; //location of anaconda activation script
	string venvLoc = "E:/anaconda3/envs/180DP"; // TODO: link both loc lines with Unity user input
	string pyPath = "PyOut/wrapperTest.py"; // path to .py file
	string pyStart = "";
	string procArgs = "";
	GameObject startObj;
	cmdStarter startScript;
    // Start is called before the first frame update
    void Start()
    {
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
		startScript.CMDStart(procArgs);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
