using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
namespace Server
{
    class Program
    {
        static TcpListener tcpListener;

        static void CancelKeyPress(object sender,ConsoleCancelEventArgs e)
        {
            tcpListener.Stop();
            Console.WriteLine("Severn stängdes av!");
        }


        static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelKeyPress);

            //Skapa ett tcplistener ocjekt börja lyssna och vänta på anslutning
            IPAddress myIp = IPAddress.Parse("127.0.0.1");
            tcpListener = new TcpListener(myIp, 8001);
            tcpListener.Start();
            Console.WriteLine("Väntar på anslutning....");

            //Något försöker ansluta.Acceptera anslutningen 
            Socket socket = tcpListener.AcceptSocket();
            Console.WriteLine("Anslutning accepted från" + socket.RemoteEndPoint);


            //tag emot meddelandet
            Byte[] bMessage = new Byte[256];
            int messageSize = socket.Receive(bMessage);
            Console.WriteLine("Meddelandet mottogs...");

            // Konvertera meddelandet till en stringvariabel och skriv ut 
            string message = "";
            for (int i = 0; i < messageSize; i++)
                message += Convert.ToChar(bMessage[i]);

            Console.WriteLine("meddelandet:" + message);
            Byte[] bSend = System.Text.Encoding.ASCII.GetBytes("tack");
            socket.Send(bSend);
            Console.WriteLine("svar skicakt");



            //stäng anslutningen mot just den här klienten
            tcpListener.Stop();
            Console.ReadKey();
        }
    }
}
