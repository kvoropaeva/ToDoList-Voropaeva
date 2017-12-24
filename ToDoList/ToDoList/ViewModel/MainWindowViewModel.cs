using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ToDoList.Commands;
using BisnessLogic;
using Contracts;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;


namespace ToDoList.ViewModel
{
    public class MainWindowViewModel
    {
        public ObservableCollection<TaskModel> TaskColletion { get; set; }

        DataProcesing getdatafrombl = new DataProcesing();
        public MainWindowViewModel()
        {

            Config path = new Config();
            path.FilePath = ToDoList.Properties.Settings.Default.FilePath; 
            TaskColletion = new ObservableCollection<Contracts.TaskModel>();
            foreach (var t in getdatafrombl.GetData())
            {
                TaskColletion.Add(t);
            }
        }
        private Command _deleteTask;
        public Command DeleteTask
        {
            get
            {
                return _deleteTask ?? (_deleteTask = new Command(obj =>
                {
                    Contracts.TaskModel task = obj as Contracts.TaskModel;
                    if (task != null)
                    {
                        TaskColletion.Remove(task);
 
                        for (int i = task.Index; i < TaskColletion.Count; i++)
                        {
                           TaskColletion[i].Number = TaskColletion[i].Index;
                           TaskColletion[i].Index = TaskColletion[i].Index - 1;
                        }
                        //REVIEW: Тут бы try...catch
                        XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<Contracts.TaskModel>));
                        Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                        try
                        {
                            using (FileStream fs = new FileStream(ToDoList.Properties.Settings.Default.FilePath, FileMode.Create))
                            {
                                formatter.Serialize(fs, TaskColletion);
                            }
                        }
                        catch
                        {

                        }

                    }
   
                },
               (obj) => TaskColletion.Count > 0));
            }
        }
    }
}
