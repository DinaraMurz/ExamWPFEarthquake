using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using Newtonsoft.Json;

namespace EarthQuakesExamWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _url = "https://earthquake.usgs.gov/fdsnws/event/1/query?format=geojson&orderby=time&limit=";
        private EarthquakesFromApi _earthquakeFromApi = new EarthquakesFromApi();
        private List<Earthquake> _earthquake = new List<Earthquake>();
        //private Rootobject earthquake = new Rootobject();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Convert.ToInt32(textBox.Text);

            WebClient client = new WebClient();
            string json = "";

            using (Stream stream = client.OpenRead(new Uri(_url + textBox.Text.ToString())))
            {
                using (var reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        json += line;
                    }
                }
            }

            EarthquakesFromApi earthquakeService = JsonConvert.DeserializeObject<EarthquakesFromApi>(json);

            _earthquakeFromApi = Newtonsoft.Json.JsonConvert.DeserializeObject<EarthquakesFromApi>(json);

                for (int i = 0; i < Convert.ToInt32(textBox.Text); i++)
                {
                    _earthquake.Add(new Earthquake
                    {
                        Depth = _earthquakeFromApi.Features[i].Information.Depth,
                        Magnitude = _earthquakeFromApi.Features[i].Information.Magnitude,
                        Locality = _earthquakeFromApi.Features[i].Information.Place,
                    });

                }

                dataGrid.ItemsSource = _earthquake;
            }
            catch
            {
                textBox.Text = "Введите коректные данные";
            }
        }


        private DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            epoch = epoch.AddMilliseconds(timestamp).ToLocalTime();
            return epoch;
        }
    }
}
