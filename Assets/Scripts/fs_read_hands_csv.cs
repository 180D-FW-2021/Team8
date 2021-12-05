using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


// TODO: reader will need to retrieve the name of the python output .csv somehow. In the actual game, we might just make the name consistent.
namespace CursorObject {
public class fs_read_hands_csv : MonoBehaviour
    {
        public Transform Cursor;
        public float x = -1;
        public float y = -1;
        public float z = -1;
        public float newx = -1;
        public float newy = -1;
        public float newz = -1;
        bool readX, readY, readZ; // used in TryParse().
        bool readSuccess;
        string[] coordString;
        string filepath = "PyOut/wrist_single.csv";
        int delay_ms = 13; // not exact correspondence between delay and frame rate due to processing time.
                           // i.e. delay less than you think you need for a desired frame rate. (16 ms should be approx. 60 fps but produces lower fps in practice)
                           // there should be some delay to preserve resource use. Noticed less CPU temp increase when delay is used.
                           // Start is called before the first frame update
        
     
        void Start()
        {
            Debug.Log("hands read script start");
        }

        // Update is called once per frame
        void Update()
        {
            asyncCSVRead();
            if (readSuccess)
            {
                x = newx; y = newy; // z = newz;
                posUpdate(5);
            }
        }

        async void asyncCSVRead()
        {
            /*
    		Reads from csv in filepath. Updates x and y according to first and second values in csv, respectively.
    		Run wrapperTest.py to start OpenCV window before starting game.
    		*/

            // using example written here https://www.c-sharpcorner.com/article/working-with-c-sharp-streamreader/
            // and here https://docs.microsoft.com/en-us/dotnet/api/system.io.stringreader.readlineasync?view=net-6.0
            try
            {
                using (StreamReader sr = new StreamReader(new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    string line;
                    if ((line = await sr.ReadLineAsync()) != null)
                    {
                        coordString = line.Split(',');
                        // Debug.Log(coordString[0]);
                        readX = float.TryParse(coordString[0], out newx);
                        readY = float.TryParse(coordString[1], out newy); // slow?
                                                                          // readZ = float.TryParse(coordString[2], out newz); // why doesn't this work?
                        readSuccess = readX && readY; // all reads must be successful to update
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Debug.Log("Exception in asyncCSVRead():");
                Debug.Log(e.Message);
                readSuccess = false;
            }
            Thread.Sleep(delay_ms);
        }

        void posUpdate(float scale = 1) // perhaps make scale dependent on z-distance of wrist node? 
                                        // i.e. increase scale for higher distance to maintain sensitivity.
        {

            Cursor.position = new Vector3(-scale * x, 0, -scale * y); // actually corresponds to z in unity. Change according to final camera orientation
                                                                      // needs scaling and tuning for actual game use.   
        }

    }
}