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
            DataTable dataTable = new Service().Init();
            var result = from n in dataTable.AsEnumerable() select new NotesCl(n.Field<int>("Id"), n.Field<string>("Titel"), n.Field<string>("Content"));
            foreach ( var item in result ) {
                NoteList.Items.Add(item);
            }
        }
    }
}