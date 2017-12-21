using GalaSoft.MvvmLight;

namespace Logic.UI
{
    using BaseTypes;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : BaseViewModel
    {

        #region const

        /// <summary>
        /// Static instance of the left generator control ViewModel.
        /// </summary>
        readonly static GeneratorControlViewModel _leftGeneratorVM = new GeneratorControlViewModel();

        /// <summary>
        /// Static instance of the right generator control ViewModel.
        /// </summary>
        readonly static GeneratorControlViewModel _rightGeneratorVM = new GeneratorControlViewModel();



        #endregion

        #region constructors and destructors

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // code runs in blend --> create design time data.
                Title = "GMaCS (Designmode)";

                LeftGeneratorVM = _leftGeneratorVM;
                RightGeneratorVM = _rightGeneratorVM;
            }
            else
            {
                // code runs "for real"
                Title = "GMaCS";

                LeftGeneratorVM = _leftGeneratorVM;
                RightGeneratorVM = _rightGeneratorVM;

                GenerateImageCommand = new RelayCommand(() => GenerateImage());

            }
        }


        #endregion


        #region properties

        /// <summary>
        /// The setter is private since only this 
        /// class can change the view via a command.  If the View is changed,
        /// we need to raise a property changed event (via INPC).
        /// </summary>
        public GeneratorControlViewModel LeftGeneratorVM { get; private set; }
        public GeneratorControlViewModel RightGeneratorVM { get; private set; }


        public RelayCommand GenerateImageCommand { get; }

        public WriteableBitmap OutputBitmap { get; private set; } 

        #endregion


        #region methods

        public void GenerateImage()
        {
            // #TODO: IMPLEMENT LEFT/RIGHT CH MIXER
            OutputBitmap = LeftGeneratorVM.GeneratorModel.GenerateImage();
        }

        #endregion
    }
}