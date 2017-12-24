using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoList.Commands;

namespace ToDoList.View
{
    /// <summary>
    /// Логика взаимодействия для NewTasksPage.xaml
    /// </summary>
    public partial class NewTasksPage : Page
    {
        public NewTasksPage()
        {
            InitializeComponent();
        }
        private Command _goHome; 
        public Command GoHome => _goHome ?? (_goHome = new Command(delegate
        {
            Page pg = GetDependencyObject(this, typeof(Page)) as Page;
            pg.NavigationService?.Navigate(new MainPage());
        }));
        private DependencyObject
        GetDependencyObject(DependencyObject startObject, Type type)
        {
            DependencyObject parent = startObject;
            while (parent != null)
            {
                if (type.IsInstanceOfType(parent))
                {
                    break;
                }
                else
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
            }
            return parent;
        }
    }

}

