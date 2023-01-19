using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PC_App.Images
{
    internal class PC_Images
    {
        BitmapImage bitmap = null;

        public PC_Images(string path)
        {
            Uri uri = new Uri(path);
            bitmap = new BitmapImage(uri);
        }

        internal BitmapImage GetBitmap() => bitmap;
        

    }
}
