using MaterialDesignColors;
using StockroomBinar.Class;
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

namespace StockroomBinar.Pages
{
    /// <summary>
    /// Логика взаимодействия для PlasticStorage.xaml
    /// </summary>
    public partial class PlasticStorage : Page
    {
        string[,] NameTaypePlt = new string[99, 99];//массив с названиями платика из бд
        string TypeNamePlast;//для запси названия типа платика, выбранного из комбобокс
        public PlasticStorage()
        {
            InitializeComponent();
            PlastitStoageView.ItemsSource = Connect.bd.PlasticStor.ToList();
            
            int x = 0;
            int y = 0;
            var a = Connect.bd.PlasticType.Where(p => p.ID != 0).Count();
            PlastType.Items.Add("Все типы");
            for (int j = 1; j <= int.Parse(a.ToString()); j++)
            {
                var a1 = Connect.bd.PlasticType.First(p => p.ID == j);
                NameTaypePlt[x, y] = a1.NameType;
                x++;
                PlastType.Items.Add(a1.NameType.ToString());
            }
            PlastType.SelectedIndex = 0;
        }

        private void PlastType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = PlastType.SelectedIndex;
            if (PlastType.SelectedIndex == index)
            {
                if (index > 0)
                { 
                    var a1 = Connect.bd.PlasticType.First(p => p.ID == index);
                    TypeNamePlast = a1.NameType;
                    PlastitStoageView.ItemsSource = Connect.bd.PlasticStor.Where(p => p.PlasticType == TypeNamePlast).ToList();
                }
            }
            if (PlastType.SelectedIndex == 0)
            {   
                PlastitStoageView.ItemsSource = Connect.bd.PlasticStor.ToList();
            }
        }

        private void SearchColor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            PlastitStoageView.ItemsSource = Connect.bd.PlasticStor.Where(p => p.ColorName.StartsWith(SearchColor.Text)).ToList();
        }

        private void AddPlatic_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new AddPlasticPage());
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
