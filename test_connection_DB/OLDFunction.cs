using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace test_connection_DB
{
    class OLDFunction
    {
        void oldFunc()
        {
            /* ======================= Form Atribut Generate DB COL=======================
            
            List<string> spisok = new List<string>();
            spisok.Add(@"w:\10\AsArchStorage\V3\ASBIBLIO_CONTENT\active");
            spisok.Add(@"r:\AsArchStorage\V3\ASBIBLIO_CONTENT4\active");
            spisok.Add(@"v:\2\AsArchStorage\V3\ASBIBLIO_CONTENT2\active");
            spisok.Add(@"p:\15\AsArchStorage\V3\ASBIBLIO_CONTENT5\active");

            foreach (string sdf in spisok)
            {
                string db = sdf.Split('\\')[sdf.Split('\\').Length - 2];
                Console.WriteLine(sdf.Split('\\').Length);
                Console.WriteLine("База данных: {0}", db);
            }

            try
            {
                string[] dirs = Directory.GetDirectories(@"s:\", "sdk-to*", SearchOption.AllDirectories);
                Console.WriteLine("The number of directories starting with p is {0}.", dirs.Length);
                foreach (string dir in dirs)
                {
                    Console.WriteLine(dir);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            */

            //=======================SEND TCP Message=========================

            //TCPClient test = new TCPClient("10.78.122.191", 8438);
            //Console.WriteLine(test.Exchange("index path4.txt siasbib 100"));

           
            //========================Thread avalable======================

            /*List<Thread> myTh = new List<Thread>();

            //myTh.Add(new Thread(Demo));
            for (int i = 0; i < 3; i++)
            {
                myTh.Add(new Thread(Demo));
            }
            foreach (Thread th in myTh)
            {
                th.Start();
            }
            //string[] ava = new string[4];
            int s = 0;
            while (true)
            {

                Console.Clear();
                Console.WriteLine(myTh.Count);
                foreach (Thread ps in myTh)
                {
                    if (!ps.IsAlive)
                    {
                        s++;
                    }
                    Console.WriteLine(ps.IsAlive);
                }
                if (s == myTh.Count)
                {
                    break;
                }
            }*/

            //=========================symbol analize======================

            //string str = "23659";
            //char[] ch = str.ToCharArray();
            //var count = ch.Where((n) => n >= '0' && n <= '9').Count();
            //Console.WriteLine("Количество цифр в строке: " + count);

            //=====================thread count============================

            /*
              List<Thread> myth = new List<Thread>();

            myth.Add(new Thread(ololo));

            for (int i = 0; i < 199; i++)
            {
                myth.Add(new Thread(ololo));
                myth[i].Name = i.ToString();
                myth[i].Start();
                myth[i].IsBackground = true;
                Console.WriteLine("Поток {0} начал работу!", myth[i].Name);
            }

            int ky = 0;
            while (ky != myth.Count())
            {
                
                foreach (Thread th in myth)
                {
                    if (th.ThreadState.ToString() == "Unstarted")
                    {
                        Console.WriteLine("Поток {0} закончил работу!", th.Name);

                        ky++;
                    }
                }
            }
            Console.WriteLine("2 - Потоки закончили работу!");
             */
            /*
            public static void ololo()
       {
           Random rnd = new Random();
           int _time = rnd.Next(100000,200000);
           Thread.Sleep(_time);

       } 
            */
            //===================info HDD==================================

            //HDDInfo dInfo = new HDDInfo("sasbibsrv01", "freespacehdd", "DBName");
            //dInfo._RecInfo();
            //Console.ReadLine();

            //=========================работа с сервисом==================================
            //static string serviceName = "Spooler";
            //static string serverName = "masbibsrv01.main.prlib.ru";

            //ServiceController service = new ServiceController(serviceName, serverName);
            //============================================================================

            //=========================Правильность путей копирования=====================
            //Console.Write("Название таблицы со сведениями о копирование: ");

            //string tbName = Console.ReadLine();

            //service.Start()
            /*
            v:\20\AsArchStorage\V3\ASBIBLIO_CONTENT\active\
            w:\AsArchStorage\V3\ASBIBLIO_CONTENT\active\
            w:\1\AsArchStorage\V3\ASBIBLIO_CONTENT\active\
            w:\2\AsArchStorage\V3\ASBIBLIO_CONTENT\active\
            w:\3\AsArchStorage\V3\ASBIBLIO_CONTENT\active\
            w:\4\AsArchStorage\V3\ASBIBLIO_CONTENT\active\
            w:\5\AsArchStorage\V3\ASBIBLIO_CONTENT\active\
            w:\6\AsArchStorage\V3\ASBIBLIO_CONTENT\active\
            w:\7\AsArchStorage\V3\ASBIBLIO_CONTENT\active\
            w:\8\AsArchStorage\V3\ASBIBLIO_CONTENT\active\
            w:\9\AsArchStorage\V3\ASBIBLIO_CONTENT\active\
            w:\10\AsArchStorage\V3\ASBIBLIO_CONTENT\active\
            w:\14\AsArchStorage\V3\ASBIBLIO_CONTENT\active\
            w:\15\AsArchStorage\V3\ASBIBLIO_CONTENT\active\
            w:\16\AsArchStorage\V3\ASBIBLIO_CONTENT\active\
            C:\gpfs\0001\ASBIBLIO_CONTENT\
            C:\gpfs\0003\ASBIBLIO_CONTENT\active\
            */


            //_ThreadTaskCopy._TaskCopy("listdb", "masbibsrv01", "disk", 9);
            //Console.WriteLine(1233434265744.ToString("#,##"));
            /*if (_SQLQuery._existsSQL(tbName) == false)
            {
                _SQLQuery._creatTable(String.Format("CREATE TABLE {0}(" +
                                                                        "ID INT NOT NULL," +
                                                                        "name VARCHAR(255) NULL," +
                                                                        "path VARCHAR(255) NULL," +
                                                                        "size VARCHAR(255) NULL," +
                                                                        "data_creat VARCHAR(255) NULL," +
                                                                        "data_access VARCHAR(255) NULL," +
                                                                        "data_write VARCHAR(255) NULL," + 
                                                                       " PRIMARY KEY(ID));", tbName), tbName);
            }
            else
            {
                Console.WriteLine("Таблица {0} уже существует!", tbName);
            }*/

            /*string[] data = { @"w:\AsArchStorage\V3\ASBIBLIO_CONTENT\active",
                              @"w:\1\AsArchStorage\V3\ASBIBLIO_CONTENT\active",
                              @"C:\gpfs\0001\ASBIBLIO_CONTENT",
                              @"C:\gpfs\0003\ASBIBLIO_CONTENT\active" };

            for (int i = 0; i < data.Length; i++)
            {
                string[] splpath = data[i].Split(new char[] { '\\' });

                if (splpath.Length == 4)
                {
                    Console.WriteLine(splpath.Length);
                    Console.WriteLine(@"AsArchStorage\V3\" + data[i].Remove(0, 13) + @"\active");
                    //targetPath = _FreeSpaceDisk(DiskFileToPath) + data[g][2].Remove(0, 3);
                }
                else if (splpath.Length == 5)
                {
                    if (splpath[1] == "gpfs")
                    {
                        Console.WriteLine(splpath.Length);
                        Console.WriteLine(@"AsArchStorage\V3\" + data[i].Remove(0, 13));
                    }
                    else
                    {
                        Console.WriteLine(splpath.Length);
                        Console.WriteLine(data[i].Remove(0, 3));
                    }
                    //targetPath = _FreeSpaceDisk(DiskFileToPath) + data[g][2].Remove(0, 3);
                }
                else if (splpath.Length == 6)
                {
                    Console.WriteLine(splpath.Length);
                    Console.WriteLine(data[i].Remove(0, 5));
                    //targetPath = _FreeSpaceDisk(DiskFileToPath) + data[g][2].Remove(0, 6);
                }
                else
                {
                    Console.WriteLine("Ошибка пути для записи!");
                    //_RepLog._toFileLog("Ошибка пути для записи!");
                    //break;
                }
            }/*
            //============================================================================

            //============================================================================
            //====================Select с выборкой столбцов из БД========================
            //============================================================================
            //List<List<string>> gf = new List<List<string>>();

            /*gf = _SQLQuery._Select("select * from masbibsrv01;");
            double d = 0;
            foreach (List<string> line in gf)
            {
                //Console.WriteLine("===================");
                int a = 0;
                foreach (string item in line)
                {
                    try
                    {
                        if (a == 2)
                        {
                            d = d + Convert.ToDouble(item);
                        }
                        //Console.WriteLine(item);

                        a++;
                    } catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
                //Console.WriteLine("===================");
            }*/
            //============================================================================
            //===========================Свободное место на дисках========================
            //============================================================================
            /*double free = 0;
            double a = 0;
            string Vol = "";
            string freVol = "";

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo MyDriveInfo in allDrives)
            {
                if (MyDriveInfo.IsReady == true)
                {
                    free = MyDriveInfo.AvailableFreeSpace;
                    a = (free / (1024 * 1024));
                    if (a >= 55000)
                    {
                        freVol += MyDriveInfo.Name + ": " + a.ToString("#.##") + "\n";
                    }
                    Vol += MyDriveInfo.Name + ": " + a.ToString("#.##") + "\n";
                    //Environment.NewLine;
                    //Console.WriteLine(Vol);
                }
            }
            Console.WriteLine("весь перечень дисков");
            Console.WriteLine(Vol);
            Console.WriteLine("подходят для записи");
            Console.WriteLine(freVol);*/

            //Console.WriteLine("Всего записей: {0}  \nОбщий объем: {1} Gb", gf.Count, d/(1024*1024*1024));

            //============================================================================
            //=========================Создать таблицу====================================
            //============================================================================
            //_SQLQuery._creatTable("CREATE TABLE test(ID INT NOT NULL,NAME VARCHAR(20), AGE INT, ADDRESS CHAR(25), SALARY DECIMAL(18, 2), PRIMARY KEY(ID));");
            //============================================================================
            //========================Отчичстить и вставить в таблицу БД==================
            //============================================================================
            /*string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=sivirt06;Initial Catalog=replicationfile;Persist Security Info=True;User ID=src;Password=1234";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                Console.WriteLine("Connection Open ! ");
                //_SQLQuery._clear("masbibsrv01");
                //_SQLQuery._insert("masbibsrv01", "100000540_doc1.jpg", @"D:\AsArchStorage\V3\ASBIBLIO_FONDCOMPLECT\active", "244563", "7/31/2019 2:58:22 PM", "2/27/2020 12:50:08 PM", "1/11/2019 3:12:00 PM");


                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection: " + ex);
            }*/
            //============================================================================
            //============================Потоковая запись в БД===========================
            //============================================================================
            /*Console.Write("Установите число потоков(2-8):");
            int myThreadsCount = Convert.ToInt32(Console.ReadLine());

            Console.Write("Укажите Таблицу для записи(masbibsrv01 или sasbibsrv01):");
            string _mTable = Console.ReadLine();

            Console.Write("Укажите имя файла с путями поиска: ");
            string path = Console.ReadLine();
            //_ThreadTask._TaskIndex(path, _mTable, myThreadsCount);

            Console.WriteLine("path {0}, table {1}, count thread {2}", path, _mTable, myThreadsCount);*/
            //============================================================================
            //============================Таймер времени выполнения=======================
            //============================================================================
            //DateTime date1 = DateTime.Now;
            //Console.WriteLine(date1.ToString("hh:mm:ss.FFFFFFF"));

            //Stopwatch stopwatch = new Stopwatch();
            //long initialMemoryFootPrint = GC.GetTotalMemory(true);
            //stopwatch.Start();

            //Console.WriteLine("Time elapsed: (ms)" + stopwatch.ElapsedMilliseconds);
            //Console.WriteLine("Memory usage: " + (GC.GetTotalMemory(false) - initialMemoryFootPrint));*/

            /*Task.Factory.StartNew(() =>
            {
                // lets simulate something that takes a while
                int k = 0;
                while (true)
                {
                    if (k++ > 100000)
                        break;
                }

                if ((iCopy + 1) % 200 == 0) // By the way, what does your sendMessage(list[0]['1']); mean? what is this '1'? if it is i you are not thread safe.
                    Console.WriteLine(iCopy + " - Time elapsed: (ms)" + stopwatch.ElapsedMilliseconds);
            });*/
            //============================================================================
            //============================================================================
            //============================================================================

            /*
                class Program
                {
                    public static int Main(String[] args)
                    {
                        //Console.WriteLine(AnalizeHDD.FreeDisk("masbib", 300000));
                        //TCPClient send = new TCPClient("sasbibsrv01", 8438);
                       //Console.WriteLine(send.Exchange("index path3.txt sasbib 15"));
                        //_Serch(@"D:\Denis\e\Android\HoffmanUtilitySpotlight");

                        List<string> Spisok = new List<string>();

                        Spisok.Add(@"'6656219_doc1.jpg', 'w:\10\AsArchStorage\V3\ASBIBLIO_CONTENT\active', '1258291', '8/2/2018 12:08:50 PM', '8/2/2018 12:08:50 PM', '3/30/2012 11:15:08 AM', 'jpg'");
                        Spisok.Add(@"'6656212_doc1.jpg', 'w:\10\AsArchStorage\V3\ASBIBLIO_CONTENT\active', '1228743', '8/2/2018 12:08:50 PM', '8/2/2018 12:08:50 PM', '3/30/2012 11:15:00 AM', 'jpg'");
                        Spisok.Add(@"'6656220_doc1.jpg', 'w:\10\AsArchStorage\V3\ASBIBLIO_CONTENT\active', '1065986', '8/2/2018 12:08:50 PM', '8/2/2018 12:08:50 PM', '3/30/2012 11:15:09 AM', 'jpg'");
                        Spisok.Add(@"'211821_doc1.jpg', 'r:\AsArchStorage\V3\ASBIBLIO_CONTENT4\active', '1917077', '7/23/2018 8:54:45 AM', '7/23/2018 8:54:45 AM', '4/13/2015 3:28:58 PM', 'jpg'");
                        Spisok.Add(@"'6423240_doc1.jpg', 'D:\AsArchStorage\V3\ASBIBLIO_CONTENT\active', '314095', '6/7/2018 12:55:43 PM', '6/7/2018 12:55:43 PM', '10/27/2011 2:56:01 PM', 'jpg'");
                        Spisok.Add(@"'238159_doc1.JPG', 'p:\3\AsArchStorage\V3\ASBIBLIO_CONTENT4\active', '2914130', '7/20/2018 3:06:40 PM', '7/20/2018 3:06:40 PM', '4/22/2016 12:53:56 PM', 'JPG'");
                        Spisok.Add(@"'6404673_doc1.jpg', 't:\AsArchStorage\V3\ASBIBLIO_CONTENT\active', '4638597', '7/25/2018 10:17:18 AM', '7/25/2018 10:17:18 AM', '10/12/2011 2:16:17 PM', 'jpg'");
                        Spisok.Add(@"'219587_doc1.jpg', 'r:\AsArchStorage\V3\ASBIBLIO_CONTENT4\active', '3681555', '7/23/2018 8:54:45 AM', '7/23/2018 8:54:45 AM', '3/6/2015 1:27:48 PM', 'jpg'");
                        Spisok.Add(@"'1008267_doc1.jpg', 'v:\2\AsArchStorage\V3\ASBIBLIO_CONTENT2\active', '1422628', '7/26/2018 10:19:57 AM', '7/26/2018 10:19:57 AM', '11/17/2014 9:43:21 AM', 'jpg'");
                        Spisok.Add(@"'219869_doc1.jpg', 'r:\AsArchStorage\V3\ASBIBLIO_CONTENT4\active', '950240', '7/23/2018 8:54:54 AM', '7/23/2018 8:54:54 AM', '3/6/2015 1:59:23 PM', 'jpg'");
                        Spisok.Add(@"'188058_doc1.jpg', 'r:\2\AsArchStorage\V3\ASBIBLIO_CONTENT5\active', '25113509', '7/23/2018 2:11:20 PM', '7/23/2018 2:11:20 PM', '9/1/2016 3:24:10 PM', 'jpg'");
                        Spisok.Add(@"'1078201_doc1.jpg', 'D:\AsArchStorage\V3\ASBIBLIO_CONTENT\active', '393163', '6/7/2018 12:55:27 PM', '6/7/2018 12:55:27 PM', '8/31/2009 11:40:58 AM', 'jpg'");
                        Spisok.Add(@"'6656222_doc1.jpg', 'w:\10\AsArchStorage\V3\ASBIBLIO_CONTENT\active', '216408', '8/2/2018 12:08:50 PM', '8/2/2018 12:08:50 PM', '3/30/2012 11:15:10 AM', 'jpg'");
                        Spisok.Add(@"'6423245_doc1.jpg', 'D:\AsArchStorage\V3\ASBIBLIO_CONTENT\active', '2705636', '6/7/2018 12:55:43 PM', '6/7/2018 12:55:43 PM', '10/27/2011 2:56:07 PM', 'jpg'");
                        Spisok.Add(@"'5535785_doc1.jpg', 't:\9\AsArchStorage\V3\ASBIBLIO_CONTENT\active', '897369', '7/25/2018 2:58:33 PM', '7/25/2018 2:58:33 PM', '4/2/2009 2:45:52 AM', 'jpg'");
                        Spisok.Add(@"'1008492_doc1.jpg', 'v:\2\AsArchStorage\V3\ASBIBLIO_CONTENT2\active', '1292958', '7/26/2018 10:20:11 AM', '7/26/2018 10:20:11 AM', '11/17/2014 9:44:51 AM', 'jpg'");
                        Spisok.Add(@"'214745_doc1.jpg', 'I:\AsArchStorage\V3\ASBIBLIO_CONTENT4\active', '1294032', '6/7/2018 2:07:57 PM', '6/7/2018 2:07:57 PM', '1/20/2014 10:29:14 AM', 'jpg'");
                        Spisok.Add(@"'219591_doc1.jpg', 'r:\AsArchStorage\V3\ASBIBLIO_CONTENT4\active', '1483511', '7/23/2018 8:54:46 AM', '7/23/2018 8:54:46 AM', '3/6/2015 1:28:25 PM', 'jpg'");
                        Spisok.Add(@"'159521_doc1.jpg', 'v:\5\AsArchStorage\V3\ASBIBLIO_CONTENT4\active', '6998126', '7/26/2018 10:17:13 AM', '7/26/2018 10:17:13 AM', '3/27/2013 2:14:04 PM', 'jpg'");
                        Spisok.Add(@"'1007549_doc1.jpg', 'p:\6\AsArchStorage\V3\ASBIBLIO_CONTENT2\active', '1626447', '7/21/2018 4:24:38 PM', '7/21/2018 4:24:38 PM', '11/13/2014 2:57:46 PM', 'jpg'");
                        Spisok.Add(@"'191547_doc1.jpg', 'p:\15\AsArchStorage\V3\ASBIBLIO_CONTENT5\active', '4612957', '7/22/2018 8:58:37 PM', '7/22/2018 8:58:37 PM', '8/31/2016 3:11:35 PM', 'jpg'");

                        //string sdf = @"w:\10\AsArchStorage\V3\ASBIBLIO_CONTENT\active";
                        //string sdf1 = @"D:\AsArchStorage\V3\ASBIBLIO_CONTENT\active";
                        //_Analase("sasbib_all", Spisok, 6);
                        //Console.WriteLine(sdf.Split('\\')[4]);
                        //Console.WriteLine(sdf1.Split('\\')[3]);
                        Stopwatch stopwatch = new Stopwatch();

                        // Begin timing
                        stopwatch.Start();

                        _FindFile();

                        stopwatch.Stop();

                        // Write result
                        Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);

                        Console.ReadLine();
                        return 0;
                    }
                    /// <summary>
                    /// Функция анализа и записи в таблицу данных
                    /// </summary>
                    /// <param name="pTable">Имя таблицы</param>
                    /// <param name="Args">Список записей для бвзы данных</param>
                    /// <param name="limit">Ограничение для записи в базу данных</param>
                    public static void _Analase(string pTable, List<string> Args, int limit = 990)
                    {
                        string sql = "";
                        string pTempValue = null;
                        int count = 0;

                        foreach (string s in Args)
                        {

                            if (count >= limit)
                            {
                                pTempValue += String.Format("({0});", s);
                                sql = String.Format("INSERT INTO {0} VALUES {1}", pTable, pTempValue);
                                Console.WriteLine("========================================");
                                Console.WriteLine(sql);
                                pTempValue = "";
                                count = 0;
                            }
                            else
                            {
                                pTempValue += String.Format("({0}),", s);
                                count++;
                            }
                        }
                        if (pTempValue != "")
                        {
                            sql = String.Format("INSERT INTO {0} VALUES {1}", pTable, pTempValue.Remove(pTempValue.Length-1,1) + ";");
                            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
                            Console.WriteLine(sql);
                        }
                    }

                    public static void _FindFile()
                    {
                        string sourceDirectory = @"D:\Denis\e";
                        //string archiveDirectory = @"C:\archive";
                        List<string> mFormat = new List<string>();
                        int count = 0;
                        try
                        {
                            var txtFiles = Directory.EnumerateFiles(sourceDirectory, "*.*", SearchOption.AllDirectories);
                
                            foreach (string currentFile in txtFiles)
                            {
                                string fileName = currentFile.Substring(sourceDirectory.Length + 1);
                                string format = Path.GetExtension(fileName);

                                if (mFormat.Count == 0)
                                {
                                    mFormat.Add(format);
                                }
                                if (!mFormat.Exists(x => x == format))
                                {
                                    mFormat.Add(format);
                                }
                    
                                Console.WriteLine(fileName);
                                count++;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        Console.WriteLine("========End Process!=======");
                        Console.WriteLine("Всего найдено: {0} файлов", count);
                        int formats = 0;
                        foreach (string data in mFormat)
                        {
                            formats++;
                            //Console.WriteLine(data);
                        }
                        Console.WriteLine("========End Process!=======");
                        Console.WriteLine("Всего найдено форматов: {0}", formats);
                    }
                }

                public class Watcher
                {
                    public static string[] argsss = null;
                    public static void _Main()
                    {
                        string[] liness = File.ReadAllLines("test.txt");
                        foreach (var dirName in liness)
                        {
                            Console.WriteLine("На проверке папка: {0}", dirName);
                            var t = Task.Run(() => Wattcher.Runner(dirName));
                            //t.Wait();
                        }
                        while (true)
                        {
                            Console.Write("Закрыть программу?(yes/no):");
                            string request = Console.ReadLine();
                            if (request == "no")
                            {
                                Console.WriteLine("Программа работает дальше!");
                            }
                            else
                            {
                                break;
                            }
                        }
                        Environment.Exit(0);

                    }
                }
             */
        }
    }
}
