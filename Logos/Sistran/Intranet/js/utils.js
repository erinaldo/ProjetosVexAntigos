//Exibir a div com a mensagem LOAD
function ExibirLoad() {
    document.getElementById('load').style.display = "inline";


    var input = document.getElementsByTagName('input')

    for (var i = 0; i < input.length; i++)
        input[i].disabled = true;
}

function OcultarLoad() {

    document.getElementById('load').style.display = "none";

    var input = document.getElementsByTagName('input')
    for (var i = 0; i < input.length; i++)
        input[i].disabled = false;
}

//centralizada
function NewWindow(mypage, myname, w, h, scroll) {
    var winl = (screen.width - w) / 2;
    var wint = (screen.height - h) / 2;
    winprops = 'status=yes,height=' + h + ',width=' + w + ',top=' + wint + ',left=' + winl + ',scrollbars=' + scroll + ',resizable=yes'
    win = window.open(mypage, myname, winprops)
    if (parseInt(navigator.appVersion) >= 4) { win.window.focus(); }
}

//full
function FullWindow(mypage, myname, scroll) {
    var winl = (screen.width-10);
    var wint = (screen.height-10);

    winprops = 'status=no,height=' + wint + ',width=' + winl + ',top=0,left=0,scrollbars=' + scroll + ',resizable=yes'
   // winprops = 'status=no,height=' + 760 + ',width=' + 1024 + ',top=0,left=0,scrollbars=' + scroll + ',resizable=no'
    win = window.open(mypage, myname, winprops)
    if (parseInt(navigator.appVersion) >= 4) { win.window.focus(); }
}

// Mostra ajuda
//ex.: onFocus=SetHelp('Informe o valor com os centavos.') // coloque isso no campo que quer que dispare o help
function SetHelp(txt) 
{
    alert(document.getElementById('lblStatus1'));
    document.getElementById('lblStatus1').innerHTML = txt;
}

function NovaJanela(url)
{
    window.open('" + url + "', 'news', 'width=1000,height=750,toolbar=no,location=no, directories=no,status=no,menubar=no,scrollbars=no,resizable=no');
}
// Função para exibis / ocultar divs
// Parametros: objeto do botao (this) e strig id da div
function Expandir(btn, div) {
    obj = document.getElementById(div);

    if (obj.style.display != 'none') {
        obj.style.display = 'none';
        btn.value = "Exibir";
    }
    else {
        obj.style.display = 'block'
        btn.value = "Ocultar";
    }
    return false;
}

function ConfirmaExclusao() {
    return confirm('Deseja realmente excluir este registro?');
}

function Redireciona(url) {
    window.location.href = url;
}

//Formata valor
//ex.: onKeyDown='FormataValor("valor", 13, event)
//Obs.: "valor" é o nome do campo, 13 o tamanho máximo permitido de carac. do campo e event é a tecla pressionada'
function FormataValor(obj, tammax, teclapres) {
    var tecla = teclapres.keyCode;
    vr = obj.value;
    vr = vr.replace("/", "");
    vr = vr.replace("/", "");
    vr = vr.replace(",", "");
    vr = vr.replace(",", "");
    vr = vr.replace(".", "");
    vr = vr.replace(".", "");
    vr = vr.replace(".", "");
    vr = vr.replace(".", "");
    //Replaces adicionais
    //vr = vr.replace( "-", "" );
    //vr = vr.replace( "+", "" );
    //vr = vr.replace( "*", "" );
    tam = vr.length;

    if (tam < tammax && tecla != 8) { tam = vr.length + 1; }

    if (tecla == 8) { tam = tam - 1; }

    if (tecla == 8 || tecla >= 48 && tecla <= 57 || tecla >= 96 && tecla <= 105) {
        if (tam <= 2) {
            obj.value = vr;
        }
        if ((tam > 2) && (tam <= 5)) {
            obj.value = vr.substr(0, tam - 2) + ',' + vr.substr(tam - 2, tam);
        }
        if ((tam >= 6) && (tam <= 8)) {
            obj.value = vr.substr(0, tam - 5) + '.' + vr.substr(tam - 5, 3) + ',' + vr.substr(tam - 2, tam);
        }
        if ((tam >= 9) && (tam <= 11)) {
            obj.value = vr.substr(0, tam - 8) + '.' + vr.substr(tam - 8, 3) + '.' + vr.substr(tam - 5, 3) + ',' + vr.substr(tam - 2, tam);
        }
        if ((tam >= 12) && (tam <= 14)) {
            obj.value = vr.substr(0, tam - 11) + '.' + vr.substr(tam - 11, 3) + '.' + vr.substr(tam - 8, 3) + '.' + vr.substr(tam - 5, 3) + ',' + vr.substr(tam - 2, tam);
        }
        if ((tam >= 15) && (tam <= 17)) {
            obj.value = vr.substr(0, tam - 14) + '.' + vr.substr(tam - 14, 3) + '.' + vr.substr(tam - 11, 3) + '.' + vr.substr(tam - 8, 3) + '.' + vr.substr(tam - 5, 3) + ',' + vr.substr(tam - 2, tam);
        }
    }
}


