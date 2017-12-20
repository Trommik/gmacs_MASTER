/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:UI.Desktop"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Logic.UI
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        #region constructors and destructors

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
            }
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<GeneratorControlViewModel>();


        }

        #endregion

        #region methods

        /// <summary>
        /// Cleans up resources.
        /// </summary>
        public static void Cleanup()
        {
        }

        #endregion

        #region properties

        /// <summary>
        /// Retrieves the view model for the main view.
        /// </summary>
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        /// <summary>
        /// Retrieves the view model for the child view.
        /// </summary>
        public GeneratorControlViewModel Generator => ServiceLocator.Current.GetInstance<GeneratorControlViewModel>();

        #endregion
    }
}