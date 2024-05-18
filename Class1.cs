using System;
using System.Net.Sockets;
using System.Text;

namespace TcpLibrary
{
    public class TcpClientLib
    {
        private TcpClient _client;
        private NetworkStream _stream;

        public void Connect(string server, int port)
        {
            _client = new TcpClient(server, port);
            _stream = _client.GetStream();
        }

        public void SendData(string data)
        {
            byte[] bytesToSend = Encoding.UTF8.GetBytes(data);
            _stream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        public string ReceiveData()
        {
            byte[] bytesToRead = new byte[_client.ReceiveBufferSize];
            int bytesRead = _stream.Read(bytesToRead, 0, _client.ReceiveBufferSize);
            return Encoding.UTF8.GetString(bytesToRead, 0, bytesRead);
        }

        public void Close()
        {
            _stream.Close();
            _client.Close();
        }
    }
}