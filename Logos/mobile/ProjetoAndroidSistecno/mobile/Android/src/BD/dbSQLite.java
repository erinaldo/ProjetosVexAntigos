package BD;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Iterator;

import WSS.ChamadasWebServices;
import android.R.string;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.provider.Settings.Secure;
import android.text.method.DateTimeKeyListener;
import android.util.Log;
import android.view.GestureDetector.OnDoubleTapListener;
import android.widget.Toast;
import br.com.sistecno.DT.DTO.Aparelho;
import br.com.sistecno.DT.DTO.Conf;
import br.com.sistecno.DT.DTO.DT;
import br.com.sistecno.DT.DTO.Ocorrencias;
import br.com.sistecno.DT.DTO.Rastreamento;

public class dbSQLite implements Runnable
{
    public SQLiteDatabase dataBase = null;
    public Cursor cursor;
    private Context context;
    public final static String NomeBanco = "TRANSPORTE";
    public final static String camposDocumentos = "_id INTEGER PRIMARY KEY, IDDOCUMENTOOCORRENCIA varchar(100), NUMERODOCUMENTO varchar(100), NUMERO varchar(100), IDDOCUMENTO  varchar(100), IDFILIALATUAL varchar(100), VOLUMES varchar(100),  PESOBRUTO  varchar(100), PLACA  varchar(100), NUMEROPLACA  varchar(100),IDDT varchar(100),REMETENTE varchar(100), DESTINATARIO  varchar(100), CIDADE  varchar(100), PENDENTE  varchar(100), TRANSMITIDO  varchar(100), ENDERECO  varchar(100),ESTADO varchar(100), DATADAOCORRENCIA ESTADO varchar(100), IDOCORRENCIA ESTADO varchar(100), EMPRESA varchar(5), LATITUDE varchar(50), LONGITUDE varchar(50), NDOCUMENTORECEBEDOR varchar(50), NOMERECEBEDOR varchar(50)";
    public final static String camposOcorrencia = "_id INTEGER PRIMARY KEY, IDOCORRENCIA varchar(10),  CODIGO varchar(50),  NOME varchar(50),  RESPONSABILIDADE varchar(50),  FINALIZADOR varchar(50) ";
    public final static String camposRastreamento = "_id INTEGER PRIMARY KEY, LATITUDE varchar(50), LONGITUDE varchar(50), DATAHORA varchar(50), PONTODEOCORRENCIA varchar(3), IDDT varchar(10), SINC varchar(1)";
    public final static String camposAparelho = "_id INTEGER PRIMARY KEY, IDRASTREADOR INT,  CHAVE varchar(20), NOME varchar(50), ENVIARPOSICAOZERADA varchar(1), TEMPO INT, CD_CLIENTE INT";
    public final static String camposConf = "_id INTEGER PRIMARY KEY, PLACA varchar(20), NDT varchar(20), CD_CLIENTE INT";
    //public final static String camposImagemDocumento ="_id INTEGER PRIMARY KEY, IDDOCUMENTO  varchar(100), IMAGEM blob";
    
    
    public dbSQLite(Context context)
    { 
        this.context = context;       
    }
    
	public dbSQLite(Runnable runnable) {
		// TODO Auto-generated constructor stub
	}



	public void iniciaBanco()
    {
        try
        {
        	        	
            dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
            String query = "CREATE TABLE IF NOT EXISTS OCORRENCIA ("+camposOcorrencia+")";
            dataBase.execSQL(query);
            
            dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
            query = "CREATE TABLE IF NOT EXISTS DOCUMENTO ("+camposDocumentos+")";
            dataBase.execSQL(query);
            
            dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
            query = "CREATE TABLE IF NOT EXISTS RASTREAMENTO ("+camposRastreamento+")";
            dataBase.execSQL(query);
            
            dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
            query = "CREATE TABLE IF NOT EXISTS APARELHO ("+camposAparelho+")";
            dataBase.execSQL(query);
            
            dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
            query = "CREATE TABLE IF NOT EXISTS CONF ("+camposConf+")";
            dataBase.execSQL(query);
            //CarregarDadosAparelho( ChaveAparelho, cd_cliente);
            
        }
        catch(Exception erro)
        {
            Log.e("DATABASE","Erro ao iniciar o banco: "+erro);
        }
    }
        
