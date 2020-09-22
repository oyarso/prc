using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ServiceProcess;
using FileHelpers;
using System.Configuration;
using System.Collections.Specialized;


namespace PPValidaWindowsService
{
    public class filecreate
    {
        public static void AlCambiar(object source, FileSystemEventArgs e)
        {
            //WatcherChangeTypes tipoDeCambio = e.ChangeType;
            //string path = @"C:\home\global\lonporta\log.txt";
            //if (!File.Exists(path))
            //{
            //    File.Create(path);
            //    TextWriter tw = new StreamWriter(path);
            //    tw.WriteLine("El archivo " + e.FullPath.ToString() + " tuvo un cambio de: " + tipoDeCambio.ToString() + ". ");
            //    tw.Close();
            //}
            //else if (File.Exists(path))
            //{
            //using (var tw = new StreamWriter(path, true))
            //{
            //    tw.WriteLine("El archivo " + e.FullPath.ToString() + " tuvo un cambio de: " + tipoDeCambio.ToString() + ". ");
            //    tw.Close();
            //}
            //Copiar archivo
            string Ruta;
            Ruta = ConfigurationManager.AppSettings.Get("Pathout");
            string fileToCopy = e.FullPath.ToString();
                string destinationDirectory = Ruta;
                try
                {
                    var engine = new FileHelperEngine<Documents>();
                    var records = engine.ReadFile(e.FullPath);
                    //string path2 = @"C:\home\global\lonporta\logout.txt";
                    /// Para escribir usamos:
                    //using (var tw = new StreamWriter(path2, true))
                    //{
                    //    tw.WriteLine(records[0].tipoDoc);
                    //    tw.WriteLine(records[2].tipoDoc);
                    //    tw.WriteLine(records[3].tipoDoc);
                    //}    && Documents.EsFecha(records[3].tipoDoc)
                    if (Documents.EsDoc(records[0].tipoDoc) && Documents.EsPrint(records[2].tipoDoc) && Documents.EsFecha(records[3].tipoDoc))
                    {
                        try
                        {
                            File.Copy(fileToCopy, destinationDirectory + Path.GetFileName(fileToCopy));
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
                //fin de copiar archivo
                ////inicio leer archivo plano
                //string[] lines = System.IO.File.ReadAllLines(@"C:\home\global\lonporta\" + Path.GetFileName(e.FullPath));
                //String TipoDeDocumento = lines[1];
                //String TipoDeImpresora = lines[3];
                //String Fecha = lines[4];
                //using (var tw = new StreamWriter(path, true))
                //{
                //    tw.WriteLine("Documento: " + TipoDeDocumento + " Printer: " + TipoDeImpresora + " Fecha: " + Fecha + ".");
                //    tw.Close();
                //}
                //fin leer archivo plano
            }
            //fin de crear un archivo .txt si no existe, y si agrega una nueva línea
        //}
        //public static void AlOcurrirUnError(object source, ErrorEventArgs e)
        //{
        //    string path = @"C:\home\global\lonporta\log.txt";
        //    if (!File.Exists(path))
        //    {
        //        File.Create(path);
        //        TextWriter tw = new StreamWriter(path);
        //        tw.WriteLine("Error: " + e.GetException().Message);
        //        tw.Close();
        //    }
        //    else if (File.Exists(path))
        //    {
        //        TextWriter tw = new StreamWriter(path);
        //        tw.WriteLine("Error: " + e.GetException().Message);
        //        tw.Close();
        //    }
        //}
    }
}