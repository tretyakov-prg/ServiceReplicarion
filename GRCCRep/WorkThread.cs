using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GRCCRep
{
    public class WorkThread
    {
        public WorkThread()
        {

        }

        public void WorkAllTread()
        {
            int index = 0;
            List<Thread> threads = new List<Thread>();
            Thread tr = null;

            for (int i = 0; i < 100; i++)
            {
                InumFilePath obj = new InumFilePath("test: " + i);

                tr = new Thread(obj.ThreadMain);

                tr.Name = "SID:" + i;
                tr.Start();
                threads.Add(tr);
            }

            while (index != threads.Count)
            {
                index = 0;

                for (int j = 0; j < threads.Count; j++)
                {
                    if (threads[j].ThreadState == System.Threading.ThreadState.Stopped)
                    {
                        index++;
                    }
                }

                Console.WriteLine(index);
            }
        }
    }
}
