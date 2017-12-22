using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Logic.UI.Models
{
    public class ColorGenerator : Generator
    {

        public Color MatrixColor { get; set; }


        public override WriteableBitmap GenerateImage()
        {
            WriteableBitmap writeableBmp = BitmapFactory.New(10, 10);

            // Black triangle with the points P1(10, 5), P2(20, 40) and P3(30, 10)
            writeableBmp.FillRectangle(0,0,10,10, MatrixColor);
            writeableBmp.FillRectangle(0, 0, 1, 1, Colors.Black);

            return writeableBmp;
        }
    }
}
