using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.Models
{
    using BaseTypes;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class Mixer : BaseModel
    {

        #region constructor and destructor

        public Mixer()
        {

        }

        #endregion


        #region properties

        /// <summary>
        /// A list of <see cref="Generator"/>.
        /// </summary>
        private ObservableCollection<Generator> Generators { get; }

        #endregion

        #region methods

        /// <summary>
        /// Override this method in derived types to initialize command logic.
        /// </summary>
        protected override void InitCommands()
        {
            base.InitCommands();

        }

        /// <summary>
        /// Can be overridden by derived types to react on the finisihing of error-collections.
        /// </summary>
        protected override void OnErrorsCollected()
        {
            base.OnErrorsCollected();
        }



        #region mixer methods

        public WriteableBitmap MixAdd(WriteableBitmap inA, WriteableBitmap inB)
        {
            Rect cRect = new Rect(new Size(10,10)); // Matrix SIZE!
            inA.Blit(cRect, inB, cRect, WriteableBitmapExtensions.BlendMode.Additive);

            return inA;
        }

        private WriteableBitmap MixSubstract(WriteableBitmap inA, WriteableBitmap inB)
        {
            Rect cRect = new Rect(new Size(10, 10)); // Matrix SIZE!
            inA.Blit(cRect, inB, cRect, WriteableBitmapExtensions.BlendMode.Subtractive);

            return inA;
        }

        private WriteableBitmap MixMultiply(WriteableBitmap inA, WriteableBitmap inB)
        {
            Rect cRect = new Rect(new Size(10, 10)); // Matrix SIZE!
            inA.Blit(cRect, inB, cRect, WriteableBitmapExtensions.BlendMode.Multiply);

            return inA;
        }
    

        private WriteableBitmap MixDivide(WriteableBitmap inA, WriteableBitmap inB)
        {

            return BitmapFactory.New(10, 10);
        }

        private WriteableBitmap MixDifference(WriteableBitmap inA, WriteableBitmap inB)
        {

            return BitmapFactory.New(10, 10);
        }

        private WriteableBitmap MixAND(WriteableBitmap inA, WriteableBitmap inB)
        {

            return BitmapFactory.New(10, 10);
        }

        private WriteableBitmap MixOR(WriteableBitmap inA, WriteableBitmap inB)
        {

            return BitmapFactory.New(10, 10);
        }

        private WriteableBitmap MixXOR(WriteableBitmap inA, WriteableBitmap inB)
        {

            return BitmapFactory.New(10, 10);
        }
        #endregion

        #endregion
    }
}
