using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;


// TODO: reader will need to retrieve the name of the python output .csv somehow. In the actual game, we might just make the name consistent.

public class read_hands_csv : MonoBehaviour
{
	public int x = -1;
	public int y = -1;
	string filename = "PyOut/wrist_single.csv";
	int delay_ms = 1000;
    // Start is called before the first frame update
    void Start()
    {
		Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
		// using example written here https://docs.microsoft.com/en-us/dotnet/api/system.io.streamreader?view=net-6.0
		try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    Debug.Log(line);
                }
            }
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Debug.Log("The file could not be read:");
            Debug.Log(e.Message);
        }
        Thread.Sleep(delay_ms);
    }
}
