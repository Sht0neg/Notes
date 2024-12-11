using System;
using System.Collections.Generic;
using System.Data;
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

namespace Notes
{
    /// <summary>
    /// Логика взаимодействия для SetWindow.xaml
    /// </summary>
    public partial class SetWindow : Window
    {
        public SetWindow()
        {
            InitializeComponent();
            for (int i = 10; i < 20; i++) {
                SizeBox.Items.Add(i);
            }
            CorolBox.Items.Add("red");
            CorolBox.Items.Add("blue");
            CorolBox.Items.Add("pink");
            CorolBox.Items.Add("black");
            CorolBox.Items.Add("purple");
            DataTable dt = new Service().SettingsInit();
            SizeBox.SelectedItem = dt.Rows[0].Field<int>("FontSize");
            CorolBox.SelectedItem = dt.Rows[0].Field<string>("TextColor");
        }

        private void CurButton_Click(object sender, RoutedEventArgs e)
        {
            new Service().UpdateSettings(Convert.ToInt32(SizeBox.SelectedItem), Convert.ToString(CorolBox.SelectedItem));
            DialogResult = true;
        }

        private void CanButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
