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
	String filename = "wrist_single.csv"
    // Start is called before the first frame update
    void Start()
    {
		// using example written here https://docs.microsoft.com/en-us/dotnet/api/system.io.streamreader?view=net-6.0
		try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader("TestFile.txt"))
            {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
		Debug.Log("Once per second")
        Thread.Sleep(1000);
    }
}
