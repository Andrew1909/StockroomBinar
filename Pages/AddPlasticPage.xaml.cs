using StockroomBinar.BD;
using StockroomBinar.Class;
using StockroomBinar.DialogWindow;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StockroomBinar.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddPlasticPage.xaml
    /// </summary>
    public partial class AddPlasticPage : Page
    {
        public PlasticStor plasticStor = new PlasticStor();
        public ColorPlastic colorPlastic = new ColorPlastic();
        string ColorNamePlast;//для запси названия цвета платика, выбранного из комбобокс
        string TypeNamePlast;//для запси названия типа платика, выбранного из комбобокс
        public AddPlasticPage()
        {
            InitializeComponent();


            var a = Connect.bd.ColorPlastic.Where(p => p.ID != 0).Count();
            AddColordNamePlastic.Items.Add("Выберите цвет платика");
            for (int j = 1; j <= int.Parse(a.ToString()); j++)
            {
                var a1 = Connect.bd.ColorPlastic.First(p => p.ID == j);
                AddColordNamePlastic.Items.Add(a1.NameColor.ToString());
            }
            AddColordNamePlastic.SelectedIndex = 0;


            var b = Connect.bd.PlasticType.Where(p => p.ID != 0).Count();
            AddTypePlastic.Items.Add("Выберите тип пластика");
            for (int j = 1; j <= int.Parse(b.ToString()); j++)
            {
                var b1 = Connect.bd.PlasticType.First(p => p.ID == j);
                AddTypePlastic.Items.Add(b1.NameType.ToString());
            }
            AddTypePlastic.SelectedIndex = 0;
        }

        private void AddPlast_Click(object sender, RoutedEventArgs e)
        {
            int index1 = AddColordNamePlastic.SelectedIndex;
            if (AddColordNamePlastic.SelectedIndex == index1)
            {
                if (index1 > 0)
                {
                    var a1 = Connect.bd.ColorPlastic.First(p => p.ID == index1);
                    ColorNamePlast = a1.NameColor;
                }
            }
            //int index2 = AddColordNamePlastic.SelectedIndex;
            //if (AddColordNamePlastic.SelectedIndex == index2)
            //{
            //    if (index2 > 0)
            //    {
            //        var a1 = Connect.bd.PlasticType.First(p => p.ID == index2);
            //        TypeNamePlast = a1.NameType;
            //    }
            //}

            var objA = Connect.bd.PlasticStor.Where(p => p.ColorName == ColorNamePlast).Count();
            if (objA != null)
            {
               
                var ObjB=Connect.bd.PlasticStor.First(p=>p.ColorName == ColorNamePlast);
                ObjB.Weight = decimal.Parse(AddWightPlastic.Text) + ObjB.Weight;
                ObjB.NumberСoils=int.Parse(AddCoilsPlastic.Text)+ ObjB.NumberСoils;
                Connect.bd.SaveChanges();
                MessageBox.Show("Пластик добавлен!");
                AddColordNamePlastic.SelectedIndex = 0;
                AddColordNamePlastic.SelectedIndex = 0;
            }

        }

        private void AddCoilsPlastic_SelectionChanged(object sender, RoutedEventArgs e)
        {
            AddWightPlastic.Text = AddCoilsPlastic.Text;
        }

        private void AddColordNamePlastic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
   
            int index2 = AddColordNamePlastic.SelectedIndex;

            if (AddColordNamePlastic.SelectedIndex == index2)
            {
                if (index2 > 0)
                {
                    var a1 = Connect.bd.ColorPlastic.First(p => p.ID == index2);
              
                    var objA = Connect.bd.PlasticStor.Where(p => p.ColorName == a1.NameColor).Count(); //добавить проверку на то, есть ли цвет или тип в главной таблице
                    if (objA != null)
                    {
                        var ObjB = Connect.bd.PlasticStor.First(p => p.ColorName == a1.NameColor);
                        var objC = Connect.bd.PlasticType.First(p => p.NameType == ObjB.PlasticType);
                        AddTypePlastic.SelectedIndex = objC.ID;

                        AddManufactPlastic.Text = ObjB.Manufacturer;
                    }
                }
            }

            
        }

        private void AddNewNameColor_Click(object sender, RoutedEventArgs e)
        {
            AddNewNameColorWindow ColorWindow = new AddNewNameColorWindow();
            var a = Connect.bd.ColorPlastic.Where(p => p.ID != null).Count();

            if (ColorWindow.ShowDialog() == true)
            {
                colorPlastic.ID = a + 1;
                colorPlastic.NameColor = ColorWindow.Color;
                Connect.bd.ColorPlastic.Add(colorPlastic);
                Connect.bd.SaveChanges();
                MessageBox.Show("Новый цвет добавлен!");
                a = 0;

            }
            
        }
    }
}
