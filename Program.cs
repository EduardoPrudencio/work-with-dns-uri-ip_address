using System;
using System.Net;
using System.Net.NetworkInformation;
using static System.Console;

namespace WorkerWithText
{
    class Program
    {
        static ConsoleColor colorDefault = ForegroundColor = ConsoleColor.Cyan;

        static void Main(string[] args)
        {
            try
            {
                UrlVerify();
            }
            catch (Exception)
            {
                WriteBAdMessage("Houve um erro inesperado. Verifique o formato da url informada e tente novamente. ex: http://google.com");
                UrlVerify();
            }
            ReadKey();
        }

        private static void UrlVerify()
        {
            string url = null;

            do
            {
                ForegroundColor = ConsoleColor.Cyan;
                WriteLine("Informe um endereço válido de internet.");
                url = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(url));

            var uri = new Uri(url);

            WriteLine("");
            WriteHiglightMessage($"Url: {uri}\n");
            WriteHiglightMessage($"Scheme:  {uri.Scheme}\n");
            WriteHiglightMessage($"Port: {uri.Port}\n");
            WriteHiglightMessage($"Host: {uri.Host}\n");
            WriteHiglightMessage($"Path: {uri.AbsolutePath}\n");
            WriteHiglightMessage($"Query: {uri.Query}\n");
            WriteLine("");

            IPHostEntry entry = Dns.GetHostEntry(uri.Host);
            WriteLine($"{entry.HostName} possui os seguintes endereços de IP:");
            WriteLine("");

            foreach (IPAddress address in entry.AddressList)
                WriteLine($" {address}");

            WriteLine("");
            var ping = new Ping();
            WriteLine("Realizando o ping para o servidor, por favor, aguarde...");
            WriteLine("");

            PingReply reply = ping.Send(uri.Host);

            Write("O servidor foi pingado e retornou o seguinte status: ");

            if (reply.Status == IPStatus.Success) ForegroundColor = ConsoleColor.Green; else ForegroundColor = ConsoleColor.Red;

            Write(reply.Status);
            WriteLine("");

            ForegroundColor = ConsoleColor.Cyan;

            if (reply.Status == IPStatus.Success)
                WriteLine($"O tempo de resposta foi de: {reply.RoundtripTime:N0}ms para {reply.Address}\n");

            WriteHiglightMessage("\nPressione uma tecla para finalizar.\n");
        }

        private static void WriteGoodMessage(string message)
        {
            ForegroundColor = ConsoleColor.Green;
            Write(message);
            ForegroundColor = colorDefault;
        }

        private static void WriteBAdMessage(string message)
        {
            ForegroundColor = ConsoleColor.Red;
            Write(message);
            ForegroundColor = colorDefault;
        }

        private static void WriteHiglightMessage(string message)
        {
            ForegroundColor = ConsoleColor.Yellow;
            Write(message);
            ForegroundColor = colorDefault;
        }
    }
}
