<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ListView.Proj._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="IdRastreador" 
            DataSourceID="SqlDataSource1" EnableModelValidation="True" 
            InsertItemPosition="LastItem">
        <AlternatingItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" 
                        Text="Delete" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="IdRastreadorLabel" runat="server" 
                        Text='<%# Eval("IdRastreador") %>' />
                </td>
                <td>
                    <asp:Label ID="ChaveLabel" runat="server" Text='<%# Eval("Chave") %>' />
                </td>
                <td>
                    <asp:Label ID="NomeLabel" runat="server" Text='<%# Eval("Nome") %>' />
                </td>
                <td>
                    <asp:Label ID="TempoLabel" runat="server" Text='<%# Eval("Tempo") %>' />
                </td>
                <td>
                    <asp:Label ID="EnviaPosicaoZeradaLabel" runat="server" 
                        Text='<%# Eval("EnviaPosicaoZerada") %>' />
                </td>
                <td>
                    <asp:Label ID="EnviaFotosLabel" runat="server" 
                        Text='<%# Eval("EnviaFotos") %>' />
                </td>
                <td>
                    <asp:Label ID="NumeroChipLabel" runat="server" 
                        Text='<%# Eval("NumeroChip") %>' />
                </td>
                <td>
                    <asp:Label ID="EnviaFotoLabel" runat="server" Text='<%# Eval("EnviaFoto") %>' />
                </td>
                <td>
                    <asp:Label ID="UltimaSincronizacaoLabel" runat="server" 
                        Text='<%# Eval("UltimaSincronizacao") %>' />
                </td>
                <td>
                    <asp:Label ID="UltimaDTLabel" runat="server" Text='<%# Eval("UltimaDT") %>' />
                </td>
                <td>
                    <asp:Label ID="UltimaPlacaLabel" runat="server" 
                        Text='<%# Eval("UltimaPlaca") %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                        Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                        Text="Cancel" />
                </td>
                <td>
                    <asp:Label ID="IdRastreadorLabel1" runat="server" 
                        Text='<%# Eval("IdRastreador") %>' />
                </td>
                <td>
                    <asp:TextBox ID="ChaveTextBox" runat="server" Text='<%# Bind("Chave") %>' />
                </td>
                <td>
                    <asp:TextBox ID="NomeTextBox" runat="server" Text='<%# Bind("Nome") %>' />
                </td>
                <td>
                    <asp:TextBox ID="TempoTextBox" runat="server" Text='<%# Bind("Tempo") %>' />
                </td>
                <td>
                    <asp:TextBox ID="EnviaPosicaoZeradaTextBox" runat="server" 
                        Text='<%# Bind("EnviaPosicaoZerada") %>' />
                </td>
                <td>
                    <asp:TextBox ID="EnviaFotosTextBox" runat="server" 
                        Text='<%# Bind("EnviaFotos") %>' />
                </td>
                <td>
                    <asp:TextBox ID="NumeroChipTextBox" runat="server" 
                        Text='<%# Bind("NumeroChip") %>' />
                </td>
                <td>
                    <asp:TextBox ID="EnviaFotoTextBox" runat="server" 
                        Text='<%# Bind("EnviaFoto") %>' />
                </td>
                <td>
                    <asp:TextBox ID="UltimaSincronizacaoTextBox" runat="server" 
                        Text='<%# Bind("UltimaSincronizacao") %>' />
                </td>
                <td>
                    <asp:TextBox ID="UltimaDTTextBox" runat="server" 
                        Text='<%# Bind("UltimaDT") %>' />
                </td>
                <td>
                    <asp:TextBox ID="UltimaPlacaTextBox" runat="server" 
                        Text='<%# Bind("UltimaPlaca") %>' />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="">
                <tr>
                    <td>
                        No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                        Text="Insert" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                        Text="Clear" />
                </td>
                <td>
                    <asp:TextBox ID="IdRastreadorTextBox" runat="server" 
                        Text='<%# Bind("IdRastreador") %>' />
                </td>
                <td>
                    <asp:TextBox ID="ChaveTextBox" runat="server" Text='<%# Bind("Chave") %>' />
                </td>
                <td>
                    <asp:TextBox ID="NomeTextBox" runat="server" Text='<%# Bind("Nome") %>' />
                </td>
                <td>
                    <asp:TextBox ID="TempoTextBox" runat="server" Text='<%# Bind("Tempo") %>' />
                </td>
                <td>
                    <asp:TextBox ID="EnviaPosicaoZeradaTextBox" runat="server" 
                        Text='<%# Bind("EnviaPosicaoZerada") %>' />
                </td>
                <td>
                    <asp:TextBox ID="EnviaFotosTextBox" runat="server" 
                        Text='<%# Bind("EnviaFotos") %>' />
                </td>
                <td>
                    <asp:TextBox ID="NumeroChipTextBox" runat="server" 
                        Text='<%# Bind("NumeroChip") %>' />
                </td>
                <td>
                    <asp:TextBox ID="EnviaFotoTextBox" runat="server" 
                        Text='<%# Bind("EnviaFoto") %>' />
                </td>
                <td>
                    <asp:TextBox ID="UltimaSincronizacaoTextBox" runat="server" 
                        Text='<%# Bind("UltimaSincronizacao") %>' />
                </td>
                <td>
                    <asp:TextBox ID="UltimaDTTextBox" runat="server" 
                        Text='<%# Bind("UltimaDT") %>' />
                </td>
                <td>
                    <asp:TextBox ID="UltimaPlacaTextBox" runat="server" 
                        Text='<%# Bind("UltimaPlaca") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" 
                        Text="Delete" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="IdRastreadorLabel" runat="server" 
                        Text='<%# Eval("IdRastreador") %>' />
                </td>
                <td>
                    <asp:Label ID="ChaveLabel" runat="server" Text='<%# Eval("Chave") %>' />
                </td>
                <td>
                    <asp:Label ID="NomeLabel" runat="server" Text='<%# Eval("Nome") %>' />
                </td>
                <td>
                    <asp:Label ID="TempoLabel" runat="server" Text='<%# Eval("Tempo") %>' />
                </td>
                <td>
                    <asp:Label ID="EnviaPosicaoZeradaLabel" runat="server" 
                        Text='<%# Eval("EnviaPosicaoZerada") %>' />
                </td>
                <td>
                    <asp:Label ID="EnviaFotosLabel" runat="server" 
                        Text='<%# Eval("EnviaFotos") %>' />
                </td>
                <td>
                    <asp:Label ID="NumeroChipLabel" runat="server" 
                        Text='<%# Eval("NumeroChip") %>' />
                </td>
                <td>
                    <asp:Label ID="EnviaFotoLabel" runat="server" Text='<%# Eval("EnviaFoto") %>' />
                </td>
                <td>
                    <asp:Label ID="UltimaSincronizacaoLabel" runat="server" 
                        Text='<%# Eval("UltimaSincronizacao") %>' />
                </td>
                <td>
                    <asp:Label ID="UltimaDTLabel" runat="server" Text='<%# Eval("UltimaDT") %>' />
                </td>
                <td>
                    <asp:Label ID="UltimaPlacaLabel" runat="server" 
                        Text='<%# Eval("UltimaPlaca") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table ID="itemPlaceholderContainer" runat="server" border="0" style="">
                            <tr runat="server" style="">
                                <th runat="server">
                                </th>
                                <th runat="server">
                                    IdRastreador</th>
                                <th runat="server">
                                    Chave</th>
                                <th runat="server">
                                    Nome</th>
                                <th runat="server">
                                    Tempo</th>
                                <th runat="server">
                                    EnviaPosicaoZerada</th>
                                <th runat="server">
                                    EnviaFotos</th>
                                <th runat="server">
                                    NumeroChip</th>
                                <th runat="server">
                                    EnviaFoto</th>
                                <th runat="server">
                                    UltimaSincronizacao</th>
                                <th runat="server">
                                    UltimaDT</th>
                                <th runat="server">
                                    UltimaPlaca</th>
                            </tr>
                            <tr ID="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                    ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                <asp:NumericPagerField />
                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" 
                                    ShowNextPageButton="False" ShowPreviousPageButton="False" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" 
                        Text="Delete" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="IdRastreadorLabel" runat="server" 
                        Text='<%# Eval("IdRastreador") %>' />
                </td>
                <td>
                    <asp:Label ID="ChaveLabel" runat="server" Text='<%# Eval("Chave") %>' />
                </td>
                <td>
                    <asp:Label ID="NomeLabel" runat="server" Text='<%# Eval("Nome") %>' />
                </td>
                <td>
                    <asp:Label ID="TempoLabel" runat="server" Text='<%# Eval("Tempo") %>' />
                </td>
                <td>
                    <asp:Label ID="EnviaPosicaoZeradaLabel" runat="server" 
                        Text='<%# Eval("EnviaPosicaoZerada") %>' />
                </td>
                <td>
                    <asp:Label ID="EnviaFotosLabel" runat="server" 
                        Text='<%# Eval("EnviaFotos") %>' />
                </td>
                <td>
                    <asp:Label ID="NumeroChipLabel" runat="server" 
                        Text='<%# Eval("NumeroChip") %>' />
                </td>
                <td>
                    <asp:Label ID="EnviaFotoLabel" runat="server" Text='<%# Eval("EnviaFoto") %>' />
                </td>
                <td>
                    <asp:Label ID="UltimaSincronizacaoLabel" runat="server" 
                        Text='<%# Eval("UltimaSincronizacao") %>' />
                </td>
                <td>
                    <asp:Label ID="UltimaDTLabel" runat="server" Text='<%# Eval("UltimaDT") %>' />
                </td>
                <td>
                    <asp:Label ID="UltimaPlacaLabel" runat="server" 
                        Text='<%# Eval("UltimaPlaca") %>' />
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GrupoLogosConnectionString %>" 
            DeleteCommand="DELETE FROM [Rastreador] WHERE [IdRastreador] = @IdRastreador" 
            InsertCommand="INSERT INTO [Rastreador] ([IdRastreador], [Chave], [Nome], [Tempo], [EnviaPosicaoZerada], [EnviaFotos], [NumeroChip], [EnviaFoto], [UltimaSincronizacao], [UltimaDT], [UltimaPlaca]) VALUES (@IdRastreador, @Chave, @Nome, @Tempo, @EnviaPosicaoZerada, @EnviaFotos, @NumeroChip, @EnviaFoto, @UltimaSincronizacao, @UltimaDT, @UltimaPlaca)" 
            SelectCommand="SELECT * FROM [Rastreador]" 
            UpdateCommand="UPDATE [Rastreador] SET [Chave] = @Chave, [Nome] = @Nome, [Tempo] = @Tempo, [EnviaPosicaoZerada] = @EnviaPosicaoZerada, [EnviaFotos] = @EnviaFotos, [NumeroChip] = @NumeroChip, [EnviaFoto] = @EnviaFoto, [UltimaSincronizacao] = @UltimaSincronizacao, [UltimaDT] = @UltimaDT, [UltimaPlaca] = @UltimaPlaca WHERE [IdRastreador] = @IdRastreador">
            <DeleteParameters>
                <asp:Parameter Name="IdRastreador" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="IdRastreador" Type="Int32" />
                <asp:Parameter Name="Chave" Type="String" />
                <asp:Parameter Name="Nome" Type="String" />
                <asp:Parameter Name="Tempo" Type="Int32" />
                <asp:Parameter Name="EnviaPosicaoZerada" Type="String" />
                <asp:Parameter Name="EnviaFotos" Type="String" />
                <asp:Parameter Name="NumeroChip" Type="String" />
                <asp:Parameter Name="EnviaFoto" Type="String" />
                <asp:Parameter Name="UltimaSincronizacao" Type="DateTime" />
                <asp:Parameter Name="UltimaDT" Type="Int32" />
                <asp:Parameter Name="UltimaPlaca" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="Chave" Type="String" />
                <asp:Parameter Name="Nome" Type="String" />
                <asp:Parameter Name="Tempo" Type="Int32" />
                <asp:Parameter Name="EnviaPosicaoZerada" Type="String" />
                <asp:Parameter Name="EnviaFotos" Type="String" />
                <asp:Parameter Name="NumeroChip" Type="String" />
                <asp:Parameter Name="EnviaFoto" Type="String" />
                <asp:Parameter Name="UltimaSincronizacao" Type="DateTime" />
                <asp:Parameter Name="UltimaDT" Type="Int32" />
                <asp:Parameter Name="UltimaPlaca" Type="String" />
                <asp:Parameter Name="IdRastreador" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
