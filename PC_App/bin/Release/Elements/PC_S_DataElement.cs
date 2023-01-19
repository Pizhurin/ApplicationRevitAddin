using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using PC_App.Constants;

namespace PC_App.Elements
{
    internal static class PC_S_DataElement
    {
        
        /// <summary>
        /// Получение параметра типа string по экземпляру. Возвращает null, если параметр не найден.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="eId"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetParameterByInstance(Document doc, ElementId eId, string param)
        {
            string parameter = null;
            if(eId != null)
            {
                Element element = doc.GetElement(eId);
                parameter = element.LookupParameter(param).AsValueString();
            }
            return parameter;
        }

        /// <summary>
        /// Получение параметра типа string по типу. Возвращает null, если параметр не найден.
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
                Element elementType = doc.GetElement(eId);
                // Получение параметров типа
                ParameterSet ps = doc.GetElement(elementType.GetTypeId()).Parameters;
                foreach (Parameter p in ps)
                {
                    if (p.Definition.Name == param)
                    {
                        parameter = p.AsValueString();
                        break;
                    }
                }
            }
            return parameter;
        }

        /// <summary>
        /// Получить значение параметра экземпляра в единицах хранимых Revit-ом (double), или 0
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="eId"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static double GetValueByInstance(Document doc, ElementId eId, string param)
        {
            double value = 0;

            if (eId != null)
            {
                Element element = doc.GetElement(eId);
                value = element.LookupParameter(param).AsDouble();
            }

            return value;
        }

        /// <summary>
        /// Получить значение параметра по типу в единицах хранимых Revit-ом (double), или 0
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="eId"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static double GetValueByType(Document doc, ElementId eId, string param)
        {
            double value = 0;
            if (eId != null)
            {
                Element elementType = doc.GetElement(eId);
                // Получение параметров типа
                ParameterSet ps = doc.GetElement(elementType.GetTypeId()).Parameters;
                foreach (Parameter p in ps)
                {
                    if (p.Definition.Name == param)
                    {
                        value = p.AsDouble();
                        break;
                    }
                }
            }

            return value;
        }







    }
}
