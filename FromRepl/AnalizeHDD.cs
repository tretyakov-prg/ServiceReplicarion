using System;
using System.Collections.Generic;
using System.IO;
using System.Management;

namespace RepDLL
{
    public static class AnalizeHDD
    {
        public static string FreeDisk(string HDDTable, long limit = 2199020)
        {
            var applicationDirectory = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);

            var path_size = Path.Combine(applicationDirectory, "sizeLimit.txt");

            if (File.Exists(path_size))
            {
                string readSizeHDD = File.ReadAllText(path_size);
                limit = Convert.ToInt64(readSizeHDD);
            }

            _SQLQuery fdisk = new _SQLQuery("DBName");
            string logicDisk = "";
            //_RepLog._toFileLog(String.Format("SELECT drive,totalave FROM {0}", HDDTable));
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Volume");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    foreach (List<string> tbHdd in fdisk._Select(String.Format("SELECT drive,totalave FROM {0}", HDDTable)))
                    {
                        if (tbHdd[0] == queryObj["Caption"].ToString())
                        {
                            if (Convert.ToInt64(queryObj["FreeSpace"]) >= limit)
                            {
                                logicDisk = tbHdd[0];
                                break;
                            }
                        }
                    }
                }
            }
            catch (ManagementException e)
            {
                _RepLog._toFileLog("An error occurred while querying for WMI data: " + e.Message);
            }
            return logicDisk;
        }
        
        /// <summary>
        /// Функция для поиска свободного места на дисках
        /// </summary>
        /// <param name="limit">Лимит места 2Tb</param>
        /// <returns></returns>
        public static string FreeSpaceDisk(string fileNameToOpen, long limit = 2199023255552)
        {
            var applicationDirectory = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);

            var path = Path.Combine(applicationDirectory, fileNameToOpen);

            var path_size = Path.Combine(applicationDirectory, "sizeLimit.txt");

            if (File.Exists(path_size))
            {
                string readSizeHDD = File.ReadAllText(path_size);
                limit = Convert.ToInt64(readSizeHDD);
            }
            
            string[] allDisk = File.ReadAllLines(path);

            string namedisk = "";

            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Volume");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    foreach (string disk in allDisk)
                    {
                        if (disk == queryObj["Caption"].ToString())
                        {
                            if (Convert.ToInt64(queryObj["FreeSpace"]) >= limit)
                            {
                                namedisk = queryObj["Caption"].ToString();
                                break;
                            }
                        }
                    }
                }
            }
            catch (ManagementException e)
            {
                _RepLog._toFileLog("An error occurred while querying for WMI data: " + e.Message);
            }
            return namedisk;
        }
    }
}
