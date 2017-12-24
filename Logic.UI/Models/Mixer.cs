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


        /// <summary>
        /// Mixes a list of Generators to one writableBitmap image.
        /// </summary>
        /// <param name="generators"> The list of tGenerators. </param>
        /// <returns> The mixed Image. </returns>
        public WriteableBitmap MixList(IList<Generator> generators)
        {
            WriteableBitmap mixedImage;

            // More than 1 generator in the list 
            if (generators.Count > 1)
            {
                mixedImage = generators[0].GenerateImage();

                for(int i = 0; i < generators.Count - 2; i++)
                {

                    switch (generators[i + 1].MixerType)
                    {
                        case MixerTypes.Add:
                            mixedImage = MixAdd(mixedImage, generators[i + 1].GenerateImage());
                            break;
                        case MixerTypes.Substract:
                            mixedImage = MixSubstract(mixedImage, generators[i + 1].GenerateImage());
                            break;
                        case MixerTypes.Multiply:
                            mixedImage = MixMultiply(mixedImage, generators[i + 1].GenerateImage());
                            break;
                        default:
                            mixedImage = MixAdd(mixedImage, generators[i + 1].GenerateImage());
                            break;
                    }
                }
            }
            // Only 1 generator in the list
            else if (generators.Count == 1)
            {
                mixedImage = generators[0].GenerateImage();
            }
            // No generator in the list 
            else
            {
                mixedImage = null;
            }

            return mixedImage;
        }


        #region mixer methods

        public WriteableBitmap MixAdd(WriteableBitmap inA, WriteableBitmap inB)
        {
            Rect cRect = new Rect(new Size(inA.Width,inA.Height));
            inA.Blit(cRect, inB, cRect, WriteableBitmapExtensions.BlendMode.Additive);

            return inA;
        }

        private WriteableBitmap MixSubstract(WriteableBitmap inA, WriteableBitmap inB)
        {
            Rect cRect = new Rect(new Size(inA.Width, inA.Height));
            inA.Blit(cRect, inB, cRect, WriteableBitmapExtensions.BlendMode.Subtractive);

            return inA;
        }

        private WriteableBitmap MixMultiply(WriteableBitmap inA, WriteableBitmap inB)
        {
            Rect cRect = new Rect(new Size(inA.Width, inA.Height));
            inA.Blit(cRect, inB, cRect, WriteableBitmapExtensions.BlendMode.Multiply);

            return inA;
        }
    


        private WriteableBitmap MixDivide(WriteableBitmap inA, WriteableBitmap inB)
        {

           throw new NotImplementedException();
        }

        private WriteableBitmap MixDifference(WriteableBitmap inA, WriteableBitmap inB)
        {

            throw new NotImplementedException();
        }

        private WriteableBitmap MixAND(WriteableBitmap inA, WriteableBitmap inB)
        {

            throw new NotImplementedException();
        }

        private WriteableBitmap MixOR(WriteableBitmap inA, WriteableBitmap inB)
        {

            throw new NotImplementedException();
        }

        private WriteableBitmap MixXOR(WriteableBitmap inA, WriteableBitmap inB)
        {

            throw new NotImplementedException();
        }
        #endregion

        #endregion
    }
}
