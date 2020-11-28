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
using System.Windows.Shapes;

namespace Yazlab_1
{
    /// <summary>
    /// StartScreen.xaml etkileşim mantığı
    /// </summary>
    public partial class StartScreen : Window
    {
        MainWindow mainWindow;
        public StartScreen()
        {
            InitializeComponent();
        }
        
        private void start_btn_Click(object sender, RoutedEventArgs e)
        {

            mainWindow = new MainWindow(Convert.ToInt32(border_x_txt.Text), Convert.ToInt32(border_y_txt.Text), Convert.ToInt32(gold_txt.Text), Convert.ToInt32(hidden_gold_txt.Text),
                Convert.ToInt32(a_gold_txt.Text), Convert.ToInt32(a_move_txt.Text), Convert.ToInt32(a_cost_txt.Text),
                Convert.ToInt32(b_gold_txt.Text), Convert.ToInt32(b_move_txt.Text), Convert.ToInt32(b_cost_txt.Text), 
                Convert.ToInt32(c_gold_txt.Text), Convert.ToInt32(c_move_txt.Text), Convert.ToInt32(c_cost_txt.Text),
                Convert.ToInt32(d_gold_txt.Text), Convert.ToInt32(d_move_txt.Text), Convert.ToInt32(d_cost_txt.Text));
            mainWindow.Show();


        }
    }
}
