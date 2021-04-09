using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace RepDLL
{
    public class TCPServer
    {
        public string url = "";
        public int port = 0;
        public string pars = "";
        public TCPServer(string url, int port)
        {
            this.url = url;
            this.port = port;
        }
        ~TCPServer()
        {

        }
        public void Start()
        {
            if (url == "") url = "localhost";
            if (port == 0) port = 8888;

            IPAddress localAddr = IPAddress.Parse(url);
            
            TcpListener server = new TcpListener(localAddr, port);

            // Запуск в работу
            server.Start();
            // Бесконечный цикл
            while (true)
            {
                try
                {
                    // Подключение клиента
                    TcpClient client = server.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    // Обмен данными
                    try
                    {
                        if (stream.CanRead)
                        {
                            byte[] myReadBuffer = new byte[1024];
                            StringBuilder myCompleteMessage = new StringBuilder();
                            int numberOfBytesRead = 0;
                            do
                            {
                                numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                                myCompleteMessage.AppendFormat("{0}", Encoding.UTF8.GetString(myReadBuffer, 0, numberOfBytesRead));
                                pars = myCompleteMessage.ToString();
                            }
                            while (stream.DataAvailable);

                            _RepLog._toFileLog("TCP Message: Данные обработаны!");

                            //_RepLog._toFileLog("TCP Message: " + pars);

                            if (TaskWorks.TaskWorkReplication(pars.Split(' ')))
                            {
                                Byte[] responseDataOk = Encoding.UTF8.GetBytes("true");
                                stream.Write(responseDataOk, 0, responseDataOk.Length);
                            }
                            else
                            {
                                Byte[] responseDataErr = Encoding.UTF8.GetBytes("false");
                                stream.Write(responseDataErr, 0, responseDataErr.Length);
                            }
                        }
                    }
                    finally
                    {
                        stream.Close();
                        client.Close();
                    }
                }
                catch
                {
                    _RepLog._toFileLog("TCP Error: Ошибка обработки данных!");
                    server.Stop();
                    break;
                }
            }
        }
    }
}
