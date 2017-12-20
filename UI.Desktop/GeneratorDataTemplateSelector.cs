using Logic.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UI.Desktop
{
    public class GeneratorDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate dataTemplate = null;

            if (item is ColorGenerator)
                dataTemplate = Application.Current.MainWindow.FindResource(typeof(ColorGenerator)) as DataTemplate;

            if (item is TextGenerator)
                dataTemplate = Application.Current.MainWindow.FindResource(typeof(TextGenerator)) as DataTemplate;


            return dataTemplate;
        }
    }
}
