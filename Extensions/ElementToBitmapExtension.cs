using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfLib.Extensions
{
  public static class ElementToBitmapExtension
  {
    [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DeleteObject([In] IntPtr hObject);

    public static Bitmap ToBitmap(this FrameworkElement element)
    {
      double dpi = 96;
      double width = element.ActualWidth;
      double height = element.ActualHeight;

      RenderTargetBitmap bmpCopied = new RenderTargetBitmap((int)Math.Round(width), (int)Math.Round(height), dpi, dpi, PixelFormats.Default);
      DrawingVisual dv = new DrawingVisual();
      using (DrawingContext dc = dv.RenderOpen())
      {
        VisualBrush vb = new VisualBrush(element);
        dc.DrawRectangle(vb, null, new Rect(new System.Windows.Point(), new System.Windows.Size(width, height)));
      }
      bmpCopied.Render(dv);
      Bitmap bitmap;
      using (MemoryStream outStream = new MemoryStream())
      {
        // from System.Media.BitmapImage to System.Drawing.Bitmap 
        PngBitmapEncoder enc = new PngBitmapEncoder();
        //BitmapEncoder enc = new BmpBitmapEncoder();
        enc.Frames.Add(BitmapFrame.Create(bmpCopied));
        enc.Save(outStream);
        bitmap = new Bitmap(outStream);
      }

      Bitmap btm = new Bitmap(bitmap);
      return btm;
    }
  }
}
