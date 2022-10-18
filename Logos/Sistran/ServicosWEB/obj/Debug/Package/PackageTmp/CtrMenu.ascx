<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrMenu.ascx.cs" Inherits="ServicosWEB.CtrMenu" %>
<div style="background-color:Black; font-size:10px; text-align:center; height:27px" >
    <asp:Button ID="Button2" runat="server" Text="Página Inicial"  
        style="border:1px solid white; background-color:white; text-transform:uppercase; font-family:Verdana; width:200px; border-left:1px solid black; margin-top:3px " 
        PostBackUrl="~/EnviarConferencia.aspx"/>
    <asp:Button ID="Button3" runat="server" Text="Volumes Faltantes" 
        style="border:1px solid white; background-color:white; text-transform:uppercase; font-family:Verdana; width:200px; border-left:1px solid black; " 
        PostBackUrl="~/rptFaltaDeVolumes.aspx" />
    <asp:Button ID="Button4" runat="server" Text="Faltas Filiais" 
        style="border:1px solid white; background-color:white; text-transform:uppercase; font-family:Verdana; width:200px; border-left:1px solid black; " 
        PostBackUrl="~/rptFilialFalta.aspx" />
    <asp:Button ID="Button1" runat="server" Text="Sobras Filiais (Em Breve)" 
        style="border:1px solid white; background-color:white; text-transform:uppercase; font-family:Verdana; width:200px; border-left:1px solid black; " 
        Visible="False" />
    <asp:Button ID="Button5" runat="server" Text="Itens que mais Faltam(Em Breve)" 
        style="border:1px solid white; background-color:white; text-transform:uppercase; font-family:Verdana; width:200px; border-left:1px solid black; " 
        Visible="False" />

    <asp:Button ID="Button6" runat="server" Text="Sobras"  
        style="border:1px solid white; background-color:white; text-transform:uppercase; font-family:Verdana; width:200px; border-left:1px solid black; margin-top:3px " 
        PostBackUrl="~/Sobras.aspx"/>

</div>