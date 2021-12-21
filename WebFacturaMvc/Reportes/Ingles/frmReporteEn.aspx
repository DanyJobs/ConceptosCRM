<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmReporteEn.aspx.cs" Inherits="WebFacturaMvc.Reportes.Ingles.frmReporteEn" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {
            height: 647px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:reportviewer id="Reporte1" runat="server" backcolor="" clientidmode="AutoID" highlightbackgroundcolor="" internalbordercolor="204, 204, 204" internalborderstyle="Solid" internalborderwidth="1px" linkactivecolor="" linkactivehovercolor="" linkdisabledcolor="" primarybuttonbackgroundcolor="" primarybuttonforegroundcolor="" primarybuttonhoverbackgroundcolor="" primarybuttonhoverforegroundcolor="" secondarybuttonbackgroundcolor="" secondarybuttonforegroundcolor="" secondarybuttonhoverbackgroundcolor="" secondarybuttonhoverforegroundcolor="" splitterbackcolor="" toolbardividercolor="" toolbarforegroundcolor="" toolbarforegrounddisabledcolor="" toolbarhoverbackgroundcolor="" toolbarhoverforegroundcolor="" toolbaritembordercolor="" toolbaritemborderstyle="Solid" toolbaritemborderwidth="1px" toolbaritemhoverbackcolor="" toolbaritempressedbordercolor="51, 102, 153" toolbaritempressedborderstyle="Solid" toolbaritempressedborderwidth="1px" toolbaritempressedhoverbackcolor="153, 187, 226" width="1018px" Height="561px">
            <LocalReport ReportPath="Reportes\Ingles\rptFacturaEn.rdlc">
            </LocalReport>
        </rsweb:reportviewer>
    </form>
</body>
</html>
