using Logic.UI.UserControlsVM;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace UI.Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for MatrixViewport.xaml
    /// </summary>
    public partial class MatrixViewport : UserControl
    {
        public MatrixViewport()
        {
            InitializeComponent();
        }

        private void MatrixViewport_Loaded(object sender, RoutedEventArgs e)
        {
            // Get the viewmodel from the DataContext
            MatrixViewportViewModel vm = this.DataContext as MatrixViewportViewModel;

            
            //Call command from viewmodel     
            if ((vm != null) && (vm.ViewportSizeInitCommand.CanExecute(Viewport)))
                vm.ViewportSizeInitCommand.Execute(Viewport);
        }
    }
}
