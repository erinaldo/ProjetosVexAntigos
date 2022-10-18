<%@ control language="C#" autoeventwireup="true" inherits="UserControls_ctrCargasDispDet, App_Web_ctrcargasdispdet.ascx.6bb32623" %>
<script language="javascript" type="text/javascript">
    function ExpandirGrupo(qtdLinhas, dv, idAgrupamento) 
    {

        //alert(document.getElementById("dv_"+idAgrupamento));
        //return;

        for (var i = 0; i < qtdLinhas; i++) {



            if (document.getElementById(idAgrupamento + i.toString()).style.display == 'block') {
                // alert('block');
                document.getElementById(idAgrupamento + i.toString()).style.display = 'none';
                document.getElementById(dv).style.background = "url('plus.gif') no-repeat";

            }
            else {
                // alert('none');
                document.getElementById(idAgrupamento + i.toString()).style.display = 'block';
                document.getElementById(dv).style.background = "url('minus.gif') no-repeat";
            }
        }
    }


    function ExpandirRegiao(qtdLinhas, dv, linha) {

        for (var i = 0; i < qtdLinhas; i++) {
            if (document.getElementById(linha + i.toString()).style.display == 'block') {
                document.getElementById(linha + i.toString()).style.display = 'none';
                document.getElementById(dv).style.background = "url('plus.gif') no-repeat";

            }
            else {
                document.getElementById(linha + i.toString()).style.display = 'block';
                document.getElementById(dv).style.background = "url('minus.gif') no-repeat";
            }
        }
    }

    function ExpandirSetor(qtdLinhas, dv, linha) 
    {

        //alert(dv);
        //return;


        for (var i = 0; i < qtdLinhas; i++) 
        {
            if (document.getElementById(linha + i.toString()).style.display == 'block') {
                document.getElementById(linha + i.toString()).style.display = 'none';
                document.getElementById(dv).style.background = "url('plus.gif') no-repeat";

            }
            else 
            {
                document.getElementById(linha + i.toString()).style.display = 'block';
                document.getElementById(dv).style.background = "url('minus.gif') no-repeat";
            }
        }
    }
    
</script>
<asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
