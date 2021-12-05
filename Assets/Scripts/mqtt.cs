using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net.Sockets;

using uPLibrary.Networking.M2Mqtt.Exceptions;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Internal;
using System.Security.Cryptography.X509Certificates;
using uPLibrary.Networking.M2Mqtt.Session;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt;
using System.Net.Security;
using System;
using System.Text;

public class MQTT : MonoBehaviour
{
	private MqttClient client;

	// Connection information
	public string brokerHostname = "mqtt.eclipseprojects.io";
	public int brokerPort = 8883;
	public string userName = "test";
	public string password = "test";
	public TextAsset certificate;

	static string subTopic = "ece180d/team8/test"; //For testing purposes

	void Start()
	{
		if (brokerHostname != null && userName != null && password != null)
		{
			Debug.Log("Connecting to " + brokerHostname + ":" + brokerPort);
			Connect();
			client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
			byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE };
			client.Subscribe(new string[] { subTopic }, qosLevels);
		}
	}


	private void Connect()
	{
		Debug.Log("About to connect on '" + brokerHostname + "'");
		// Forming a certificate based on a TextAsset
		X509Certificate cert = new X509Certificate(); //certificate.bytes
		Debug.Log("Using the certificate '" + cert + "'");
		client = new MqttClient(brokerHostname, brokerPort, true, cert, null, MqttSslProtocols.TLSv1_0, MyRemoteCertificateValidationCallback);
		string clientId = Guid.NewGuid().ToString();
		Debug.Log("About to connect using '" + userName + "' / '" + password + "'");
		try {
			client.Connect(clientId, userName, password);
		}
		catch (Exception e) {
			Debug.LogError("Connection error: " + e);
		}
	}

	public static bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
	{
		return true;
	}

	void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
	{
		string msg = System.Text.Encoding.UTF8.GetString(e.Message);
		Debug.Log("Received message from " + e.Topic + " : " + msg);
	}

	private void Publish(string _topic, string msg)
	{
		client.Publish(_topic, Encoding.UTF8.GetBytes(msg), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
	}
}