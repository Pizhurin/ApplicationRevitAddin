using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Microsoft.Win32;
using PC_App.Converts;
using PC_App.Elements;
using PC_App.Windows;
using System.IO;
//using Excel = Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Interop.Excel;
using Parameter = Autodesk.Revit.DB.Parameter;
using PC_App.Constants;
using PC_App.Updaters;

namespace PC_App.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class command_Test : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uIdoc = uiapp.ActiveUIDocument;
            Document document = uIdoc.Document;
            Selection selection = uIdoc.Selection;
            Reference reference = null;
            //reference = selection.PickObject(ObjectType.Element);
            //Element element = document.GetElement(reference);


            #region Выбрать элемент, изолировать и сфокусироваться
            ////Выбрать элемент, изолировать и сфокусироваться
            //UIApplication uiApp = commandData.Application;
            //PC_SelectElement selectElements = new PC_SelectElement(uiApp);
            //selectElements.IsolateSelectedElements();
            //selectElements.FocusSelectedElements();
            #endregion


            #region Задание рамкой определенного BoundingBoxXYZ (cropBox)
            //    View view = commandData.View;
            //    string mes = "View: ";

            //    //get the name of the view
            //    mes += "\nView name: " + view.Name;

            //    PickedBox pickedBox = selection.PickBox(PickBoxStyle.Enclosing, "111");
            //    // Max - это первая точка (а не верх право, как в описании Revit DOCS)
            //    XYZ pb_max = pickedBox.Max;
            //    // Min - это вторая точка (а не низ лево, как в описании Revit DOCS)
            //    XYZ pb_min = pickedBox.Min;


            //    //В наборах полей обрезки отображаются границы представления
            //    BoundingBoxXYZ cropBox = view.CropBox;

            //    using (Transaction tr = new Transaction(document))
            //    {
            //        tr.Start("start test");

            //        // ОЧЕНЬ ВАЖНО Активировать грани подрезки, т.к. с отключенным видом (откл. желтая лампа) вылетает ошибка
            //        view.CropBoxActive = true;


            //        // ПЕРЕДАТЬ ПРАВИЛЬНО КООРДИНАТЫ в p1, p2, p3, p4
            //        List<Curve> nl = new List<Curve>();
            //        XYZ point1 = PC_S_ConvertXYZ.FromValue(-10000, 5000, 0);
            //        //XYZ point1 = new XYZ (-pb_max.X, pb_max.Y, 0);
            //        XYZ point2 = PC_S_ConvertXYZ.FromValue(10000, 5000, 0);
            //        //XYZ point2 = new XYZ (pb_max.X, pb_max.Y, 0);
            //        XYZ point3 = PC_S_ConvertXYZ.FromValue(10000, -5000, 0);
            //        //XYZ point3 = new XYZ (-pb_min.X, pb_min.Y, 0);
            //        XYZ point4 = PC_S_ConvertXYZ.FromValue(-10000, -5000, 0);
            //        //XYZ point4 = new XYZ (pb_min.X, pb_min.Y, 0);

            //        nl.Add(Line.CreateBound(point1, point2));
            //        nl.Add(Line.CreateBound(point2, point3));
            //        nl.Add(Line.CreateBound(point3, point4));
            //        nl.Add(Line.CreateBound(point4, point1));

            //        CurveLoop cl = CurveLoop.Create(nl);
            //        ViewCropRegionShapeManager vpcr = view.GetCropRegionShapeManager();
            //        bool cropValid = vpcr.IsCropRegionShapeValid(cl);
            //        if (cropValid)
            //        {
            //            vpcr.SetCropShape(cl);                    
            //        }


            //        tr.Commit();
            //    }

            //    //
            //        List<Element> listFamilyInstances = new FilteredElementCollector(document, document.ActiveView.Id).ToList();
            //        IList<ElementId> elID = new List<ElementId>(); 
            //        foreach (Element element in listFamilyInstances)
            //        {
            //            if(element.Name == view.Name)
            //            {
            //                elID.Add(element.Id);
            //            }
            //        }                
            //        uIdoc.Selection.SetElementIds(elID);
            //        uIdoc.ShowElements(elID);
            //        uIdoc.RefreshActiveView();

            //    //


            //    XYZ max = cropBox.Max; //Maximum coordinates (upper-right-front corner of the box).
            //    XYZ min = cropBox.Min; //Minimum coordinates (lower-left-rear corner of the box).
            //    mes += "\nCrop Box: ";
            //    mes += "\nMaximum coordinates: (" + max.X + ", " + max.Y + ", " + max.Z + ")";
            //    mes += "\nMinimum coordinates: (" + min.X + ", " + min.Y + ", " + min.Z + ")";


            //    //get the origin of the screen
            //    XYZ origin = view.Origin;
            //    mes += "\nOrigin: (" + origin.X + ", " + origin.Y + ", " + origin.Z + ")";


            //    //The bounds of the view in paper space (in inches).
            //    BoundingBoxUV outline = view.Outline;
            //    UV maxUv = outline.Max; //Maximum coordinates (upper-right corner of the box).
            //    UV minUv = outline.Min; //Minimum coordinates (lower-left corner of the box).
            //    mes += "\nOutline: ";
            //    mes += "\nMaximum coordinates: (" + maxUv.U + ", " + maxUv.V + ")";
            //    mes += "\nMinimum coordinates: (" + minUv.U + ", " + minUv.V + ")";

            //    //The direction towards the right side of the screen
            //    XYZ rightDirection = view.RightDirection;
            //    mes += "\nRight direction: (" + rightDirection.X + ", " +
            //                   rightDirection.Y + ", " + rightDirection.Z + ")";

            //    //The direction towards the top of the screen
            //    XYZ upDirection = view.UpDirection;
            //    mes += "\nUp direction: (" + upDirection.X + ", " +
            //                   upDirection.Y + ", " + upDirection.Z + ")";

            //    //The direction towards the viewer
            //    XYZ viewDirection = view.ViewDirection;
            //    mes += "\nView direction: (" + viewDirection.X + ", " +
            //                   viewDirection.Y + ", " + viewDirection.Z + ")";

            //    //The scale of the view
            //    mes += "\nScale: " + view.Scale;
            //    //// Scale is meaningless for Schedules
            //    //if (view.ViewType != ViewType.Schedule)
            //    //{
            //    //    int testScale = 5;
            //    //    //set the scale of the view
            //    //    view.Scale = testScale;
            //    //    mes += "\nScale after set: " + view.Scale;
            //    //}

            //    TaskDialog.Show("Revit", mes);
            #endregion


            #region Выбор пересекающих элементов (для нахождения количества светильников в помещении) _ Для одного элемента
            //try
            //{

            //    reference = uIdoc.Selection.PickObject(
            //        ObjectType.Element, "Выберите элемент, который нужно проверить на перечение со всеми экземплярами семейства");

            //    Element e = document.GetElement(reference);

            //    GeometryElement geomElement = e.get_Geometry(new Options());

            //    Solid solid = null;
            //    foreach (GeometryObject geomObj in geomElement)
            //    {
            //        solid = geomObj as Solid;
            //        if (solid != null) break;
            //    }

            //    //ElementIntersectsSolidFilter filterSolid = new ElementIntersectsSolidFilter(solid);

            //    FilteredElementCollector collector
            //      = new FilteredElementCollector(document)
            //        .OfClass(typeof(FamilyInstance))
            //        .WherePasses(new ElementIntersectsSolidFilter(
            //          solid));

            //    string result = string.Empty;            
            //    string key = string.Empty;

            //    Dictionary<string, int> dictionary = new Dictionary<string, int>();
            //    foreach (Element element in collector)
            //    {
            //        ParameterSet ps = document.GetElement(element.GetTypeId()).Parameters;
            //        foreach(Parameter p in ps)
            //        {
            //            if(p.Definition.Name == "ADSK_Марка")
            //            {
            //                key = p.AsValueString();
            //            }
            //        }                
            //        if (dictionary.ContainsKey(key))
            //        {
            //            dictionary[key] +=1; 
            //        }
            //        if (!dictionary.ContainsKey(key))
            //        {
            //            dictionary.Add(key, 1);
            //        }
            //    }
            //    foreach (var item in dictionary)
            //    {
            //        result += item.Key + " - " + item.Value + " шт.\n";
            //    }
            //    //TaskDialog.Show("Revit", collector.Count() +
            //    //  " экземпляров семейства <<" + name + ">> пересекаются с выбранным элементом ("
            //    //  + e.Category.Name + "ID:" + e.Id + ")");

            //    //TaskDialog.Show("Revit", collector.Count() +" экземпляров: \n" 
            //    //    + result);


            //    using(Transaction tr = new Transaction(document))
            //    {
            //        tr.Start("test add parameter into ADSK_Примечание");
            //        ParameterSet parameterSet =  e.Parameters;
            //        foreach(Parameter p in parameterSet)
            //        {
            //            if(p.Definition.Name == "ADSK_Примечание")
            //            {
            //                p.Set(result);
            //            }

            //        }
            //        tr.Commit();
            //    }
            //}
            //catch
            //{

            //}
            #endregion


            #region Выбор пересекающих элементов (для нахождения светильников в помещении) _ Для нескольких по предварительному выбору в модели
            //try
            //{
            //    ICollection<ElementId> elementsRooms = selection.GetElementIds();

            //    foreach (ElementId elementId in elementsRooms)
            //    {
            //        Element e = document.GetElement(elementId);

            //        GeometryElement geomElement = e.get_Geometry(new Options());

            //        Solid solid = null;
            //        foreach (GeometryObject geomObj in geomElement)
            //        {
            //            solid = geomObj as Solid;
            //            if (solid != null) break;
            //        }

            //        //ElementIntersectsSolidFilter filterSolid = new ElementIntersectsSolidFilter(solid);

            //        FilteredElementCollector collector
            //          = new FilteredElementCollector(document)
            //            .OfClass(typeof(FamilyInstance))
            //            .OfCategory(BuiltInCategory.OST_LightingFixtures)
            //            .WherePasses(new ElementIntersectsSolidFilter(
            //              solid));

            //        string result = string.Empty;
            //        string key = string.Empty;

            //        Dictionary<string, int> dictionary = new Dictionary<string, int>();
            //        foreach (Element element in collector)
            //        {
            //            ParameterSet ps = document.GetElement(element.GetTypeId()).Parameters;
            //            foreach (Parameter p in ps)
            //            {
            //                if (p.Definition.Name == "ADSK_Марка")
            //                {
            //                    key = p.AsValueString();
            //                }
            //            }
            //            if (dictionary.ContainsKey(key))
            //            {
            //                dictionary[key] += 1;
            //            }
            //            if (!dictionary.ContainsKey(key))
            //            {
            //                dictionary.Add(key, 1);
            //            }
            //        }
            //        foreach (var item in dictionary)
            //        {
            //            result += item.Key + " - " + item.Value + " шт.\n";
            //        }
            //        //TaskDialog.Show("Revit", collector.Count() +
            //        //  " экземпляров семейства <<" + name + ">> пересекаются с выбранным элементом ("
            //        //  + e.Category.Name + "ID:" + e.Id + ")");

            //        //TaskDialog.Show("Revit", collector.Count() +" экземпляров: \n" 
            //        //    + result);

            //        using (Transaction tr = new Transaction(document))
            //        {
            //            tr.Start("test add parameter into ADSK_Примечание");
            //            ParameterSet parameterSet = e.Parameters;
            //            foreach (Parameter p in parameterSet)
            //            {
            //                if (p.Definition.Name == "ADSK_Примечание")
            //                {
            //                    p.Set(result);
            //                }
            //            }
            //            tr.Commit();
            //        }
            //    }

            //}
            //catch
            //{
            //    TaskDialog.Show("Error", "Походу, что-то пошло не по плану )");
            //}
            #endregion


            #region Работа с рабочими наборами (в т.ч. связанного файла)

            //string info_str = string.Empty;
            //// Получить рабочие наборы текущего документа
            //FilteredWorksetCollector worksetCollector =
            //        new FilteredWorksetCollector(document);

            //worksetCollector.OfKind(WorksetKind.UserWorkset);
            //foreach (var ws in worksetCollector)
            //{
            //    info_str += "Link: " + ws.Name + "\n";
            //}
            //TaskDialog.Show("InfoWorkset", "This document contain any worksets: \n" + info_str);


            //// Получить документы линкованых файлов (ххх.rvt)
            //var categoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_RvtLinks);
            //var typeFilter = new ElementClassFilter(typeof(Instance));
            //var logicalFilter = new LogicalAndFilter(categoryFilter, typeFilter);

            //var collector = new FilteredElementCollector(document);

            //var linkInstances = collector
            //        .WherePasses(logicalFilter)
            //        .OfType<RevitLinkInstance>();

            //linkInstances.Where(x => RevitLinkType.IsLoaded(document, x.GetTypeId())).Select(x => x.Document);

            //info_str = string.Empty;
            //Document tempDocument = null;
            //foreach (var linkInstance in linkInstances)
            //{                
            //    info_str += "Link - " + linkInstance.Name + "contains other worksets:\n";
            //    // Получить документ рабочего набора КМ и получить его рабочие наборы
            //    tempDocument = linkInstance.GetLinkDocument();
            //    if(tempDocument != null)
            //    {
            //        FilteredWorksetCollector fwsc = new FilteredWorksetCollector(tempDocument);
            //        fwsc.OfKind(WorksetKind.UserWorkset);

            //        foreach (var ws in fwsc)
            //        {
            //            info_str += "\t- " + ws.Name + "\n";
            //        }
            //    }

            //    info_str += "*\t*\t*\t\t*\t*\t*\t*\t*\t*\t*\t*\t*\t*\t*\t*\t*\t*\t*\t*\t\n";

            //    //if (linkInstance.Id.IntegerValue == 945129)
            //    //{
            //    //    // Получить документ рабочего набора КМ и получить его рабочие наборы
            //    //    Document docKM = linkInstance.GetLinkDocument();
            //    //    FilteredWorksetCollector fwsc = new FilteredWorksetCollector(docKM);
            //    //    fwsc.OfKind(WorksetKind.UserWorkset);

            //    //    foreach(var ws in fwsc)
            //    //    {
            //    //        if (ws.Name == "KM_FrameПРК")
            //    //        {
            //    //            TaskDialog.Show("123", "Workset is KM_FrameПРК");
            //    //            //document.ActiveView.SetWorksetVisibility(ws.Id, WorksetVisibility.Hidden);
            //    //        }
            //    //    }
            //    //}
            //}

            //TaskDialog.Show("infobylinksfiles", info_str);
            #endregion


            #region Добавление рабочих наборов из файла .xlsx
            ////Открыть и добавить в список все рабочие наборы
            //List<string> worksetsExcel = new List<string>();
            //List<string> worksetDocument = new List<string>();
            //string path = string.Empty;
            //// Рабочие наборы в проекте
            //FilteredWorksetCollector worksetCollector =
            //        new FilteredWorksetCollector(document);
            //worksetCollector.OfKind(WorksetKind.UserWorkset);
            ////Словарь для проверки уже существующих наборов

            //foreach (var workset in worksetCollector)
            //{
            //    worksetDocument.Add(workset.Name);
            //}            

            //// Создание приложения Excel
            //Excel.Application application = new Excel.Application();
            //try
            //{
            //    OpenFileDialog openFileDialog = new OpenFileDialog();
            //    openFileDialog.Filter = "Excel files|*.xls;*.xlsx";
            //        Nullable<bool> fileExists = openFileDialog.ShowDialog();
            //    if (fileExists==true)
            //    {
            //        path = openFileDialog.FileName;
            //    }

            //    Workbook workbook = application.Workbooks.Open(path);
            //    Worksheet worksheet = workbook.Worksheets[1];

            //    Range usedColumn = worksheet.UsedRange.Columns[1];
            //    Array myvalues = (Array)usedColumn.Cells.Value2;
            //    string[] worksets = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();
            //    foreach(string ws in worksets)
            //    {
            //        worksetsExcel.Add(ws);
            //    }

            //    application.Quit();
            //}
            //catch (Exception ex)
            //{
            //    TaskDialog.Show("Error open Excel", ex.Message);
            //}

            //try
            //{
            //    if (worksetDocument.Count > 0)
            //    {
            //        using (Transaction t = new Transaction(document))
            //        {
            //            t.Start("AddWorksets");
            //            foreach (string ws in worksetsExcel)
            //            {
            //                if (!worksetDocument.Contains(ws))
            //                {
            //                    Workset.Create(document, ws);
            //                }                        
            //            }
            //            t.Commit();
            //        }
            //        TaskDialog.Show("Successfull", "Рабочие наборы успешно добавлены");
            //    }
            //    else
            //    {
            //        TaskDialog.Show("Error create worksets", "Рабочие наборы не добавлены. Не организована совместная работа");
            //    }

            //}
            //catch
            //{
            //    TaskDialog.Show("Error create worksets", "Рабочие наборы не добавлены. Не организована совместная работа");
            //}
            #endregion


            #region Получение слоев стены
            //Element element = document.GetElement(reference);
            //string s = string.Empty;
            //TaskDialog.Show("InfoAPI", element.Id.ToString());

            //if ((BuiltInCategory)element.Category.Id.IntegerValue == BuiltInCategory.OST_Roofs)
            //{
            //    RoofBase roof = element as RoofBase;
            //    CompoundStructure cs = roof.RoofType.GetCompoundStructure();
            //    IList<CompoundStructureLayer> list_cs = cs.GetLayers();

            //    string res = string.Empty;
            //    double width = 0.0;
            //    foreach (CompoundStructureLayer layer in list_cs)
            //    {
            //        Material material = document.GetElement(layer.MaterialId) as Material;
            //        width = layer.Width * PC_S_SystemSI.K_Length();

            //        if (material != null) res += material.Name + ", width: " + width.ToString() + "мм\n";
            //        else res += "Материал не задан" + ", width: " + width.ToString() + "мм\n";
            //    }

            //    ParameterSet ps = document.GetElement(element.Id).Parameters;
            //    foreach (Parameter p in ps)
            //    {
            //        if (p.Definition.Name == "ADSK_Примечание")
            //        {
            //            using (Transaction t = new Transaction(document))
            //            {
            //                t.Start("ADSK_Mark");
            //                p.Set(res);
            //                t.Commit();
            //            }
            //        }
            //    }
            //}
            //if ((BuiltInCategory)element.Category.Id.IntegerValue == BuiltInCategory.OST_Walls)
            //{
            //    Wall wall = element as Wall;
            //    CompoundStructure cs = wall.WallType.GetCompoundStructure();
            //    IList<CompoundStructureLayer> list_cs = cs.GetLayers();


            //    string res = string.Empty;
            //    double width = 0.0;
            //    foreach (CompoundStructureLayer layer in list_cs)
            //    {
            //        Material material = document.GetElement(layer.MaterialId) as Material;
            //        width = layer.Width * PC_S_SystemSI.K_Length();


            //        if (material != null) res += material.Name + ", width: " + width.ToString() + "мм\n";
            //        else res += "Материал не задан" + ", width: " + width.ToString() + "мм\n";
            //    }

            //    ParameterSet ps = document.GetElement(element.Id).Parameters;
            //    foreach (Parameter p in ps)
            //    {
            //        if (p.Definition.Name == "ADSK_Примечание")
            //        {
            //            using (Transaction t = new Transaction(document))
            //            {
            //                t.Start("ADSK_Mark");
            //                p.Set(res);
            //                t.Commit();
            //            }
            //        }
            //    }
            //}

            #endregion








            return Result.Succeeded;
        }
    }
}
