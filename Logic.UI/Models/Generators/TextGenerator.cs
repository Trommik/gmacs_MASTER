using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Logic.UI.Models
{
    public class TextGenerator : Generator
    {
        public string DisplayText { get; set; }

        public override WriteableBitmap GenerateImage()
        {
            WriteableBitmap writeableBmp = BitmapFactory.New(512, 512);

            return writeableBmp;
        }
    }
}
