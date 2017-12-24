using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace Contracts
{
    [Serializable]
        public class TaskModel : INotifyPropertyChanged
        {
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
            private int _number;
            public int Number
            {
                get { return _number; }
                set
                {
                    _number = value;
                    OnPropertyChange("Number");
                }
            }
            private int _index;
            public int Index
            {
                get { return _index; }
                set
                {
                    _index = value;
                    OnPropertyChange("Index");
                }
            }
            public TaskModel()
            {

            }
            public TaskModel(string TaskName, bool IsDone, int Number, int Index)
            {
                this.TaskName = TaskName;
                this.IsDone = IsDone;
                this.Index = Number - 1;
            }
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
