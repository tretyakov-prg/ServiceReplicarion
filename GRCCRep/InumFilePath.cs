using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GRCCRep
{
    public class InumFilePath
    {
        private string data;

        public InumFilePath(string data)
        {
            this.data = data;
        }

        public void ThreadMain()
        {
            Console.WriteLine("Running in a thread, data: {0}", data);
            Random random = new Random();
            int  rx = random.Next(2000, 5000);
            Thread.Sleep(rx);
            Console.WriteLine("End in a thread, data: {0}", data);
        }
    }
}
