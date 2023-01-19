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
    internal class command_Background : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            Document document = uiApp.ActiveUIDocument.Document;

            try
            {
                using (Transaction tr = new Transaction(document))
                {
                    tr.Start("CreateNewTextNote");

                    //Создание Background для текущего вида. Работает только с 3D и сечениями (не работает с планами)
                    View view = uiApp.ActiveUIDocument.ActiveView;
                    ColorSelectionDialog csd = new ColorSelectionDialog();
                    csd.Show();
                    Color c1 = csd.SelectedColor;
                    csd.Show();
                    Color c2 = csd.SelectedColor;
                    csd.Show();
                    Color c3 = csd.SelectedColor;
                    view.SetBackground(ViewDisplayBackground.CreateGradient(c1, c2, c3));
                    tr.Commit();
                }
            }
            catch(Exception ex)
            {

            }         

            return Result.Succeeded;
        }
    }
}
