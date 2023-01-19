using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace PC_App.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class command_AddRib : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Начало команды (Окно потом закоментить)
            TaskDialog.Show("PinMessage", "Раздел в разработке (");

            //UIApplication uiApp = commandData.Application;
            //UIDocument uIdoc = uiApp.ActiveUIDocument;
            //Document document = uIdoc.Document;

            //FilteredElementCollector fec = new FilteredElementCollector(document);
            //IList<Element> symbols = new List<Element>();

            //symbols = fec.OfClass(typeof(FamilySymbol)).WhereElementIsElementType().ToElements();

            //FamilySymbol fs_ribPlate = null;

            //foreach (Element e in symbols)
            //{
            //    if (e.Name == "GRN_РеброЖесткости_R22")
            //    {
            //        fs_ribPlate = e as FamilySymbol;
            //        break;
            //    }
            //}

            //uIdoc.PostRequestForElementTypePlacement(fs_ribPlate);

            return Result.Succeeded;

        }
    }
}
