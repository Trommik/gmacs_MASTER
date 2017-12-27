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


        public ColorGenerator()
        {
            Type = GeneratorTypes.COLOR;
        }


        public override WriteableBitmap GenerateImage()
        {
            WriteableBitmap writeableBmp = BitmapFactory.New(10, 10);

            writeableBmp.Clear(MatrixColor);

            return writeableBmp;
        }
    }
}
