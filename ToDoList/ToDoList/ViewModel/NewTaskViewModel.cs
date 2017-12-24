using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;
using System.ComponentModel;
using ToDoList.Commands;
using BisnessLogic;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using BisnessLogic;
using Contracts;
using System.Collections.ObjectModel;
using System.Windows;
using System.Runtime.Serialization.Formatters.Binary;

namespace ToDoList.ViewModel
{
    public class NewTaskViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Contracts.TaskModel> TasksCollection { get; set; }
        public Contracts.TaskModel task { get; set; }

        public DataProcesing getdatafrombl { get; set; }
        public ObservableCollection<Contracts.TaskModel> GetTasks()
        {
            TasksCollection = new ObservableCollection<Contracts.TaskModel>();
            getdatafrombl = new DataProcesing();
            foreach (var t in getdatafrombl.GetData())
            {
                TasksCollection.Add(t);
            }
            return TasksCollection;
        }
        private string _taskName;
        public string TaskName
        {
            get
            {
                return _taskName;
            }
            set
            {
                _taskName = value;
                OnPropertyChange("TaskName");
            }
        }
        private bool _isDone;
        public bool IsDone
        {
            get
            {
                return _isDone;
            }
            set
            {
                _isDone = value;
                OnPropertyChange("IsDone");
            }
        }

        public NewTaskViewModel()
        {
            countTasks = new DataProcesing();
        }
        public DataProcesing countTasks { get; set; }
        private Command _addTask;
        public Command AddTask=> _addTask ?? (_addTask = new Command(obj =>
        {
            GetTasks(); 
            task = new Contracts.TaskModel();
            getdatafrombl = new DataProcesing(); 
            Contracts.TaskModel newtask = new Contracts.TaskModel();
            //REVIEW: А если newTask==null?
            newtask.TaskName = TaskName;
            if (!string.IsNullOrEmpty(newtask.TaskName))
            {
                if (getdatafrombl.GetData().Count == 0)
                {
                    newtask.Number = 1;
                    newtask.Index = newtask.Number - 1;
                    GetTasks();
                    TasksCollection.Add(newtask);
                    BinaryFormatter formatter = new BinaryFormatter();
                    Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    using (FileStream fs = new FileStream(ToDoList.Properties.Settings.Default.FilePath, FileMode.OpenOrCreate))
                    {
                        formatter.Serialize(fs, TasksCollection);
                    }
                    getdatafrombl.GetData();
                    MessageBox.Show("Добавлено");
                }
                else
                {
                    GetTasks();
                    newtask.Number = countTasks.GetData().Count + 1;
                    newtask.Index = newtask.Number - 1;
                    TasksCollection.Add(newtask);
                    BinaryFormatter formatter = new BinaryFormatter();

                    Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    using (FileStream fs = new FileStream(ToDoList.Properties.Settings.Default.FilePath, FileMode.OpenOrCreate))
                    {
                        formatter.Serialize(fs, TasksCollection);
                    }
                    MessageBox.Show("Добавлено");
                }
            }
            else
            {
                MessageBox.Show("Введите задание!");
            }
          
            //GetTasks();
        }
        ));
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(String propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }

        }
    }
}
