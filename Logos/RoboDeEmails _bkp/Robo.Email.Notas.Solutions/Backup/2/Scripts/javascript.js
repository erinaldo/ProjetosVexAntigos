//*****************************************************************************
//Imprimir pagina
//*****************************************************************************
function print_page_view(){
	if (typeof(window.print) != 'undefined') {
		window.print();		
	}
}

function print_page_closed(){
	window.moveTo(-200,-200);
	window.resizeTo(0,0);
	
	if (typeof(window.print) != 'undefined') {
		window.print();		
	}
	
	window.close();
}
//fim
//*****************************************************************************


//*****************************************************************************
//Data
//*****************************************************************************
	function format_date(conteudo){
		var conteudo;
		
		if(conteudo != ''){
			data_mes_txt = trim(conteudo.substring(0,3));
			data_dia_txt = trim(conteudo.substring(3,6));
			data_ano = trim(conteudo.substring(6,11));
			
			data_mes = Array();
			data_mes['Jan'] = '01';
			data_mes['Feb'] = '02';
			data_mes['Mar'] = '03';
			data_mes['Apr'] = '04';
			data_mes['May'] = '05';
			data_mes['Jun'] = '06';
			data_mes['Jul'] = '07';
			data_mes['Aug'] = '08';
			data_mes['Sep'] = '09';
			data_mes['Oct'] = '10';
			data_mes['Nov'] = '11';
			data_mes['Dec'] = '12';
	
			data_dia = Array();
			data_dia[1] = '01';
			data_dia[2] = '02';
			data_dia[3] = '03';
			data_dia[4] = '04';
			data_dia[5] = '05';
			data_dia[6] = '06';
			data_dia[7] = '07';
			data_dia[8] = '08';
			data_dia[9] = '09';
			data_dia[10] = '10';
			data_dia[11] = '11';
			data_dia[12] = '12';
			data_dia[13] = '13';
			data_dia[14] = '14';
			data_dia[15] = '15';
			data_dia[16] = '16';
			data_dia[17] = '17';
			data_dia[18] = '18';
			data_dia[19] = '19';
			data_dia[20] = '20';
			data_dia[21] = '21';
			data_dia[22] = '22';
			data_dia[23] = '23';
			data_dia[24] = '24';
			data_dia[25] = '25';
			data_dia[26] = '26';
			data_dia[27] = '27';
			data_dia[28] = '28';
			data_dia[29] = '29';
			data_dia[30] = '30';
			data_dia[31] = '31';
			
				
			return data_dia[data_dia_txt]+' - '+data_mes[data_mes_txt]+' - '+data_ano;
		}else{
			return '';
		}
	}
	
	function data(obj){// temporario grade
		var conteudo, data_mes;
		if(obj.title != undefined){
			conteudo = obj.title;
		}
		
		if(obj.alt != undefined){
			conteudo = obj.alt;
		}
		
		if(conteudo != ''){
		
		data_mes_txt = trim(conteudo.substring(0,3));
		data_dia_txt = trim(conteudo.substring(3,6));
		data_ano = trim(conteudo.substring(6,11));
		
		data_mes = Array();
		data_mes['Jan'] = '01';
		data_mes['Feb'] = '02';
		data_mes['Mar'] = '03';
		data_mes['Apr'] = '04';
		data_mes['May'] = '05';
		data_mes['Jun'] = '06';
		data_mes['Jul'] = '07';
		data_mes['Aug'] = '08';
		data_mes['Sep'] = '09';
		data_mes['Oct'] = '10';
		data_mes['Nov'] = '11';
		data_mes['Dec'] = '12';

		data_dia = Array();
		data_dia[1] = '01';
		data_dia[2] = '02';
		data_dia[3] = '03';
		data_dia[4] = '04';
		data_dia[5] = '05';
		data_dia[6] = '06';
		data_dia[7] = '07';
		data_dia[8] = '08';
		data_dia[9] = '09';
		data_dia[10] = '10';
		data_dia[11] = '11';
		data_dia[12] = '12';
		data_dia[13] = '13';
		data_dia[14] = '14';
		data_dia[15] = '15';
		data_dia[16] = '16';
		data_dia[17] = '17';
		data_dia[18] = '18';
		data_dia[19] = '19';
		data_dia[20] = '20';
		data_dia[21] = '21';
		data_dia[22] = '22';
		data_dia[23] = '23';
		data_dia[24] = '24';
		data_dia[25] = '25';
		data_dia[26] = '26';
		data_dia[27] = '27';
		data_dia[28] = '28';
		data_dia[29] = '29';
		data_dia[30] = '30';
		data_dia[31] = '31';
		
			
		obj.innerHTML = data_dia[data_dia_txt]+' - '+data_mes[data_mes_txt]+' - '+data_ano;
		obj.title = data_dia[data_dia_txt]+' - '+data_mes[data_mes_txt]+' - '+data_ano;
		obj.alt = data_dia[data_dia_txt]+' - '+data_mes[data_mes_txt]+' - '+data_ano;
		}
	}//fim do temporario
//fim
//*****************************************************************************	

function focus_obj(id){
	document.getElementById(id).className = 'db_text_box_focus';
}

function blur_obj(id){
	document.getElementById(id).className = 'db_text_box_blur';
}



