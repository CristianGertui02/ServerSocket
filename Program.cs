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
            // Lister: in ascolto quando si parla dei server
            // EndPoint: identifica una coppia IP/Porta

            //Creare il mio socketlistener
            //1) specifico che versione IP
            //2) tipo di socket. Stream.
            //3) protocollo a livello di trasporto
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                                    ProtocolType.Tcp);
            // config: IP dove ascoltare. Possiamo usare l'opzione Any: ascolta da tutte le interfaccie all'interno del mio pc.
            IPAddress ipaddr = IPAddress.Any;

            // config: devo configurare l'EndPoint
            IPEndPoint ipep = new IPEndPoint(ipaddr, 23000);

            // config: Bind -> collegamento
            // listenerSocket lo collego all'endpoint che ho appena configurato
            listenerSocket.Bind(ipep);

            // Mettere in ascolto il server.
            // parametro: il numero massimo di connessioni da mettere in coda.
            listenerSocket.Listen(5);
            Console.WriteLine("Server in ascolto...");
            Console.WriteLine("in attesa di connessione da parte del client...");
            // Istruzione bloccante
            // restituisce una variabile di tipo socket.
            Socket client = listenerSocket.Accept();


            Console.WriteLine("Client IP: " + client.RemoteEndPoint.ToString());

            // mi attrezzo per ricevere un messaggio dal client
            // siccome è di tipo stream io riceverò dei byte, o meglio un byte array
            // riceverò anche il numero di byte.
            byte[] buff = new byte[128];
            int receivedBytes = 0;
            int sendedBytes = 0;
            //Ricevo effettivamente dei byte
            // la funzione receive restituisce il numero di byte ricevuti
            // e nel primo parametro vengono messi i byte effettivamente ricevuti
            while (true)
            {
                receivedBytes = client.Receive(buff);
                Console.WriteLine("Numero di byte ricevuti: " + receivedBytes);
                // I bytes devono essere convertiti in stringa
                // Parametri: i bytes,da dove iniziare a convertirli (0), quanti convertirne
                string receivedString = Encoding.ASCII.GetString(buff, 0, receivedBytes);
                Console.WriteLine("Stringa ricevuta: " + receivedString);

                

                if (receivedString.ToLower() == "quit") 
                {
                    break;
                }

                string sendedString = "Benvenuto " + client.RemoteEndPoint.ToString() + "! Al tuo servizio!\n";

                Array.Clear(buff, 0, buff.Length);

                switch (receivedString.ToLower())
                {
                    case "ciao":
                        sendedString = "Salve";
                        buff = Encoding.ASCII.GetBytes(sendedString);
                        client.Send(buff);

                        break;

                    case "buonasera":
                        sendedString = "Salve";
                        buff = Encoding.ASCII.GetBytes(sendedString);
                        client.Send(buff);
                        break;

                    case "buongiorno":
                        sendedString = "Salve";
                        buff = Encoding.ASCII.GetBytes(sendedString);
                        client.Send(buff);
                        break;

                    case "come stai?":
                        sendedString = "Bene";
                        buff = Encoding.ASCII.GetBytes(sendedString);
                        client.Send(buff);
                        break;

                    case "che fai?":
                        sendedString = "Niente";
                        buff = Encoding.ASCII.GetBytes(sendedString);
                        client.Send(buff);
                        break;

                    default:
                        sendedString = "Non ho capito";
                        buff = Encoding.ASCII.GetBytes(sendedString);
                        client.Send(buff);
                        break;

                }
                // Inviare messaggio al client
                // per inviarlo riutilizzo lo stesso buffer
                // pulisco il buffer
                Array.Clear(buff, 0, buff.Length);

                
                // crea il messaggio

            }
        }
    }
}
