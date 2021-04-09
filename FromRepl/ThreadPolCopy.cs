using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace RepDLL
{
    public class ThreadPolCopy
    {
        private string namethread;
        private string sTableName;
        private string dTableName;
        private string DataFreeSpaceHDD;
        private string SourcePath = @"\\sasbibsrv01\";
        private List<List<string>> data;
        private int polstart;
        private int polend;

        /// <summary>
        /// Функция для создания потоков на копирование
        /// </summary>
        /// <param name="sTableDB">Имя таблицы с перечнем данных для копирования </param>
        /// <param name="dTableDB">Имя результирующей таблицы с данными копирования</param>
        /// <param name="DataFreeSpaceHDD">Имя файла с перечнем HDD для копирования на них данных</param>
        /// <param name="SourcePath">Источник места копирования</param>
        /// <param name="data">База даных для копирования</param>
        /// <param name="polstart">Начальный номер потока</param>
        /// <param name="polend">Конечный номер потока</param>
        /// <returns></returns>
        public ThreadPolCopy(string namethread, string sTableName, string dTableName, string DataFreeSpaceHDD, string SourcePath, List<List<string>> data, int polstart, int polend)
        {
            this.namethread = namethread;
            this.sTableName = sTableName;
            this.dTableName = dTableName;
            this.DataFreeSpaceHDD = DataFreeSpaceHDD;
            this.SourcePath = SourcePath;
            this.data = data;
            this.polstart = polstart;
            this.polend = polend;
        }
        /// <summary>
        /// Функция формирования потка капирования
        /// </summary>
        public void _ThreadMain()
        {
            try
            {
                _SQLQuery P_Copy = new _SQLQuery("DBName");

                List<int> ValDel = new List<int>();
                List<string> ValInsStr = new List<string>();

                string sourceFile = "";
                string destFile = "";

                for (int g = polstart; g <= polend; g++)
                {
                    string[] splpath = null;
                    string fileName = "", sourcePath = "", targetPath = "";

                    splpath = data[g][2].Split(new char[] { '\\' });

                    fileName = data[g][1];
                    sourcePath = SourcePath + data[g][2].Replace(":", "$");
                    targetPath = "";

                    if (splpath.Length == 4)
                    {
                        targetPath = AnalizeHDD.FreeDisk(DataFreeSpaceHDD) + @"AsArchStorage\V3\" + data[g][2].Remove(0, 13) + @"\active";
                    }
                    else if (splpath.Length == 5)
                    {
                        if (splpath[1] == "gpfs")
                        {
                            targetPath = AnalizeHDD.FreeDisk(DataFreeSpaceHDD) + @"AsArchStorage\V3\" + data[g][2].Remove(0, 13);
                        }
                        else
                        {
                            targetPath = AnalizeHDD.FreeDisk(DataFreeSpaceHDD) + data[g][2].Remove(0, 3);
                        }
                    }
                    else if (splpath.Length == 6)
                    {
                        var count = 1;

                        char[] ch = splpath[1].ToCharArray();
                        count = ch.Where((n) => n >= '0' && n <= '9').Count();

                        if (count == 2)
                        {
                            targetPath = AnalizeHDD.FreeDisk(DataFreeSpaceHDD) + data[g][2].Remove(0, 6);
                        }
                        else
                        {
                            targetPath = AnalizeHDD.FreeDisk(DataFreeSpaceHDD) + data[g][2].Remove(0, 5);
                        }
                    }
                    else
                    {
                        _RepLog._toFileLog("Ошибка пути для записи!");
                    }

                    sourceFile = Path.Combine(sourcePath, fileName);
                    destFile = Path.Combine(targetPath, fileName);

                    if (File.Exists(sourceFile))
                    {
                        if (!File.Exists(destFile))
                        {
                            try
                            {
                                File.Copy(sourceFile, destFile, false);

                                ValInsStr.Add(String.Format("'{0}','{1}','{2}','{3}','{4}','{5}','{6}'", data[g][1], data[g][2], data[g][3], data[g][4], data[g][5], data[g][6], data[g][7]));
                                ValDel.Add(Convert.ToInt32(data[g][0]));
                                _RepLog._toFileLog("Скопирован файл ID: " + Convert.ToInt32(data[g][0]));
                            }
                            catch
                            {
                                _RepLog._toFileLog(String.Format("Ошибка копирования в потоке: ID записи {0} = Source({1}) - Destination({2})", data[g][0], sourceFile, destFile));
                            }
                        }
                        else
                        {
                            _RepLog._toFileLog(destFile + " - Уже существует");
                            ValDel.Add(Convert.ToInt32(data[g][0]));
                        }
                    }
                    else
                    {
                        _RepLog._toFileLog(sourceFile + " - Файл не найден");
                        if (!File.Exists(destFile))
                        {
                            ValDel.Add(Convert.ToInt32(data[g][0]));
                        }
                        else
                        {
                            File.Delete(destFile);
                            ValDel.Add(Convert.ToInt32(data[g][0]));
                        }
                    }
                }
                if (ValInsStr.Count != 0)
                {
                    P_Copy._insertForeach(dTableName, ValInsStr);
                }
                if (ValDel.Count != 0)
                {
                    P_Copy._deleteForeach(sTableName, ValDel);
                }
                
            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Ошибка пути: " + ex.ToString());
            }

        }
    }
}
