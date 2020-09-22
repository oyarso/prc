using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Timers;
using System.Configuration;
using System.Collections.Specialized;



namespace PPValidaWindowsService
{
    public partial class PPVWindowsService : ServiceBase
    {
        public PPVWindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string Ruta;
            Ruta = ConfigurationManager.AppSettings.Get("Pathin");
            EventLog.WriteEntry("PPValida Windows Service se esta iniciando", EventLogEntryType.Information);
            FileSystemWatcher observador = new FileSystemWatcher(Ruta);
            observador.NotifyFilter = (
            NotifyFilters.LastAccess |
            NotifyFilters.LastWrite |
            NotifyFilters.FileName |
            NotifyFilters.DirectoryName);

            observador.Filter = "*.txt";
            try
            {
                observador.Created += filecreate.AlCambiar;
                //observador.Error += filecreate.AlOcurrirUnError;
                //observador.Changed += filecreate.AlCambiar;
                //observador.Deleted += filecreate.AlCambiar;
                //observador.Renamed += filecreate.AlRenombrar;
                observador.EnableRaisingEvents = true;
            }
            catch
            {
                ServiceController sc = new ServiceController();
                sc.ServiceName = "PPVWindowsService";

                if (sc.Status == ServiceControllerStatus.Stopped)
                {
                    // Start the service if the current status is stopped.

                    try
                    {
                        // Start the service, and wait until its status is "Running".
                        sc.Start();
                        sc.WaitForStatus(ServiceControllerStatus.Running);

                        // Display the current service status.
                    }
                    catch (InvalidOperationException)
                    {

                    }
                }
            }
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("PPValida Windows Service se esta deteniendo", EventLogEntryType.Information);
        }
    }


}