    public void CarregarDadosAparelho(String ChaveAparelho, String cd_cliente) 
    {
    	try
    	{
    		if(ChaveAparelho !=null && cd_cliente !="" && cd_cliente != null)
    		{
    			ChamadasWebServices wss = new ChamadasWebServices();
    			wss.VerificarAparelho(context, cd_cliente , ChaveAparelho);
    		}
    	}
    	catch (Exception e) {
    		Log.e("DadosAparelho", "Problema em gravar Dados do Aparelho");
		}
	}

	public void fecharBanco(){
        try{
            dataBase.close();
        }catch(Exception erro){
            Log.e("DATABASE","Erro ao finalizar o banco: "+erro);
        }
    }        

    
	
	public void inserirConf(ArrayList<Conf> ldto)    
    {           	
        dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
        
    	try
        {
    		Iterator<Conf> i = ldto.iterator();
    		
    		dataBase.execSQL("DELETE FROM CONF");
    		
    		 while (i.hasNext()) 
 	        { 
    			 Conf dto = (Conf) i.next();   			 
		    	 dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
		         String query = "INSERT INTO CONF (PLACA, NDT, CD_CLIENTE) "; 
		         
		   		 query +=" VALUES ('"+ dto.getPLACA() +"','"+ dto.getNDT() + "','"+dto.getCD_CLIENTE()+ "') ";
		   		 dataBase.execSQL(query);  
		   		 dataBase.close();
		         Log.d("DATABASE","Inseriu CONF");
 	        }
    		 
    	}
        catch(Exception erro)
        {
            Log.e("DATABASE","Erro ao inserir no banco: "+erro);
        }
    }
	
    public void insertDocTransp(ArrayList<DT> ldto)    
    {     	
    	        	
        dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
        String query = "CREATE TABLE IF NOT EXISTS DOCUMENTO ("+camposDocumentos+")";
        dataBase.execSQL(query);     
        
    	try
        {
    		Iterator<DT> i = ldto.iterator();
    		
    		 while (i.hasNext()) 
 	        { 
    			 DT dto = (DT) i.next();   			 
		    	 dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
		         query = "INSERT INTO DOCUMENTO (NUMERO, IDDOCUMENTOOCORRENCIA,NUMERODOCUMENTO,IDDOCUMENTO,IDFILIALATUAL,VOLUMES,PESOBRUTO,PLACA,NUMEROPLACA,IDDT,REMETENTE,DESTINATARIO,CIDADE,PENDENTE,TRANSMITIDO,ENDERECO,ESTADO, EMPRESA) "; 
		   		 query +=" VALUES ('"+ dto.get_NUMERO() +"','"+ dto.get_IDDOCUMENTOOCORRENCIA() + "','"+dto.get_NUMERODOCUMENTO()+ "','"+dto.get_IDDOCUMENTO() + "','"+dto.get_IDFILIALATUAL()+ "','"+dto.get_VOLUMES()+ "','"+dto.get_PESOBRUTO()+ "','"+dto.get_PLACA()+ "','"+dto.get_NUMEROPLACA()+ "','"+dto.get_IDDT().replace("'", "")+ "','"+dto.get_REMETENTE().replace("'", "")+ "','"+dto.get_DESTINATARIO().replace("'", "")+ "','"+dto.get_CIDADE().replace("'", "")+ "','"+dto.get_PENDENTE()+ "','"+dto.get_TRANSMITIDO()+ "','"+dto.get_ENDERECO()+ "','"+dto.get_ESTADO()+ "','"+dto.get_EMPRESA()+ "') ";
		   		 dataBase.execSQL(query);  
		   		 dataBase.close();
		         Log.d("DATABASE","Inseriu");
 	        }
    		 
    	}
        catch(Exception erro)
        {
            Log.e("DATABASE","Erro ao inserir no banco: "+erro);
        }
    }
            
