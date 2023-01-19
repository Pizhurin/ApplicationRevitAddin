using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.UI;
using System.Windows.Controls;
using Autodesk.Revit.Attributes;

namespace PC_App.Elements
{
    [Transaction(TransactionMode.Manual)]
    internal class PC_SelectElement
    {
        UIApplication application;
        UIDocument uiDoc;
        Document document;
        IList<ElementId> elementsId = null;

        public PC_SelectElement(UIApplication uiapp)
        {
            application = uiapp;
            uiDoc = application.ActiveUIDocument;
            document = uiDoc.Document;
            Selection selection = uiDoc.Selection;
            Reference reference = null;
            elementsId = new List<ElementId>();

            // Здесь нужно разделить на выбрать один элемент или рамка или несколько

            reference = selection.PickObject(ObjectType.Element);
            if(reference != null)
            {
                Element element = document.GetElement(reference);
                elementsId.Add(element.Id);
                
                // Если проверка на вложенные семейства вернет true
                if(PC_S_DataStructuralElement.CheckSubComponents(document, element.Id))
                {
                    // Собрать все вложенные семейства
                    IList<ElementId> addElementsId = PC_S_DataStructuralElement.GetSubComponents(document, element.Id);
                    // Обойти вложенные семейства и добавить их в список выбраных
                    foreach(ElementId addElementId in addElementsId)
                    {
                        elementsId.Add(addElementId);
                    }
                }
                //

                //Если выбраный элемент группа, необходимо также добавить все элементы группы
                if((BuiltInCategory)element.Category.Id.IntegerValue == BuiltInCategory.OST_IOSModelGroups)
                {
                    ElementClassFilter filter = new ElementClassFilter(typeof(FamilyInstance));
                    
                    IList<ElementId> elementGroup = element.GetDependentElements(filter);

                    foreach(ElementId eGroup in elementGroup)
                    {
                        // Если проверка на вложенные семейства вернет true
                        if (PC_S_DataStructuralElement.CheckSubComponents(document, eGroup))
                        {
                            // Собрать все вложенные семейства
                            IList<ElementId> addElementsId = PC_S_DataStructuralElement.GetSubComponents(document, eGroup);
                            // Обойти вложенные семейства и добавить их в список выбраных
                            foreach (ElementId addElementId in addElementsId)
                            {
                                elementsId.Add(addElementId);
                            }
                        }
                        elementsId.Add(eGroup);
                    }
                }
            }
        }

        /// <summary>
        /// Получить список всех выбраных элементов
        /// </summary>
        /// <returns></returns>
        public IList<ElementId> GetSelectElements() { return elementsId; }

        /// <summary>
        /// Сфокусировать выбраный элемент
        /// </summary>
        public void FocusSelectedElements()
        {
            if(elementsId != null)
            {
                application.ActiveUIDocument.Selection.SetElementIds(elementsId);
                application.ActiveUIDocument.ShowElements(elementsId);
                application.ActiveUIDocument.RefreshActiveView();
            }
            else
            {
                TaskDialog.Show("Error", "Ни один элемент не выбран");
            }
        }

        /// <summary>
        /// Изолировать выбраный элемент (синее окно)
        /// </summary>
        public void IsolateSelectedElements()
        {
            View view = document.ActiveView;

            using (Transaction t = new Transaction(document))
            {
                t.Start("start");
                //Изолирует элементы на виде (синяя рамка)
                view.IsolateElementsTemporary(elementsId);
                t.Commit();
            }

        }
    }
}