/// Formata valores
//função desabilita string
function numeros(btn, especial) {
    botao = btn.keyCode;
    var num, num2, num3, num4, num5, num6;
    switch (especial) {
        case 'data':
            num = 193;
            num2 = 111;
            num3 = 0;
            num4 = 0;
            num5 = 0;
            num6 = 0;
            break;
        case 'hora':
            num = 191;
            num2 = 0;
            num3 = 0;
            num4 = 0;
            num5 = 0;
            num6 = 0;
            break;
        case 'valor':
            num = 188;
            num2 = 190;
            num3 = 110;
            num4 = 194;
            num5 = 109;
            num6 = 189;
            break;
        case 'numero':
            num = 0;
            num2 = 0;
            num3 = 0;
            num4 = 0;
            num5 = 0;
            num6 = 189;
            break;
    }

    if (!(botao >= 48 && botao <= 57 || botao >= 96 && botao <= 105 || num == botao || num2 == botao || num3 == botao || num4 == botao || num5 == botao || num6 == botao || botao == 8 || botao == 46 || botao == 39 || botao == 37 || botao == 46 || botao == 9)) {
        event.returnValue = false;
    }
}


function validaDinheiro(fld, e) {
    var milSep = ".";
    var decSep = ",";
    var sep = 0;
    var key = '';
    var i = j = 0;
    var len = len2 = 0;
    var strCheck = '0123456789';
    var aux = aux2 = '';
    var whichCode = (window.Event) ? e.which : e.keyCode;

    if (whichCode == 13)
        return true;

    key = String.fromCharCode(whichCode);

    if (strCheck.indexOf(key) == -1)
        return false;

    len = fld.value.length;

    for (i = 0; i < len; i++)
        if ((fld.value.charAt(i) != '0') && (fld.value.charAt(i) != decSep))
            break;

    aux = '';

    for (; i < len; i++)
        if (strCheck.indexOf(fld.value.charAt(i)) != -1)
            aux += fld.value.charAt(i);

    aux += key;
    len = aux.length;

    if (len == 0)
        fld.value = '';

    if (len == 1)
        fld.value = '0' + decSep + '0' + aux;

    if (len == 2)
        fld.value = '0' + decSep + aux;

    if (len > 2) {
        aux2 = '';

        for (j = 0, i = len - 3; i >= 0; i--) {
            if (j == 3) {
                aux2 += milSep;
                j = 0;
            }
            aux2 += aux.charAt(i);
            j++;
        }

        fld.value = '';
        len2 = aux2.length;

        for (i = len2 - 1; i >= 0; i--)
            fld.value += aux2.charAt(i);
        fld.value += decSep + aux.substr(len - 2, len);
    }

    return false;

}

/// Troca virgula por pontos no javascript
function trocaVir(valor) {
    valor.value = valor.value.replace(',', '.');
    cont = 0;
    i = 0;
    vlr = '';
    for (pos = 0; pos <= valor.value.length; pos++) {
        if (valor.value.substr(pos, 1) == '.') {
            cont = cont + 1;
        }
    }
    if (cont > 1) {
        for (pos = 0; pos <= valor.value.length; pos++) {
            if (valor.value.substr(pos, 1) != '.') {
                vlr += valor.value.substr(pos, 1);
            }
            else {
                i++;
            }
            if (cont == i && valor.value.substr(pos, 1) == '.') {
                vlr += valor.value.substr(pos, 1);
            }
        }
        valor.value = vlr;
    }
}

