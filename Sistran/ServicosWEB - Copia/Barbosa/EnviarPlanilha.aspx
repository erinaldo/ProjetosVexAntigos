<%@ Page Title="" Language="C#" MasterPageFile="~/Girotrade/Site1.Master" AutoEventWireup="true" CodeBehind="EnviarPlanilha.aspx.cs" Inherits="ServicosWEB.Barbosa.EnviarPlanilha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        body{
            font-family:   'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: 14px;
        }
        body input{
            border: 1px solid silver;
            font-size: 14px;
            background-color: white;

        }
    </style>
    <h1></h1>

    <div style="width:50%; margin:0 auto; border: 1px solid silver; border-radius: 5px">
    <table style="width:100%; padding:50px">
        <tr>
            <td>Selecione um arquivo Excel:</td>
            <td><asp:FileUpload ID="FileUpload1" style=" margin-left:30px;" runat="server" />    </td>
        </tr>

        <tr>
            <td></td>
            <td style="text-align:right"><asp:button id="button1" runat="server" text="Confirmar" OnClick="button1_Click"    /> </td>
        </tr>


        <tr>
            <td style="text-align:center" colspan="2"><asp:Label ID="Label1" runat="server"></asp:Label></td>
            
        </tr>

    </table>

    
    </div>
    
    

</asp:Content>
