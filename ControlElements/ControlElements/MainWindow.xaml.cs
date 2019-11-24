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
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Timers;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ControlElements
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            series.ItemsSource = MyValue;
            timer = new Timer();
            timer.Interval = 90;// amount of second to be passed before next elapse
            timer.Elapsed += Timer_Elapsed; //calling the method for doing that thing
            timer.Start();
          
        }
        #region Timer Elapsed(chart update)
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (My.Count > 27)
                My.RemoveAt(0);
            //if the amount of letters exceeding the limit then we deleting the first occurence '
            // regular case of using
            My.Add(new KeyValuePair<char, int>(MyValue.First().Key,MyValue.First().Value));// change to collection values
            MyValue.RemoveAt(0);
            series.Dispatcher.BeginInvoke(new Action(() => {
                series.ItemsSource = null;
                series.InvalidateVisual();
                series.ItemsSource = My;
                BetterThanAnyGraph.InvalidateVisual();
            }));

        }
        #endregion

        #region global variables
        Random r = new Random();
        List<KeyValuePair<char, int>> MyValue = new List<KeyValuePair<char, int>>();
        List<KeyValuePair<char, int>> My = new List<KeyValuePair<char, int>>();
        public static class Serializer
        {
            public static void Save(string filePath, object objToSerialize)
            {
                try
                {
                    using (Stream stream = File.Open(filePath, FileMode.Create))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        bin.Serialize(stream, objToSerialize);
                    }
                }
                catch (IOException)
                {
                }
            }

            public static T Load<T>(string filePath) where T : new()
            {
                T rez = new T();

                try
                {
                    using (Stream stream = File.Open(filePath, FileMode.Open))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        rez = (T)bin.Deserialize(stream);
                    }
                }
                catch (IOException)
                {
                }

                return rez;
            }
        }

        public Timer timer;
        #endregion

        private void TextBox_Insert_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (TextBox_Insert.Text == "Insert Text Here")
            {
                TextBox_Insert.Text = "";
            }
        }
        private void showChart()
        {
            BetterThanAnyGraph.DataContext = MyValue;
        }

        #region Analyzing Button
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            //analyse
            string text = TextBox_Insert.Text;
            for (char c = 'a'; c <= 'z'; c++)
            {
                Regex regex = new Regex(c.ToString());
                MatchCollection match = regex.Matches(text);
                MyValue.Add(new KeyValuePair<char, int>(c, match.Count));
            }
            /* series.ItemsSource = null;
             series.InvalidateVisual();
             series.ItemsSource = MyValue;
             BetterThanAnyGraph.InvalidateVisual();*/
        }

        #endregion

        #region refresh Button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //refreshing button

            while (MyValue.Count()!=0)
            {
                My.Remove(My.First());
                MyValue.Remove(MyValue.First());
            }
            series.ItemsSource = null;
        }
        #endregion

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (MyValue.Count() != 0)
            {
                MessageBox.Show("Nothing to serialize");
            }
            else
            {
                Serializer.Save("Letters.dat", MyValue);
                MessageBox.Show("Serialization has done successfuly");

            }
        }
    }

}
//line graphics

/*     if (My.Count > 50)
         My.RemoveAt(0);
     My.Add(new KeyValuePair<int, double>(i, r.Next(200)));// change to collection values

     i++;
     series.Dispatcher.BeginInvoke(new Action(() => {
         series.ItemsSource = null;
         series.InvalidateVisual();
         series.ItemsSource = My;
         BetterThanAnyGraph.InvalidateVisual();
     }));*/
