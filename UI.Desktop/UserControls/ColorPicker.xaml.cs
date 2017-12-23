using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UI.Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {

        #region DP properties

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor",
                                                                          typeof(Color),
                                                                          typeof(ColorPicker), 
                                                                          new FrameworkPropertyMetadata(Color.FromArgb(255,0,0,0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion


        #region constructor and destructor

        public ColorPicker()
        {
            InitializeComponent();
        }

        #endregion


        #region properties

        /// <summary>
        /// 
        /// </summary>
        public Color SelectedColor {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }


        #endregion


        #region methods

        /// <summary>
        /// 
        /// </summary>
        public void GetClickedPoint(Object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(PickerImage);

            double pixelWidth = PickerImage.Source.Width;
            double pixelHeight = PickerImage.Source.Height;
            double x = pixelWidth * p.X / PickerImage.ActualWidth;
            double y = pixelHeight * p.Y / PickerImage.ActualHeight;


            BitmapImage bitmap = new BitmapImage(new Uri("pack://application:,,,/UI.Desktop;component/Resources/colors.jpg", UriKind.Absolute));
            WriteableBitmap colorPickerBmp = new WriteableBitmap(bitmap);

            SelectedColor = colorPickerBmp.GetPixel((int)x, (int)y);
        }

        /// <summary>
        /// 
        /// </summary>
        private void PasteHtmlButton_Click(object sender, RoutedEventArgs e)
        {

            if (Clipboard.ContainsData(DataFormats.Text))
            {

                string data = Clipboard.GetText(TextDataFormat.Text);

                try
                {
                    TypeConverter tc = new ColorConverter();
                    SelectedColor = (Color)tc.ConvertFromString(data);
                }
                catch (Exception err)
                {
                    // #TODO: Show fancy error message that the value is not valid.
                }
            }
        }


        #endregion


    }
}