/// Troca virgula por pontos no javascript
function TrataVirgula(valor) {
    valor = valor.replace(',', '.');
    cont = 0;
    i = 0;
    vlr = '';
    for (pos = 0; pos <= valor.length; pos++) {
        if (valor.substr(pos, 1) == '.') {
            cont = cont + 1;
        }
    }
    if (cont > 1) {
        for (pos = 0; pos <= valor.length; pos++) {
            if (valor.substr(pos, 1) != '.') {
                vlr += valor.substr(pos, 1);
            }
            else {
                i++;
            }
            if (cont == i && valor.substr(pos, 1) == '.') {
                vlr += valor.substr(pos, 1);
            }
        }
        valor = vlr;
    }

    return valor;
}

// formata string passando apenas o parametro 'this'
// onkeypress="return txtBoxFormat(this, '99999-999', event);"> 
function txtBoxFormat(objForm, sMask, evtKeyPress) {
    var i, nCount, sValue, fldLen, mskLen, bolMask, sCod, nTecla;

    if (document.all) { // Internet Explorer
        nTecla = evtKeyPress.keyCode;
    }
    else if (document.layers) { // Nestcape
        nTecla = evtKeyPress.which;
    }

    sValue = objForm.value;

    // Limpa todos os caracteres de formatação que
    // já estiverem no campo.
    sValue = sValue.toString().replace(":", "");
    sValue = sValue.toString().replace(":", "");
    sValue = sValue.toString().replace("-", "");
    sValue = sValue.toString().replace("-", "");
    sValue = sValue.toString().replace(".", "");
    sValue = sValue.toString().replace(".", "");
    sValue = sValue.toString().replace("/", "");
    sValue = sValue.toString().replace("/", "");
    sValue = sValue.toString().replace("(", "");
    sValue = sValue.toString().replace("(", "");
    sValue = sValue.toString().replace(")", "");
    sValue = sValue.toString().replace(")", "");
    sValue = sValue.toString().replace(" ", "");
    sValue = sValue.toString().replace(" ", "");
    fldLen = sValue.length;
    mskLen = sMask.length;

    i = 0;
    nCount = 0;
    sCod = "";
    mskLen = fldLen;

    while (i <= mskLen) {
        bolMask = ((sMask.charAt(i) == ":") || (sMask.charAt(i) == "-") || (sMask.charAt(i) == ".") || (sMask.charAt(i) == "/"))
        bolMask = bolMask || ((sMask.charAt(i) == "(") || (sMask.charAt(i) == ")") || (sMask.charAt(i) == " "))

        if (bolMask) {
            sCod += sMask.charAt(i);
            mskLen++;
        }
        else {
            sCod += sValue.charAt(nCount);
            nCount++;
        }

        i++;
    }

    objForm.value = sCod;

    if (nTecla != 8) { // backspace
        if (sMask.charAt(i - 1) == "9") { // apenas números...
            return ((nTecla > 47) && (nTecla < 58));
        } // números de 0 a 9
        else { // qualquer caracter...
            return true;
        } 
    }
    else {
        return true;
    }
}

function ValidaHora(obj) {

    if (event.keyCode < 48 || event.keyCode > 57) {
        event.returnValue = false;
    }
    if (obj.value.length == 2) {
        obj.value += ":";
    }

}

function VoltarPaginaAnterior() {
    window.history.go(-1); return false;
}

function virgula(obj) {
    if (event.keyCode == 44) {
        event.keyCode = 46;
    }
}

