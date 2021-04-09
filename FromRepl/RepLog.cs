using System;
using System.IO;

namespace RepDLL
{
    public class _RepLog
    {
        public static void _toFileLog(string log)
        {
            DateTime thisDay = DateTime.Now;

            var applicationDirectory = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"\log";

            try
            {
                if (!Directory.Exists(applicationDirectory))
                {
                    Directory.CreateDirectory(applicationDirectory);
                }

                string writePath = thisDay.ToString("o").Remove(10, 23) + ".txt";
                var path = Path.Combine(applicationDirectory, writePath);

                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(String.Format("={0}= {1}", thisDay.ToString("g"), log)); //WriteLineAsync
                }
            }
            catch (Exception ex)
            {
                string _error = ex.Message;
            }

        }
        public static void _monitor(string log)
        {
            DateTime thisDay = DateTime.Now;

            var applicationDirectory = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"\monitor";

            try
            {
                if (!Directory.Exists(applicationDirectory))
                {
                    Directory.CreateDirectory(applicationDirectory);
                }

                string writePath = thisDay.ToString("o").Remove(10, 23) + ".txt";
                var path = Path.Combine(applicationDirectory, writePath);

                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                {
                    //sw.WriteLineAsync(String.Format("={0}= {1}", thisDay.ToString("g"), log));
                    sw.WriteLine(String.Format("={0}= {1}", thisDay.ToString("g"), log));
                }
            }
            catch (Exception ex)
            {
                string _error = ex.Message;
            }

        }
    }
}
