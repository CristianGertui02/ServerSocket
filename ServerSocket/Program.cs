using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipaddr = IPAddress.Any;
            IPEndPoint ipep = new IPEndPoint(ipaddr, 23000);
            listenerSocket.Bind(ipep);
            listenerSocket.Listen(5);
            Console.WriteLine("Server in ascolto...");
            Socket client = listenerSocket.Accept();
            Console.WriteLine("client connesso. client info: " + client.RemoteEndPoint.ToString());
            byte[] buff = new byte[128];
            int numberByteReceived = 0;
            numberByteReceived = client.Receive(buff);
            string mexRicevuto = Encoding.ASCII.GetString(buff, 0, numberByteReceived);
            Console.WriteLine("il client dice: " + mexRicevuto);
            string mexDaInviare = "benvenuto mi hai appena scritto: " + mexRicevuto;
            Array.Clear(buff, 0, buff.Length);
            buff = Encoding.ASCII.GetBytes(mexDaInviare);
            Console.WriteLine("Sto inviando la risposta...");
            int SendedBytes = 0;
            SendedBytes = client.Send(buff);
            Console.WriteLine("risposta inviata termino");

        }
    }
}