/////////////////////////////////////////////////////////////////
//
//		Função de mascara com 4 casas decimais
//
/////////////////////////////////////////////////////////////////
function MascaraMoeda(valor, pais) {
    novoVlr = '';


    if (valor.indexOf(',') < valor.indexOf('.')) {
        for (pos = 0; pos <= valor.length - 1; pos++) {
            if (valor.substr(pos, 1) == ',') {
                novoVlr += '.';
            }
            else if (valor.substr(pos, 1) == '.') {
                novoVlr += ',';
            }
            else {
                novoVlr += valor.substr(pos, 1);
            }
        }
    } else {
        novoVlr = valor;
    }



    if (novoVlr.indexOf(',') > 0) {
        novoVlrArray = novoVlr.split(',');
        novoVlr = '';
        i = 0;

        for (pos = novoVlrArray[0].length; pos >= 0; pos--) {
            if (novoVlrArray[0].substr(pos, 1) != '.') {
                i++;
                if (i == 4 && pos != 0) {
                    novoVlr = '.' + novoVlrArray[0].substr(pos, 1) + novoVlr;
                    i = 1;
                }
                else {
                    novoVlr = novoVlrArray[0].substr(pos, 1) + novoVlr;
                }
            }
        }

        if (novoVlrArray[1].length == 1) {
            novoVlr += ',' + novoVlrArray[1] + '0';
        }
        else {
            novoVlr += ',' + novoVlrArray[1];
        }
    }
    else {
        novoVlrAux = novoVlr;
        novoVlr = '';
        i = 0;

        for (pos = novoVlrAux.length; pos >= 0; pos--) {
            if (novoVlrAux.substr(pos, 1) != '.') {

                i++;

                if (i == 4 && pos != 0) {
                    novoVlr = '.' + novoVlrAux.substr(pos, 1) + novoVlr;
                    i = 1;
                }
                else {
                    novoVlr = novoVlrAux.substr(pos, 1) + novoVlr;
                }
            }
        }
        novoVlr += ',0000';
    }
    return novoVlr;
}


function pDia(Data_DDMMYYYY) {
    string_data = Data_DDMMYYYY.toString();
    posicao_barra = string_data.indexOf("/");
    if (posicao_barra != -1) {
        dia = string_data.substring(0, posicao_barra);
        return dia;
    }
    else {
        return false;
    }
}

function pMes(Data_DDMMYYYY) {
    string_data = Data_DDMMYYYY.toString();
    posicao_barra = string_data.indexOf("/");
    if (posicao_barra != -1) {
        dia = string_data.substring(0, posicao_barra);
        string_mes = string_data.substring(posicao_barra + 1, string_data.length);
        posicao_barra = string_mes.indexOf("/");
        if (posicao_barra != -1) {
            mes = string_mes.substring(0, posicao_barra);
            mes = Math.floor(mes);
            return mes;
        }
        else {
            return false;
        }

    }
    else {
        return false;
    }
}

function pAno(Data_DDMMYYYY) {
    string_data = Data_DDMMYYYY.toString();
    posicao_barra = string_data.indexOf("/");
    if (posicao_barra != -1) {
        dia = string_data.substring(0, posicao_barra);
        string_mes = string_data.substring(posicao_barra + 1, string_data.length);
        posicao_barra = string_mes.indexOf("/");
        if (posicao_barra != -1) {
            mes = string_mes.substring(0, posicao_barra);
            mes = Math.floor(mes);
            ano = string_mes.substring(posicao_barra + 1, string_mes.length);
            return ano;
        }
        else {
            return false;
        }

    }
    else {
        return false;
    }
}

function DimensionaDiv(div,tableId,pain, tblPrinc2)
{
    var de = document.documentElement;
    var table = document.getElementById(tableId);
    var altura = screen.height;
    var alturaCorpo = window.innerHeight || self.innerHeight || (de&&de.clientHeight) || document.body.clientHeight;
    var alturaFinal, versao, temp;
    var tableHeight = table.offsetHeight;   
      
    if(navigator.appVersion.indexOf("MSIE")!= -1)
    {
        temp = navigator.appVersion.split("MSIE");
        versao = parseFloat(temp[1]);
    }
    else
    {
        versao = 0;
    }
    
    
    
    alturaFinal = alturaCorpo;
    
    if(versao>0 && versao<8)
    {
        alturaFinal = alturaFinal;
    }
    else if(versao == 8)
    {
	alturaFinal = alturaFinal;
    }
    else
    {
        alturaFinal = alturaFinal;
    }

	    var objDiv = document.getElementById(div);
	    var objpain = document.getElementById(pain);	 	    
	    var objpain2 = document.getElementById(tblPrinc2);	 
	    
    if(alturaFinal > 0)
    {
	    if(objDiv != null)
	    {
	            objDiv.style.height = alturaFinal;        	            
	    }
	    
	     if(objpain != null)
	    {
	            objpain.style.height =alturaFinal;        	                   
	    }	
	    
	    if(objpain2 != null)
	    {
	            objpain2.style.height = alturaFinal-115;   	            
	    }
	}	    
   
    
}

