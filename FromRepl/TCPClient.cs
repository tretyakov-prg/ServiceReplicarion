using System;
using System.Text;
using System.Net.Sockets;


namespace RepDLL
{
    public class TCPClient
    {
        public string hostname = "";
        public int port = 0;

        public TCPClient(string address, int port)
        {
            this.hostname = address;
            this.port = port;
        }
        public string Exchange(string outMessage)
        {
            TcpClient client = null;
            try
            {
                // Инициализация
                client = new TcpClient(hostname, port);
            }
            catch(Exception ex)
            {
                _RepLog._toFileLog("TCP Connection: " + ex);
            }
            Byte[] data = Encoding.UTF8.GetBytes(outMessage);

            NetworkStream stream = client.GetStream();
            try
            {
                // Отправка сообщения
                stream.Write(data, 0, data.Length);
                // Получение ответа
                Byte[] readingData = new Byte[256];
                String responseData = String.Empty;
                StringBuilder completeMessage = new StringBuilder();
                int numberOfBytesRead = 0;
                do
                {
                    numberOfBytesRead = stream.Read(readingData, 0, readingData.Length);
                    completeMessage.AppendFormat("{0}", Encoding.UTF8.GetString(readingData, 0, numberOfBytesRead));
                }
                while (stream.DataAvailable);
                responseData = completeMessage.ToString();
                return responseData;
            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("TCP error: " + ex);
                return "false";
            }
            finally
            {
                stream.Close();
                client.Close();
            }
        }
    }
}
