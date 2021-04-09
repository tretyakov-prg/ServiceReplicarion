using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace RepDLL
{
    public  class ThreadPolDB
    {
        private string namethread;
        private string TableName;
        private string[] data;
        private int polstart;
        private int polend;
        
        /// <summary>
        /// Параметры формирования потока
        /// </summary>
        /// <param name="namethread">Уникальное имя потока</param>
        /// <param name="TableName">Имя таблицы для записи данных</param>
        /// <param name="data">Массив данных</param>
        /// <param name="polstart">Начальное значение потока</param>
        /// <param name="polend">Конечное значение потока</param>
        public  ThreadPolDB(string namethread, string TableName, string[] data, int polstart, int polend)
        {
            this.namethread = namethread;
            this.TableName = TableName;
            this.data = data;
            this.polstart = polstart;
            this.polend = polend;
        }
        
        /// <summary>
        /// Основной поток записи в базу данных
        /// </summary>
        public void ThreadMain()
        {
            //string[] exts = { ".txt", ".TXT", ".pdf", ".jpg", ".jp2", "Jpg", "JPG", "jpeg", "JPEG" };

            _RepLog._toFileLog(String.Format("Running in a thread name: {0} and polStart: {1} || polEnd: {2}", namethread, polstart, polend));
            _SQLQuery ins = new _SQLQuery("DBName");

            double countFile = 0;

            List<string> pVal = new List<string>();

            for (int g = polstart; g <= polend; g++)
            {
                try
                {
                    string[] poolfile = null;
                    try
                    {
                        poolfile = Directory.GetFiles(data[g], "*.*", SearchOption.AllDirectories);
                        //_FileSearch.GetFilesList(data[g]).ToArray();
                    }
                    catch (Exception ex)
                    {
                        _RepLog._toFileLog("Ошибка формирования поиска: " + ex);
                        break;
                    }
                    countFile += poolfile.Length;

                    _RepLog._toFileLog(poolfile.Length.ToString("#,##"));

                    List<ThreadPolDB> insSQL = new List<ThreadPolDB>();
                    List<Thread> nTh = new List<Thread>();

                    if (poolfile.Length < 3)
                    {
                        for (int i = 0; i < poolfile.Length; i++)
                        {
                            try
                            {
                                string _value = String.Format("'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'",    _AttribbutFile.Atribut(data[g])[0],
                                                                                                                            _AttribbutFile.Atribut(data[g])[2],
                                                                                                                            _AttribbutFile.Atribut(data[g])[2].Split('\\')[_AttribbutFile.Atribut(data[g])[2].Split('\\').Length - 2],
                                                                                                                            _AttribbutFile.Atribut(data[g])[1],
                                                                                                                            _AttribbutFile.Atribut(data[g])[3],
                                                                                                                            _AttribbutFile.Atribut(data[g])[4],
                                                                                                                            _AttribbutFile.Atribut(data[g])[5],
                                                                                                                            _AttribbutFile.Atribut(data[g])[0].Split(new char[] { '.' })[1]);
                                //_RepLog._monitor("Smale: " + _value);
                                ins._insertInline(TableName, _value);
                                //pVal.Add(_value);
                            }
                            catch (Exception ex)
                            {
                                _RepLog._toFileLog("Ошибка атрибутов в потоке: " + ex);
                                _RepLog._monitor(_AttribbutFile.Atribut(data[g])[2]);
                            }

                        }
                        //if (pVal.Count != 0)
                        //{
                           // _RepLog._monitor("" + pVal[0] + pVal[1]);
                            //ins._insertForeach(TableName, pVal);
                        //}
                    }
                    else
                    {
                        int rez = poolfile.Length / 3;
                        int rez0 = rez * 3;
                        int allrez = poolfile.Length - rez0;


                        for (int i = 0; i < 3; i++)
                        {
                            if (i == 0)
                            {
                                insSQL.Add(new ThreadPolDB("поток-" + i, TableName, poolfile, i * rez, i * rez + rez));
                                nTh.Add(new Thread(insSQL[i].ThreadInsSQL));
                                nTh[i].Start();
                            }
                            else
                            {
                                if (i == 3 - 1)
                                {
                                    insSQL.Add(new ThreadPolDB("поток-" + i, TableName, poolfile, i * rez + 1, i * rez + rez + allrez - 1));
                                    nTh.Add(new Thread(insSQL[i].ThreadInsSQL));
                                    nTh[i].Start();
                                }
                                else
                                {
                                    insSQL.Add(new ThreadPolDB("поток-" + i, TableName, poolfile, i * rez + 1, i * rez + rez));
                                    nTh.Add(new Thread(insSQL[i].ThreadInsSQL));
                                    nTh[i].Start();
                                }
                            }
                        }
                        bool threadEnd = true;
                        while (threadEnd)
                        {
                            Thread.Sleep(5000);
                            threadEnd = false;
                            for (int f = 0; f < nTh.Count; f++)
                            {
                                if (nTh[f].ThreadState == System.Threading.ThreadState.Running)
                                {
                                    threadEnd = true;
                                    break; // +1 is runnnig
                                }
                                else
                                {
                                    //_RepLog._toFileLog("DEBUG: Thread " + f.ToString() + "is NOT runnig.");
                                }
                            }
                        }
                        //_TelegramBot.TelegramSend("Таблица -" + TableName + " сформированна!");
                    }
                }
                catch (Exception ex)
                {
                    _RepLog._toFileLog("Ошибка формирования потоков: " + ex.Message);
                }
            }
            _RepLog._toFileLog(String.Format("End thread name: {0} and polStart: {1} || polEnd: {2}", namethread, polstart, polend));
        }

        public void ThreadInsSQL()
        {
            _RepLog._toFileLog(String.Format("Running in a DOWN thread name: {0} and polStart: {1} || polEnd: {2}", namethread, polstart, polend));
            _SQLQuery ins = new _SQLQuery("DBName");

            List<string> pValTh = new List<string>();

            try
            {
                for (int g = polstart; g <= polend; g++)
                {
                    try
                    {
                        string _value = String.Format("'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'",    _AttribbutFile.Atribut(data[g])[0],
                                                                                                                    _AttribbutFile.Atribut(data[g])[2],
                                                                                                                    _AttribbutFile.Atribut(data[g])[2].Split('\\')[_AttribbutFile.Atribut(data[g])[2].Split('\\').Length - 2],
                                                                                                                    _AttribbutFile.Atribut(data[g])[1],
                                                                                                                    _AttribbutFile.Atribut(data[g])[3],
                                                                                                                    _AttribbutFile.Atribut(data[g])[4],
                                                                                                                    _AttribbutFile.Atribut(data[g])[5],
                                                                                                                    _AttribbutFile.Atribut(data[g])[0].Split(new char[] { '.' })[1]);
                        //_RepLog._monitor("Big: " + _value);
                        ins._insertInline(TableName, _value);
                        //pValTh.Add(_value);
                    }
                    catch (Exception ex)
                    {
                        _RepLog._toFileLog("Ошибка атрибутов в потоке: " + ex);
                        _RepLog._monitor(_AttribbutFile.Atribut(data[g])[2]);
                    }

                }
                //if (pValTh.Count != 0)
                //{
                    //_RepLog._monitor("" + pValTh[0] + pValTh[1]);
                    //ins._insertForeach(TableName, pValTh);
                //}
                

            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Ошибка записи в базу: " + ex);
            }
            _RepLog._toFileLog(String.Format("End Down thread name: {0} and polStart: {1} || polEnd: {2}", namethread, polstart, polend));
        }

        public void ThreadMainCount()
        {
            string[] exts = { ".txt", ".pdf", ".jpg" };

            for (int g = polstart; g <= polend; g++)
            {
                try
                {
                    //Console.WriteLine(_FileSearch.GetFilesList(data[g], exts).Count.ToString("#,##"));
                }
                catch (Exception ex)
                {
                    _RepLog._toFileLog("Ошибка чтения файла: " + ex.Message);
                }
            }
        }
    }
}
