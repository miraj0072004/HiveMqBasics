using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace HiveMq_Producer
{
    class Sender
    {
        static void Main(string[] args)
        {
            MqttClient client = new MqttClient("127.0.0.1");
            byte code = client.Connect(Guid.NewGuid().ToString());

            client.MqttMsgPublished += client_MqttMsgPublished;

            ushort msgId = client.Publish("/my_topic", // topic
                Encoding.UTF8.GetBytes("MyMessageBody"), // message body
                MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, // QoS level
                false); // retained

            Console.WriteLine("Press [Enter] to exit the sender app..");
            Console.ReadLine();
            client.Disconnect();
            
        }

        static void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Debug.WriteLine("MessageId = " + e.MessageId + " Published = " + e.IsPublished);
        }
    }
}
