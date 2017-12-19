using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;

namespace Logic.UI.BaseTypes
{


    /// <summary>
    /// Abstract base class for all view models.
    /// </summary>
    public abstract class BaseViewModel : ViewModelBase
    {
        #region constructors and destructors

        public BaseViewModel()
        {
            if (!IsInDesignModeStatic && !IsInDesignMode)
            {
                DispatcherHelper.Initialize();
            }
        }

        #endregion

        #region properties

        /// <summary>
        /// The caption of the window.
        /// </summary>
        public string Title { get; protected set; }

        #endregion
    }
}
