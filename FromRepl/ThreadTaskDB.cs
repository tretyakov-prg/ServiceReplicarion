using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace RepDLL
{
    public static class _ThreadTaskDB
    {
        public static void _TaskIndex(string myPath, string myTableDB, int myThreadsCount = 3)
        {
            _SQLQuery ins = new _SQLQuery("DBName");

            if (ins._existsSQL(myTableDB) == false)
            {
                ins._creatTable(String.Format("CREATE TABLE {0}(id INT IDENTITY(1,1) NOT NULL,name VARCHAR(255) NULL,path VARCHAR(255) NULL,dbname VARCHAR(255) NULL,size VARCHAR(255) NULL,data_creat VARCHAR(255) NULL,data_access VARCHAR(255) NULL,data_write VARCHAR(255) NULL, format VARCHAR(255) NULL, PRIMARY KEY(id));", myTableDB), myTableDB);
            }
            else
            {
                ins._clear(myTableDB);
                _RepLog._toFileLog("Таблица " + myTableDB + " отчищена");
            }

            var applicationDirectory = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            var path = Path.Combine(applicationDirectory, myPath);
            int row_file = 0;

            string[] flp = new string[File.ReadAllLines(path).Length];
            foreach (string pol in File.ReadAllLines(path))
            {
                flp[row_file] = pol;
                row_file++;
            }

            List<ThreadPolDB> my_threads = new List<ThreadPolDB>();
            List<Thread> threads = new List<Thread>();

            int rez = File.ReadAllLines(path).Length / myThreadsCount;
            int rez0 = rez * myThreadsCount;
            int allrez = File.ReadAllLines(path).Length - rez0;

            for (int f = 0; f < myThreadsCount; f++)
            {
                if (f == 0)
                {
                    my_threads.Add(new ThreadPolDB("поток-" + f, myTableDB, flp, f * rez, f * rez + rez));
                    threads.Add(new Thread(my_threads[f].ThreadMain));
                    threads[f].Start();
                    //threads[f].IsBackground = true;
                }
                else
                {
                    if (f == myThreadsCount - 1)
                    {
                        my_threads.Add(new ThreadPolDB("поток-" + f, myTableDB, flp, f * rez + 1, f * rez + rez + allrez - 1));
                        threads.Add(new Thread(my_threads[f].ThreadMain));
                        threads[f].Start();
                        //threads[f].IsBackground = true;
                    }
                    else
                    {
                        my_threads.Add(new ThreadPolDB("поток-" + f, myTableDB, flp, f * rez + 1, f * rez + rez));
                        threads.Add(new Thread(my_threads[f].ThreadMain));
                        threads[f].Start();
                        //threads[f].IsBackground = true;
                    }
                }
            }
            bool threadEnd = true;
            while (threadEnd)
            {
                Thread.Sleep(5000);
                threadEnd = false;
                for (int f = 0; f < threads.Count; f++)
                {
                    if (threads[f].ThreadState == System.Threading.ThreadState.Running)
                    {
                        //_RepLog._toFileLog("DEBUG: Thread " + f.ToString() + "is runnig.");
                        threadEnd = true;
                        break; // +1 is runnnig
                    }
                    else
                    {
                        //_RepLog._toFileLog("DEBUG: Thread " + f.ToString() + "is NOT runnig.");
                    }
                }
            }
            //_TelegramBot.TelegramSend("Потоки для " + myTableDB + " закончены!");
        }

        public static void _TaskIndexCount(string myPath, string myTableDB, int myThreadsCount = 3)
        {
            var applicationDirectory = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            var path = Path.Combine(applicationDirectory, myPath);

            int row_file = 0;

            string[] flp = new string[File.ReadAllLines(path).Length];
            foreach (string pol in File.ReadAllLines(path))
            {
                flp[row_file] = pol;
                row_file++;
            }

            List<ThreadPolDB> my_threads = new List<ThreadPolDB>();
            List<Thread> threads = new List<Thread>();

            int rez = File.ReadAllLines(path).Length / myThreadsCount;
            int rez0 = rez * myThreadsCount;
            int allrez = File.ReadAllLines(path).Length - rez0;

            for (int f = 0; f < myThreadsCount; f++)
            {
                if (f == 0)
                {
                    my_threads.Add(new ThreadPolDB("поток-" + f, myTableDB, flp, f * rez, f * rez + rez));
                    threads.Add(new Thread(my_threads[f].ThreadMainCount));
                    threads[f].Start();
                    //threads[f].IsBackground = true;
                }
                else
                {
                    if (f == myThreadsCount - 1)
                    {
                        my_threads.Add(new ThreadPolDB("поток-" + f, myTableDB, flp, f * rez + 1, f * rez + rez + allrez - 1));
                        threads.Add(new Thread(my_threads[f].ThreadMainCount));
                        threads[f].Start();
                        //threads[f].IsBackground = true;
                    }
                    else
                    {
                        my_threads.Add(new ThreadPolDB("поток-" + f, myTableDB, flp, f * rez + 1, f * rez + rez));
                        threads.Add(new Thread(my_threads[f].ThreadMainCount));
                        threads[f].Start();
                        //threads[f].IsBackground = true;
                    }
                }
            }
        }
    }
}
