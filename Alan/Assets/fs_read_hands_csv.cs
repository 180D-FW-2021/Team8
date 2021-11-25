using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;


// TODO: reader will need to retrieve the name of the python output .csv somehow. In the actual game, we might just make the name consistent.

public class fs_read_hands_csv : MonoBehaviour
{
	public int x = -1;
	public int y = -1;
	string filepath = "PyOut/wrist_single.csv";
	int delay_ms = 13; // not exact correspondence between delay and frame rate due to processing time.
					   // i.e. delay less than you think you need for a desired frame rate.
					   // there should be some delay to preserve resource use. Noticed less CPU temp increase when delay is used.
    // Start is called before the first frame update
    void Start()
    {
		Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
		asyncCSVRead();
    }
	
	async void asyncCSVRead()
	{
		// using example written here https://www.c-sharpcorner.com/article/working-with-c-sharp-streamreader/
		// and here https://docs.microsoft.com/en-us/dotnet/api/system.io.stringreader.readlineasync?view=net-6.0
		try
        {
			using (StreamReader sr = new StreamReader(new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))  
			{  
				string line;
				while ((line = await sr.ReadLineAsync()) != null)
                {
                    Debug.Log(line); // parse numbers and replace with cursor position
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
