using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using PC_App.Constants;
using System.Windows.Controls;

namespace PC_App.Elements
{
    internal static class PC_S_DataStructuralElement
    {

        /// <summary>
        /// Получение параметра типа string по экземпляру для несущих колонн и несущего каркаса. Возвращает null, если параметр не найден.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="eId"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetParameterByInstance(Document doc, ElementId eId, string param)
        {
            string parameter = null;

            if (eId != null)
            {
                Element element = doc.GetElement(eId);
                
                // Проверка элемента на несущую колонну и несущий каркас (нам нужны толко они)
                if ((BuiltInCategory)element.Category.Id.IntegerValue == BuiltInCategory.OST_StructuralColumns
                    ||
                    (BuiltInCategory)element.Category.Id.IntegerValue == BuiltInCategory.OST_StructuralFraming)
                {
                    parameter = PC_S_DataElement.GetParameterByInstance(doc, element.Id, param);
                    
                }
            }



            return parameter;
        }

        /// <summary>
        /// Получение параметра типа string по типу для несущих колонн и несущего каркаса. Возвращает null, если параметр не найден.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="eId"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetParameterByType(Document doc, ElementId eId, string param)
        {
            string parameter = null;

            if (eId != null)
            {
                Element element = doc.GetElement(eId);
                //ElementId id = elementType.GetTypeId();

                // Проверка элемента на несущую колонну и несущий каркас (нам нужны толко они)
                if ((BuiltInCategory)element.Category.Id.IntegerValue == BuiltInCategory.OST_StructuralColumns
                    ||
                    (BuiltInCategory)element.Category.Id.IntegerValue == BuiltInCategory.OST_StructuralFraming)
                {
                    parameter = PC_S_DataElement.GetParameterByType(doc, element.Id, param);
                }

            }
            return parameter;
        }

        /// <summary>
        /// Получить вес элемента в кг. для несущего каркаса или колонны
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="eId"></param>
        /// <returns></returns>
        public static double GetWeight(Document doc, ElementId eId)
        {
            double weight = 0.0;
            double length = 0.0;

            if (eId != null)
            {
                Element elementType = doc.GetElement(eId);
                //ElementId id = elementType.GetTypeId();

                // Проверка элемента на несущую колонну и несущий каркас (нам нужны толко они)
                if ((BuiltInCategory)elementType.Category.Id.IntegerValue == BuiltInCategory.OST_StructuralColumns
                    ||
                    (BuiltInCategory)elementType.Category.Id.IntegerValue == BuiltInCategory.OST_StructuralFraming)
                {
                    // Получение параметров типа
                    //ParameterSet ps = doc.GetElement(id).Parameters;
                    ParameterSet ps = doc.GetElement(elementType.GetTypeId()).Parameters;
                    foreach (Parameter p in ps)
                    {
                        if (p.Definition.Name == "ADSK_Масса на единицу длины")
                        {
                            weight = p.AsDouble() * PC_S_SystemSI.K_Weight();
                            break;
                        }
                    }
                    //Если ADSK_Масса на единицу длины не ялвяется параметром по типу, то проверить параметр экземпляра 
                    if (weight == 0)
                    {
                        weight = elementType.LookupParameter("ADSK_Масса на единицу длины").AsDouble()*PC_S_SystemSI.K_Weight();
                    }
                }                

                // Получить длину и перевести в метры
                length = GetLength(doc, eId) / 1000;
            }

            return Math.Round((weight * length), 4);
        }

        /// <summary>
        /// Получить длину элемента в мм. для несущего каркаса или колонны
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="eId"></param>
        /// <returns></returns>
        public static double GetLength(Document doc, ElementId eId)
        {
            if (eId != null)
            {
                Element element = doc.GetElement(eId);
                
                return Math.Round((element.LookupParameter("ADSK_Размер_Длина").AsDouble()) * PC_S_SystemSI.K_Length(), 2);
            }

            return 0.0;
        }

        /// <summary>
        /// Получить список (ElementId) вложеных семейств
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static IList<ElementId> GetSubComponents (Document doc, ElementId eId)
        {
            IList<ElementId> subComponents = new List<ElementId>();

            // Создать FamilyInstance для проверки на SuperComponent
            FamilyInstance instance = doc.GetElement(eId) as FamilyInstance;

            if (instance.SuperComponent == null)
            {
                subComponents = instance.GetSubComponentIds().ToList();                
            }

            return subComponents;
        }

        /// <summary>
        /// Проверить элемент на SuperComponent, но это не значит, что есть вложенные семейства
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="eId"></param>
        /// <returns></returns>
        public static bool CheckSubComponents(Document doc, ElementId eId)
        {
            FamilyInstance familyInstance = doc.GetElement(eId) as FamilyInstance;
            if (familyInstance.SuperComponent == null) return true;
            else return false;
        }

    }
}
