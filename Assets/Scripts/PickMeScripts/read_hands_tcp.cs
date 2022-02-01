using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class read_hands_tcp : MonoBehaviour
{
    public Transform Cursor;
	public float x = -1;
	public float y = -1;
	public float z = -1;
	public float newx = -1;
	public float newy = -1;
	public float newz = -1;
	bool readX, readY, readZ; // used in TryParse().
	int readSuccess;
	string[] coordString;
	
	const int delay_ms = 10; // not exact correspondence between delay and frame rate due to processing time.
					   // i.e. delay less than you think you need for a desired frame rate. (16 ms should be approx. 60 fps but produces lower fps in practice)
					   // there should be some delay to preserve resource use. Noticed less CPU temp increase when delay is used.
    
    NetworkStream firstStream;
	TcpListener server;
	int status = 0;
	int frameCount = 0; // temp for testing-- sets limit on runtime before closeHandsTcp() is called
    // Start is called before the first frame update
    void Start()
    {
		// Start the sender Python script
		GameObject startObj = GameObject.Find("ProcessStartObject");
		PyVenvStart startScript = startObj.GetComponent<PyVenvStart>();
		startScript.pyStart("180DP", "PyOut/wrapperTest.py");
        firstStream = TcpInit(ref server);
    }

    // Update is called once per frame
    void Update()
    {
        status = TcpListen(ref server, ref firstStream, delay_ms);
		if(status == -1 || firstStream == null) //exception thrown!
		{
			Debug.Log("TcpReceive script disabled.");
			enabled = false; // disable update script.
		}
		if(readSuccess == 1) // new position available
		{
			x = newx; y = newy; // z = newz;
			posUpdate(16, 9);
		}
		// frameCount++;
		/* if(frameCount >= 800)
		{
			Debug.Log("TcpReceive max runtime reached.");
			closeHandsTcp(ref server, ref firstStream);
			enabled = false;
		} */
    }
	
	NetworkStream TcpInit(ref TcpListener server, int port = 13000)
	{
		// TcpListener server=null;
		try
		{
		    IPAddress localAddr = IPAddress.Parse("127.0.0.1");

		    // TcpListener server = new TcpListener(port);
		    server = new TcpListener(localAddr, port);

		    // Start listening for client requests.
		    server.Start();

		    // Enter the listening section
			Debug.Log("Waiting for a connection... ");

			// Perform a blocking call to accept requests.
			// You could also use server.AcceptSocket() here.
			TcpClient client = server.AcceptTcpClient();
			Debug.Log("Connected!");

			// Get a stream object for reading and writing
			NetworkStream stream = client.GetStream();
			Debug.Log("Stream acquired");
			//client.Close();
			return stream;
		}
		catch(SocketException e)
		{
		  Debug.Log("SocketException: " + e);
		  //client.Close();
		  // Stop listening for new clients.
		  server.Stop();
		  return null;
		}
	}
	
	int TcpListen(ref TcpListener server, ref NetworkStream stream, int delay)
	{
		// listens to stream
		// returns 1 if successful read, 0 if no read done or unsuccessful read, and -1 upon exception
		try
		{
			Thread.Sleep(delay);
			if(stream.CanRead && stream.DataAvailable){
				byte[] myReadBuffer = new byte[1024];
				StringBuilder myCompleteMessage = new StringBuilder();
				int numberOfBytesRead = 0;

				numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
				if(numberOfBytesRead > 0)
				{
					myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
					
					if(myCompleteMessage.Length > 0)
					{
						coordString = myCompleteMessage.ToString().Split(',');
						// Debug.Log(coordString[0]);
						readX = float.TryParse(coordString[0], out newx);
						readY = float.TryParse(coordString[1], out newy); // slow?
						// readZ = float.TryParse(coordString[2], out newz); // why doesn't this work?
						readSuccess = Convert.ToInt32(readX && readY); // all reads must be successful to update
						return readSuccess;
						
					}
				}
			}
			else{
				// Debug.Log("Sorry.  You cannot read from this NetworkStream.");
			}
			/*
			// Shutdown and end connection
			client.Close();
			return 0;
			*/
			return 0;
		}
		catch(SocketException e)
		{
		  Debug.Log("SocketException: " + e);
		  stream.Close();
		  server.Stop();
		  return -1;
		}

	}
	
	void posUpdate(float scalex = 100, float scaley = 55) // perhaps make scale dependent on z-distance of wrist node? 
									// i.e. increase scale for higher distance to maintain sensitivity.
	{
		float xOffset = -45;  //-232; //-5;
		float yOffset = -21;  //-277; //-277;  //-6;
							 //Cursor.position = new Vector3(-scalex * x - xOffset, 0, -scaley * y - yOffset); // actually corresponds to z in unity. Change according to final camera orientation needs scaling and tuning for actual game use.   
		Cursor.position = new Vector3(-scalex * x - xOffset, -scaley * y - yOffset, 0);
		//Cursor.position = new Vector3(-scalex * x - xOffset, 0, -scaley * y - yOffset);
	}
	
	void closeHandsTcp(ref TcpListener server, ref NetworkStream stream)
	{
		// closes the TCP connection, closing both the local TCP client and its corresponding socket
		// before closure, sends a signal to HandsWrapper.py to close the OpenCV window and halt its TCP connection.
		
		// send a signal to python tcp client
		byte[] exitFlag = {0xFF};
		stream.Write(exitFlag, 0, 1);
		// close stream and server
		// Thread.Sleep(1000); // wait for python side to process it
		stream.Close();
		server.Stop();
	}
	
	void OnDestroy()
	{
		closeHandsTcp(ref server, ref firstStream);
	}
}
