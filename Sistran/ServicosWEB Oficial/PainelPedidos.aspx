<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PainelPedidos.aspx.cs" Inherits="ServicosWEB.PainelPedidos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Painel de Pedidos</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
</head>


<body>
    <form id="form1" runat="server">
        <div class="container-fluid" style="background-color: black">
            <div class="row">
                <div class="col">
                    <img src="Img/logo.png" style="height: 100px;" />
                </div>
                <div class="col">

                    <h2 style="color: white; font-size: 20px; text-align: center; vertical-align: middle; margin-top: 30px">PAINEL DE PEDIDOS</h2>
                </div>
                <div class="col">
                    ....
                </div>
            </div>

        </div>
        <br />
        <div class="container-fluid" style="width:95%; margin:0 auto">
            <div class="row">
                <table class="table table-warning table-striped">
                    <tr>
                        
                        <th colspan="2" style="text-align:center">FILIAL / CLIENTE</th>
                        <th style="text-align:right; text-transform:uppercase">Total de Pedidos</th>
                        <th style="width:50px; background-color:black"></th>

                        <th style="text-align:right; text-transform:uppercase">Aguardando o Cliente Autorizar</th>
                        <th style="text-align:right; text-transform:uppercase">Liberado Para Separação</th>
                        <th style="text-align:right; text-transform:uppercase">Em Separação</th>
                        <th style="text-align:right; text-transform:uppercase">Separação Concluída</th>
                        <th style="text-align:right; text-transform:uppercase">Notas Fiscais Emitidas</th>
                        <th style="text-align:right; text-transform:uppercase">Aguardando Embarque</th>
                        <th style="text-align:right; text-transform:uppercase">Em Entrega</th>
                        <th style="text-align:right; text-transform:uppercase">Entregue</th>


                    </tr>


                    <tr>
                        <th></th>
                        <th style="white-space:nowrap">LOGISTICA "C"</th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>


                    </tr>


                    <tr>
                        <td style="text-align:right">114091</td>
                        <td style="white-space:nowrap">GIROTRADE S/A</td>
                        <td style="text-align:right">100</td>
                        <td></td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right"></td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>

                        
                    </tr>

                     <tr>
                        <td style="text-align:right">3102</td>
                        <td style="white-space:nowrap">BONDUELLE DO BRASIL PRODUTOS A</td>
                        <td style="text-align:right">100</td>
                        <td></td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right"></td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>

                        
                    </tr>

                     <tr>
                        <td style="text-align:right">5009059</td>
                        <td style="white-space:nowrap">CASA SANTA LUZIA IMPORTADORA L</td>
                        <td style="text-align:right">100</td>
                        <td></td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right"></td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>

                        
                    </tr>


                     <tr>
                        <th></th>
                        <th style="white-space:nowrap">MAIS BRASIL</th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>


                    </tr>

                    <tr>
                        <td style="text-align:right">5300980</td>
                        <td style="white-space:nowrap">MAIS BRASIL ARMAZENAGEM</td>
                        <td style="text-align:right">100</td>
                        <td></td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right"></td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>

                        
                    </tr>

                     <tr>
                        <th></th>
                        <th style="white-space:nowrap">LAPA</th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>


                    </tr>

                    <tr>
                        <td style="text-align:right">62694</td>
                        <td style="white-space:nowrap">INTERG COMERCIO E DISTRIBUIDOR</td>
                        <td style="text-align:right">100</td>
                        <td></td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right"></td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>
                        <td style="text-align:right">10</td>

                        
                    </tr>




                    <%--  <tr>
                        <td>3102</td>
                        <td>BONDUELLE DO BRASIL PRODUTOS A</td>
                        <td>10</td>
                        <td>10</td>
                        <td>10</td>
                        <td>10</td>
                        <td>10</td>
                        <td>10</td>
                        <td>10</td>
                        <td>20</td>
                        <td>20</td>
                        <td>20</td>

                    </tr>

                     <tr>
                        <td>232323</td>
                        <td>CASA SANTA LUZIA IMPORTADORA L</td>
                        <td>10</td>
                        <td>10</td>
                        <td>10</td>
                        <td>10</td>
                        <td>10</td>
                        <td>10</td>
                        <td>10</td>
                        <td>30</td>
                        <td>30</td>
                        <td>30</td>

                    </tr>--%>
                   
  
</table>

        </div>
            </div>



    </form>
</body>
</html>
