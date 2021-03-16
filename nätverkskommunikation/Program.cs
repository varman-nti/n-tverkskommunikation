using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;




namespace nätverkskommunikation
{
    class Program
    {


        static void Main(string[] args)
        {

            String address = "127.0.0.1";
            int port = 8001;

            //anslut till servern
            Console.WriteLine("Ansulter...");
            TcpClient tcpClient = new TcpClient();

            tcpClient.Connect(address, port);
            Console.WriteLine("Ansluten");
            while (true)
            {
                //skriv in meddelande att skicka
                Console.Write("Skriv in meddelande: ");
                String message = Console.ReadLine();


                //konvertera meddelande till ACHII- bytes
                Byte[] bMessage = System.Text.Encoding.ASCII.GetBytes(message);

                Console.Write("Skickar");
                NetworkStream tcpStream = tcpClient.GetStream();
                tcpStream.Write(bMessage, 0, bMessage.Length);

                //TAG EMOT MEDDELANDE FRÅN SERVER
                byte[] bRead = new byte[256];
                int bReadSize = tcpStream.Read(bRead, 0, bRead.Length);

                //konvertera meddelandet till ett string object och skriv ut
                string read = "";
                for (int i = 0; i < bReadSize; i++)
                    message += Convert.ToChar(bRead[i]);

                Console.WriteLine("Servern säger: " + read);
            }

            Console.ReadKey();
        }
    }
}
