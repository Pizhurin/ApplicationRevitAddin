using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Windows.Media.Imaging;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using PC_App.Images;
using PC_App.Updaters;

namespace PC_App
{
    internal class Application : IExternalApplication
    {
        // Создание имя вкладки
        const string RIBBON_TAB = "GREEN";
        //Создание имя панели
        const string RIBBON_PANEL = "Edit model";
        // Создание объектов класса PC_Images для получения картинки Bitmap
        PC_Images imageLarge = null;
        PC_Images imageSmall = null;

        public Result OnStartup(UIControlledApplication application)
        {
            #region Создание вкладки и панели
            //Создание вкладки
            //List<RibbonPanel> ribbonPanels = application.GetRibbonPanels();
            //bool check = false;
            //foreach (RibbonPanel panel in ribbonPanels)
            //{
            //    if (panel.Name == RIBBON_TAB)
            //    {
            //        check = true;
            //        break;
            //    }
            //}
            //if(!check) application.CreateRibbonTab(RIBBON_TAB);
            application.CreateRibbonTab(RIBBON_TAB);

            //Создание панели на вкладке
            RibbonPanel panelEditModel = null;
            IList<RibbonPanel> panels = application.GetRibbonPanels(RIBBON_TAB);
            foreach (RibbonPanel thisPanel in panels)
            {
                // Усли данная панель существует уже, то ее и принять
                if(thisPanel.Name == RIBBON_PANEL)
                {
                    panelEditModel = thisPanel;
                    break;
                }
            }

            //Если панель отсутствует, то создать панель на вкладке
            if (panelEditModel == null)
            {
                panelEditModel = application.CreateRibbonPanel(RIBBON_TAB, RIBBON_PANEL);
            }

            // Путь к сборке
            string pathThisAssembly = Assembly.GetExecutingAssembly().Location;
            #endregion

            #region кнопка ТЕСТ
            PushButtonData buttonDataTest = new PushButtonData
                (
                "Test",
                "Test command",
                pathThisAssembly,
                "PC_App.Commands.command_Test"
                );
            buttonDataTest.ToolTip = "Тест команды";
            buttonDataTest.LongDescription = "Полное описание команды";

            // Добавить кнопку на панель
            PushButton buttonTest = panelEditModel.AddItem(buttonDataTest) as PushButton;
            buttonTest.Enabled = true;


            // Большая иконка для кнопки            
            imageLarge = new PC_Images(Path.Combine(Path.GetDirectoryName(pathThisAssembly), "Reference", "05.png"));
            buttonTest.LargeImage = imageLarge.GetBitmap();
            // Маленькая иконка для кнопки
            imageSmall = new PC_Images(Path.Combine(Path.GetDirectoryName(pathThisAssembly), "Reference", "05s.png"));
            buttonTest.Image = imageSmall.GetBitmap();
            #endregion

            #region Добавить кнопку добавления рбра жесткости
            // Создание информации о кнопке
            PushButtonData buttonDataAddRib = new PushButtonData
                (
                "AddRib",
                "Add rib",
                pathThisAssembly,
                "PC_App.Commands.command_AddRib"
                );
            buttonDataAddRib.ToolTip = "Добавить ребро жесткости";
            buttonDataAddRib.LongDescription = "Полное описание функции кнопки по созданию спецификации";

            // Добавить кнопку на панель
            PushButton buttonAddRib = panelEditModel.AddItem(buttonDataAddRib) as PushButton;
            buttonAddRib.Enabled = true;

            
            // Большая иконка для кнопки            
            imageLarge = new PC_Images(Path.Combine(Path.GetDirectoryName(pathThisAssembly), "Reference", "01.png"));
            buttonAddRib.LargeImage = imageLarge.GetBitmap();
            // Маленькая иконка для кнопки
            imageSmall = new PC_Images(Path.Combine(Path.GetDirectoryName(pathThisAssembly), "Reference", "01s.png"));
            buttonAddRib.Image = imageSmall.GetBitmap();
            #endregion

            #region Добавить кнопку создания спецификации металлопроката
            PushButtonData buttonDataCreateSchedule = new PushButtonData
                (
                "CreateSchedule",
                "Create steel schedule",
                pathThisAssembly,
                "PC_App.Commands.command_AddSchedule"
                );

            buttonDataCreateSchedule.ToolTip = "Создать спецификацию металлопроката";
            buttonDataCreateSchedule.LongDescription = "Полное описание функции кнопки по созданию спецификации";

            PushButton buttonCreateSchedule = panelEditModel.AddItem(buttonDataCreateSchedule) as PushButton;

            #region Подумать как эту операции переделать в класс
            // Большая иконка для кнопки            
            string pathImgCreateSchedule = Path.Combine(Path.GetDirectoryName(pathThisAssembly), "Reference", "02.png");
            imageLarge = new PC_Images(pathImgCreateSchedule);
            buttonCreateSchedule.LargeImage = imageLarge.GetBitmap();            
            // Маленькая иконка для кнопки
            //pathImgCreateSchedule = Path.Combine(Path.GetDirectoryName(pathThisAssembly), "Reference", "02s.png");
            // Путь можно создать в самом конструкторе
            imageSmall = new PC_Images(Path.Combine(Path.GetDirectoryName(pathThisAssembly), "Reference", "02s.png"));
            buttonCreateSchedule.Image = imageSmall.GetBitmap();
            #endregion
            #endregion

            #region Добавить кнопку добавления фланца
            PushButtonData buttonDataAddPlate = new PushButtonData
                (
                "AddPlate",
                "Add plate",
                pathThisAssembly,
                "PC_App.Commands.command_AddPlate"
                );
            buttonDataAddPlate.ToolTip = "Добавить фланец";
            buttonDataAddPlate.LongDescription = "Полное описание функции кнопки по добавлению фланца";

            PushButton buttonAddPlate = panelEditModel.AddItem(buttonDataAddPlate) as PushButton;

            // Большая иконка для кнопки
            imageLarge = new PC_Images(Path.Combine(Path.GetDirectoryName(pathThisAssembly), "Reference", "03.png"));
            buttonAddPlate.LargeImage = imageLarge.GetBitmap();
            //Маленькая иконка для кнопки
            imageSmall = new PC_Images(Path.Combine(Path.GetDirectoryName(pathThisAssembly), "Reference", "03s.png"));
            buttonAddPlate.Image = imageSmall.GetBitmap();
            #endregion

            #region Добавить кнопку сбора светильников в помещении
            PushButtonData buttonDataQuantityLight = new PushButtonData
                (
                "QuantityLight",
                "Light system",
                pathThisAssembly,
                "PC_App.Commands.command_QuantityLight"
                );
            buttonDataQuantityLight.ToolTip = "Сбор количества светильников в выбранном помещении";
            buttonDataQuantityLight.LongDescription = "Полное описание функции собирающей светильники в помещениях. Описать совместно с электриками";

            PushButton buttonQuantityLight = panelEditModel.AddItem(buttonDataQuantityLight) as PushButton;

            // Большая иконка для кнопки
            imageLarge = new PC_Images(Path.Combine(Path.GetDirectoryName(pathThisAssembly), "Reference", "06_Light.png"));
            buttonQuantityLight.LargeImage = imageLarge.GetBitmap();
            //Маленькая иконка для кнопки
            imageSmall = new PC_Images(Path.Combine(Path.GetDirectoryName(pathThisAssembly), "Reference", "06s_Light.png"));
            buttonQuantityLight.Image = imageSmall.GetBitmap();
            #endregion

            #region Изменение фона 
            PushButtonData buttonDataAddScrew = new PushButtonData
                (
                "SetBackground",
                "Set background",
                pathThisAssembly,
                "PC_App.Commands.command_Background"
                );
            buttonDataAddScrew.ToolTip = "Изменить фон вида";
            buttonDataAddScrew.LongDescription = "Полное описание функции кнопки по добавлению болта";

            PushButton buttonAddScrew = panelEditModel.AddItem(buttonDataAddScrew) as PushButton;

            // Большая иконка для кнопки
            imageLarge = new PC_Images(Path.Combine(Path.GetDirectoryName(pathThisAssembly), "Reference", "04.png"));
            buttonAddScrew.LargeImage = imageLarge.GetBitmap();
            //Маленькая иконка для кнопки
            imageSmall = new PC_Images(Path.Combine(Path.GetDirectoryName(pathThisAssembly), "Reference", "04s.png"));
            buttonAddScrew.Image = imageSmall.GetBitmap();
            #endregion




            #region Updater для стен и кровли (Добавление в ADSK_Примечание состав слоев). Сама команда в command_Test #Получение слоев стены
            ElementCategoryFilter categoryFilterWalls = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            ElementCategoryFilter categoryFilterRoofs = new ElementCategoryFilter(BuiltInCategory.OST_Roofs);

            PC_ParameterUpdater _ParameterUpdater = new PC_ParameterUpdater(application.ActiveAddInId);
            if (!UpdaterRegistry.IsUpdaterRegistered(_ParameterUpdater.GetUpdaterId()))
            {
                UpdaterRegistry.RegisterUpdater(_ParameterUpdater);
                //UpdaterRegistry.AddTrigger(_ParameterUpdater.GetUpdaterId(), categoryFilter, Element.GetChangeTypeAny());
                UpdaterRegistry.AddTrigger(_ParameterUpdater.GetUpdaterId(), categoryFilterWalls, Element.GetChangeTypeAny());
                UpdaterRegistry.AddTrigger(_ParameterUpdater.GetUpdaterId(), categoryFilterRoofs, Element.GetChangeTypeAny());
            }
            #endregion



            return Result.Succeeded;
        }



        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

    }
}
