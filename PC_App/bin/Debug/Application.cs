using Autodesk.Revit.UI;
using PC_App.Commands;
using PC_App.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace PC_App
{
    internal class Application : IExternalApplication
    {
        // Создание имя вкладки
        const string RIBBON_TAB = "TAB";
        //Создание имя панели
        const string RIBBON_PANEL = "PANEL";

        public Result OnStartup(UIControlledApplication application)
        {
            application.CreateRibbonTab(RIBBON_TAB);
            string pathThisAssembly = Assembly.GetExecutingAssembly().Location; // Путь к сборке
            string pathDirectoryName = Path.GetDirectoryName(pathThisAssembly); // Имя каталога в которомнаходится запускаемая сборка
            string pathDirectoyAssemblyCommands = Path.Combine(pathDirectoryName, "PC_App.dll");    // Путь к директории сборки (где исполняемая команда)
            string commandName = string.Empty;
            
            #region RIBBON_PANEL
            //Создание панели на вкладке RIBBON_PANEL
            RibbonPanel panel = UIAppPanel.CreatePanel(application, RIBBON_TAB, RIBBON_PANEL);


            //Кнопка с командой SetBacground _ RIBBON_PANEL_EDIT_MODEL
            commandName = "PC_App.Commands.LoadWorksetFromExcel";
            UIAppButton buttonLoadWorkSetFromExcel = new UIAppButton(panel, "LoadWorkset", "Load Worset", pathDirectoyAssemblyCommands, commandName);
            buttonLoadWorkSetFromExcel.CreatLargeImage(Path.Combine(pathDirectoryName, "Images", "01.png"));
            buttonLoadWorkSetFromExcel.CreateSmallImage(Path.Combine(pathDirectoryName, "Images", "01s.png"));
            buttonLoadWorkSetFromExcel.ToolTip = string.Empty;
            buttonLoadWorkSetFromExcel.LongDescription = "Добавляемые рабочие наборы должны быть заполнены в первой колонке";

            commandName = "PC_App.Commands.CountElements";
            UIAppButton buttonCountElements = new UIAppButton(panel, "CountElements", "Count Elements", pathDirectoyAssemblyCommands, commandName);
            buttonCountElements.CreatLargeImage(Path.Combine(pathDirectoryName, "Images", "02.png"));
            buttonCountElements.CreateSmallImage(Path.Combine(pathDirectoryName, "Images", "02s.png"));
            buttonCountElements.ToolTip = string.Empty;
            buttonCountElements.LongDescription = string.Empty;




            #endregion
            return Result.Succeeded;
        }



        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

    }
}