using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThoughtWorks.QRCode.Codec;

namespace makeQrcode
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void createQrCode_Click(object sender, EventArgs e)
        {
            createQrCode(this.codeInfo.Text);
        }

        private void createQrCode(string text)
        {
            Bitmap bt;
            string enCodeString = text;
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //bt = qrCodeEncoder.Encode(enCodeString, Encoding.UTF8);
            bt = qrCodeEncoder.Encode(enCodeString);
            string filename = string.Format(DateTime.Now.ToString(), "yyyymmddhhmmss");
            filename = filename.Replace(" ", "");
            filename = filename.Replace(":", "");
            filename = filename.Replace("-", "");
            filename = filename.Replace(".", "");
            bt.Save(Server.MapPath("~/image/") + filename + ".jpg");
            this.qrCodeImage.ImageUrl = "~/image/" + filename + ".jpg";
        }

    }
}