using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Initialisation();
        }

        public void Initialisation() {
            NoteList.Items.Clear();
            DataTable dataTable = new Service().Init();
            var result = from n in dataTable.AsEnumerable() select new NotesCl(n.Field<int>("Id"), n.Field<string>("Titel"), n.Field<string>("Content"));
            foreach ( var item in result ) {
                NoteList.Items.Add(item);
            }
        }

        private void InfButton_Click(object sender, RoutedEventArgs e)
        {
            if (NoteList.SelectedItem != null)
            {
                Note add = new Note(this, (NoteList.SelectedItem as NotesCl).Id, "inf");
                bool? result = add.ShowDialog();
                Initialisation();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Note add = new Note(this, 0, "hf");
            bool? result = add.ShowDialog();
            Initialisation();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            new Service().DeleteNote((NoteList.SelectedItem as NotesCl).Id);
            Initialisation();
        }
    }
}