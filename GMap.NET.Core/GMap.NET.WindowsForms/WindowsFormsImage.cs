﻿
namespace GMap.NET.WindowsForms
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    /// <summary>
    /// image abstraction
    /// </summary>
    public class WindowsFormsImage : PureImage
    {
        public System.Drawing.Image Img;

        public override void Dispose()
        {
            if (Img != null)
            {
                Img.Dispose();
                Img = null;
            }
        }
    }

    /// <summary>
    /// image abstraction proxy
    /// </summary>
    public class WindowsFormsImageProxy : PureImageProxy
    {
        public override PureImage FromStream(Stream stream)
        {
            WindowsFormsImage ret = null;
            try
            {
                if (!GMaps.Instance.IsRunningOnMono)
                {
                    Image m = Image.FromStream(stream, true, true);
                    if (m != null)
                    {
                        ret = new WindowsFormsImage();
                        ret.Img = m;
                    }
                }
                else // mono yet do not support validation
                {
                    Image m = Image.FromStream(stream);
                    if (m != null)
                    {
                        ret = new WindowsFormsImage();
                        ret.Img = m;
                    }
                }
            }
            catch (Exception ex)
            {
                ret = null;
                Debug.WriteLine("FromStream: " + ex.ToString());
            }
            finally
            {
                try
                {
                    stream.Seek(0, System.IO.SeekOrigin.Begin);

                    if (ret == null)
                    {
                        stream.Dispose();
                    }
                }
                catch
                {
                }
            }
            return ret;
        }

        public override bool Save(Stream stream, GMap.NET.PureImage image)
        {
            WindowsFormsImage ret = image as WindowsFormsImage;
            bool ok = true;

            if (ret.Img != null)
            {
                // try png
                try
                {
                    ret.Img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                }
                catch
                {
                    // try jpeg
                    try
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        ret.Img.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        ok = false;
                    }
                }
            }
            else
            {
                ok = false;
            }

            return ok;
        }
    }
}
