using System;
using RepDLL;

namespace Replication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length <= 2)
                {
                    _RepLog._toFileLog("Запуск без аргументов!");
                }
                else
                {
                    switch (args[0])
                    {
                        case "hdd":
                            string _mServerName = args[1];
                            string _mTable = args[2];
                            HDDInfo dInfo = new HDDInfo(_mServerName, _mTable + "_freehdd", "DBName");
                            dInfo.GetHardwareInfo();
                            break;
                        case "index":
                            string Path = args[1];
                            string _mbTable = args[2];
                            int myThreadsCount = Convert.ToInt32(args[3]);
                            if (Path == "") Path = "dbase.txt";
                            _TelegramBot.TelegramSend(String.Format("Started index to {0} streams ", args[3]));
                            _ThreadTaskDB._TaskIndex(Path, _mbTable + "_all", myThreadsCount);
                            break;
                        case "copy":
                            string SourceTable = args[1];
                            string DestinationTable = args[2];
                            string FileDisk = args[3];
                            int ThreadCount = Convert.ToInt32(args[4]);
                            int TopRecord = Convert.ToInt32(args[5]);
                            _TelegramBot.TelegramSend(String.Format("Started copying to {0} streams with a step of {1}", args[4], args[5]));
                            _ThreadTaskCopy._TaskCopy(SourceTable, DestinationTable, FileDisk, ThreadCount, TopRecord);
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
            }
        }
    }
}
