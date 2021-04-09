using System;
using System.Threading.Tasks;

namespace RepDLL
{
    public static class TaskWorks
    {
        private static Task hdd = null;
        private static Task index = null;
        private static Task copy = null;
        public static bool TaskWorkReplication(string[] args)
        {
            bool rezultTCPQuery = false;
            //_RepLog._toFileLog("Аргументов в запросе: " + args.Length.ToString());
            try
            {
                if (args.Length <= 2)
                {
                    _RepLog._toFileLog("Запуск без аргументов!");
                }
                else
                {
                    //_RepLog._toFileLog(args[0]);
                    switch (args[0])
                    {
                        case "hdd":
                            if (hdd == null || hdd.IsCompleted)
                            {
                                hdd = Task.Factory.StartNew(() =>
                                {
                                    string _mServerName = args[1];
                                    string _mTable = args[2];
                                    HDDInfo dInfo = new HDDInfo(_mServerName, _mTable + "_freehdd", "DBName");
                                    dInfo.GetHardwareInfo();
                                });
                                rezultTCPQuery = true;
                            }
                            else
                            {
                                _RepLog._toFileLog("Задача: Запрос свободного метса на дисках. Выполняеться.");
                                rezultTCPQuery = false;
                            }
                            break;
                        case "infohdd":
                            if (hdd == null || hdd.IsCompleted)
                            {
                                rezultTCPQuery = true;
                            }
                            else
                            {
                                rezultTCPQuery = false;
                            }
                            break;
                        case "index":
                            if (index == null || index.IsCompleted)
                            {
                                index = Task.Factory.StartNew(() =>
                                {
                                    string Path = args[1];
                                    string _mbTable = args[2];
                                    int myThreadsCount = Convert.ToInt32(args[3]);
                                    if (Path == "") Path = "dbase.txt";
                                    //_TelegramBot.TelegramSend(String.Format("Started index to {0} streams ", args[3]));
                                    //_ThreadTaskDB._TaskIndex(Path, _mbTable, myThreadsCount);
                                    ThreadPoolIndex indexHDD = null;
                                    if (args.Length > 2)
                                    {
                                        indexHDD = new ThreadPoolIndex(myThreadsCount);
                                    }
                                    else
                                    {
                                        indexHDD = new ThreadPoolIndex();
                                    }
                                    try
                                    {
                                        indexHDD.ThreadPoolIndexDirectory(Path, _mbTable); //("file.txt", "masbibi06", 70)
                                    }
                                    catch (Exception ex)
                                    {
                                        _RepLog._monitor("Pool error " + ex);
                                    }
                                });
                                rezultTCPQuery = true;
                            }
                            else
                            {
                                _RepLog._toFileLog("Задача: Индексации в статусе: Выполняеться");
                                rezultTCPQuery = false;
                            }
                            break;
                        case "infoindex":
                            if (index == null || index.IsCompleted)
                            {
                                rezultTCPQuery = true;
                            }
                            else
                            {
                                rezultTCPQuery = false;
                            }
                            break;
                        case "copy":
                            if (copy == null || copy.IsCompleted)
                            {
                                copy = Task.Factory.StartNew(()=> 
                                {
                                    //_RepLog._toFileLog("Insert case COPY " + args[0]);
                                    string SourceTable = args[1];
                                    string DestinationTable = args[2];
                                    string FileDisk = args[3];
                                    int ThreadCount = Convert.ToInt32(args[4]);
                                    int TopRecord = Convert.ToInt32(args[5]);
                                    _TelegramBot.TelegramSend(String.Format("Started copying to {0} streams with a step of {1}", args[4], args[5]));
                                    _ThreadTaskCopy._TaskCopy(SourceTable, DestinationTable, FileDisk, ThreadCount, TopRecord);
                                });
                                rezultTCPQuery = true;
                            }
                            else
                            {
                                _RepLog._toFileLog("Задача: Копирование файлов в статусе: Выполняеться");
                                rezultTCPQuery = false;
                            }
                            break;
                        case "infocopy":
                            if (copy == null || copy.IsCompleted)
                            {
                                rezultTCPQuery = true;
                            }
                            else
                            {
                                rezultTCPQuery = false;
                            }
                            break;
                        default:
                            _RepLog._toFileLog("Запуск без аргументов!");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Ошибка преднастройки: " + ex);
                rezultTCPQuery = false;
            }
            _RepLog._toFileLog("Завершение программы!");
            return rezultTCPQuery;
        }
    }
}
