<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="makeQrcode.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>makeQRCode</title>
</head>
<body style="height: 227px">
    <form id="form1" runat="server">
    <div>
        <asp:Image ID="qrCodeImage" runat="server" Height="136px" />
        <br />
        <br />
    </div>
    <div>
        <asp:TextBox ID="codeInfo" runat="server" MaxLength="200" Width="800px"></asp:TextBox>
        <br />
        <br />
    </div>
    <div>
        <asp:Button ID="createQrCodebtn" runat="server" OnClick="createQrCode_Click" Text="生成二维码" />
    </div>
    </form>
</body>
</html>
