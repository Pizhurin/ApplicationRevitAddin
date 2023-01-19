using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;


namespace PC_App.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class LoadWorksetFromExcel : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uIdoc = uiapp.ActiveUIDocument;
            Document document = uIdoc.Document;
            Selection selection = uIdoc.Selection;



            #region Добавление рабочих наборов из файла .xlsx
            //Открыть и добавить в список все рабочие наборы
            List<string> worksetsExcel = new List<string>();
            List<string> worksetDocument = new List<string>();
            string path = string.Empty;
            // Рабочие наборы в проекте
            FilteredWorksetCollector worksetCollector =
                    new FilteredWorksetCollector(document);
            worksetCollector.OfKind(WorksetKind.UserWorkset);
            //Словарь для проверки уже существующих наборов

            foreach (var workset in worksetCollector)
            {
                worksetDocument.Add(workset.Name);
            }

            // Создание приложения Excel
            Excel.Application application = new Excel.Application();
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel files|*.xls;*.xlsx";
                Nullable<bool> fileExists = openFileDialog.ShowDialog();
                if (fileExists == true)
                {
                    path = openFileDialog.FileName;
                }

                Workbook workbook = application.Workbooks.Open(path);
                Worksheet worksheet = workbook.Worksheets[1]; 

                Range usedColumn = worksheet.UsedRange.Columns[1];
                Array myvalues = (Array)usedColumn.Cells.Value2;
                string[] worksets = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();
                foreach (string ws in worksets)
                {
                    worksetsExcel.Add(ws);
                }

                application.Quit();
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error open Excel", ex.Message);
            }

            try
            {
                if (worksetDocument.Count > 0)
                {
                    using (Transaction t = new Transaction(document))
                    {
                        t.Start("AddWorksets");
                        foreach (string ws in worksetsExcel)
                        {
                            if (!worksetDocument.Contains(ws))
                            {
                                Workset.Create(document, ws);
                            }
                        }
                        t.Commit();
                    }
                    TaskDialog.Show("Successfull", "Рабочие наборы успешно добавлены");
                }
                else
                {
                    TaskDialog.Show("Error create worksets", "Рабочие наборы не добавлены. Не организована совместная работа");
                }

            }
            catch
            {
                TaskDialog.Show("Error create worksets", "Рабочие наборы не добавлены. Не организована совместная работа");
            }
            #endregion


            return Result.Succeeded;
        }
    }
}
