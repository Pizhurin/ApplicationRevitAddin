using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_App.Converts
{
    internal static class PC_S_ConvertXYZ
    {
        /// <summary>
        /// Перевод экземпляра класса Point в миллиметры.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static XYZ FromValue(double x, double y, double z)
        {
            return new XYZ(x * 0.00328084, y * 0.00328084, z * 0.00328084);
        }




    }
}
