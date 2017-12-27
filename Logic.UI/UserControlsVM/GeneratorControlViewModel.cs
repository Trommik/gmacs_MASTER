
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Logic.UI.UserControlsVM
{
    using BaseTypes;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Threading;
    using Logic.UI.Models;
    using PropertyChanged;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class GeneratorControlViewModel : BaseViewModel
    {
        #region constructor and destructor

        public GeneratorControlViewModel()
        {

            // Create some default generators for Debugging.
            var startupGeneratorList = new List<Generator>
            {
                new ColorGenerator
                {
                    Title = "Color 1",
                    Speed = 50,
                    Level = 0,
 
                },
                new ColorGenerator
                {
                    Title = "Color 2",
                    Speed = 75,
                    Level = 75,
                }
            };

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
            //GeneratorsView.SortDescriptions.Add(new SortDescription("name of property to sort after", ListSortDirection.Ascending));

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

            GeneratorModelTypeChangedCommand = new RelayCommand<SelectionChangedEventArgs>((e) => GeneratorModelTypeChanged(e));

        }

        #endregion


        #region properties

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
        /// 
        /// </summary>
        public RelayCommand<SelectionChangedEventArgs> GeneratorModelTypeChangedCommand { get; }


        /// <summary>
        /// The type of the GeneratorModel Generator.
        /// </summary>
        public GeneratorInfo SelectedGeneratorType { get; set; }

        /// <summary>
        /// A IEnumerable with all <see cref="MixerTypes"/> for the MixerType Combobox
        /// </summary>
        public IEnumerable<MixerTypes> MixerTypesList { get => Enum.GetValues(typeof(MixerTypes)).Cast<MixerTypes>(); }

        /// <summary>
        /// A List with all availabel <see cref="GeneratorTypes"/> for the Types Combobox as <see cref="GeneratorInfo"/> object.
        /// </summary>
        public ObservableCollection<GeneratorInfo> GeneratorTypesList { get => GeneratorTypesExtensionMethods.InfosList; }

        /// <summary>
        /// The GeneratorType the GeneratorModel should be.
        /// </summary>
        public GeneratorTypes GeneratorType { get; set; }

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
        public ObservableCollection<Generator> Generators { get; }

        #endregion

        #region methods


        private void GeneratorModelTypeChanged(SelectionChangedEventArgs e)
        {
            // If the user selected a different type in the Combobox
            if (!SelectedGeneratorType.Equals(GeneratorModel.Info))
            {
                int modelIndex = Generators.IndexOf(GeneratorModel);

                Generators.RemoveAt(modelIndex);


                Generator newGen = SelectedGeneratorType.Create();

                // Add the new Generator to the list at the old position
                Generators.Insert(modelIndex, newGen);

                // Move the view to the same item again 
                GeneratorModel = newGen;
            }
        }


        /// <summary>
        /// Moves the <see cref="GeneratorModel"/> down in the List.
        /// </summary>
        public void MoveGeneratorDown()
        {
            int modelIndex = Generators.IndexOf(GeneratorModel);

            // Is already at the bottom!
            if (modelIndex == Generators.Count - 1)
                return;


            Generator temp = GeneratorModel;

            Generators[modelIndex] = Generators[modelIndex + 1];
            Generators[modelIndex + 1] = temp;

            // Set the view to the item we moved
            GeneratorModel = temp;
        }

        /// <summary>
        /// Moves the <see cref="GeneratorModel"/> up in the List.
        /// </summary>
        public void MoveGeneratorUp()
        {
            int modelIndex = Generators.IndexOf(GeneratorModel);

            // Is already at the top!
            if (modelIndex == 0)
                return;


            Generator temp = GeneratorModel;

            Generators[modelIndex] = Generators[modelIndex - 1];
            Generators[modelIndex - 1] = temp;

            // Set the view to the item we moved
            GeneratorModel = temp;
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
