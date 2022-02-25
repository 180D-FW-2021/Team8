using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Linq;
using System;
using System.Text.RegularExpressions;

public class fryingPanMovement : MonoBehaviour
{
    private MqttClient client;
    private string username = "com";

    private float xTilt;
    private float yTilt;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.Rotate(0.0f,0.0f,0.0f);
        username = PlayerPrefs.GetString("Username");
        Debug.Log("mqtt " + username);

        client = new MqttClient("test.mosquitto.org");
        client.MqttMsgPublished += client_MqttMsgPublished;
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

        string clientId = Guid.NewGuid().ToString();

        client.Connect(clientId);
        client.Subscribe(new string[] { "ece180d/team8/movements" + username },
            new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(xTilt,yTilt,0.0f,Space.World);
    }

    void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
    {
        Debug.Log("Subscribed for id = " + e.MessageId);
    }

    void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
    {
        Debug.Log("inside client_MqttMsgPublished");
        Debug.Log("MessageId = " + e.MessageId + " Published = " + e.IsPublished);
        Debug.Log("MessageId = " + e.MessageId);
    }

    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        //this function is called everytime you receive message
        //e.Message is a byte[]
        var str = System.Text.Encoding.UTF8.GetString(e.Message);

        //Debug.Log("received a message");

        if (String.Equals(e.Topic, "ece180d/team8/movements" + username))
        {
            string xt = str.Substring(0,6);
            string yt = str.Substring(6,6);

            xTilt = float.Parse(xt);
            yTilt = float.Parse(yt);
        }
    }
}
