using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using ThoughtWorks.QRCode.Codec;

namespace QRCode.QRCode
{
    /// <summary>
    /// qrcodeImg 的摘要说明
    /// </summary>
    public class qrcodeImg : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            string text = context.Request["CodeText"];
            string text2 = context.Request["img"];
            if (text2 == "" || text2 == null)
            {
                text2 = "../images/wm.png";
            }
            if (!string.IsNullOrEmpty(text))
            {
                QRCodeEncoder qRCodeEncoder = new QRCodeEncoder();
                //qRCodeEncoder.set_QRCodeEncodeMode(2);
                //qRCodeEncoder.set_QRCodeScale(4);
                //qRCodeEncoder.set_QRCodeVersion(8);
                //qRCodeEncoder.set_QRCodeErrorCorrect(1);
                qRCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qRCodeEncoder.QRCodeScale = 4;
                qRCodeEncoder.QRCodeVersion = 8;
                qRCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                System.Drawing.Image image = qRCodeEncoder.Encode(text);
                MemoryStream memoryStream = new MemoryStream();
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                MemoryStream memoryStream2 = new MemoryStream();
                CombinImage(image, context.Server.MapPath(text2)).Save(memoryStream2, System.Drawing.Imaging.ImageFormat.Png);
                context.Response.ClearContent();
                context.Response.ContentType = "image/png";
                context.Response.BinaryWrite(memoryStream2.ToArray());
                memoryStream.Dispose();
                memoryStream2.Dispose();
            }
            context.Response.Flush();
            context.Response.End();
        }
        public static System.Drawing.Image CombinImage(System.Drawing.Image imgBack, string destImg)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(destImg);
            if (image.Height != 65 || image.Width != 65)
            {
                image = KiResizeImage(image, 65, 65, 0);
            }
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(imgBack);
            graphics.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);
            graphics.DrawImage(image, imgBack.Width / 2 - image.Width / 2, imgBack.Width / 2 - image.Width / 2, image.Width, image.Height);
            GC.Collect();
            return imgBack;
        }
        public static System.Drawing.Image KiResizeImage(System.Drawing.Image bmp, int newW, int newH, int Mode)
        {
            System.Drawing.Image result;
            try
            {
                System.Drawing.Image image = new System.Drawing.Bitmap(newW, newH);
                System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(image);
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(bmp, new System.Drawing.Rectangle(0, 0, newW, newH), new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.GraphicsUnit.Pixel);
                graphics.Dispose();
                result = image;
            }
            catch
            {
                result = null;
            }
            return result;
        }

    }
}