    public ArrayList<Ocorrencias> ListarOcorrencias(Context contxto)
    {
    	 try
    	 {
	    	ArrayList<Ocorrencias> loco = new ArrayList<Ocorrencias>();
	    	
	    	dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
	    	String query = "SELECT * FROM OCORRENCIA ORDER BY  CODIGO ASC"; 
	        Cursor c =  dataBase.rawQuery(query, null);
	        c.moveToFirst();
	                
	        do 
	        {            
	        	
	        	Ocorrencias odoDto = new Ocorrencias();
	        	
	            odoDto.set_CODIGO(c.getString(c.getColumnIndex("CODIGO")));                 
	            odoDto.set_FINALIZADOR(c.getString(c.getColumnIndex("FINALIZADOR")));
	            odoDto.set_IDOCORRENCIA(c.getString(c.getColumnIndex("IDOCORRENCIA")));  
	            odoDto.set_NOME(c.getString(c.getColumnIndex("NOME")));              
	            odoDto.set_RESPONSABILIDADE(c.getString(c.getColumnIndex("RESPONSABILIDADE")));  
	            
	            loco.add(odoDto);
	        	
	        } 
	        while (c.moveToNext());
	        c.close();
	        
	       return loco;
	    }
	    catch(Exception erro)
	    {
	        Log.e("DATABASE","Erro ao inserir no banco: "+erro);
	        Toast.makeText(contxto, erro.toString() ,Toast.LENGTH_SHORT).show();
	        return null;
	    }
    }
    
    
    public ArrayList<Conf> retornarConfiguracoes(Context contxto)
    {
    	 try
    	 {
	    	ArrayList<Conf> loco = new ArrayList<Conf>();
	    	
	    	dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
	    	String query = "SELECT * FROM CONF"; 
	        Cursor c =  dataBase.rawQuery(query, null);
	        c.moveToFirst();
	                
	        do 
	        {            
	        	
	        	Conf odoDto = new Conf();	        	
	            odoDto.set_Id(c.getString(c.getColumnIndex("_id")));
	            odoDto.setCD_CLIENTE(c.getString(c.getColumnIndex("CD_CLIENTE")));
	            odoDto.setPLACA(c.getString(c.getColumnIndex("PLACA")));
	            odoDto.setNDT(c.getString(c.getColumnIndex("NDT")));	            
	            loco.add(odoDto);
	        	
	        } 
	        while (c.moveToNext());
	        c.close();
	        
	       return loco;
	    }
	    catch(Exception erro)
	    {
	        Log.e("DATABASE","Erro ao pegar configurações: "+erro);
	        //Toast.makeText(contxto, erro.toString() ,Toast.LENGTH_SHORT).show();
	        return null;
	    }
    }

