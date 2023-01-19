using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using PC_App.Constants;

namespace PC_App.Elements
{
    internal class PC_Element
    {
        Element element = null;

        //Имя профиля
        public string Name { get; }
        // Символ профиля
        public string Symbol { get; }
        //ГОСТ на профиль
        public string GOST { get; }
        // Полное ниаменование ГОСТа
        public string DescriptionGOST { get; }
        // Материал профиля
        public string Material { get; }
        // Вес элемента
        public double Weight { get; }
        // Длина элемента
        public double Length { get; }
        //Группа конструкций
        public int GroupConstruction { get; }
        //ID элемента в проекте 
        public int IDElement { get; }

        #region Свои метки. Данные получаем НЕ ИЗ REVIT ЭЛЕМЕНТОВ
        // Своя строковая метка для элемента
        public string Comment { get; set; }

        // Своя целочисленная метка для элемента (столбец или строка или любую другую метку можно установить/получить).
        // В спецификации можно использовать как Категория детали
        public int Lable { get; set; }
        #endregion


        public PC_Element(Document doc, Element elem)
        {
            element = elem;
            if((BuiltInCategory)element.Category.Id.IntegerValue != BuiltInCategory.OST_IOSModelGroups)
            {
                Name = PC_S_DataStructuralElement.GetParameterByType(doc, element.Id, "ADSK_Наименование");
                Material = PC_S_DataStructuralElement.GetParameterByInstance(doc, element.Id, "Материал несущих конструкций");
                GOST = PC_S_DataStructuralElement.GetParameterByType(doc, element.Id, "ADSK_Обозначение");
                DescriptionGOST = PC_S_DataStructuralElement.GetParameterByType(doc, element.Id, "ADSK_Наименование профиля");
                Symbol = PC_S_DataStructuralElement.GetParameterByType(doc, element.Id, "ADSK_Наименование_Префикс");
                GroupConstruction = PC_S_Convert.StringToInt(PC_S_DataStructuralElement.GetParameterByInstance(doc, element.Id, "ADSK_Группа конструкции"));
                Weight = PC_S_DataStructuralElement.GetWeight(doc, element.Id);
                Length= PC_S_DataStructuralElement.GetLength(doc, element.Id);
                IDElement = element.Id.IntegerValue;
            }
        }









    }
}
