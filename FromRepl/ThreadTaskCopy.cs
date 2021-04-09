using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RepDLL
{
    public static class _ThreadTaskCopy
    {
        /// <summary>
        /// Функция для создания потоков на копирование
        /// </summary>
        /// <param name="sTableDB">Имя таблицы с перечнем данных для копирования </param>
        /// <param name="dTableDB">Имя результирующей таблицы с данными копирования</param>
        /// <param name="DataFreeSpaceHDD">Имя файла с перечнем HDD для копирования на них данных</param>
        /// <param name="myThreadsCount">Количество потоков копирования</param>
        /// <param name="TopRecordDB">Число максмимальной выборки из базы</param>
        /// <returns></returns>
        public static bool _TaskCopy(string sTableDB, string dTableDB, string DataFreeSpaceHDD, int myThreadsCount = 3, int TopRecordDB = 1000)
        {
            int countThreadWhile = 0;
            bool status = false;
            //_RepLog._toFileLog("Internal in TaskCopy " + DataFreeSpaceHDD);
            try
            {
                while (true) 
                {
                    List<ThreadPolCopy> my_ThreadsCopy = new List<ThreadPolCopy>();
                    List<Thread> threadsCopy = new List<Thread>();

                    List<List<string>> gf = new List<List<string>>();

                    _SQLQuery T_Copy = new _SQLQuery("DBName");


                    if (T_Copy._Select("select count(*) from " + sTableDB + ";")[0][0] == "0")
                    {
                        _RepLog._toFileLog("В базе нет запесей");

                        return status = true;
                        break;
                    }
                    gf = T_Copy._Select("select top " + TopRecordDB + " * from " + sTableDB + ";");

                    _RepLog._toFileLog("В переборе из таблицы найдено " + gf.Count + "  элементов");

                    int rez = gf.Count / myThreadsCount;
                    int rez0 = rez * myThreadsCount;
                    int allrez = gf.Count - rez0;
                    try
                    {
                        for (int f = 0; f < myThreadsCount; f++)
                        {

                            if (f == 0)
                            {
                                my_ThreadsCopy.Add(new ThreadPolCopy("поток-" + f, sTableDB, dTableDB, DataFreeSpaceHDD, @"\\sasbibsrv01\", gf, f * rez, f * rez + rez));
                                threadsCopy.Add(new Thread(my_ThreadsCopy[f]._ThreadMain));
                                threadsCopy[f].Start();
                                //threadsCopy[f].Join();
                                _RepLog._toFileLog("Поток - " + f + " запущен.");
                            }
                            else
                            {
                                if (f == myThreadsCount - 1)
                                {
                                    my_ThreadsCopy.Add(new ThreadPolCopy("поток-" + f, sTableDB, dTableDB, DataFreeSpaceHDD, @"\\sasbibsrv01\", gf, f * rez + 1, f * rez + rez + allrez - 1));
                                    threadsCopy.Add(new Thread(my_ThreadsCopy[f]._ThreadMain));
                                    threadsCopy[f].Start();
                                    //threadsCopy[f].Join();
                                    _RepLog._toFileLog("Поток - " + f + " запущен.");
                                }
                                else
                                {
                                    my_ThreadsCopy.Add(new ThreadPolCopy("поток-" + f, sTableDB, dTableDB, DataFreeSpaceHDD, @"\\sasbibsrv01\", gf, f * rez + 1, f * rez + rez));
                                    threadsCopy.Add(new Thread(my_ThreadsCopy[f]._ThreadMain));
                                    threadsCopy[f].Start();
                                    //threadsCopy[f].Join();
                                    _RepLog._toFileLog("Поток - " + f + " запущен.");
                                }
                            }
                        }
                        
                    }
                    catch (ThreadStartException ex)
                    {
                        _RepLog._toFileLog("Статус потока:" + ex);
                    }

                    //_RepLog._toFileLog("DEBUG: Start thread status check.");
                    bool threadEnd = true;
                    while (threadEnd)
                    {
                        Thread.Sleep(5000);
                        threadEnd = false;
                        for (int f = 0; f < threadsCopy.Count; f++)
                        {
                            if (threadsCopy[f].ThreadState == ThreadState.Running)
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
                    
                    _RepLog._toFileLog(threadsCopy.Count + " - потоков завершено!");
                    countThreadWhile++;
                }
                //status = true;

            } catch (Exception ex)
            {
                _RepLog._toFileLog("Статус Цикла while :" + ex);
            }
            status = true;
            _TelegramBot.TelegramSend("Репликация завершилась. Всего " + countThreadWhile + " обходов!");
            _RepLog._toFileLog("Для копирования потребоволось (" + countThreadWhile + ") обходов таблицы");
            return status;
        }
    }
}
