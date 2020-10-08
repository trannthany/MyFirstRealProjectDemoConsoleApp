using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Xml;
using System.IO;
using System.Security.Permissions;
namespace Sqlite_Automation_XML
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // const string CSTRING = @"Data Source=C:\Users\User\Documents\C#\Experiments\Sqlite_Automation_XML\Sqlite_Automation_XML\bin\Debug\Qoutation_Jobs.db;Version=3";

            FileWatcher();
            Console.ReadLine();
        }
        static void FileWatcher() 
        {
            FileSystemWatcher watcher = new FileSystemWatcher(@"xml");
          
            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = true;

            //add Event Hanlders
            watcher.Changed += Watcher_Changed;
            watcher.Created += Watcher_Created;
            watcher.Deleted += Watcher_Deleted;
            watcher.Renamed += Watcher_Renamed;
    
            
        }

        private static void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine("File: {0} renamed to {1} at time: {2}", e.OldName, e.Name, DateTime.Now.ToLocalTime());
        }

        private static void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File: {0} delete at {1}", e.Name, DateTime.Now.ToLocalTime() );
        }

        private static void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File: {0} created at time: {1}", e.Name, DateTime.Now.ToLocalTime());
            if(Path.GetExtension(e.Name) == ".xml")
                CreateQuote(e.Name);
            else
                Console.WriteLine("I read xml only!!");
        }

        private static void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File: {0} changed at time: {1}", e.Name, DateTime.Now.ToLocalTime());
        }

        static void CreateQuote(string xmlFileName) 
        {
            string path = "xml\\"+xmlFileName;
            XmlTextReader xmlReader = new XmlTextReader(path);
            string xmlTag = "";
           
            Quote_Job qJob = new Quote_Job();
            while (xmlReader.Read())
            {
               
                switch (xmlReader.NodeType) 
                {
                    case XmlNodeType.Element:
                        xmlTag = xmlReader.Name;
                       
                        break;
                    case XmlNodeType.Text:
                        switch (xmlTag)
                        {                            
                            case "QUOTEID":
                                qJob.QuoteID = Int32.Parse(xmlReader.Value);
                                break;
                            case "QUOTEDESC":
                                qJob.QuoteDesciptn = xmlReader.Value;
                                break;
                            case "REVISIONDATE":
                                qJob.RevisionDate = xmlReader.Value;
                                break;
                            case "DELADDR3":
                                qJob.City = xmlReader.Value;
                                break;
                            case "SALESPERSON":
                                qJob.SalesPerson = xmlReader.Value;
                                break;
                            case "CONTACTPERSON":
                                qJob.ContactPerson = xmlReader.Value;
                                break;
                            case "COSTPRICE":
                                qJob.CostPrice = Double.Parse(xmlReader.Value);
                                break;
                            case "GROSSPRICE":
                                qJob.GrossPrice = Double.Parse(xmlReader.Value);
                                break;
                            case "JOBDISCPCNT":
                                qJob.JobDiscription = xmlReader.Value;
                                break;
                            case "NETQUOTATION":
                                qJob.NetQuotation = Double.Parse(xmlReader.Value);
                                break;
                            case "HOURS":
                                qJob.Hours = Double.Parse(xmlReader.Value);
                                break;

                        }
                        break;

                }

            }
            xmlReader.Close();
           Console.WriteLine(qJob.ToString());
            MoveFileToBackupFolder(xmlFileName);
                
        }

        static void MoveFileToBackupFolder(string xmlFile) 
        {
            File.Move("xml\\"+xmlFile,"bkp\\"+xmlFile);
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e) =>
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");

        private static void OnRenamed(object source, RenamedEventArgs e) =>
            // Specify what is done when a file is renamed.
            Console.WriteLine($"File: {e.OldFullPath} renamed to {e.FullPath}");
    }
}