function ajusteCursorMenu(idMenu)
{
    var i = 0;
    var menu = document.getElementById(idMenu);
    if (menu != null) {
        var items = menu.getElementsByTagName('td');
        for (i == 0; i < items.length; i++) {
            items[i].style.cursor = "pointer";
        }
    }
}
function Impressao()
{
    var i = 0;
    var id;
    var obj;
    var msg="Para imprimir, antes você deve configurar o modo de página para Paisagem!"

    for(i==0;i<100;i++)
    {
        id = "ctl00_cphMain_gvDados_ctl" + i + "_pager1";
        if(document.getElementById(id) != null)
        {
            obj = document.getElementById(id);
        }
    }
    
    if(obj != null)
    {
        obj.style.display = "none";
    }
    
    var frameString=""+
    "<html>\n\r"+
    "<head>\n\r"+
    "<link href='../App_Themes/Main/estilos.css' type='text/css' rel='stylesheet'/>\n\r"+
    "<style type='text/css'>\n\r"+
    "BODY{size:landscape; margin:0px;padding:0px;}"+
    ".p1 { \n\r"+
    " font-family: Arial, Helvetica, sans-serif;\n\r"+
    " font-size: 9pt;\n\r"+
    " font-weight: bold;\n\r"+
    " text-align: justify;}\n\r"+
    ".p2 {	font-family: Arial, Helvetica, sans-serif;\n\r"+
    " font-size: 9pt;\n\r"+
    " text-align: justify;}\n\r"+
    "P{ font-family : Verdana, Arial, Sans-Serif;\n\r"+
    " font-size : 9pt; \n\r"+
    " color : #000000;\n\r"+
    ".p2 {font-family: Arial, Helvetica, sans-serif;\n\r"+
    " font-size: 9pt;\n\r"+
    " text-align: left;}\n\r"+
    ".p3 {font-family: Arial, Helvetica, sans-serif;\n\r"+
    " font-size: 14px;\n\r"+
    " font-weight: bold;}\n\r"+
    "</style>\n\r"+
    "<script>function Alerta(){alert('"+msg+"');}<\/script>"+
    "<title>Impressão</title>\n\r"+ 
    "</head>\n\r"+
    "<body bgcolor='white' onload='javascript: Alerta(); self.print();'>\n\r"+
    "<div class='landscape'>"+
    "<table border='0' width='100%' valign='top' style='font-family:Arial; font-size:9pt;'>\n\r"+
    "<tr>\n\r<td valign='top' width='100%'>\n\r" 
    + document.getElementById('divConteudo').innerHTML +
    "\n\r</td>\n\r</tr>\n\r</table></div>\n\r<br>\n\r"+
    "</body>\n\r</html>";

    agilePopper = window.open("","popAgile","top=30,left=50,width=600,height=400, scrollbars=YES,location=no,menubar=no,resizable=no,status=no,toolbar=yes");
    agilePopper.focus();

    agilePopper.document.open();
    agilePopper.document.write(frameString);
    agilePopper.document.close();
    
    if(obj != null)
    {
        obj.style.display = "block";
    }
}


function SomenteNumero(e){
    var tecla=(window.event)?event.keyCode:e.whi…
    if((tecla > 47 && tecla < 58)) return true;
    else{
    if (tecla != 8) return false;
    else return true;
    }
    
    
}

//function wins(url)
//{
//        open('" + url + "', 'new', 'width=300,height=150,toolbar=no,location=no, directories=no,status=no,menubar=no,scrollbars=no,resizable=no')">

////    var w = window.open();
////    w.opener = null;
////    w.document.location = url;
//}

function Somaiuscula(formato, keypress, objeto)
{
campo = eval(objeto);
caracteres = 'abcdefghijklmnopqrstuvxyzABCDEFGHIJKLMNOPQSTUVWXYZ01234567890-./: ';
if (caracteres.search(String.fromCharCode(keypress))!=1)
{
campo.value = campo.value.toUpperCase();
}
}

  function newPopup1(url) 
  {
    popupWindow1 = window.open(url,'popupWindow1','height=500,width=8 00')
    }
    
    function avisoAguarde() 
    { 
    if(document.getElementById('divProcessando')) 
    { 
    document.getElementById('divProcessando').style.display=''; return; 
    } 
    oDiv = document.createElement("div");
     with (oDiv) { id = "divProcessando"; 
     } 
     document.body.appendChild(oDiv); 
     } 