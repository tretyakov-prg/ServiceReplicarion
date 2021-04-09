using System;
using System.Collections.Generic;
using System.IO;

namespace RepDLL
{
    public class _FileSearch
    {
        public static List<string> GetFilesList(string path, string[] extensions)
        {
            List<string> fileList = new List<string>();
            try
            {
                foreach (string fileName in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
                {
                    string ext = Path.GetExtension(fileName);
                    if (Array.IndexOf(extensions, ext) >= 0)
                    {
                        fileList.Add(fileName);
                    }
                }
            } catch (Exception ex)
            {
                _RepLog._toFileLog("Ошибка поиска: " + ex.Message);
            }
            return fileList;
        }
        public static List<string> GetFilesList(string path)
        {
            List<string> fileList = new List<string>();
            try
            {
                foreach (string fileName in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
                {
                    fileList.Add(fileName);
                }
            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Ошибка поиска: " + ex.Message);
            }
            return fileList;
        }
        public static string[] GetFilesListAll(string path)
        {
            List<string> fileList = new List<string>();
            try
            {
                return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Ошибка поиска: " + ex.Message);
                return new string[0];
            }
     
        }

    }
}
