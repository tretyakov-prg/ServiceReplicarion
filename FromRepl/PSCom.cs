﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RepDLL
{
    class PSCom
    {
        public void PSComands()
        {
            //Open up PowerShell with no window
            Process ps = new Process();
            ProcessStartInfo psinfo = new ProcessStartInfo();
            psinfo.FileName = "powershell.exe";
            psinfo.WindowStyle = ProcessWindowStyle.Hidden;
            psinfo.UseShellExecute = false;
            psinfo.RedirectStandardInput = true;
            psinfo.RedirectStandardOutput = true;
            ps.StartInfo = psinfo;
            ps.Start();
            //Done with that.

            //Run the command.
            ps.StandardInput.WriteLine("Get-Process");
            ps.StandardInput.Flush();
            ps.StandardInput.Close();
            ps.WaitForExit();
            //Done running it.

            //Write it to the console.
            Console.WriteLine(ps.StandardOutput.ReadToEnd());
            //Done with everything.

            //Wait for the user to press any key.
            Console.ReadKey(true);
        }
    }
}
