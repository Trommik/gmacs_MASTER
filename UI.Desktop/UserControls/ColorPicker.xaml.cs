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
using Logic.UI.UserControlsVM;

namespace UI.Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public ColorPicker()
        {
            InitializeComponent();
        }

        public void GetClickedPoint(Object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(PickerImage);

            double pixelWidth = PickerImage.Source.Width;
            double pixelHeight = PickerImage.Source.Height;
            double x = pixelWidth * p.X / PickerImage.ActualWidth;
            double y = pixelHeight * p.Y / PickerImage.ActualHeight;
            Point clickedPixel = new Point(x, y);

            Console.WriteLine(clickedPixel.ToString())
;
            // Get the viewmodel from the DataContext
            ColorPickerViewModel vm = this.DataContext as ColorPickerViewModel;


            //Call command from viewmodel     
            if ((vm != null) && (vm.ImageMouseDownCommand.CanExecute(clickedPixel)))
                vm.ImageMouseDownCommand.Execute(clickedPixel);
        }
      
    }
}
