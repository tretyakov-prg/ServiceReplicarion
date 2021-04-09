using System;
using System.IO;
using System.Threading;

namespace RepDLL
{
    public class ThreadPoolIndex
    {
        public int MaxThread;
        public ThreadPoolIndex(int MaxThreades)
        {
            this.MaxThread = MaxThreades;
        }
        public ThreadPoolIndex()
        {
            this.MaxThread = 50;
        }
        /// <summary>
        /// Потоковая индексация на основе пулов
        /// </summary>
        /// <param name="myPath">Путь к файлу содеращий список проверяемых папок</param>
        /// <param name="myTableDB">Таблица хранения индексированных путей</param>
        /// <param name="MaxThread">Максимально колличество одновременных потоков</param>
        public void ThreadPoolIndexDirectory(string myPath, string myTableDB)
        {
            _SQLQuery ins = new _SQLQuery("DBName");

            if (ins._existsSQL(myTableDB) == false)
            {
                ins._creatTable(String.Format("CREATE TABLE {0}(id INT IDENTITY(1,1) NOT NULL,name VARCHAR(255) NULL,path VARCHAR(255) NULL,dbname VARCHAR(255) NULL,size VARCHAR(255) NULL,data_creat VARCHAR(255) NULL,data_access VARCHAR(255) NULL,data_write VARCHAR(255) NULL, format VARCHAR(255) NULL, PRIMARY KEY(id));", myTableDB), myTableDB);
                _RepLog._toFileLog("Таблица " + myTableDB + " созданна");
            }
            else
            {
                ins._clear(myTableDB);
            }

            var applicationDirectory = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            var path = Path.Combine(applicationDirectory, myPath);
            foreach (string pol in File.ReadAllLines(path))
            {
                ThreadPoolIndexEnum polfile = new ThreadPoolIndexEnum(pol, myTableDB);
                ThreadPool.SetMaxThreads(MaxThread, MaxThread);
                ThreadPool.QueueUserWorkItem(polfile.IndexFileDirectory);
            }
        }
    }
}
