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
                ReadQuote(e.Name);
            else
                Console.WriteLine("I read xml only!!");
        }

        private static void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File: {0} changed at time: {1}", e.Name, DateTime.Now.ToLocalTime());
        }
      
        static void AddNewQuote(string db_CSTRING, string filePath) 
        { 
            
        }

        static void ReadQuote(string xmlFileName) 
        {
            string path = "xml\\"+xmlFileName;
            XmlTextReader xmlReader = new XmlTextReader(path);
            string xmlTag = "";
           
            Quote_Job qJob = new Quote_Job();
            while (xmlReader.Read())
            {
                //Console.WriteLine(xmlReader.Name); //print the tag name

                //switch (xmlReader.NodeType)
                //{
                //    case XmlNodeType.Element: // The node is an element.
                //        Console.Write("<" + xmlReader.Name);
                //        Console.WriteLine(">");
                //        break;
                //    case XmlNodeType.Text: //Display the text in each element.
                //        Console.WriteLine(xmlReader.Value);
                //        break;
                //    case XmlNodeType.EndElement: //Display the end of the element.
                //        Console.Write("</" + xmlReader.Name);
                //        Console.WriteLine(">");
                //        break;
                //}

                /* this does not work
                if (xmlReader.Name == "HOURS") 
                {
                   // string something = xmlReader.Value.ToString();
                    Console.WriteLine(xmlReader.Value);
                  //  Console.WriteLine(something.GetType());
                }
                */

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

        static string GetTagValue(string xmlTag, XmlTextReader xmlReader) 
        {
            switch (xmlTag)
            {
                case "QUOTEID":
                    return xmlReader.Value;
                  
                case "QUOTEDESC":
                    return xmlReader.Value;
                   
                case "REVISIONDATE":
                    return xmlReader.Value;
                  
                case "DELADDR3":
                    return xmlReader.Value;
                    
                case "SALESPERSON":
                    return xmlReader.Value;
                  
                case "CONTACTPERSON":
                    return xmlReader.Value;
                   
                case "COSTPRICE":
                    return xmlReader.Value;
                   
                case "GROSSPRICE":
                    return xmlReader.Value;
                   
                case "JOBDISCPCNT":
                    return xmlReader.Value;
                  
                case "NETQUOTATION":
                    return xmlReader.Value;
                   
                case "HOURS":
                    return xmlReader.Value;
                default:
                    return ""; 

            }
        }
        static void MoveFileToBackupFolder(string xmlFile) 
        {
            File.Move("xml\\"+xmlFile,"bkp\\"+xmlFile);
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void Run()
        {
          //  string[] args = Environment.GetCommandLineArgs();

            // If a directory is not specified, exit program.
            //if (args.Length != 2)
            //{
            //    // Display the proper way to call the program.
            //    Console.WriteLine("Usage: Watcher.exe (directory)");
            //    return;
            //}

            // Create a new FileSystemWatcher and set its properties.
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
              // watcher.Path = args[1];
                watcher.Path = @"xml";
                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                watcher.NotifyFilter = NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

                // Only watch text files.
                watcher.Filter = "*.txt";

                // Add event handlers.
                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;
                watcher.Deleted += OnChanged;
                watcher.Renamed += OnRenamed;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                Console.WriteLine("Press 'q' to quit the sample.");
                while (Console.Read() != 'q') ;
            }
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
