using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_App.Constants
{
    internal static class PC_S_Convert
    {
        /// <summary>
        /// Перевод строкового параметра AsValueString() в double
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double StringToDouble(string str)
        {
            double result = 0;

            string tempW = null;
            foreach (char c in str)
            {
                if (c != ' ')
                {
                    if (c == '.')
                    {
                        tempW += ',';
                        continue;
                    }
                    tempW += c;
                }
                else
                {
                    break;
                }
            }
            result = Convert.ToDouble(tempW);


            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int StringToInt(string str)
        {
            int result = -1;
            string strTemp=String.Empty;
            foreach (char c in str)
            {
                if (c != ' ')
                {
                    if (c == '.' || c == ',')
                    {
                        return result;
                    }
                    if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == 9)
                    {
                        strTemp += c;
                    }
                }
            }
            result = Convert.ToInt32(strTemp);
            return result;
        }


    }
}
