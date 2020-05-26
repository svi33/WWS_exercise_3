using ClassLibraryForDB.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfPartNextInteger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApplicationContext db;
        public MainWindow()
        {
           InitializeComponent();
           db = ApplicationContext.SetDBcon();
           List<Result> listResults = db.Results.OrderBy(b => b.Id)
                      .Skip(Math.Max(0, db.Results.OrderBy(b => b.Id).Count() - 5)).ToList();
           ResultsGrid.ItemsSource = listResults;
           db.Dispose();
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string input = Input.Text;
            long number = -1;

            bool success = Int64.TryParse(input, out number);
            if (success && number > 0)
            {
                Result currentResult = new Result();
                currentResult.Input = number;
                currentResult.Answer = ConsoleFindInt.Work.CalcResult.GetNextSmaller((long)number);
                db = ApplicationContext.SetDBcon(); 
                db.Results.Add(currentResult);
                db.SaveChanges();
                List<Result> listResults = db.Results.OrderBy(b => b.Id)
                       .Skip(Math.Max(0, db.Results.OrderBy(b => b.Id).Count() - 5)).ToList();
                ResultsGrid.ItemsSource = listResults;
                db.Dispose();
                Answer.Visibility = Visibility.Visible;
                Answer.Text = "Next smaller: " + currentResult.Answer.ToString();

            }
            else
            {
                Answer.Visibility = Visibility.Visible;
                Answer.Text = "Input must be positive Integer!";
            }
        }
    }
}
