<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title># SistranWeb</title>

(#css)

(#script)

</head>

<body>
<div id="imprimir">
</div>


<div id="layout_background">
</div>
<div id="layout_loading">
</div>
    
<div id="principal" style="position:absolute; left:0; top:0; width:100%; height:100%; z-index:1; background-color:#FFFFFF; visibility: hidden;">

<div id="popmenu" style="visibility:hidden; position:absolute; left:0px; top:0px; z-index:9000;">
	<div id="popmenu_show">
    </div>
    <input type="hidden" id="popmenu_posicao_x">
    <input type="hidden" id="popmenu_posicao_y">
</div>


<div id="topo" style="position:absolute; left:0; top:0; width:100%; z-index:2; visibility: visible;">
	<div style="position:absolute; left:20; top:10;">
	(#logo)
	</div>
	  <table width="100%"  border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td height="49" colspan="2" background="img/topo_1_2.jpg">&nbsp;</td>
          <td align="right" background="img/topo_1_2.jpg">&nbsp;
			
		  </td>
        </tr>
        <tr>
          <td width="220" height="27" background="img/topo_2_1.jpg">&nbsp;</td>
          <td width="1" valign="top"><img src="img/topo_2_2.jpg" width="90" height="27"></td>
          <td background="img/topo_2_3.jpg">&nbsp;</td>
        </tr>
      </table>
  </div>
  
	<div 
	 id="menu" 
	 style="position:absolute; left:0px; top:0px; width:20%; height:100%; z-index:2; visibility:visible;">
	 	<table
		 style="position: absolute; top:76px; width:100%; height:100%;" 
		 border="0" cellpadding="0" cellspacing="0">
	 		<tr>
	 			<td
				 style="height:25px; background-image:url(img/menu_3_2.jpg);">
				 	<img src="img/menu_3_1.png">
				</td>
			</tr>
	 		<tr>
	 			<td>
	 				<div 
					 id="show_menu" 
					 style="position:relative; width:100%; height:100%;"> 
						(#menu)
					</div>
				</td>
			</tr>
	 		<tr>
	 			<td 
				 style="height:22px; background-image:url(img/menu_3_2.jpg);">
					<img src="img/menu_6_1.jpg">
				</td>
			</tr>
			<tr>
	 			<td style="height:180px;">
				 	<div 
					 id="show_login" 
					 style="position:relative; width:100%; height:100%; overflow:auto;"> 
						(#login)
					</div>
				</td>
			</tr>
		</table>
		<div 
		 style="position: absolute; top:100%; height:0px; width:100%;">
			<div style="position: absolute; top:-30px; width:100%; height:33px; background-image:url(img/menu_4_1.jpg);">
				<img src="img/menu_4_1.png">
			</div>
	    </div>
	</div>
	
	<div id="barra_menu" style="position:absolute; left:20%; top:0; width:15; height:100%; z-index:4; visibility: visible;">
	  <table width="100%" onClick="moveMenu();" height="100%"  border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td height="76">&nbsp;</td>
        </tr>
        <tr>
          <td width="10" background="img/menu_1_1.jpg"><a href="#"> <img src="img/menu_1_2.jpg" width="15" height="41" border="0"> </a> </td>
        </tr>
        <tr>
          <td height="32"><img src="img/menu_2_1.jpg" width="15" height="32"></td>
        </tr>
      </table>
	</div>
  <div id="conteudo" style="position:absolute; left:20%; top:0; width:80%; height:100%; z-index:3; visibility: visible;">
        
    <div id="carregador" style="position:absolute; top:127; width:100%; z-index: 300;">    </div>
    
    <!-- <div id="conteudo_botao_cliente" align="center" style="position: absolute; top:83px; left:88%; width:100px; z-index: 200;">
	  <a href="#" onClick="msg_window_show('SESSÃO', 'LOG00004.PHP', '600px', '120px');">
		  <img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/22/actions/identity.png" border="0">
		  <br>
		  Alterar Cliente
	  </a> 	
	</div>
	 -->
	 <div id="conteudo_botao_manual" align="center" style="position: absolute; top:83px; left:88%; width:100px; z-index: 200;">
	  <a href="#" onClick="SISTRAN_downloads('manual.pdf')">
		  <img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/22/places/user-desktop.png" border="0">
		  <br>
		  Manual
	  </a> 	
	</div>
	
    <div id="conteudo_botao_ajuda" style="position:absolute; top:77; left:490; z-index: 102; visibility: hidden;">
      <table height="47" border="0" cellpadding="1" cellspacing="1">
        <tr>
          <td width="50" align="center">
		  	<a id="conteudo_botao_link_ajuda" href="#">
		  		<img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/22/actions/help-faq.png" border="0"><br>
        		Ajuda 
			</a>
		  </td>
        </tr>
      </table>
    </div>
    
    <div id="conteudo_botao_transferir" style="position:absolute; top:77; left:490; z-index: 102; visibility: hidden;">
      <table height="47" border="0" cellpadding="1" cellspacing="1">
        <tr>
          <td width="50" align="center"><a id="conteudo_botao_link_transferir" href="#"><img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/22/actions/transferir.png" border="0"><br>
        Transferir</a></td>
        </tr>
      </table>
    </div>
        
    <div id="conteudo_botao_filtrar" style="position:absolute; top:77; left:490; z-index: 102; visibility: hidden;">
      <table height="47" border="0" cellpadding="1" cellspacing="1">
        <tr>
          <td width="50" align="center"><a id="conteudo_botao_link_filtrar" href="#"><img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/22/actions/system-run.png" border="0"><br>
        Filtrar </a></td>
        </tr>
      </table>
    </div>

    <div id="conteudo_botao_config" style="position:absolute; top:77; left:490; z-index: 102; visibility: hidden;">
      <table height="47" border="0" cellpadding="1" cellspacing="1">
        <tr>
          <td width="50" align="center"><a id="conteudo_botao_link_config" href="#"><img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/22/actions/tools-check-spelling.png" border="0"><br>
        Lay-out </a></td>
        </tr>
      </table>
    </div>
    
    <div id="conteudo_botao_novo" style="position:absolute; top:77; left:20; z-index: 102; visibility: hidden;">
      <table height="47" border="0" cellpadding="1" cellspacing="1">
        <tr>
          <td width="50" align="center"><p><a id="conteudo_botao_link_novo" href="#"><img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/22/actions/document-new.png" border="0"><br>
          Novo </a></p></td>
        </tr>
      </table>
    </div>
    <div id="conteudo_botao_editar" style="position:absolute; top:77; left:75; z-index: 102; visibility: hidden;">
      <table height="47" border="0" cellpadding="1" cellspacing="1">
        <tr>
          <td width="50" align="center"><a id="conteudo_botao_link_editar" href="#"><img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/22/actions/edit.png" border="0"><br>
        Editar </a></td>
        </tr>
      </table>
    </div>
    <div id="conteudo_botao_salvar" style="position:absolute; top:77; left:130; z-index: 102; visibility: hidden;">
      <table height="47" border="0" cellpadding="1" cellspacing="1">
        <tr>
          <td width="50" align="center"><a id="conteudo_botao_link_salvar" href="#"><img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/22/actions/document-save-all.png" border="0"><br>
        Salvar</a> </td>
        </tr>
      </table>
    </div>
    <div id="conteudo_botao_excluir" style="position:absolute; top:77; left:185; z-index: 102; visibility: hidden;">
      <table height="47" border="0" cellpadding="1" cellspacing="1">
        <tr>
          <td width="50" align="center"><a id="conteudo_botao_link_excluir" href="#"><img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/22/actions/edit-delete.png" border="0"><br>
        Excluir</a> </td>
        </tr>
      </table>
    </div>    
    <div id="conteudo_botao_voltar" style="position:absolute; top:77; left:240; z-index: 102; visibility: hidden;">
      <table height="47" border="0" cellpadding="1" cellspacing="1">
        <tr>
          <td width="50" align="center"><a id="conteudo_botao_link_voltar" href="#"><img id="conteudo_botao_link_voltar_img" src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/22/actions/go-previous.png" border="0"><br>
        Voltar</a> </td>
        </tr>
      </table>
    </div>

    <div id="conteudo_botao_sair" style="position:absolute; top:77; left:240; z-index: 102; visibility: hidden;">
      <table height="47" border="0" cellpadding="1" cellspacing="1">
        <tr>
          <td width="50" align="center"><a id="conteudo_botao_link_sair" href="#"><img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/22/actions/application-exit.png" border="0"><br>
        Sair</a> </td>
        </tr>
      </table>
    </div>

    <div id="conteudo_botao_avancar" style="position:absolute; top:77; left:295; z-index: 102; visibility: hidden;">
      <table height="47" border="0" cellpadding="1" cellspacing="1">
        <tr>
          <td width="50" align="center"><a id="conteudo_botao_link_avancar" href="#"><img id="conteudo_botao_link_avancar_img" src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/22/actions/go-next.png" border="0"><br>
        Avan&ccedil;ar</a></td>
        </tr>
      </table>
    </div>
    <div id="conteudo_botao_imprimir" style="position:absolute; top:77; left:350; z-index: 198; width: 70; visibility: hidden;" onMouseOver="pop_menu('popup_imprimir');" onMouseOut="pop_menu('popup_imprimir');">
      <table height="47" border="0" cellpadding="1" cellspacing="1">
        <tr>
          <td width="50" align="center"><a href="javascript:return false;"><img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/22/actions/document-print.png" border="0"><br>
        Imprimir</a></td>
          <td width="10" align="center"><img src="../../vcl-bin/qooxdoo/framework/resource/widget/windows/arrows/down.gif" width="7" height="4"></td>
      </table>
      <div id="popup_imprimir" style="visibility:hidden; z-index: 53; ">
        <table class="conteudo_barra_popmenu" border="0" cellpadding="1" cellspacing="1">
          <tr>
            <td height="25" width="10" align="center"><img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/22/actions/document-print.png" width="22" height="22"> </td>
            <td><a id="conteudo_botao_link_imprimir_imprimir" href="#">Imprimir</a></td>
          </tr>
          <tr>
            <td height="25" width="10" align="center"><img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/22/actions/zoom.png" width="22" height="22"> </td>
            <td><a id="conteudo_botao_link_imprimir_visualizar" href="#">Visualizar</a></td>
          </tr>
        </table>
      </div>
    </div>
    <div id="conteudo_botao_exportar" style="position:absolute; top:77; left:420px; width: 70; z-index: 199; visibility: hidden;" onMouseOver="pop_menu('popup_exportar');" onMouseOut="pop_menu('popup_exportar');">
      <table height="47" border="0" cellpadding="1" cellspacing="1">
        <tr>
          <td width="50" align="center"><a href="javascript:return false;"><img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/22/actions/folder-new.png" border="0"><br>
        Exportar</a></td>
          <td width="10" align="center"><img src="../../vcl-bin/qooxdoo/framework/resource/widget/windows/arrows/down.gif" width="7" height="4"></td>
      </table>
      <div id="popup_exportar" style="visibility:hidden; z-index:53;">
        <table class="conteudo_barra_popmenu" width="100%" border="0" cellpadding="1" cellspacing="1">
          <tr>
            <td height="25" width="16" align="center"><img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/16/application/vnd.ms-excel.png" width="16" height="16"></td>
            <td><a id="conteudo_botao_link_exportar_excel" href="#">Excel</a></td>
          </tr>
          <tr>
            <td height="25" width="16" align="center"><img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/16/application/msword.png" width="16" height="16"></td>
            <td><a id="conteudo_botao_link_exportar_word" href="#">Word</a></td>
          </tr>
          <tr>
            <td height="25" width="16" align="center"><img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/16/text/html.png"></td>
            <td><a id="conteudo_botao_link_exportar_html" href="#">HTML</a></td>
          </tr>
          <!--
		  <tr>
            <td height="25" width="16" align="center"><img src="../../vcl-bin/qooxdoo/framework/resource/icon/VistaInspirate/16/application/pdf.png"></td>
            <td background="#"><a id="conteudo_botao_link_exportar_html" href="PDF00001.PHP"  target="_blank">Acrobat</a></td>
          </tr>
		  -->
        </table>
      </div>
    </div>
<div id="conteudo_aba" style="position:absolute; left:0; top:50; width:100%; height:25; z-index:200; visibility: visible;">
  		<div id="conteudo_aba_manutencao_inabilitado" style="position:absolute; left:75%; top:0; z-index: 201; visibility: hidden; width:50%;">
	  <table width="50%" border="0" cellspacing="0" cellpadding="0">
			<tr valign="bottom">
			  <td width="17" align="right"><img src="img/contMenu_1_1.jpg" width="17" height="27"></td>
			  <td align="center" background="img/contMenu_1_2.jpg"><a id="conteudo_aba_link_manutencao" href="JavaScript:return false;" onClick="show_items('conteudo_aba_manutencao_inabilitado','','hide','conteudo_aba_manutencao_habilitado','','show', 'conteudo_aba_consulta_inabilitado','','show','conteudo_aba_consulta_habilitado','','hide');" class="fontConteudoMenuLink2 fontConteudoMenuVisited2 fontConteudoMenuHover2 fontConteudoMenuActive2">CADASTRAR</a> </td>
			  <td width="17"><img src="img/contMenu_1_6.jpg" width="17" height="27"></td>
			</tr>
		</table>
	</div>
	<div id="conteudo_aba_consulta_habilitado" style="position:absolute; left:50%; top:0; z-index:202; visibility: hidden; width:50%; height:50%">	
		  <table width="50%" border="0" cellpadding="0" cellspacing="0">
			<tr valign="bottom">
			  <td width="17" align="right"><img src="img/contMenu_1_3.jpg" width="17" height="27"></td>
			  <td align="center" background="img/contMenu_1_5.jpg"><span class="fontConteudoMenuLink1">CONSULTA</span></td>
			  <td width="17"><img src="img/contMenu_1_7.jpg" width="17" height="27"></td>
			</tr>
		  </table>
	</div>
	<div id="conteudo_aba_manutencao_habilitado" style="position:absolute; left:75%; top:0; z-index:202; visibility: hidden; width:50%;">
		  <table width="50%" border="0" cellpadding="0" cellspacing="0">
			<tr valign="bottom">
			  <td width="17" align="right"><img src="img/contMenu_1_3.jpg" width="17" height="27"></td>
			  <td align="center" background="img/contMenu_1_5.jpg"><span class="fontConteudoMenuLink1">CADASTRAR</span></td>
			  <td width="17"><img src="img/contMenu_1_7.jpg" width="17" height="27"></td>
			</tr>
		  </table>
	</div>
	<div id="conteudo_aba_consulta_inabilitado" style="position:absolute; left:50%; top:0; z-index: 201; visibility: hidden; width:50%; height:50%">
	  <table width="50%" border="0" cellspacing="0" cellpadding="0">
        <tr valign="bottom">
          <td width="17" align="right"><img src="img/contMenu_1_1.jpg" width="17" height="27"></td>
          <td align="center" background="img/contMenu_1_2.jpg"><a  id="conteudo_aba_link_consulta" href="JavaScript:return false;" onClick="show_items('conteudo_aba_consulta_inabilitado','','hide','conteudo_aba_consulta_habilitado','','show','conteudo_aba_manutencao_inabilitado','','show','conteudo_aba_manutencao_habilitado','','hide');" class="fontConteudoMenuLink2 fontConteudoMenuVisited2 fontConteudoMenuHover2 fontConteudoMenuActive2">CONSULTA</a> </td>
          <td width="17"><img src="img/contMenu_1_6.jpg" width="17" height="27"></td>
        </tr>
      </table>
	  </div>
</div>
<!------------------------------------------------------------------------->	  
<div id="conteudo_barra" style="position:absolute; top:76; left:0; height:47; width:100%; width: 100%; overflow:hidden; z-index: 101; visibility: visible;">
	  <table width="100%" height="47"  border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td background="img/contMenu_2_1.jpg">&nbsp;</td>
        </tr>
      </table>
</div>
	<div style="position:absolute; width:100%; height:100%; z-index: 50;">
		<table border="0" width="100%" height="100%">	
			<tr>
				<td height="120" colspan="2">&nbsp;</td>
			</tr>
			<tr>
				<td width="9">&nbsp;</td>
				<td>
					<div id="conteudo_show" style="position:relative; width:100%; height:100%;">
						(#conteudo_show)
					</div>
				</td>
			</tr>
		</table>
    </div>
	<div id="rodape" style="position:absolute; left:20%; top:0; width:80%; height:100%; z-index:2; visibility: visible;">
	</div>
</div>

<div id="view">
</div>



<div id="msg">
</div>

<div id="new_msg">
</div>

<div id="msg_alerta_tempo" 
 style="position:absolute; z-index:30001; visibility:hidden; background-color:#FFFFCC; border-top-width: 1px; border-right-width: 2px; border-bottom-width: 2px; border-left-width: 1px; border-top-style: solid; border-right-style: solid; border-bottom-style: solid; border-left-style: solid; border-top-color: #CCCCCC; border-right-color: #CCCCCC; border-bottom-color: #CCCCCC; border-left-color: #CCCCCC;">
</div>


</body>
</html>
