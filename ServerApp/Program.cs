using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ServerApp
{
    private static TcpListener listener;
    private static string fileName;

    static void Main(string[] args)
    {
        int port = 5000; // Задаем порт 
        listener = new TcpListener(IPAddress.Any, port);
        listener.Start();

        DateTime startTime = DateTime.Now;
        fileName = $"registration_marathon_{startTime:dd.MM.yyyy_HH-mm-ss}.txt";
        using (StreamWriter writer = new StreamWriter(fileName, true))
        {
            writer.WriteLine($"Сервер запущен: {startTime}");
        }

        Console.WriteLine("Сервер запущен и ожидает подключений...");

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Thread clientThread = new Thread(HandleClient);
            clientThread.Start(client);
        }
    }

    private static void HandleClient(object clientObj)
    {
        TcpClient client = (TcpClient)clientObj;
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];

        int bytesRead = stream.Read(buffer, 0, client.ReceiveBufferSize);
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        using (StreamWriter writer = new StreamWriter(fileName, true))
        {
            writer.WriteLine(dataReceived);
        }

        byte[] responseBytes = Encoding.UTF8.GetBytes("Данные успешно приняты");
        stream.Write(responseBytes, 0, responseBytes.Length);

        client.Close();
    }
}