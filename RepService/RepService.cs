using RepDLL;
using System;
using System.Threading;
using System.Configuration;
using System.ServiceProcess;
using System.Collections.Specialized;

namespace RepService
{
    public partial class RepService : ServiceBase
    {
        public RepService()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = false;
        }

        protected override void OnStart(string[] args)
        {

            Thread myServThr = new Thread(StartServ);

            GRCCRepLog.WriteEntry("Writing to event log befor Start service.");

            myServThr.Start();
            myServThr.IsBackground = true;

        }
        
        protected override void OnStop()
        {
            GRCCRepLog.WriteEntry("Writing to event log befor Stop service!.");
            this.Stop();

        }

        protected override void OnPause()
        {
            _RepLog._toFileLog("Переведено в ожидание!");
            GRCCRepLog.WriteEntry("Writing to event log befor Pause service!.");
        }

        protected void StartServ()
        {
            try
            {
                NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("TCPServers");
                TCPServer _MyTCPServer = new TCPServer(section["IP"], Convert.ToInt16(section["Port"]));
                _MyTCPServer.Start();
            }
            catch (Exception ex)
            {
                _RepLog._toFileLog("Ошибка App.config - " + ex);
            }
            
            this.OnStop();
        }
    }
}