    public ArrayList<Rastreamento> ListarRastreamentoPendentes(Context contxto)
    {
    	 try
    	 {
	    	ArrayList<Rastreamento> loco = new ArrayList<Rastreamento>();
	    	
	    	dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
	    	String query = "SELECT * FROM RASTREAMENTO WHERE SINC='N'"; 
	        Cursor c =  dataBase.rawQuery(query, null);      
	        
	        
	        c.moveToFirst();
	        
	        if(c.getCount()==0)
	        	return null;
	                
	        do 
	        {            
	        	Rastreamento oRast = new Rastreamento();
        	
	        	oRast.set_Id(c.getString(c.getColumnIndex("_id")));
	        	oRast.set_DataHora(c.getString(c.getColumnIndex("DATAHORA")));
	        	oRast.set_IdDt(c.getString(c.getColumnIndex("IDDT")));
	        	oRast.set_Latitude(c.getString(c.getColumnIndex("LATITUDE")));
	        	oRast.set_Longitude(c.getString(c.getColumnIndex("LONGITUDE")));
	        	oRast.set_PontoDeOcorrencia(c.getString(c.getColumnIndex("PONTODEOCORRENCIA")));
	        	oRast.set_Sinc(c.getString(c.getColumnIndex("SINC")));
	        	
	        	loco.add(oRast);	        	
	        } 
	        while (c.moveToNext());
	        c.close();

	        
			//String sql = "DELETE FROM RASTREAMENTO";
			//dataBase.execSQL(sql);
			dataBase.close();
			
	       return loco;
	    }
	    catch(Exception erro)
	    {
	        Log.e("DATABASE","Erro ao inserir no banco: "+erro);
	        Toast.makeText(contxto, erro.toString() ,Toast.LENGTH_SHORT).show();
	        return null;
	    }
    }
    
    
    public void SincronizarOcorrencias(ArrayList<Ocorrencias> loco)
    {     	
    	try
        {
    		Iterator<Ocorrencias> i = loco.iterator();    
    		deletarTodasOcorrencias();
    		
    		
	        while (i.hasNext()) 
	        {        
	        	Ocorrencias coc = (Ocorrencias) i.next();
	        	
	        	dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
		        String query = "INSERT INTO OCORRENCIA (IDOCORRENCIA, CODIGO, NOME, RESPONSABILIDADE, FINALIZADOR ) "; 
		   		query +=" VALUES ('"+ coc.get_IDOCORRENCIA() +"', '"+coc.get_CODIGO() +"', '"+ coc.get_NOME().toUpperCase() +"', '"+ coc.get_RESPONSABILIDADE() +"', '"+ coc.get_FINALIZADOR() +"') ";
		
		        dataBase.execSQL(query);  
		        dataBase.close();
		        Log.d("DATABASE","Inseriu Ocorrencia:" + coc.get_NOME());
	        	
	        }    	 
	 	    
        }
        catch(Exception erro)
        {
            Log.e("DATABASE","Erro ao inserir no banco: "+erro);
        }
    }
    
    public void GarvarDadosDoAparelho(ArrayList<Aparelho> apar)
    {     	
    	try
        {
    		Iterator<Aparelho> i = apar.iterator();    
    		deletarDadosAparelhos();
    		
    		
	        while (i.hasNext()) 
	        {        
	        	Aparelho coc = (Aparelho) i.next();
	        	
	        	dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
		        String query = "INSERT INTO APARELHO (IDRASTREADOR, CHAVE , NOME, ENVIARPOSICAOZERADA , TEMPO, CD_CLIENTE  ) "; 
		   		query +=" VALUES ("+ coc.getIdRastreador() +", '"+coc.getChave() +"', '"+ coc.getNome().toUpperCase() +"', '"+ coc.getEnviaPosicaoZerada().trim() +"', "+ coc.getTempo() +", "+ coc.getCd_cliente() +") ";
		
		        dataBase.execSQL(query);  
		        dataBase.close();
		        Log.d("DATABASE","Inseriu APARELHO: " + coc.getNome());
	        	
	        }    	 
	 	    
        }
        catch(Exception erro)
        {
            Log.e("DATABASE","Erro ao inserir no banco: "+erro);
        }
    }
    
    private void deletarTodasOcorrencias()
    {        
        dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
		String sql = "DELETE FROM OCORRENCIA";
		dataBase.execSQL(sql);
		dataBase.close();
    }
    
    
    private void deletarDadosAparelhos()
    {        
        dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
		String sql = "DELETE FROM APARELHO";
		dataBase.execSQL(sql);
		dataBase.close();
    }
    
    public  void deletarTodosDocumentos()
    {        
        dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
		String sql = "DELETE FROM DOCUMENTO";
		dataBase.execSQL(sql);
		
		sql = "DELETE FROM RASTREAMENTO";
		dataBase.execSQL(sql);
		
		dataBase.close();
    }
    
