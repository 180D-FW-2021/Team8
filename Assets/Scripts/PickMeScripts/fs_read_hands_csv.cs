using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


// TODO: reader will need to retrieve the name of the python output .csv somehow. In the actual game, we might just make the name consistent.
namespace CursorObject 
{
public class fs_read_hands_csv : MonoBehaviour
    {
        public Transform Cursor;
        public float x = -1; //232;//-1;
        public float y = -1; //277;//-1;
        public float z = -1; //0;//-1;
        public float newx = -1; //232; //-1;
        public float newy = -1; //277; //-1;
        public float newz = -1;
        bool readX, readY, readZ; // used in TryParse().
        bool readSuccess;
        string[] coordString;
        string filepath = "wrist_single.csv";
		string exitFlag = "exit.txt";
        int delay_ms = 13; // not exact correspondence between delay and frame rate due to processing time.
                           // i.e. delay less than you think you need for a desired frame rate. (16 ms should be approx. 60 fps but produces lower fps in practice)
                           // there should be some delay to preserve resource use. Noticed less CPU temp increase when delay is used.
                           // Start is called before the first frame update
        Process process = new Process();
     
        void Start() //maybe move startup to game startup time?
        {
            UnityEngine.Debug.Log("hands read script start");
			
			bool onWin = Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor; // 1 if using Windows
			bool onMac = Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor; // 1 if using Mac
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
        }

        // Update is called once per frame
        void Update()
        {
            asyncCSVRead();
            if (readSuccess)
            {
                x = newx; y = newy; // z = newz;
                posUpdate();
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
                UnityEngine.Debug.Log("Exception in asyncCSVRead():");
                UnityEngine.Debug.Log(e.Message);
                readSuccess = false;
            }
            Thread.Sleep(delay_ms);
        }
        void posUpdate(float scalex = 24, float scaley = 15) // perhaps make scale dependent on z-distance of wrist node? 
                                        // i.e. increase scale for higher distance to maintain sensitivity.
        //void posUpdate(float scalex = 200, float scaley = 200)
        //void posUpdate(float scalex = 100, float scaley = 50)
		{
            float xOffset = -50; //-50;  //-232; //-5;
            float yOffset = -25;//-25;  //-277; //-277;  //-6;
                                 //Cursor.position = new Vector3(-scalex * x - xOffset, 0, -scaley * y - yOffset); // actually corresponds to z in unity. Change according to final camera orientation needs scaling and tuning for actual game use.   
            Cursor.position = new Vector3(-scalex * x - xOffset, -scaley * y - yOffset, 0);
            //Cursor.position = new Vector3(-scalex * x - xOffset, 0, -scaley * y - yOffset);
        }

		void OnDestroy()
		{
			process.CloseMainWindow(); // why doesn't this work?
			//process.Kill()
			//process.WaitForExit();
			process.Dispose();
			// write to file to tell python to close
			// python will rewrite it on startup.
			using(FileStream exitwrite = new FileStream(
					exitFlag, FileMode.OpenOrCreate,
					FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				exitwrite.WriteByte((byte) 'f');
			}
			Thread.Sleep(1000); // give python time to stop
			using(StreamWriter cursorreset = new StreamWriter(new FileStream(filepath, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite)))
			{
				cursorreset.WriteLine("0,0,0"); // why doesn't this consistently write?
			}
		}
    }
}