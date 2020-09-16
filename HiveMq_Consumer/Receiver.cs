using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace HiveMq_Consumer
{
    class Receiver
    {
        static void Main(string[] args)
        {
            MqttClient client = new MqttClient("127.0.0.1");
            byte code = client.Connect(Guid.NewGuid().ToString());

            ushort msgId = client.Subscribe(new string[] { "/my_topic", "/topic_2" },
                new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
                    MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            Console.WriteLine("Press [Enter] to exit the receiver app..");
            Console.ReadLine();
            client.Disconnect();

        }

        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Debug.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);
        }


    }
}
