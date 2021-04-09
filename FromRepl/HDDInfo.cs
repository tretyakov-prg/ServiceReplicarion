using System;
using System.IO;
using System.Management;

namespace RepDLL
{
   
    public class HDDInfo
    {
        public string NamePC = null;
        public string TableName = null;
        public string DBConnection = null;

        public HDDInfo(string NamePC, string TableName, string DBConnection)
        {
            this.NamePC = NamePC;
            this.TableName = TableName;
            this.DBConnection = DBConnection;
        }
        public void _RecInfo()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            _SQLQuery hddins = new _SQLQuery(DBConnection);
            hddins._clear(TableName);
            long mb = 1024 * 1024;

            var applicationDirectory = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            const string fileNameToOpen = "disk.txt";
            var path = Path.Combine(applicationDirectory, fileNameToOpen);

            //String path = @"c:\tmp\disk.txt";

            FileInfo file = new FileInfo(path);
            if (!file.Exists)
            {
                using (FileStream stream = file.Create())
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        foreach (DriveInfo d in allDrives)
                        {
                            try
                            {
                                if (d.IsReady == true)
                                {
                                    writer.WriteLine(d.Name);
                                    hddins._inserthdd(TableName, d.Name, d.DriveType.ToString(), d.VolumeLabel, d.DriveFormat, d.AvailableFreeSpace / mb, d.TotalFreeSpace / mb, d.TotalSize / mb, NamePC);
                                }
                            }
                            catch (Exception ex)
                            {
                                _RepLog._toFileLog("Ошибка обращения к диску: " + ex.Message);
                            }
                        }
                    }
                }
            }
            else
            {
                using (FileStream stream = new FileStream(path, FileMode.Truncate))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        foreach (DriveInfo d in allDrives)
                        {
                            try
                            {
                                if (d.IsReady == true)
                                {
                                    writer.WriteLine(d.Name);
                                    hddins._inserthdd(TableName, d.Name, d.DriveType.ToString(), d.VolumeLabel, d.DriveFormat, d.AvailableFreeSpace / mb, d.TotalFreeSpace / mb, d.TotalSize / mb, NamePC);
                                }
                            }
                            catch (Exception ex)
                            {
                                _RepLog._toFileLog("Ошибка обращения к диску: " + ex.Message);
                            }
                        }
                    }
                }
            }
        }

        public void GetHardwareInfo()
        {
            _SQLQuery hddins = new _SQLQuery(DBConnection);
            hddins._clear(TableName);

            long mb = 1024 * 1024;

            string[] ClassItemField = new string[] { "Name", "FreeSpace", "Capacity", "FileSystem", "Caption" };

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Volume");

            try
            {
                var applicationDirectory = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
                const string fileNameToOpen = "disk.txt";
                var path = Path.Combine(applicationDirectory, fileNameToOpen);
                FileStream stream2 = null;
                FileInfo file = new FileInfo(path);

                if (!file.Exists) stream2 = file.Create(); else stream2 = new FileStream(path, FileMode.Truncate);

                using (FileStream stream = stream2)
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        foreach (ManagementBaseObject obj in searcher.Get())
                        {
                            try
                            {
                                if (obj[ClassItemField[1]] != null)
                                {
                                    Int64 used = Int64.Parse(obj[ClassItemField[2]].ToString().Trim()) / mb - Int64.Parse(obj[ClassItemField[1]].ToString().Trim()) / mb;

                                    writer.WriteLine(obj[ClassItemField[0]].ToString().Trim());

                                    hddins._inserthdd(TableName,
                                        obj[ClassItemField[0]].ToString().Trim(),
                                        "",
                                        obj[ClassItemField[4]].ToString().Trim(),
                                        obj[ClassItemField[3]].ToString().Trim(),
                                        used,
                                        Int64.Parse(obj[ClassItemField[1]].ToString().Trim()) / mb,
                                        Int64.Parse(obj[ClassItemField[2]].ToString().Trim()) / mb,
                                        NamePC);
                                }
                            } catch (Exception ex)
                            {
                                _RepLog._toFileLog("Ошибка обращения к диску: " + ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Ошибка общего потока данных: " + ex.Message);
            }
        }
    }
}
