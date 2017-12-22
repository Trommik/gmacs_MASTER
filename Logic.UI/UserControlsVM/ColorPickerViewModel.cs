using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.UserControlsVM
{
    using BaseTypes;
    using GalaSoft.MvvmLight.Command;
    using Logic.UI.Models;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class ColorPickerViewModel : BaseViewModel
    {
        #region constructor and destructor

        public ColorPickerViewModel()
        {
            Title = "Color Picker";

            CopyHtmlValueCommand = new RelayCommand(() => throw new NotImplementedException());

            ImageMouseDownCommand = new RelayCommand<Point>((e) => PickColor(e));
        }
        #endregion

        #region properties

        /// <summary>
        /// Command to copy the Html value in the clipboard.
        /// </summary>
        public RelayCommand CopyHtmlValueCommand { get; }

        /// <summary>
        /// Command which picks the color from the Image.
        /// </summary>
        public RelayCommand<Point> ImageMouseDownCommand { get; }

        /// <summary>
        /// The Picked Color.
        /// </summary>
        public Color SelectedColor { get; set; }


        #endregion

        #region methods 

        public void PickColor(Point clickedPixel)
        {
            BitmapImage bitmap = new BitmapImage(new Uri("pack://application:,,,/UI.Desktop;component/Resources/colors.jpg", UriKind.Absolute));
            WriteableBitmap colorBmp = new WriteableBitmap(bitmap);

            SelectedColor = colorBmp.GetPixel((int)clickedPixel.X, (int)clickedPixel.Y);
        }

        #endregion

    }
}
