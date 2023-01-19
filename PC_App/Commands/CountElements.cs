using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using PC_App.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PC_App.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CountElements : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            View_CountElements viewCountElements = new View_CountElements(commandData);
            viewCountElements.Height = 200;
            viewCountElements.Width = 350;
            viewCountElements.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            viewCountElements.ShowDialog();

            return Result.Succeeded;
        }
    }
}
