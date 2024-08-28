using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace 软件系统客户端Wpf
{


    /***********************************************************************************
     * 
     *    Description: Used to develop some WPF-specific methods and various conversion methods.
     * 
     ***********************************************************************************/



    public class AppWpfHelper
    {
        public static BitmapImage TranslateImageToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            if(bitmap.RawFormat != null) bitmap.Save(ms, bitmap.RawFormat);
            else bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();
            return bi;
        }

    }
}
