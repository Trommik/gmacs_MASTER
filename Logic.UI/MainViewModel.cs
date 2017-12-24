using GalaSoft.MvvmLight;

namespace Logic.UI
{
    using BaseTypes;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using UserControlsVM;
    using System;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

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

        /// <summary>
        /// Static instance of the output Matix Viewport ViewModel.
        /// </summary>
        readonly static MatrixViewportViewModel _leftMatrixViewportVM = new MatrixViewportViewModel("Left Mixer Matrix");

        /// <summary>
        /// Static instance of the output Matix Viewport ViewModel.
        /// </summary>
        readonly static MatrixViewportViewModel _outputMatrixViewportVM = new MatrixViewportViewModel("Output Matrix");

        /// <summary>
        /// Static instance of the output Matix Viewport ViewModel.
        /// </summary>
        readonly static MatrixViewportViewModel _rightMatrixViewportVM = new MatrixViewportViewModel("Right Mixer Matrix");

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

                InitViewModels();

                DispatcherTimer timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(1000)
                };

                timer.Tick += timer_Tick;
                timer.Start();

            }
            else
            {
                // code runs "for real"
                Title = "GMaCS";

                InitViewModels();

                GenerateImageCommand = new RelayCommand(() => GenerateImage());

                DispatcherTimer timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromTicks(100)
                };
                timer.Tick += timer_Tick;
                timer.Start();

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

        public MatrixViewportViewModel LeftMatrixViewportVM { get; private set; }
        public MatrixViewportViewModel OutputMatrixViewportVM { get; private set; }
        public MatrixViewportViewModel RightMatrixViewportVM { get; private set; }

        public Color MatrixColor { get; set; }


        public RelayCommand GenerateImageCommand { get; }

        #endregion


        #region methods

        void timer_Tick(object sender, EventArgs e)
        {
            GenerateImage();
        }


        public void GenerateImage()
        {
            if (LeftGeneratorVM.GeneratorModel == null)
                return;

            // #TODO: IMPLEMENT LEFT/RIGHT CH MIXER
            LeftMatrixViewportVM.UpdateImage(LeftGeneratorVM.GeneratorModel.GenerateImage());

            Mixer m = new Mixer();
            RightMatrixViewportVM.UpdateImage(m.MixList(RightGeneratorVM.Generators));
        }

        private void InitViewModels()
        {
            LeftGeneratorVM = _leftGeneratorVM;
            RightGeneratorVM = _rightGeneratorVM;

            LeftMatrixViewportVM = _leftMatrixViewportVM;
            OutputMatrixViewportVM = _outputMatrixViewportVM;
            RightMatrixViewportVM = _rightMatrixViewportVM;

        }


        #endregion
    }
}