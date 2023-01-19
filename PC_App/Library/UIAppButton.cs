using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PC_App.Library
{
    public class UIAppButton
    {
        private PushButtonData _pushButtonData;
        private PushButton _pushButton;
        private string _commandName;
        private string _pathAssemblyCommands;

        public string CommandName { get { return _commandName; } }
        public string ToolTip { get => _pushButtonData.ToolTip; set => _pushButtonData.ToolTip = value; }
        public string LongDescription { get => _pushButtonData.LongDescription; set => _pushButtonData.LongDescription = value; }

        public UIAppButton(RibbonPanel panel, string name, string text, string assemblyCommands, string commandName)
        {
            _commandName = commandName;
            _pathAssemblyCommands = assemblyCommands;
            _pushButtonData = new PushButtonData(name, text, _pathAssemblyCommands, _commandName);
            _pushButton = panel.AddItem(_pushButtonData) as PushButton;
            _pushButton.Enabled = true;
        }


        public void CreatLargeImage(string path)
        {
            Uri uriLarge = new Uri(path);
            BitmapImage bitmapImageLarge = new BitmapImage(uriLarge);
            _pushButton.LargeImage = bitmapImageLarge;
        }

        public void CreateSmallImage(string path)
        {
            Uri uriSmall = new Uri(path);
            BitmapImage bitmapImageSmall = new BitmapImage(uriSmall);
            _pushButton.Image = bitmapImageSmall;
        }
    }
}
