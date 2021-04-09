using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Common;


namespace RepDLL
{
    public class _SQLQuery
    {
        public string ConDBName;
        /// <summary>
        /// Подключение из конфигурационнго файла
        /// </summary>
        /// <param name="ConDBName">Имя соединения из конфигурационнго файла .config</param>
        public _SQLQuery(string ConDBName)
        {
            this.ConDBName = ConDBName;
        }
        public List<List<string>> _Select(string mQuety, bool OneQuery = false)
        {
            List<List<string>> myList = null;
            // Получить объект Connection подключенный к DB.
            _DBConfig db = new _DBConfig(ConDBName);

            SqlConnection conn = db.GetDBConnection();

            conn.Open();
            try
            {
                // Создать объект Command.
                SqlCommand cmd = new SqlCommand();

                // Сочетать Command с Connection.
                cmd.Connection = conn;
                cmd.CommandText = mQuety;

                if (OneQuery == true)
                {
                    string name = cmd.ExecuteScalar().ToString();
                }
                else
                {
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            myList = new List<List<string>>();

                            while (reader.Read())
                            {
                                string[] fr = new string[reader.FieldCount];
                                List<string> sublist = new List<string>();

                                for (int r = 0; r < reader.FieldCount; r++)
                                {
                                    sublist.Add(reader.GetValue(r).ToString());
                                }
                                myList.Add(sublist);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _RepLog._toFileLog("Error SQL Select List<List<string>>: " + e.Message);
                //_RepLog._toFileLog(e.StackTrace);
            }
            finally
            {
                // Закрыть соединение.
                conn.Close();
                // Разрушить объект, освободить ресурс.
                conn.Dispose();
            }
            return myList;
        }

        public void _insert(string pTable, string pName, string pPath, string pSize, string pDataC, string pDataA, string pDataW)
        {
            _DBConfig db = new _DBConfig(ConDBName);

            SqlConnection conn = db.GetDBConnection();

            conn.Open();
            // Команда Insert.
            string sql = String.Format("Insert into {0} (name, path, size, data_creat, data_access, data_write) values ('{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", pTable, pName, pPath, pSize, pDataC, pDataA, pDataW);
            try
            {
                
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Ошибка записи в базу ins: " + ex.Message);
                //_RepLog._toFileLog(sql);
                //_RepLog._toFileLog(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        public void _insertForeachLimit(string pTable, List<string> ppValue)
        {
            _DBConfig db = new _DBConfig(ConDBName);

            SqlConnection conn = db.GetDBConnection();

            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandTimeout = 360;

            string sql = "";
            string pTempValue = null;

            try
            {
                foreach (string param in ppValue)
                {
                    pTempValue += String.Format("({0}),", param);
                }

                sql = String.Format("INSERT INTO {0} VALUES {1}", pTable, pTempValue.Remove(pTempValue.Length - 1, 1) + ";");
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

            } catch (Exception ex)
            {
                _RepLog._toFileLog("Ошибка записи в базу insertForeach: " + ex);
                _RepLog._monitor("Для дополнения: " + sql);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        public void _insertForeach(string pTable, List<string> ppValue)
        {
            _DBConfig db = new _DBConfig(ConDBName);

            SqlConnection conn = db.GetDBConnection();

            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandTimeout = 360;
            try
            {
                string sql = "";
                string pTempValue = null;
                int count = 0;

                foreach (string s in ppValue)
                {
                    if (count == 900)
                    {
                        pTempValue += String.Format("({0});", s);
                        sql = String.Format("INSERT INTO {0} VALUES {1}", pTable, pTempValue);
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
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
                    sql = String.Format("INSERT INTO {0} VALUES {1}", pTable, pTempValue.Remove(pTempValue.Length - 1, 1) + ";");
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Ошибка записи в базу insertForeach: " + ex);

            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        public void _insertall(string pTable, string pName, string pPath, string pSize, string pDataC, string pDataA, string pDataW, string pFormat)
        {
            _DBConfig db = new _DBConfig(ConDBName);

            SqlConnection conn = db.GetDBConnection();

            conn.Open();
            // Команда Insert.
            string sql = String.Format("Insert into {0} (name, path, size, data_creat, data_access, data_write, format) values ('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')", pTable, pName, pPath, pSize, pDataC, pDataA, pDataW, pFormat);
            try
            {

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Ошибка записи в базу insall: " + ex.Message);
                //_RepLog._toFileLog(sql);
                //_RepLog._toFileLog(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        public void _insertInline(string pTable, string Params)
        {
            _DBConfig db = new _DBConfig(ConDBName);

            SqlConnection conn = db.GetDBConnection();

            conn.Open();
            // Команда Insert.
            string sql = String.Format("Insert into {0} (name, path, dbname, size, data_creat, data_access, data_write, format) values ({1});", pTable, Params);
            //_RepLog._monitor(sql);
            try
            {

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Ошибка записи в базу insall: " + ex);
                //_RepLog._toFileLog(sql);
                //_RepLog._toFileLog(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        public void _inserthdd(string pTable, string pDrive, string pType, string pLabel, string pFSys, long pAvUser, long pTotalAve, long TotalSize, string pNServer)
        {
            _DBConfig db = new _DBConfig(ConDBName);

            SqlConnection conn = db.GetDBConnection();

            conn.Open();

            string sql = String.Format("Insert into {0} (drive, type, vlabel, fsystem, aveuser, totalave, totalsize, nserver) values('{1}','{2}','{3}','{4}',{5},{6},{7},'{8}')",
                                       pTable, pDrive, pType, pLabel, pFSys, pAvUser, pTotalAve, TotalSize, pNServer);
            try
            {

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                int rowCount = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Ошибка записи в базу insHDD: " + ex.Message);
                //_RepLog._toFileLog(sql);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        public void _clear(string pTable)
        {
            _DBConfig db = new _DBConfig(ConDBName);

            SqlConnection conn = db.GetDBConnection();

            conn.Open();
            try
            {
                string sql_clear = String.Format("TRUNCATE TABLE {0}", pTable);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql_clear;
                int rowCount = cmd.ExecuteNonQuery();
                _RepLog._toFileLog("clear table: " + pTable);
            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Error clear table: " + ex.Message);
                //_RepLog._toFileLog(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        public void _update(int pID, string pData, string pRepl)
        {
            _DBConfig db = new _DBConfig(ConDBName);

            SqlConnection conn = db.GetDBConnection();

            conn.Open();
            try
            {
                string sql = String.Format("Update DBFiles set data = '{1}', repl = '{2}' where id = {0}", pID, pData, pRepl);

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = sql;

                // Выполнить Command (Используется для delete, insert, update).
                int rowCount = cmd.ExecuteNonQuery();

                //_RepLog._toFileLog("Row Count affected = " + rowCount);
            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Error update Table: " + ex.Message);
                //_RepLog._toFileLog(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        public void _delete(string pTable, int pID)
        {
            _DBConfig db = new _DBConfig(ConDBName);

            SqlConnection conn = db.GetDBConnection();

            conn.Open();
            try
            {
                string sql = String.Format("Delete from {0} where id = {1} ", pTable, pID);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandTimeout = 360;

                cmd.Connection = conn;
                cmd.CommandText = sql;

                cmd.ExecuteNonQuery();

                _RepLog._toFileLog(String.Format("Delete Row from Record ID - {0}", pID));
            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Error delet record: " + ex.Message);
                //_RepLog._toFileLog(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        public void _deleteForeach(string pTable, List<int> ppID)
        {
            _DBConfig db = new _DBConfig(ConDBName);

            SqlConnection conn = db.GetDBConnection();

            conn.Open();

            SqlCommand cmd = new SqlCommand();
            try
            {
                string pTempValueID = null;
                cmd.CommandTimeout = 360;
                cmd.Connection = conn;

                for (int i = 0; i < ppID.Count; i++)
                {
                    if (i == ppID.Count - 1)
                    {
                        pTempValueID +=  ppID[i];
                    }
                    else
                    {
                        pTempValueID += ppID[i] + ",";
                    }
                }
                string sql = String.Format(@"DELETE FROM {0} WHERE id IN ({1});", pTable, pTempValueID);

                cmd.CommandText = sql;

                cmd.ExecuteNonQuery();

                _RepLog._toFileLog(String.Format("Удаление {0} строк : ({1})", ppID.Count, pTempValueID));
            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Ошибка удаления записей: " + ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        public void _creatTable(string _qeryCreat, string tbName)
        {
            _DBConfig db = new _DBConfig(ConDBName);

            SqlConnection conn = db.GetDBConnection();

            conn.Open();
            try
            {
                //string sql = String.Format("CREATE TABLE @_nameTable(ID INT NOT NULL,NAME VARCHAR(20) NOT NULL, AGE INT NOT NULL, ADDRESS CHAR(25), SALARY DECIMAL(18, 2), PRIMARY KEY(ID));");

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = _qeryCreat;
                cmd.ExecuteNonQuery();
                // Выполнить Command (Используется для delete, insert, update).
                //int rowCount = cmd.ExecuteNonQuery();

                _RepLog._toFileLog(String.Format("Таблица {0} создана!", tbName));
            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Error creat Table: " + ex.Message);
                //_RepLog._toFileLog(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        public void _dropTable(string _dropTabl)
        {
            _DBConfig db = new _DBConfig(ConDBName);

            SqlConnection conn = db.GetDBConnection();

            conn.Open();
            try
            {
                string sql = String.Format("DROP TABLE {0};", _dropTabl);

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                _RepLog._toFileLog("Table " + _dropTabl + "is Drop!");
            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Error drop Table: " + ex.Message);
                //_RepLog._toFileLog(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        public bool _existsSQL(String table)
        {

            bool ExistTable = false;
            try
            {
                foreach (List<string> df in _Select("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' "))
                {
                    foreach (string dk in df)
                    {
                        if (table == dk)
                        {
                            ExistTable = true;
                        }
                    }
                }

                //string sql = String.Format("SELECT * FROM {0}", table);
                //SqlCommand cmd = new SqlCommand();

                //cmd.Connection = conn;
                //cmd.CommandText = sql;
                //cmd.ExecuteNonQuery();

                return ExistTable;
            }
            catch (Exception e)
            {
                _RepLog._toFileLog("Error exists query: " + e.Message);
                return ExistTable;
            }
        }
    }
}
