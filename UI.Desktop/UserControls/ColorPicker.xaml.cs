using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

            myEllipse = new Rectangle
            {
                Fill = new SolidColorBrush { Color = Colors.White },
                StrokeThickness = 1,
                Stroke = Brushes.Black,
                Width = 4,
                Height = 4
            };

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

        private Point lastSelectedPoint;
        private Rectangle myEllipse;
        #endregion


        #region methods

        /// <summary>
        /// 
        /// </summary>
        public void GetPointColor(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(CPCanvas);

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                double smallestDim;
                if (CPCanvas.ActualWidth > CPCanvas.ActualHeight)
                    smallestDim = CPCanvas.ActualHeight;
                else
                    smallestDim = CPCanvas.ActualWidth;

                // Position relative to center of canvas
                double xRel = mousePos.X - CPCanvas.ActualWidth / 2;
                double yRel = mousePos.Y - CPCanvas.ActualHeight / 2;

                // Hue is angle in deg, 0-360
                double angleRadians = Math.Atan2(yRel, xRel);
                double hue = angleRadians * (180 / Math.PI);
                if (hue < 0)
                    hue = 360 + hue;

                // Saturation is distance from center
                double saturation = Math.Min(Math.Sqrt(xRel * xRel + yRel * yRel) / (smallestDim / 2), 1.0);


                SelectedColor = ColorUtil.HsvToColor(hue, saturation, 1.0);

                // Abstand kleiner als radius
                if (Math.Sqrt(xRel * xRel + yRel * yRel) < smallestDim / 2)
                {
                    lastSelectedPoint = mousePos;
                }
                else
                {
                    double vectorLen = Math.Sqrt(xRel * xRel + yRel * yRel);

                    double xN = (1 / vectorLen) * xRel ;
                    double yN = (1 / vectorLen) * yRel ;

                    lastSelectedPoint.X = xN * smallestDim / 2 + CPCanvas.ActualWidth / 2;
                    lastSelectedPoint.Y = yN * smallestDim / 2 + CPCanvas.ActualHeight / 2;
                         
                }


                Canvas.SetTop(myEllipse, lastSelectedPoint.Y - 2);
                Canvas.SetLeft(myEllipse, lastSelectedPoint.X - 2);
                
               
            }
        }



        #endregion

        private void CPCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            canvas.Children.Clear();

            double halfWidth = canvas.ActualWidth / 2;
            double halfHeight = canvas.ActualHeight / 2;


            double smallestDim;
            if (e.NewSize.Width > e.NewSize.Height)
                smallestDim = e.NewSize.Height;
            else
                smallestDim = e.NewSize.Width;

            double increment = 2 * Math.PI / (smallestDim * 2);

            for (double angle = 0; angle < 2 * Math.PI; angle += increment)
            {
                TranslateTransform tt = new TranslateTransform(e.NewSize.Width / 2, e.NewSize.Height / 2);
                RotateTransform rt = new RotateTransform() { Angle = ColorUtil.RadianToDegree(angle) };

                TransformGroup tg = new TransformGroup();
                tg.Children.Add(rt);
                tg.Children.Add(tt);

                LinearGradientBrush lgb = new LinearGradientBrush();
                lgb.GradientStops.Add(new GradientStop() { Color = Colors.White, Offset = 0 });
                lgb.GradientStops.Add(new GradientStop() { Color = ColorUtil.HsvToColor(ColorUtil.RadianToDegree(angle), 1.0, 1.0), Offset = 1 });

                Line line = new Line { X1 = 0, Y1 = 0, X2 = smallestDim / 2, Y2 = 0, StrokeThickness = 2, Stroke = lgb, RenderTransform = tg };

                canvas.Children.Add(line);
            }
            
            
            Canvas.SetZIndex(myEllipse, 100);
            canvas.Children.Add(myEllipse);

        }

        private void HmtlFromClipboard_MouseDown(object sender, MouseButtonEventArgs e)
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
    }


    public static class ColorUtil
    {
        /// <summary>
        /// Converts Radians to Degrees.
        /// </summary>
        public static double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        /// <summary>
        /// Convert HSV to RGB
        /// h is from 0-360
        /// s,v values are 0-1
        /// r,g,b values are 0-255
        /// Based upon http://ilab.usc.edu/wiki/index.php/HSV_And_H2SV_Color_Space#HSV_Transformation_C_.2F_C.2B.2B_Code_2
        /// </summary>
        public static Color HsvToColor(double h, double S, double V)
        {

            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }

            return Color.FromRgb(Clamp((byte)(R * 255.0)), Clamp((byte)(G * 255.0)), Clamp((byte)(B * 255.0)));
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        private static byte Clamp(byte i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }
    }
}
