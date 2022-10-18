<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="GaiolaDiiver.aspx.cs" Inherits="ServicosWEB.GaiolaDiiver" %>




<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
<h2 style="text-align: center">GAIOLAS COM DIVERGÊNCIAS</h2>
</div>



<%--<fieldset style="margin:0 auto; width:99%; border: 1px solid silver;min-height:400px;">
<legend  style="font-size:12px">Pendencias</legend>
    <br />

            <asp:GridView ID="GridView1" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                EnableModelValidation="True" ForeColor="Black" GridLines="Horizontal"
                Width="99%" onrowcommand="GridView1_RowCommand" 
        style="margin:0 auto" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="IDGAIOLA" HeaderText="CÓDIGO" >
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DATA" HeaderText="DATA" >
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NUMEROCOLETOR" HeaderText="COLETOR" />
                    <asp:BoundField DataField="NOMEFILIAL" HeaderText="FILIAL" />
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" Height="16px" 
                                ImageUrl="~/Img/editar.png" CommandArgument='<%# Eval("[idgaiola]") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" 
                    HorizontalAlign="Left" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        
    <br />
</fieldset>
--%>


    <div style="width: 99%; margin:0 auto; ">                           
            <fieldset style="margin:0 auto; width:99%; border: 1px solid silver; min-height:400px" >
            <legend style="font-size:14px; font-weight:bold">DIVERGÊNCIAS</legend>
                            <asp:Label ID="lblMensagem" runat="server" Font-Bold="True" ForeColor="Red" ></asp:Label>

            
                <asp:GridView ID="GridView2" runat="server" 
                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="4" EnableModelValidation="True" ForeColor="Black" 
                    GridLines="Horizontal"  
                    style="margin:0 auto" Width="100%" AutoGenerateColumns="False" 
                    onrowcommand="GridView2_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="CÓDIGO DA GAIOLA" HeaderText="GAIOLA">
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDeletaGaiola" runat="server" Height="16px" CommandName='<%# Eval("IDGAIOLACONFERENCIA") + "|" +Eval("GAIOLACONFERENCIALIDA") + "|" +Eval("[CÓDIGO DA GAIOLA]") %>' CommandArgument="DeletarGaiola"
                                    ImageUrl="~/Img/images.jpg" />
                                    <%--<asp:Label ID="lblDeletaGaiola" runat="server" Visible="true" Text= '<%# Eval("IDGAIOLACONFERENCIA") + "|" +Eval("GAIOLACONFERENCIALIDA") %>'></asp:Label>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DATA" HeaderText="DATA">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NOME DA FILIAL" HeaderText="FILIAL" />
                        <asp:BoundField DataField="USUÁRIO" HeaderText="USUÁRIO" />
                        <asp:BoundField DataField="VOLUME" HeaderText="VOLUME" />
                        <asp:BoundField DataField="STATUS" HeaderText="STATUS" />
                        <asp:BoundField DataField="CÓDIGO GAIOLA LIDA" HeaderText="GAIOLA LIDA">
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDeletaGaiolaLida" runat="server" Height="16px" CommandName='<%# Eval("IDGAIOLACONFERENCIA") + "|" +Eval("GAIOLACONFERENCIALIDA") + "|" +Eval("[CÓDIGO GAIOLA LIDA]") %>'  CommandArgument="DeletarGaiolaLida"
                                    ImageUrl="~/Img/images.jpg" />
                                    

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" 
                        HorizontalAlign="Left" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            
            </fieldset>
                <br />
            

           


            <br />

    </div>

    <br />

    </asp:Content>
