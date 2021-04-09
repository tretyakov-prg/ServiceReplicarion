using System;
using System.Text;
using System.IO;
using System.Net;

namespace RepDLL
{
    public class _TelegramBot
    {
        public static void TelegramSend(string _Message)
        {
            try
            {
                string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
                string apiToken = "1260093993:AAFX4Rtml6To_qQ2ZqfE90nz3mP7fP3_7NY";
                string chatId = "-331003465";
                string text = _Message;

                urlString = String.Format(urlString, apiToken, chatId, text);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;

                WebRequest request = WebRequest.Create(urlString);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "GET";
                byte[] bytes = Encoding.ASCII.GetBytes(urlString);


                Stream rs = request.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(rs);
                string line = "";
                StringBuilder sb = new StringBuilder();
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line != null)
                        sb.Append(line);
                }
                string response = sb.ToString();
            } catch (Exception ex)
            {
                _RepLog._toFileLog("Telegram Error: " + ex);
            }
        }
    }
}
