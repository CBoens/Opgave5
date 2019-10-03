using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Library;
using Newtonsoft.Json;

namespace Opgave5TCP_server
{
    class Worker
    {
        private static List<Bog> listeBog = new List<Bog>()
        {
            new Bog("Departemantet", "Idrissa Elba", 456, "1234567890123"),
            new Bog("Mars' Geologi", "Vail Corona", 667, "1234567890124"),
            new Bog("Zappas ministerium", "Enoca Surbina", 234, "1234567890125"),
            new Bog("El Coronel Porro", "Jela Duarte Quintero", 750, "1234567890126"),
            new Bog("Klodernes Kamp", "H.G. Wells", 326, "1234567890127"),
            new Bog("1q84", "Haruki Murakami", 989, "1234567890128"),
            new Bog("Tosser", "landsby tossen", 657, "1234567890122")
        };

        public Worker() {}

        public void Start()
        {
            TcpListener server = new TcpListener(IPAddress.Loopback, 4646);
            server.Start();
            Console.WriteLine("back to work...");

            while (true)
            {
                TcpClient socket = server.AcceptTcpClient();

                Task.Run((() =>
                {
                    TcpClient tmpsocket = socket;
                    DoClient(tmpsocket);
                }));
            }
        }

        public void DoClient(TcpClient socket)
        {
            NetworkStream netstream = socket.GetStream();

            using (StreamReader sl = new StreamReader(netstream))//StreamLæser
            using (StreamWriter ss = new StreamWriter(netstream))//StreamSkriver
            {
                while (true)
                {
                    Facade(ss);
                    string input = sl.ReadLine();

                    if (input == "HentAlle")
                    {
                        sl.ReadLine();
                        ss.WriteLine(HentAlle());
                        ss.Flush();
                    }

                    if (input == "Hent")
                    {
                        string isbn13 = sl.ReadLine();
                        ss.WriteLine(Hent(isbn13));
                        ss.Flush();
                    }

                    if (input == "Gem")
                    {
                        string jsonBog = sl.ReadLine();
                        Gem(jsonBog);
                    }

                    if (input == "exit" || input == "exit")
                    {
                        break;
                    }
                }
                 
            }
            socket.Close();
        }


        private void Facade(StreamWriter ss)
        {
            ss.WriteLine("Velkommen til Bog serveren");
            ss.WriteLine("");
            ss.WriteLine("Du kan 3 forskellige ting på denne server.");
            ss.WriteLine("'HentAlle', 'Hent' og 'Gem'");
            ss.WriteLine();
            ss.WriteLine("'HentAlle' uden '' Viser alle bøgerne vi har.");
            ss.WriteLine("'Hent' uden '' efterfulgt af gyldigt ISBN. viser Bogen med det specifikke ISBN-Nummer");
            ss.WriteLine("'Gem' uden '' efterfulgt af En Bog i JSON-format Gemmer denne bog i listen");
            ss.WriteLine("F.eks: {Titel\":\"PixieBog\"Forfatter:\"Klokkeblomst\",ISBN13:\"1234567890121\",\"Sidetal\":\"25\",\"Titel\":\"PixieBog\"}");
            ss.WriteLine("");
            ss.WriteLine("(Det kan være nødvendigt at sende en Blank linie fra sockettest før input virker)");
            ss.Flush();
        }


        private string HentAlle()
        {
            return JsonConvert.SerializeObject(listeBog);
        }

        private string Hent(string isbn13)
        {
            return JsonConvert.SerializeObject(listeBog.Find(b => b.ISBN13 == isbn13));
        }

        private void Gem(string JSonBog)
        {
            Bog bog = JsonConvert.DeserializeObject<Bog>(JSonBog);
            listeBog.Add(bog);
        }
    
    }
}
