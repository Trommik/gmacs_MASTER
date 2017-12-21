using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.Models
{
    using BaseTypes;
    using System.Collections.ObjectModel;

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

        #endregion
    }
}
