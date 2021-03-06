﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.Models
{
    using BaseTypes;
    using System.Windows.Media.Imaging;

    public abstract class Generator : BaseModel
    {
        #region mvvm

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

        #endregion

        #region constructor and destructor

        public Generator()
        {
            Title = "New Generator";
        }

        #endregion

        #region properties

        /// <summary>
        /// The Info of the Generator
        /// </summary>
        public GeneratorInfo Info { get => Type.GetInfo(); protected set => Type = value.GenType; }

        /// <summary>
        /// The Type of the generator.
        /// </summary>
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Generator Type must not be empty.")] // Error Checking
        //[MaxLength(20, ErrorMessage = "Maximum of 20 characters is allowed.")] // Error Checking
        public GeneratorTypes Type { get; protected set; }

        /// <summary>
        /// The MixMode of the generator.
        /// </summary>
        public MixerTypes MixerType { get; set; }

        /// <summary>
        /// The Speed of the generator.
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// The Level of the generator.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The Title of the Generator.
        /// </summary>
        public string Title { get; set; }


        #endregion

        #region methods

        /// <summary>
        /// Implement the logic to generate the image with the desired Size.
        /// </summary>
        public abstract WriteableBitmap GenerateImage();

        #endregion

    }
}
