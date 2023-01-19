using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using PC_App.Constants;
using PC_App.Converts;
using PC_App.Elements;
using PC_App.Windows;
using Document = Autodesk.Revit.DB.Document;

namespace PC_App.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class command_AddSchedule : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // После реализации команды сообщение удалить
            //TaskDialog.Show("PiN", "Раздел в разработке (");
            // Запуск окна для создания спецификации металлопроката
            //W_ScheduleSteel _ScheduleSteel = new W_ScheduleSteel(document);
            //_ScheduleSteel.Height = 350;
            //_ScheduleSteel.Width = 500;
            //_ScheduleSteel.ShowDialog();


            UIApplication uiapp = commandData.Application;
            UIDocument uIdoc = uiapp.ActiveUIDocument;
            Document document = uIdoc.Document;

            // Получить TextNoteOptions для создания TextNote 
            ElementId defaultTextTypeId = document.GetDefaultElementTypeId(ElementTypeGroup.TextNoteType);
            TextNoteOptions opts = new TextNoteOptions(defaultTextTypeId);
            opts.VerticalAlignment = VerticalTextAlignment.Middle;
            TextNote text;


            // Создание необходимых элементов (vft) для создания чертежного вида
            FilteredElementCollector fec = new FilteredElementCollector(document).OfClass(typeof(ViewFamilyType));
            ViewFamilyType vft = fec.Cast<ViewFamilyType>().First(it => it.ViewFamily == ViewFamily.Drafting);
            
            //Определение типа чертежного вида
            ViewDrafting viewDrafting = null;

            using (Transaction tr = new Transaction(document))
            {
                tr.Start("CreateNewTextNote");

                //Создание вида, задание имени и масштаба
                viewDrafting = ViewDrafting.Create(document, vft.Id);
                //if(полеИмяВида != string.Empty)
                //{
                //    viewDrafting.Name = полеИмяВида;
                //}
                viewDrafting.Scale = 1;

                #region Вспомогательные данные
                //Количество столбцов (каткгорий детали). Необходимо получить из окна
                int countGroupConstruction = 4;
                int pointStartGropuConstruction = 110;
                int totalWidthTable = pointStartGropuConstruction + countGroupConstruction*15 + 25;

                #endregion



                #region Текст спецификации
                // Добавить новые тексты
                opts.HorizontalAlignment = HorizontalTextAlignment.Center;
                text = TextNote.Create(document, viewDrafting.Id, PC_S_ConvertXYZ.FromValue(totalWidthTable/2, 34, 0), "Спецификация металлопроката", opts);

                text = TextNote.Create(document, viewDrafting.Id, PC_S_ConvertXYZ.FromValue(20, 15, 0), "Наименование профиля" + "\n ГОСТ, ТУ", opts);
                text = TextNote.Create(document, viewDrafting.Id, PC_S_ConvertXYZ.FromValue(55, 15, 0), "Наименование" + "\nили марка" + "\nметалла ГОСТ," + "\nТУ", opts);
                text = TextNote.Create(document, viewDrafting.Id, PC_S_ConvertXYZ.FromValue(85, 15, 0), "Номер или" + "\nразмер профиля," + "\nмм", opts);
                text = TextNote.Create(document, viewDrafting.Id, PC_S_ConvertXYZ.FromValue(105, 15, 0), "№п/п", opts);
                text = TextNote.Create(document, viewDrafting.Id, PC_S_ConvertXYZ.FromValue(110+(countGroupConstruction*15)/2, 25, 0), "Масса метала по" + "\nэлементам конструкций, т", opts);

                //заполнение групп
                opts.Rotation = 1.5708; //поворот текста на 90 градусов - вертикальный
                for (int i=0; i<countGroupConstruction; i++)
                {
                    text = TextNote.Create(document, viewDrafting.Id, PC_S_ConvertXYZ.FromValue(110 + (15* i) + 8, 10, 0), "Группа " + (i + 1).ToString(), opts);
                }

                opts.Rotation = 0; // поворот текста на 0 градусов - горизонтальный
                text = TextNote.Create(document, viewDrafting.Id, PC_S_ConvertXYZ.FromValue(totalWidthTable-13, 15, 0), "Общая," + "\nмасса, т", opts);
                #endregion

                #region Границы (линии) спецификации
                //Создание массива линий
                CurveArray curveArray = new CurveArray();
                Curve curve;

                //Горизонтальные линии шапки
                curve = Line.CreateBound(PC_S_ConvertXYZ.FromValue(0, 30, 0), PC_S_ConvertXYZ.FromValue(totalWidthTable, 30, 0));
                curveArray.Append(curve);
                curve = Line.CreateBound(PC_S_ConvertXYZ.FromValue(0, 0, 0), PC_S_ConvertXYZ.FromValue(totalWidthTable, 0, 0));
                curveArray.Append(curve);
                curve = Line.CreateBound(PC_S_ConvertXYZ.FromValue(pointStartGropuConstruction, 20, 0), PC_S_ConvertXYZ.FromValue(totalWidthTable-25, 20, 0));
                curveArray.Append(curve);

                //Вертикальные линиии шапки
                curve = Line.CreateBound(PC_S_ConvertXYZ.FromValue(0, 30, 0), PC_S_ConvertXYZ.FromValue(0, 0, 0));
                curveArray.Append(curve);
                curve = Line.CreateBound(PC_S_ConvertXYZ.FromValue(40, 30, 0), PC_S_ConvertXYZ.FromValue(40, 0, 0));
                curveArray.Append(curve);
                curve = Line.CreateBound(PC_S_ConvertXYZ.FromValue(70, 30, 0), PC_S_ConvertXYZ.FromValue(70, 0, 0));
                curveArray.Append(curve);
                curve = Line.CreateBound(PC_S_ConvertXYZ.FromValue(100, 30, 0), PC_S_ConvertXYZ.FromValue(100, 0, 0));
                curveArray.Append(curve);
                curve = Line.CreateBound(PC_S_ConvertXYZ.FromValue(110, 30, 0), PC_S_ConvertXYZ.FromValue(110, 0, 0));
                curveArray.Append(curve);
                for (int i=1; i<=countGroupConstruction; i++)
                {
                    if(i< countGroupConstruction)
                    {
                        curve = Line.CreateBound(PC_S_ConvertXYZ.FromValue(pointStartGropuConstruction+(i*15), 20, 0), PC_S_ConvertXYZ.FromValue(pointStartGropuConstruction+(i*15), 0, 0));
                    }
                    else
                    {
                        curve = Line.CreateBound(PC_S_ConvertXYZ.FromValue(pointStartGropuConstruction + (i * 15), 30, 0), PC_S_ConvertXYZ.FromValue(pointStartGropuConstruction + (i * 15), 0, 0));
                    }
                    curveArray.Append(curve);
                }
                curve = Line.CreateBound(PC_S_ConvertXYZ.FromValue(totalWidthTable, 30, 0), PC_S_ConvertXYZ.FromValue(totalWidthTable, 0, 0));
                curveArray.Append(curve);

                //Добавить массив кривых на вид
                document.Create.NewDetailCurveArray(viewDrafting, curveArray);
                #endregion


                tr.Commit();
            }












            return Result.Succeeded;
        }
    }
}
