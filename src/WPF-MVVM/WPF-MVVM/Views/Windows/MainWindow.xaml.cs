using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_MVVM.Models.Decanat;

namespace WPF_MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GroupsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Models.Decanat.Group group)) return;
            if (group.Name is null) return;

            var filter = GroupNameFilterText.Text;

            if(filter.Length == 0) return;

            if(group.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)) return;
            if(group.Description != null && group.Description.Contains(filter, StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;
        }

        private void OnGroupsFilterTextChanged(object sender, TextChangedEventArgs e)
        {   
            var textbox = (TextBox)sender;
            var collection = (CollectionViewSource)textbox.FindResource("GroupsCollection");
            collection.View.Refresh();
        }
    }
}