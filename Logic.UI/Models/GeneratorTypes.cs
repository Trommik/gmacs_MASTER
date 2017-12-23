using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.Models
{
    /// <summary>
    /// Enum which holds every GeneratorType for TypeSafty.
    /// </summary>
    public enum GeneratorTypes : int
    {
        COLOR = 0,
        ANIMATED_GIF = 1,
        CAPTURE = 2,
        EXPANDING_OBJECTS = 3,
        FADE_AND_SCROLL = 4,
        FADING_PIXELS = 5,
        FALLING_OBJECTS = 6,
        FIRE = 7,
        FRACTAL = 8,
        GRID = 9,
        KNIGHT_RIDER = 10,
        META_BALLS = 11,
        OBJECTS_3D = 12,
        PLASMA = 13,
        RANDOM_PIXEL = 14,
        TEXT = 15,
        SIMPLE_SPECTRUM = 16,
        STROBO = 17,
        WAVE = 18,
    }

    /// <summary>
    /// Struct which holds all Infos about a GeneratorType.
    /// </summary>
    public struct GeneratorInfo
    {
        /// <summary>
        /// The type of the Generator
        /// </summary>
        public GeneratorTypes GenType { get; }

        /// <summary>
        /// Function which returns a new Generator of the given type
        /// </summary>
        public Func<Generator> Create { get; }

        /// <summary>
        /// The Text displayed for the Generator (the name).
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// A ToolTipText for the Generator.
        /// </summary>
        public string ToolTipText { get; }


        /// <summary>
        /// Constructor which auto fills the properties.
        /// </summary>
        /// <param name="type"> GeneratorType of the Generator. </param>
        /// <param name="text"> The "name" of the Generator. </param>
        /// <param name="toolTipText"> ToolTip Text for the Generator. </param>
        public GeneratorInfo(GeneratorTypes type, Func<Generator> createFunc, string text = null, string toolTipText = null)
        {
            // Set the type, name and tooltiptext of the GeneratorType.
            GenType = type;
            Create = createFunc;
            Text = text;
            ToolTipText = toolTipText;


            // If there is no specific text use the generator Type name.
            if (Text == null)
                Text = GenType.ToString("f"); ;

            // If there is no ToolTip use the name of the Generator.
            if (toolTipText == null)
                ToolTipText = Text;

        }
    }

    /// <summary>
    /// Extension method for the GeneratorType enum which trys to return the GeneratorInfo object associated to the enum GeneratorType.
    /// </summary>
    public static class GeneratorTypesExtensionMethods
    {

        /// <summary>
        /// Holds all Infos for each predefined GeneratorType.
        /// </summary>
        public static ObservableCollection<GeneratorInfo> InfosList = new ObservableCollection<GeneratorInfo>()
        {
            new GeneratorInfo(GeneratorTypes.COLOR, GetNew_ColorGenerator, "Color"),
            new GeneratorInfo(GeneratorTypes.ANIMATED_GIF, GetNew_TextGenerator, "Animated GIF"),
            new GeneratorInfo(GeneratorTypes.CAPTURE, GetNew_ColorGenerator, "Capture"),
            new GeneratorInfo(GeneratorTypes.EXPANDING_OBJECTS, GetNew_ColorGenerator, "Expanding Objects"),
            new GeneratorInfo(GeneratorTypes.FADE_AND_SCROLL, GetNew_ColorGenerator, "Fade and Scroll"),
            new GeneratorInfo(GeneratorTypes.FADING_PIXELS, GetNew_ColorGenerator, "Fading Pixels"),
            new GeneratorInfo(GeneratorTypes.FRACTAL, GetNew_ColorGenerator, "Fractal"),
            new GeneratorInfo(GeneratorTypes.FALLING_OBJECTS, GetNew_ColorGenerator, "Falling Objects"),
            new GeneratorInfo(GeneratorTypes.FIRE, GetNew_ColorGenerator, "Fire"),
            new GeneratorInfo(GeneratorTypes.GRID, GetNew_ColorGenerator, "Grid"),
            new GeneratorInfo(GeneratorTypes.KNIGHT_RIDER, GetNew_ColorGenerator, "Knight Rider"),
            new GeneratorInfo(GeneratorTypes.META_BALLS, GetNew_ColorGenerator, "Meta Balls"),
            new GeneratorInfo(GeneratorTypes.OBJECTS_3D, GetNew_ColorGenerator, "Objects 3D"),
            new GeneratorInfo(GeneratorTypes.PLASMA, GetNew_ColorGenerator, "Plasma"),
            new GeneratorInfo(GeneratorTypes.RANDOM_PIXEL, GetNew_ColorGenerator, "Random Pixels"),
            new GeneratorInfo(GeneratorTypes.TEXT, GetNew_TextGenerator, "Text"),
            new GeneratorInfo(GeneratorTypes.SIMPLE_SPECTRUM, GetNew_ColorGenerator, "Simple Spectrum"),
            new GeneratorInfo(GeneratorTypes.STROBO, GetNew_ColorGenerator, "Strobo"),
            new GeneratorInfo(GeneratorTypes.WAVE, GetNew_ColorGenerator, "Wave"),
        };


        /// <summary>
        /// Returns the GeneratorInfo object associated to the given GeneratorType.
        /// </summary>
        /// <param name="type"> The generatorType the Info is for. </param>
        /// <returns> The GeneratorInfo for the GeneratorType. </returns>
        public static GeneratorInfo GetInfo(this GeneratorTypes type)
        {
            // Try to get the info object
            return InfosList.Single(x => x.GenType == type);
        }


        #region 

       
        private static Generator GetNew_ColorGenerator()
        {
            return new ColorGenerator();
        }

        private static Generator GetNew_TextGenerator()
        {
            return new TextGenerator();
        }
        #endregion

    }


}