    public  void AlterarStatusDocumento(String iddocumento)
    {        
        dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
		String sql = "UPDATE DOCUMENTO SET TRANSMITIDO='S' WHERE IDDOCUMENTO='"+iddocumento+"'" ;
		dataBase.execSQL(sql);
		dataBase.close();
    }
    
    
    public String GetIdDT(Context contxto)
    {
    	 try
       	 {
    		 
    		dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
 	    	String query = "SELECT IDDT FROM DOCUMENTO "; 
 	        Cursor c =  dataBase.rawQuery(query, null);
 	        
 	        if(c.getCount() ==0)
 	        	return null;
 	        
 	        c.moveToFirst();
 	        
 	        String ret = c.getString(c.getColumnIndex("IDDT"));
 	                
 	        c.close();
	        dataBase.close();  
    		 return  ret;
       	 }
 	    catch(Exception erro)
 	    {
 	    	dataBase.close();
 	        Log.e("DATABASE","Erro ao inserir no banco: "+erro);
 	        Toast.makeText(contxto, erro.toString() ,Toast.LENGTH_SHORT).show();
 	        return null;
 	    }
    }
    
    public String qtdPosicoesPend(Context contxto)
    {
    	 try
       	 {
       	 
    		dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
 	    	String query = "SELECT count(*) X from RASTREAMENTO "; 
 	        Cursor c =  dataBase.rawQuery(query, null);
 	        
 	        if(c.getCount() ==0)
 	        	return null;
 	        
 	        c.moveToFirst();
 	        
 	        String ret = c.getString(c.getColumnIndex("X"));
 	                
 	        c.close();
	        dataBase.close();  
    		 return  ret;
       	 }
 	    catch(Exception erro)
 	    {
 	    	dataBase.close();
 	        Log.e("DATABASE","Erro ao inserir no banco: "+erro);
 	        Toast.makeText(contxto, erro.toString() ,Toast.LENGTH_SHORT).show();
 	        return null;
 	    }
    }
    
   public ArrayList<DT> ListarDocumentosPorCondicao(Context contxto, String condicao)
   {
   	 try
   	 {
	    	ArrayList<DT> lista = new ArrayList<DT>();
	    	
	    	dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
	    	String query = "SELECT * FROM DOCUMENTO WHERE "+ condicao +"  ORDER BY  NUMERODOCUMENTO ASC"; 
	        Cursor c =  dataBase.rawQuery(query, null);
	        
	        if(c.getCount() ==0)
	        	return null;
	        
	        c.moveToFirst();
	                
	        do 
	        {            
	        	
	        	DT dto = new DT();	
	        	
	        	dto.set_NUMERO(c.getString(c.getColumnIndex("NUMERO")));
            	dto.set_IDDOCUMENTOOCORRENCIA(c.getString(c.getColumnIndex("IDDOCUMENTOOCORRENCIA")));            	
            	dto.set_NUMERODOCUMENTO(c.getString(c.getColumnIndex("NUMERODOCUMENTO")));
            	dto.set_IDDOCUMENTO(c.getString(c.getColumnIndex("IDDOCUMENTO")));
            	dto.set_IDFILIALATUAL(c.getString(c.getColumnIndex("IDFILIALATUAL")));
            	dto.set_VOLUMES(c.getString(c.getColumnIndex("VOLUMES")));
            	dto.set_PESOBRUTO(c.getString(c.getColumnIndex("PESOBRUTO")));            	
            	dto.set_PLACA(c.getString(c.getColumnIndex("PLACA")));
            	dto.set_NUMEROPLACA(c.getString(c.getColumnIndex("NUMEROPLACA")));
            	dto.set_IDDT(c.getString(c.getColumnIndex("IDDT")));
            	dto.set_REMETENTE(c.getString(c.getColumnIndex("REMETENTE")));
            	dto.set_CIDADE(c.getString(c.getColumnIndex("CIDADE")));
            	dto.set_DESTINATARIO(c.getString(c.getColumnIndex("DESTINATARIO")));            	
            	dto.set_ENDERECO(c.getString(c.getColumnIndex("ENDERECO")));
            	dto.set_ESTADO(c.getString(c.getColumnIndex("ESTADO")));
            	dto.set_PENDENTE(c.getString(c.getColumnIndex("PENDENTE")));
            	dto.set_TRANSMITIDO(c.getString(c.getColumnIndex("TRANSMITIDO")));     
            	dto.set_IDOCORRENCIA(c.getString(c.getColumnIndex("IDOCORRENCIA")));           	
            	dto.set_DATAHORAOCORRENCIA(c.getString(c.getColumnIndex("DATADAOCORRENCIA")));
            	dto.set_LATTUDE(c.getString(c.getColumnIndex("LATITUDE")));
            	dto.set_LONGITUDE(c.getString(c.getColumnIndex("LONGITUDE")));
            	dto.set_DATAHORAPOSICAO(c.getString(c.getColumnIndex("DATADAOCORRENCIA")));   	

            	
            	lista.add(dto);	        	
	        } 
	        while (c.moveToNext());
	        
	        c.close();
	        dataBase.close();  
	        
	       return lista;
	    }
	    catch(Exception erro)
	    {
	    	dataBase.close();
	        Log.e("DATABASE","Erro ao inserir no banco: "+erro);
	        Toast.makeText(contxto, erro.toString() ,Toast.LENGTH_SHORT).show();
	        return null;
	    }
   }

