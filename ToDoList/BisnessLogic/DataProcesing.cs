using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using DAL;
using System.Collections.ObjectModel;
using Contracts;

namespace BisnessLogic
{
    public class DataProcesing 
    {
        public IFileReader GetRead { get; set; }

        public ObservableCollection<TaskModel> GetData()
        {
            GetRead = new DataReader();
            return GetRead.GetTaskCollection(); 
        }
        public DataProcesing()
        {
            GetRead = new DataReader();
        }
    } 
}
