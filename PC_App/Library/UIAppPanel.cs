using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_App.Library
{
    public class UIAppPanel
    {
        public static RibbonPanel CreatePanel(UIControlledApplication app, string tab, string panel)
        {
            RibbonPanel panelEditModel = null;
            IList<RibbonPanel> panels = app.GetRibbonPanels(tab);
            foreach (RibbonPanel thisPanel in panels)
            {
                // Если данная панель существует уже, то ее и принять
                if (thisPanel.Name == panel)
                {
                    panelEditModel = thisPanel;
                    break;
                }
            }

            //Если панель отсутствует, то создать панель на вкладке
            if (panelEditModel == null)
            {
                panelEditModel = app.CreateRibbonPanel(tab, panel);
            }

            return panelEditModel;
        }
    }
}
