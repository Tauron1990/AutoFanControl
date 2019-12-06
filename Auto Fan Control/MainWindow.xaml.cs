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

namespace Auto_Fan_Control
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Model = (MainWindowModel) DataContext;
        }

        private MainWindowModel Model;

        private async void Save_OnClick(object sender, RoutedEventArgs e) 
            => await Model.Options.Save();

        private async void Load_OnClick(object sender, RoutedEventArgs e) 
            => await Model.Options.Load();

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            await Model.Init();
        }
    }
}
