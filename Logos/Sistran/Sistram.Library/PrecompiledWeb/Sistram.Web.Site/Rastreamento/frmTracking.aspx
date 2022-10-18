<%@ page title="" language="C#" masterpagefile="~/Rastreamento/Rastreamento.master" autoeventwireup="true" inherits="Rastreamento_frmTracking, App_Web_2h00s21j" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
        .link
        {
            font-size: 8pt;
            font-family: Verdana;
            font-weight: normal;
            color: Black;
            cursor: Hand;
        }
        .link:link
        {
            font-size: 8pt;
            font-family: Verdana;
            font-weight: normal;
            color: Black;
            cursor: Hand;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="style2">
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black"
                    GridLines="Vertical" Width="100%">
                    <RowStyle Font-Bold="False" CssClass="link" />
                    <Columns>
                        <asp:BoundField DataField="DATAOCORRENCIA" HeaderText="DATA" DataFormatString="{0:dd/MM/yyyy}" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NOMEREDUZIDO" HeaderText="OPERAÇÃO">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FILIAL" HeaderText="FILIAL">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="OBERVAÇÕES">
                            <ItemTemplate>
                                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                    <tr>
                                        <td>
                                            <b>DATA:</b>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblData" runat="server" Text='<%# Convert.ToDateTime(Eval("DATADECONCLUSAO_OBS")).ToString("dd/MM/yyyy")  %>'></asp:Label>
                                        </td>
                                        <td>
                                            <b></b>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>OCORRÊNCIA:</b>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOcorrencia" runat="server" Text='<%# Eval("CODIGO_OBS") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <b>DESCRIÇÃO:</b>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescricao" runat="server" Text='<%# Eval("DESCRICAO_OBS") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>FILIAL:</b>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFilial" runat="server" Text='<%# Eval("NUMERODAFILIAL_OBS") %>' ></asp:Label>
                                        </td>
                                        <td>
                                            <b>NOME FILIAL:</b>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNomeFilial" runat="server" Text='<%# Eval("NOME_OBS") %>'></asp:Label>
                                        </td>
                                    </tr>
                                   
                                </table>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#CCCCCC" BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px"
                        Font-Bold="True" Font-Names="Verdana" ForeColor="Black" />
                    <AlternatingRowStyle BackColor="#CCCCCC" Font-Bold="False" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
