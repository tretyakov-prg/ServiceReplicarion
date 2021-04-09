using System;
using System.IO;

namespace RepDLL
{
    public class _AttribbutFile
    {
        public static string[] Atribut(string path)
        {
            string[] parm = new string[6];
            try
            {
                FileInfo getfil = new FileInfo(path);

                //Get name file.[0]
                parm[0] = getfil.Name;

                //Get file size.[1]
                parm[1] = (getfil.Length).ToString();

                // Get directory file.[2]
                parm[2] = getfil.DirectoryName;

                //Get cteat file.[3]
                parm[3] = (getfil.CreationTime).ToString();

                //Get lastAccess file.[4]
                parm[4] = (getfil.LastAccessTime).ToString();

                //Get LastWrite file.[5]
                parm[5] = (getfil.LastWriteTime).ToString();
            }
            catch (Exception ex)
            {
                _RepLog._monitor("Attribut error: " + ex.ToString());
                _RepLog._monitor("Attribut path: " + path);
                string[] _param = { " ", " ", " ", " ", " ", " ", " " };
                return parm = _param;
            }

            return parm;
        }
    }
}
