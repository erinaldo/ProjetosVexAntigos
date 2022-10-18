<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComproveiRec.aspx.cs" Inherits="ServicosWEB.ComproveiRec" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>BAIXAS DO COMPROVEI</title>
    <style type="text/css">
        body
        {
            font-size: 9px;
            margin: 0 0 0 0;
            font-family: Tahoma;
            white-space: nowrap;
            width:100%;
        }
        </style>
         <script language="javascript" type="text/javascript">


       


        function mascara_data(campo) {
            if (campo.value.length == 2) {
                campo.value += '/';
            }

            if (campo.value.length == 5) {
                campo.value += '/';
            }

            if (campo.value.length == 10)
                campo.value += ' ';

            if (campo.value.length == 13)
                campo.value += ':';
        }

        function formataData(campo, evt) {
            //dd/MM/yyyy
            //alert(campo);
            evt = getEvent(evt);
            var tecla = getKeyCode(evt);
            if (!teclaValida(tecla))
                return;
            vr = campo.value = filtraNumeros(filtraCampo(campo));
            tam = vr.length;
            if (tam >= 2 && tam < 4)
                campo.value = vr.substr(0, 2) + '/' + vr.substr(2);
            if (tam == 4)
                campo.value = vr.substr(0, 2) + '/' + vr.substr(2, 2) + '/';

            if (tam > 4)
                campo.value = vr.substr(0, 2) + '/' + vr.substr(2, 2) + '/' + vr.substr(4);
        }

        function fctValidaData(obj) {
            var data = obj.value;
            //alert(data);

            if (data == "")
                return true;
            //10/10/2010 13:15
            var dia = data.substring(0, 2);
            var mes = data.substring(3, 5);
            var ano = data.substring(6, 10);
            var hora = data.substring(11, 13);
            var min = data.substring(14, 16);


            //Criando um objeto Date usando os valores ano, mes e dia.
            var novaData = new Date(ano, (mes - 1), dia);

            var mesmoDia = parseInt(dia, 10) == parseInt(novaData.getDate());
            var mesmoMes = parseInt(mes, 10) == parseInt(novaData.getMonth()) + 1;
            var mesmoAno = parseInt(ano) == parseInt(novaData.getFullYear());

            if (!((mesmoDia) && (mesmoMes) && (mesmoAno))) {
                alert('Data informada é inválida!');
                obj.focus();
                return false;
            }

            if (parseInt(hora) > 23 || parseInt(min) > 59) {
                alert('Hora informada é inválida!');
                obj.focus();
                return false;
            }



            return true;
        }

    </script>
   
</head>
<body>
    <form id="form1" runat="server" >
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table class="style1" style="width:100%">
            <tr>
                <td>
                    <h1 style="text-align: center">
                       BAIXAS DO COMPROVEI
                    </h1>
                    <div style="text-align: right">
                        <asp:Label ID="txtProcessado" runat="server"></asp:Label>
                    </div>

                     <div style="text-align: center">
                         Número:&nbsp;
                         <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

                         Data:&nbsp;
                         <asp:TextBox ID="txtDataI" runat="server" BorderColor="#999999" BorderStyle="Solid"
                                BorderWidth="1px" MaxLength="16" onblur="fctValidaData(this);" onKeyUp="mascara_data(this)"
                                TabIndex="2" Width="150px"></asp:TextBox>

Cliente:&nbsp;
                         <asp:TextBox ID="txtCliente" runat="server"></asp:TextBox>

                         Filial:&nbsp;<asp:DropDownList ID="cboFilial" runat="server">
                         </asp:DropDownList>



                         <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                             Text="PESQUISAR" BackColor="White" BorderColor="#666666" />

                              <asp:Button ID="btnExcel" runat="server" 
                             Text="Excel" BackColor="White" BorderColor="#666666" 
                             onclick="btnExcel_Click" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" EnableModelValidation="True"
                        ForeColor="Black" GridLines="Vertical" Width="100%" AllowPaging="True" 
                        onpageindexchanging="GridView1_PageIndexChanging" onsorted="GridView1_Sorted" 
                        onsorting="GridView1_Sorting" PageSize="100">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" 
                            Font-Size="12px" VerticalAlign="Middle" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick">
    </asp:Timer>
    </form>
</body>
</html>
