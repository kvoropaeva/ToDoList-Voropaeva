using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Contracts;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace DAL
{
    public class DataReader:IFileReader
    {
        public ObservableCollection<TaskModel> TasksList { get; set; }
        public ObservableCollection<TaskModel> GetTaskCollection()
        {
            TasksList = new ObservableCollection<TaskModel>();
            string executableLocation = Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().Location);
            string xslLocation = Path.Combine(executableLocation, "TasksFile.bin");
            BinaryFormatter formatter = new BinaryFormatter();
       
            try
            {
                using (FileStream fs = new FileStream(xslLocation, FileMode.Open))
                {
                        TasksList = (ObservableCollection<TaskModel>)formatter.Deserialize(fs);
                        
                }
            }
            catch (Exception)
            {

            }
            return TasksList;
        }
    }
}
