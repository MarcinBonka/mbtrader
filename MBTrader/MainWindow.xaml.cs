using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace MBTrader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<string> _dts;

        public SeriesCollection MySeries { get; set; }

        public List<string> QuoteDates
        {
            get { return _dts; }
            set
            {
                _dts = value;
                OnPropertyChanged("QuoteDates");
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void btnInitChart_Click(object sender, RoutedEventArgs e)
        {
            MySeries = new SeriesCollection
            {
                //new LineSeries
                //{
                //    Values = new ChartValues<double> { 3, 5, 7, 4 }
                //},
                //new ColumnSeries
                //{
                //    Values = new ChartValues<decimal> { 5, 6, 2, 7 }
                //}
                new CandleSeries()
                {
                    Values = new ChartValues<OhlcPoint>()
                    //{
                    //    new OhlcPoint(32, 35, 30, 32),
                    //    new OhlcPoint(33, 38, 31, 37),
                    //    new OhlcPoint(35, 42, 30, 40),
                    //    new OhlcPoint(37, 40, 35, 38),
                    //    new OhlcPoint(35, 38, 32, 33)
                    //}
                }
                
            };


            QuoteDates = new List<string>();
            DataContext = this;
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnAddChartPoint_Click(object sender, RoutedEventArgs e)
        {
            ChartValues<OhlcPoint> tmpValues = new ChartValues<OhlcPoint>();

            for (int i = 0; i < 1000; i++)
            {
                tmpValues.Add(new OhlcPoint(32, 40, 30, 80));
                QuoteDates.Add(DateTime.Now.ToString("yyMMdd hh:mm"));
            }

            MySeries[0].Values.AddRange(tmpValues);
        }

        

        private void AddCandle(DateTime dt, double o, double h, double l, double c)
        {
            MySeries[0].Values.Add(new OhlcPoint(o, h, l, c));
            QuoteDates.Add(dt.ToString("yyMMdd hh:mm"));
        }
    }
}
