using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PC_App.Constants
{
    internal static class PC_S_SystemSI
    {
        /// <summary>
        /// Коэффициент перевода в килограммы
        /// </summary>
        /// <returns></returns>
        public static double K_Weight()
        {
            return 3.280839895013;
        }

        /// <summary>
        /// Коэффициент перевода в милиметры
        /// </summary>
        /// <returns></returns>
        public static double K_Length()
        {
            return 304.8;
        }        

    }
}
