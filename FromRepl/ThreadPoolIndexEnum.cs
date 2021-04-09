using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace RepDLL
{
    class ThreadPoolIndexEnum
    {
        string Directory_index;
        string TableName_index;
        public ThreadPoolIndexEnum(string Directory_index, string TableName_index)
        {
            //ins = new _SQLQuery("DBName");

            this.Directory_index = Directory_index;
            this.TableName_index = TableName_index;
        }

        /// <summary>
        /// поток индексации папки
        /// </summary>
        /// <param name="Directory_index_obj">Формируемый объект</param>
        public void IndexFileDirectory(object Directory_index_obj)
        {
            _SQLQuery ins = new _SQLQuery("DBName");

            try
            {
                IEnumerable<string> allfiles = Directory.EnumerateFiles(Directory_index);

                foreach (string _filename in allfiles)
                {
                    string[] att = _AttribbutFile.Atribut(_filename);
                    string DBName = "";

                    if (att[2].Split('\\').Length == 4)
                    {
                        DBName = att[2].Split('\\')[att[2].Split('\\').Length - 1];
                    }
                    else
                    {
                        DBName = att[2].Split('\\')[att[2].Split('\\').Length - 2];
                    }
                    
                    string _value = String.Format("'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'", att[0], att[2], DBName , att[1], att[3], att[4], att[5], att[0].Split(new char[] { '.' })[1]);

                    ins._insertInline(TableName_index, _value);
                }              

            } catch (Exception ex)
            {
                _RepLog._toFileLog("Путь не найден: " + Directory_index);
            }
        }
    }
}
