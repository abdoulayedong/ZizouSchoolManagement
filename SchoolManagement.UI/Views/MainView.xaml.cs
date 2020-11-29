using System.Windows;
using System.Windows.Controls;

namespace SchoolManagement.UI.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void Mouse_Down(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(ButtonOpenMenu.Visibility == Visibility.Visible)
            {
                ButtonCloseMenu.Visibility = Visibility.Visible;
                ButtonOpenMenu.Visibility = Visibility.Collapsed;
            }
        }

        private void Hover_over(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(ButtonOpenMenu.Visibility == Visibility.Collapsed)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_etudiants.Visibility = Visibility.Collapsed;
                tt_classes.Visibility = Visibility.Collapsed;
                tt_departments.Visibility = Visibility.Collapsed;
                //tt_signOut.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_etudiants.Visibility = Visibility.Visible;
                tt_classes.Visibility = Visibility.Visible;
                tt_departments.Visibility = Visibility.Visible;
                //tt_signOut.Visibility = Visibility.Visible;
            }


        }

        private void Changed_View(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    usc = new HomeView();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemStudent":
                    break;
                case "ItemClasses":
                    break;
                case "ItemDepartement":
                    usc = new DepartmentView();
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }
    }
}
