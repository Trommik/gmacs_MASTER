
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Logic.UI
{
    using BaseTypes;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Threading;
    using Logic.UI.Models;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Data;

    public class GeneratorControlViewModel : BaseViewModel
    {
        #region Constructor and Destructor

        public GeneratorControlViewModel()
        {

            // Create some default generators for Debugging.
            var startupGeneratorList = new List<Generator>();

            startupGeneratorList.Add(
                new TextGenerator
                {
                    Title = "Text",
                    Type = GeneratorTypes.TEXT,
                    Speed = 50,
                    Level = 0,
                });
            startupGeneratorList.Add(
                new ColorGenerator
                {
                    Title = "Color",
                    Type = GeneratorTypes.COLOR,
                    Speed = 75,
                    Level = 75,
                });

            // Add them to the real list.
            Generators = new ObservableCollection<Generator>(startupGeneratorList);


            // Gets the View from the list.
            GeneratorsView = CollectionViewSource.GetDefaultView(Generators) as ListCollectionView;

            // Event handler if the Current item changes.
            GeneratorsView.CurrentChanged += (s, e) =>
            {
                RaisePropertyChanged(() => GeneratorModel);
            };

            // Sorts the List
            GeneratorsView.SortDescriptions.Clear();
            GeneratorsView.SortDescriptions.Add(new SortDescription(nameof(GeneratorModel.Type), ListSortDirection.Ascending));

            // Event handler if a property of a item changes.
            foreach (var item in Generators)
            {
                item.PropertyChanged += GeneratorsOnPropertyChanged;
            }

            // Event handler if a item gets added or removed.
            Generators.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    foreach (INotifyPropertyChanged added in e.NewItems)
                    {
                        added.PropertyChanged += GeneratorsOnPropertyChanged;
                    }
                }
                if (e.OldItems != null)
                {
                    foreach (INotifyPropertyChanged removed in e.OldItems)
                    {
                        removed.PropertyChanged -= GeneratorsOnPropertyChanged;
                    }
                }
            };


            // Inits commands for adding and deleting Generators
            MoveGeneratorDownCommand = new RelayCommand(() => MoveGeneratorDown());
            MoveGeneratorUpCommand = new RelayCommand(() => MoveGeneratorUp());
            DeleteGeneratorCommand = new RelayCommand(() => Generators.Remove(GeneratorModel));
            AddNewGeneratorCommand = new RelayCommand(() => Generators.Add(new ColorGenerator()));

        }

        #endregion


        #region Propertyies

        /// <summary>
        /// Moves the <see cref="GeneratorModel"/> down in the List.
        /// </summary>
        public RelayCommand MoveGeneratorDownCommand { get; }

        /// <summary>
        /// Moves the <see cref="GeneratorModel"/> up in the List.
        /// </summary>
        public RelayCommand MoveGeneratorUpCommand { get; }

        /// <summary>
        /// Deletes the <see cref="GeneratorModel"/> from the <see cref="Generators"/> List.
        /// </summary>
        public RelayCommand DeleteGeneratorCommand { get; }

        /// <summary>
        /// Duplicates the selected <see cref="GeneratorModel"/> in the <see cref="Generators"/> List.
        /// </summary>
        public RelayCommand AddNewGeneratorCommand { get; }



        /// <summary>
        /// A IEnumerable with all <see cref="MixerTypes"/> for the MixerType Combobox
        /// </summary>
        public IEnumerable<MixerTypes> MixerTypesList { get => Enum.GetValues(typeof(MixerTypes)).Cast<MixerTypes>(); }

        /// <summary>
        /// A List with all availabel <see cref="GeneratorTypes"/> for the Types Combobox as <see cref="GeneratorInfo"/> object.
        /// </summary>
        public ObservableCollection<GeneratorInfo> GeneratorTypesList { get => GeneratorTypesExtensionMethods.InfosList; }

        /// <summary>
        /// A <see cref="Generator"/> to edit.
        /// </summary>
        public Generator GeneratorModel
        {
            get => GeneratorsView.CurrentItem as Generator;
            set
            {
                GeneratorsView.MoveCurrentTo(value);
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// The view for binding the UI against the <see cref="Generator"/>.
        /// </summary>
        public ListCollectionView GeneratorsView { get; }

        /// <summary>
        /// A list of <see cref="Generator"/>.
        /// </summary>
        private ObservableCollection<Generator> Generators { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Moves the <see cref="GeneratorModel"/> down in the List.
        /// </summary>
        public void MoveGeneratorDown()
        {

        }

        /// <summary>
        /// Moves the <see cref="GeneratorModel"/> up in the List.
        /// </summary>
        public void MoveGeneratorUp()
        {

        }

       
        /// <summary>
        /// Event handler for property changes on elements of <see cref="Generator"/>.
        /// </summary>
        /// <param name="sender">The Generator model.</param>
        /// <param name="e">The event arguments.</param>
        private void GeneratorsOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GeneratorModel.HasErrors) || e.PropertyName == nameof(GeneratorModel.IsOk))
            {
                return;
            }
            if (GeneratorsView.IsEditingItem || GeneratorsView.IsAddingNew)
            {
                return;
            }
            GeneratorsView.Refresh();
        }

        #endregion





    }
}