   public void SetarOcorrenciaDocumento(String idOcorrencia, String idDocumento, String Lat, String Longi)
   {
	   try
	   	 {
		   dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
		   SimpleDateFormat dateFormat = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
	       Date date = new Date();
	
			String sql = "UPDATE DOCUMENTO SET LATITUDE ='"+Lat+"', LONGITUDE='"+Longi+"', TRANSMITIDO='N',PENDENTE='N', DATADAOCORRENCIA='"+ dateFormat.format(date) +"', IDOCORRENCIA='" + idOcorrencia + "' WHERE IDDOCUMENTO=" + idDocumento;
			dataBase.execSQL(sql);
			dataBase.close();
		   	}
		    catch(Exception erro)
		    {
		    	dataBase.close();
		        Log.e("DATABASE","Erro ao inserir no banco: "+erro);
		        Toast.makeText(context, erro.toString() ,Toast.LENGTH_SHORT).show();		        
		    }
   }
   
    public  void InserirCoordenadas(String Latitude, String Longitude, String PontoDeOcorrencia, String iddt)
   {   	 
	   SimpleDateFormat dateFormat = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
	   Date date = new Date();
       dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
	   String sql = "INSERT INTO RASTREAMENTO (LATITUDE,LONGITUDE, DATAHORA, PONTODEOCORRENCIA, IDDT, SINC) VALUES ('"+Latitude+"','"+Longitude+"', '"+ dateFormat.format(date) +"', '"+PontoDeOcorrencia+"', '"+iddt+"', 'N')";
	   dataBase.execSQL(sql);
	   dataBase.close();
   }
    
    public String RetornarMinutosSincronizacao(Context contxto, String chave)
    {
    	 try
       	 {
       	 
    		dataBase = context.openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
 	    	String query = "SELECT TEMPO from APARELHO WHERE CHAVE='"+ chave +"' "; 
 	        Cursor c =  dataBase.rawQuery(query, null);
 	        
 	        if(c.getCount() ==0)
 	        	return null;
 	        
 	        c.moveToFirst();
 	        
 	        String ret = c.getString(c.getColumnIndex("TEMPO"));
 	                
 	        c.close();
	        dataBase.close();  
    		 return  ret;
       	 }
 	    catch(Exception erro)
 	    {
 	    	dataBase.close();
 	        Log.e("DATABASE","Erro ao inserir no banco: "+erro);
 	        Toast.makeText(contxto, erro.toString() ,Toast.LENGTH_SHORT).show();
 	        return null;
 	    }
    }
    

    public ArrayList<DT> ListarDocumentosPorCondicao(Runnable runnable,		String condicao) {
	// TODO Auto-generated method stub
	return null;
}

public void run() {
	// TODO Auto-generated method stub
	
}

}