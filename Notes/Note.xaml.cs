using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для Note.xaml
    /// </summary>
    public partial class Note : Window
    {
        string action;
        int index;
        Tag currentTag;
        Service serv = new Service();
        MainWindow parent;
        public Note()
        {
            InitializeComponent();
        }

        public Note(MainWindow parent, int index, string action) {
            InitializeComponent();
            this.parent = parent;
            this.index = index;
            this.action = action;
            Init();
        }

        private void NewTagButton_Click(object sender, RoutedEventArgs e)
        {
            serv.CreatNewTag(TagBox.Text);
            Init();
        }

        private void ReTagButton_Click(object sender, RoutedEventArgs e)
        {
            if (action == "inf")
            {
                if (currentTag != null) { currentTag.Name = TagBox.Text; serv.ReTag(currentTag); }
                Init();
            }
            else {
            
            }
        }

        private void TagsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TagBox.Text = (TagsList.SelectedItem as Tag).Name;
            currentTag = TagsList.SelectedItem as Tag;
        }

        private void PossTagsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TagBox.Text = (PossTagsList.SelectedItem as Tag).Name;
            currentTag = PossTagsList.SelectedItem as Tag;
        }

        private void DelTagButton_Click(object sender, RoutedEventArgs e)
        {
            serv.DelTag(currentTag);
            TagBox.Text = "";
            Init();
        }

        public void Init() {
            var possResult = from t in serv.SelTags() from tn in serv.SelTagNote().AsEnumerable() where tn.Field<int>("NoteId") != index && tn.Field<int>("TagId") == t.Id select t;
            var result = from t in serv.SelTags() from tn in serv.SelTagNote().AsEnumerable() where tn.Field<int>("NoteId") == index && tn.Field<int>("TagId") == t.Id select t;
            foreach (var tag in possResult)
            {
                PossTagsList.Items.Add(tag.ToString());
            }
            foreach (var tag in result)
            {
                TagsList.Items.Add(tag.ToString());
            }
        }
    }
}
