using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Desktop
{
    using GalaSoft.MvvmLight.Messaging;

    using Logic.UI;    

    /// <summary>
    /// Central listenere for all messages of the app.
    /// </summary>
    public class MessageListener
    {
        #region Constructors and Destructors

        public MessageListener()
        {
            InitMessenger();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Is called by the constructor to define the messages we are interested in.
        /// </summary>
        private void InitMessenger()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// We need this property so that this type can be put into the ressources.
        /// </summary>
        public bool BindableProperty => true;

        #endregion
    }
}
