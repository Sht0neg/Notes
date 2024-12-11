using MySqlX.XDevAPI.Common;
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
            this.action = action;
            this.index = index;
            Init();
            DataTable dataTable = new Service().Init();
            var result = from n in dataTable.AsEnumerable() select new NotesCl(n.Field<int>("Id"), n.Field<string>("Titel"), n.Field<string>("Content"));
            if (action == "inf")
            {
                NoteButton.Content = "Изменить";
                foreach (var i in result)
                {
                    if (i.Id == index)
                    {
                        TitelBox.Text = i.Titel;
                        TextBox.Text = i.Content;
                        break;
                    }
                }
            }
            else {
                NoteButton.Content = "Добавить";
                serv.CreateNewNote(TitelBox.Text, TextBox.Text);
                dataTable = new Service().Init();
                result = from n in dataTable.AsEnumerable() select new NotesCl(n.Field<int>("Id"), n.Field<string>("Titel"), n.Field<string>("Content"));
                //serv.UpgateTagNote(result.Last().Id);
                this.index = result.Last().Id;
            }
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
            if (TagsList.SelectedItem != null)
            {
                TagBox.Text = (TagsList.SelectedItem as Tag).Name;
                currentTag = TagsList.SelectedItem as Tag;
            }
        }

        private void PossTagsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PossTagsList.SelectedItem != null)
            {
                TagBox.Text = (PossTagsList.SelectedItem as Tag).Name;
                currentTag = PossTagsList.SelectedItem as Tag;
            }
        }

        private void DelTagButton_Click(object sender, RoutedEventArgs e)
        {
            serv.DelTag(currentTag);
            TagBox.Text = "";
            Init();
        }

        public void Init() {
            PossTagsList.Items.Clear();
            TagsList.Items.Clear();
            var result = from t in serv.SelTags() from tn in serv.SelTagNote().AsEnumerable() where tn.Field<int>("NoteId") == index && tn.Field<int>("TagId") == t.Id select t;
            var intresult = from t in result select t.Id;
            var possResult = (from t in serv.SelTags() where !intresult.Contains(t.Id) select t).Distinct();
            foreach (var tag in possResult)
            {
                PossTagsList.Items.Add(tag);
            }
            foreach (var tag in result)
            {
                TagsList.Items.Add(tag);
            }
            DataTable dt = new Service().SettingsInit();
            PossTagsList.FontSize = dt.Rows[0].Field<int>("FontSize");
            PossTagsList.Foreground = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(typeof(Brush)).ConvertFromInvariantString(dt.Rows[0].Field<string>("TextColor"));
            TagsList.FontSize = dt.Rows[0].Field<int>("FontSize");
            TagsList.Foreground = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(typeof(Brush)).ConvertFromInvariantString(dt.Rows[0].Field<string>("TextColor"));
            TagBox.FontSize = dt.Rows[0].Field<int>("FontSize");
            TagBox.Foreground = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(typeof(Brush)).ConvertFromInvariantString(dt.Rows[0].Field<string>("TextColor"));
            TitelBox.FontSize = dt.Rows[0].Field<int>("FontSize");
            TitelBox.Foreground = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(typeof(Brush)).ConvertFromInvariantString(dt.Rows[0].Field<string>("TextColor"));
            TextBox.FontSize = dt.Rows[0].Field<int>("FontSize");
            TextBox.Foreground = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(typeof(Brush)).ConvertFromInvariantString(dt.Rows[0].Field<string>("TextColor"));

        }

        private void NoteButton_Click(object sender, RoutedEventArgs e)
        {

            serv.UpdateNote(index, TitelBox.Text, TextBox.Text);
            DialogResult = true;
        }

        private void AddTagToNoteButton_Click(object sender, RoutedEventArgs e)
        {
            serv.AddTagToNote((PossTagsList.SelectedItem as Tag).Id, index);
            Init();
        }

        private void DelTagForNoteButton_Click(object sender, RoutedEventArgs e)
        {
            serv.DelTagForNote((TagsList.SelectedItem as Tag).Id, index);
            Init();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            //serv.DeleteNote(index);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var dt = new Service().Init();
            var result = from n in dt.AsEnumerable() select new NotesCl(n.Field<int>("Id"), n.Field<string>("Titel"), n.Field<string>("Content"));
            if (result.Last().Titel == "" && result.Last().Content == "") {
                serv.DeleteNote(result.Last().Id);
            }
        }
    }
}