function vencido_ou_avencer_status(obj){
	var conteudo, row; 
	if(obj.title != undefined){
		conteudo = obj.title;
	}
		
	if(obj.alt != undefined){
		conteudo = obj.alt;
	}
		
	if(conteudo != ''){
		if(conteudo < 0){
			obj.innerHTML = 'Vencido a';
			obj.alt = 'Vencido a';
			obj.title = 'Vencido a';
			
			obj.style.color = '#ff0000';
			row = obj.id.split('_');
		}else{
			obj.innerHTML = 'Utilizar ate';
			obj.alt = 'Utilizar ate';
			obj.title = 'Utilizar ate';
		}
	}
}
	
function vencido_ou_avencer_dias(obj){
	var conteudo 
	if(obj.title != undefined){
		conteudo = obj.title;
	}
		
	if(obj.alt != undefined){
		conteudo = obj.alt;
	}		
		
	if(conteudo != ''){
		obj.innerHTML = Math.abs(conteudo);
		obj.title = Math.abs(conteudo);
		obj.alt = Math.abs(conteudo);
		if(conteudo < 0){
			obj.style.color = '#ff0000';
		}
	}
}

//*****************************************************************************
function scroll_move_div(name, div_move, pos_left, pos_top){
	
	var obj = document.getElementById(name);
	var obj_move = document.getElementById(div_move);
	
	if(pos_left == 'RIGHT'){
		obj_move.style.left = obj.scrollLeft + (obj.offsetWidth - obj_move.offsetWidth - 23) + "px";		
	}else{
		obj_move.style.left = obj.scrollLeft + 5 + "px";
	}
	
	if(pos_top == 'TOP'){
		obj_move.style.top = obj.scrollTop + 5 + "px";
	}else{
		obj_move.style.top = obj.scrollTop + (obj.offsetHeight - obj_move.offsetHeight - 23) + "px";		
	}
	
	
}
	
function remove_caracter(conteudo, size){
	var novo_conteudo;
	if(conteudo.length > size){
		novo_conteudo = conteudo.substring(0, size) +'...';
	}else{
		novo_conteudo = conteudo;
	}
	
	return novo_conteudo;
}

function r_array(){
    var args = r_array.arguments;
    return args;
}

function trim(obj){
	return obj.replace(/^\s*/, "").replace(/\s*$/, "");
}

function form_selected(id_sel, id){
	var conteudo;
	if(id_sel == id){
		conteudo = ' selected="selected" ';
	}
	return conteudo;
}

function moveMenu(){
	if(document.getElementById("menu").style.visibility == 'visible'){
		document.getElementById("conteudo").style.left   = "0px";
		document.getElementById("conteudo").style.width   = "100%";
		
		document.getElementById("rodape").style.left   = "0px";
		document.getElementById("rodape").style.width   = "100%";
		
		document.getElementById("barra_menu").style.left   = "0px";
		
		document.getElementById("menu").style.visibility   = "hidden";
	}else{
		document.getElementById("conteudo").style.left   = "20%";
		document.getElementById("conteudo").style.width   = "80%";
		
		document.getElementById("rodape").style.left   = "20%";
		document.getElementById("rodape").style.width   = "80%";
		
		document.getElementById("barra_menu").style.left   = "20%";
		
		document.getElementById("menu").style.visibility   = "visible";
	}
	
}

function pop_menu(var1){
	if(document.getElementById(var1).style.visibility == "visible"){
		document.getElementById(var1).style.visibility = 'hidden';
	}else{
		document.getElementById(var1).style.visibility = 'visible';
	}
}


function items_posicao(id, topo, esquerdo){
	document.getElementById(id).style.top = topo;
	document.getElementById(id).style.left = esquerdo;
}

function items_botao_value(id, valor){
	document.getElementById(id).value = valor;
}

function items_botao_link(id, url){
	document.getElementById(id).href = url;
}

function items_botao_copiar_link(id, id_dest){
	document.getElementById(id_dest).href = document.getElementById(id).href;
}

function items_botao_add_link(id, url){
	document.getElementById(id).href = document.getElementById(id).href+url;
}


function combinar_div(url, id_div_conteudo, topo_conteudo, id_div_grupo, topo_grupo){
	document.getElementById(id_div_conteudo).style.top = topo_conteudo;
	document.getElementById(id_div_grupo).style.top = topo_grupo;
    goandget_pop(url, id_div_conteudo);
}

function programa_titulo(ttl, user){
	document.title = '# SistranWeb - '+document.getElementById('web_name_programa').value+' - '+ttl+' - '+user;
}

function programa_add_title(ttl){
	document.title = '# SistranWeb - '+ttl;
}

//*****************************************************************************


//*****************************************************************************
function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function show_items() { //v6.0
  var i,p,v,obj,args=show_items.arguments;
  for (i=0; i<(args.length-2); i+=3) if ((obj=MM_findObj(args[i]))!=null) { v=args[i+2];
    if (obj.style) { obj=obj.style; v=(v=='show')?'visible':(v=='hide')?'hidden':v; }
    obj.visibility=v; }
}

//*****************************************************************************




function ajuda_select_link(id, id_array){
	for(a=1; a <= document.getElementById(id_array).value; a++){
		document.getElementById(id_array+'_'+a).style.background = '#FFFFFF';
		document.getElementById(id_array+'_'+a).style.color = '#000000';
	}
	if(document.getElementById(id).style.background != '#335EA8' || document.getElementById(id).style.color != '#FFFFFF' ){
		document.getElementById(id).style.background = '#335EA8';
		document.getElementById(id).style.color = '#FFFFFF';
	}
}

function ajuda_textarea(id_origem, id_destino){
	document.getElementById(id_destino).value = document.getElementById(id_origem).value;
}


