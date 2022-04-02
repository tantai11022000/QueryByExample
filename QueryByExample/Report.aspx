<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Report.aspx.cs" Inherits="QueryByExample._Default" %>

<%@ Register assembly="DevExpress.XtraReports.v19.2.Web.WebForms, Version=19.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
   
    <dx:ASPxWebDocumentViewer ID="ASPxWebDocumentViewer1" runat="server">
    </dx:ASPxWebDocumentViewer>

</asp:Content>