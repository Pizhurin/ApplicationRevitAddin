using Autodesk.Revit.UI;
using PC_App.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PC_App.View
{
    /// <summary>
    /// Interaction logic for View_CountElements.xaml
    /// </summary>
    public partial class View_CountElements : Window
    {
        public View_CountElements(ExternalCommandData externalCommand)
        {
            InitializeComponent();

            ViewModel_CountElements vmCountElements= new ViewModel_CountElements(externalCommand);
            DataContext= vmCountElements;
        }
    }
}
