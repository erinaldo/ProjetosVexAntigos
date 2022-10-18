package WSS;

import java.util.ArrayList;

import org.kobjects.base64.Base64;
import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;
import BD.dbSQLite;
import android.content.Context;
import android.util.Log;
import android.widget.Toast;
import br.com.sistecno.DT.DTO.Aparelho;
import br.com.sistecno.DT.DTO.DT;
import br.com.sistecno.DT.DTO.Ocorrencias;

public class ChamadasWebServices  
{
	public ArrayList<DT> Listar_Documentos(String Placa, String Dt, String Empresa, Boolean sincronizar, Context runn, String equipamento, String cd_cliente) 
	{ 	
		ArrayList<DT> ldto = new ArrayList<DT>();
		
		String SOAP_ACTION = "http://tempuri.org/Listar_Documentos";
		String METHOD_NAME = "Listar_Documentos";
		String NAMESPACE = "http://tempuri.org/";
		String URL = "http://www.sistecno.com.br/novodotnet/WSAndroid/Service.asmx";		
		
    	SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME); 
        request.addProperty("PLACA", Placa); 
        request.addProperty("DOCTRANSP", Dt);             
        request.addProperty("EQUIPAMENTO", equipamento);
        request.addProperty("cd_cliente", cd_cliente);
        request.addProperty("senhaSeguranca", "s817i625s433t341e05c6n7o8");
               
        SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11); 
        envelope.dotNet = true; 
        envelope.setOutputSoapObject(request); 
        
        try 
        { 
            HttpTransportSE androidHttpTransport = new HttpTransportSE(URL); 
            androidHttpTransport.call(SOAP_ACTION, envelope); 
            SoapObject obj = (SoapObject)envelope.getResponse();            
            String[] listar_documentos = new String[obj.getPropertyCount()];            
            dbSQLite dataBase = new dbSQLite(runn);
            dataBase.iniciaBanco();
            
            if(sincronizar)
        	{        		
        		dataBase.deletarTodosDocumentos();
        	}
            
            for (int i = 0; i < listar_documentos.length; i++) 
	        {
            	SoapObject linha = (SoapObject)obj.getProperty(i);
            	DT dto = new DT();
            	
            	dto.set_NUMERO(linha.getPropertySafelyAsString("NUMERO").toString());
            	dto.set_IDDOCUMENTOOCORRENCIA(linha.getPropertySafelyAsString("IDDOCUMENTOOCORRENCIA").toString());            	
            	dto.set_NUMERODOCUMENTO(linha.getPropertySafelyAsString("NUMERODOCUMENTO").toString());
            	dto.set_IDDOCUMENTO(linha.getPropertySafelyAsString("IDDOCUMENTO").toString());
            	dto.set_IDFILIALATUAL(linha.getPropertySafelyAsString("IDFILIALATUAL").toString());
            	dto.set_VOLUMES(linha.getPropertySafelyAsString("VOLUMES").toString());
            	dto.set_PESOBRUTO(linha.getPropertySafelyAsString("PESOBRUTO").toString());
            	dto.set_PLACA(linha.getPropertySafelyAsString("PLACA").toString());
            	dto.set_NUMEROPLACA(linha.getPropertySafelyAsString("PLACANUMERO").toString());
            	dto.set_IDDT(linha.getPropertySafelyAsString("IDDT").toString());
            	dto.set_REMETENTE(linha.getPropertySafelyAsString("REMETENTE").toString());
            	dto.set_CIDADE(linha.getPropertySafelyAsString("CIDADE").toString());
            	dto.set_DESTINATARIO(linha.getPropertySafelyAsString("DESTINATARIO").toString());            	
            	dto.set_ENDERECO(linha.getPropertySafelyAsString("ENDERECO").toString());
            	dto.set_ESTADO(linha.getPropertySafelyAsString("ESTADO").toString());            	
            	dto.set_EMPRESA(linha.getPropertySafelyAsString("EMPRESA").toString());            	
            	int iddococo = Integer.parseInt(linha.getPropertySafelyAsString("IDDOCUMENTOOCORRENCIA").toString());
            	
            	if(linha.getPropertySafelyAsString("OCORRENCIA").toString()!="")
            	{
            		dto.set_IDOCORRENCIA(linha.getPropertySafelyAsString("OCORRENCIA").toString());
            	}
            	
            	if(iddococo == 0)
            	{
            		dto.set_PENDENTE("S");
            		dto.set_TRANSMITIDO("N");
            	}
            	else
            	{
            		dto.set_PENDENTE("N");
					dto.set_TRANSMITIDO("S");
            	}
            	ldto.add(dto);   	
            	dto = null;
	        }
            
            if(sincronizar)
        	{           		
        		dataBase.insertDocTransp(ldto);
        	}
            return ldto; 
        } 
        catch (Exception ex) 
        { 
            return ldto; 
        }
    }
	
	public void SincronizarOcorrencias(Context contexto, String cd_cliente) 
	{ 			
		
		ArrayList<Ocorrencias> loco = new ArrayList<Ocorrencias>();
		
		String SOAP_ACTION = "http://tempuri.org/Listar_All_Ocorrencias";
		String METHOD_NAME = "Listar_All_Ocorrencias";
		String NAMESPACE = "http://tempuri.org/";
		String URL = "http://www.sistecno.com.br/novodotnet/WSAndroid/Service.asmx";	
		
    	SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME); 
    	request.addProperty("cd_cliente", cd_cliente);
    	request.addProperty("senhaSeguranca", "s817i625s433t341e05c6n7o8");
    	
        SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11); 
        envelope.dotNet = true; 
        envelope.setOutputSoapObject(request); 
        
        try 
        { 
            HttpTransportSE androidHttpTransport = new HttpTransportSE(URL); 
            androidHttpTransport.call(SOAP_ACTION, envelope); 
            SoapObject obj = (SoapObject)envelope.getResponse();
            
            String[] itens = new String[obj.getPropertyCount()];
            
        	dbSQLite dataBase = new dbSQLite(contexto);
            dataBase.iniciaBanco();

            
            for (int i = 0; i < itens.length; i++) 
	        {
            	SoapObject linha = (SoapObject)obj.getProperty(i);
                Ocorrencias odoDto = new Ocorrencias();                
                odoDto.set_CODIGO(linha.getPropertySafelyAsString("CODIGO").toString());                 
                odoDto.set_FINALIZADOR(linha.getPropertySafelyAsString("FINALIZADOR").toString());
                odoDto.set_IDOCORRENCIA(linha.getPropertySafelyAsString("IDOCORRENCIA").toString());
                odoDto.set_NOME(linha.getPropertySafelyAsString("NOME").toString());                
                odoDto.set_RESPONSABILIDADE(linha.getPropertySafelyAsString("RESPONSABILIDADE").toString());
                loco.add(odoDto);
	        }           
            
            dataBase.SincronizarOcorrencias(loco);
           
        } 
        catch (Exception ex) 
        { 
            Toast.makeText(contexto, "Erro: "+ex.getMessage(), Toast.LENGTH_LONG).show();
            Log.e("Sincronizar Ocorrencias", ex.getMessage());
        }
    }
	
	@SuppressWarnings("finally")
	public boolean GravarOcorrencias(String idDocumento, String idOcorrencia, String descricaoOcorrencia, String IDFilial, byte[] image, 
			Context contexto, String longitude, String latitude , String idDt,  String DataHoraPosicao , String DataHoraOcorrencia, String cd_cliente) 
	{ 		
		boolean Result = false;
		try 
        { 
		   	String SOAP_ACTION = "http://tempuri.org/gravarDocumentoOcorrencia";
			String METHOD_NAME = "gravarDocumentoOcorrencia";
			String NAMESPACE = "http://tempuri.org/";
			String URL = "http://www.sistecno.com.br/novodotnet/WSAndroid/Service.asmx";
				
				
		     SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME);   
 
	    	request.addProperty("idDocumento", idDocumento); 
	        request.addProperty("idOcorrencia", idOcorrencia); 
	        request.addProperty("descricaoOcorrencia", descricaoOcorrencia); 
	        request.addProperty("IDFilial", IDFilial); 
	        request.addProperty("image",Base64.encode(image));	        
	        request.addProperty("longitude", longitude); 
	        request.addProperty("latitude", latitude); 
	        request.addProperty("idDt", idDt); 	        
	        request.addProperty("DataHoraPosicao", DataHoraPosicao);
	        request.addProperty("DataHoraOcorrencia", DataHoraOcorrencia);
	        request.addProperty("cd_cliente", cd_cliente);
	        request.addProperty("senhaSeguranca", "s817i625s433t341e05c6n7o8");
	            	
	        SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11); 
	        envelope.dotNet = true; 
	        envelope.setOutputSoapObject(request);                
      
            HttpTransportSE androidHttpTransport = new HttpTransportSE(URL); 
            androidHttpTransport.call(SOAP_ACTION, envelope); 
            SoapObject obj = (SoapObject)envelope.getResponse();            
            @SuppressWarnings("unused")
			String[] itens = new String[obj.getPropertyCount()];  
            
            Result = true;
            
                       
        } 
        catch (Exception ex) 
        {
        	Log.e("GravarOcorrencias", ex.getMessage());
        }
		finally
		{
			return Result;
		}
    }
	
	public boolean GravarOcorrenciasSemImagem(String idDocumento, String idOcorrencia, String descricaoOcorrencia, String IDFilial, 
			Context contexto , String longitude, String latitude , String idDt,  String DataHoraPosicao , String DataHoraOcorrencia, String cd_cliente) 
	{ 		
		String SOAP_ACTION = "http://tempuri.org/gravarDocumentoOcorrenciaSemImagem";
		String METHOD_NAME = "gravarDocumentoOcorrenciaSemImagem";
		String NAMESPACE = "http://tempuri.org/";
		String URL = "http://www.sistecno.com.br/novodotnet/WSAndroid/Service.asmx";
	
		
    	SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME); 
    	  
    	try 
        { 
	    	request.addProperty("idDocumento", idDocumento); 
	        request.addProperty("idOcorrencia", idOcorrencia); 
	        request.addProperty("descricaoOcorrencia", descricaoOcorrencia); 
	        request.addProperty("IDFilial", IDFilial); 
	        request.addProperty("longitude", longitude); 
	        request.addProperty("latitude", latitude); 
	        request.addProperty("idDt", idDt); 
	        request.addProperty("DataHoraPosicao", DataHoraPosicao);
	        request.addProperty("DataHoraOcorrencia", DataHoraOcorrencia); 
	        request.addProperty("cd_cliente", cd_cliente);
	        request.addProperty("senhaSeguranca", "s817i625s433t341e05c6n7o8");
	    	
	        SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11); 
	        envelope.dotNet = true; 
	        envelope.setOutputSoapObject(request);  
            HttpTransportSE androidHttpTransport = new HttpTransportSE(URL); 
            androidHttpTransport.call(SOAP_ACTION, envelope); 
            SoapObject obj = (SoapObject)envelope.getResponse();            
            @SuppressWarnings("unused")
			String[] itens = new String[obj.getPropertyCount()];  
         return true;            
        } 
        catch (Exception ex) 
        { 
            Toast.makeText(contexto, "Erro: "+ex.getMessage(), Toast.LENGTH_LONG).show();
            return false;
        }
    }	

	public boolean Limpar(String placa, String DOCTRANSP, String cd_cliente, Context contexto)
	{ 		
		String SOAP_ACTION = "http://tempuri.org/Limpar";
		String METHOD_NAME = "Limpar";
		String NAMESPACE = "http://tempuri.org/";
		String URL = "http://www.sistecno.com.br/novodotnet/WSAndroid/Service.asmx";
		SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME);     	  
    	try 
        { 
	    	request.addProperty("PLACA", placa); 
	        request.addProperty("DOCTRANSP", DOCTRANSP); 
	        request.addProperty("cd_cliente", cd_cliente); 
	        request.addProperty("senhaSeguranca", "s817i625s433t341e05c6n7o8");
	        
	        SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11); 
	        envelope.dotNet = true; 
	        envelope.setOutputSoapObject(request);  
            HttpTransportSE androidHttpTransport = new HttpTransportSE(URL); 
            androidHttpTransport.call(SOAP_ACTION, envelope); 
            SoapObject obj = (SoapObject)envelope.getResponse();           
         return true;            
        } 
        catch (Exception ex) 
        { 
            Toast.makeText(contexto, "Erro: "+ex.getMessage(), Toast.LENGTH_LONG).show();
            return false;
        }
    }	
		
	public boolean GravPosicao(String IDDT, String LATITUDE, String LONGITUDE, String DATAHORA, String cd_cliente, String ptoOcor, Context contexto )
	{ 	
		boolean ret = false;
		String SOAP_ACTION = "http://tempuri.org/gravarPosicaoOcorrencia";
		String METHOD_NAME = "gravarPosicaoOcorrencia";
		String NAMESPACE = "http://tempuri.org/";
		String URL = "http://www.sistecno.com.br/novodotnet/WSAndroid/Service.asmx";
		SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME);     	  
    	try 
        { 
	    	request.addProperty("IDDT", IDDT); 
	        request.addProperty("LATITUDE", LATITUDE); 
	        request.addProperty("LONGITUDE", LONGITUDE);	        
	        request.addProperty("DATAHORA", DATAHORA); 
	        request.addProperty("cd_cliente", cd_cliente);
	        request.addProperty("ptoOcoc", ptoOcor);	        
	        request.addProperty("senhaSeguranca", "s817i625s433t341e05c6n7o8");        
	       
	        SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11); 
	        envelope.dotNet = true; 
	        envelope.setOutputSoapObject(request);  
            HttpTransportSE androidHttpTransport = new HttpTransportSE(URL); 
            androidHttpTransport.call(SOAP_ACTION, envelope); 
            SoapObject obj = (SoapObject)envelope.getResponse();
            ret=true;
         return ret;            
        } 
        catch (Exception ex) 
        { 
        	Log.e("GravPosicao", "Erro: "+ex.getMessage());
           // Toast.makeText(contexto, "Erro: "+ex.getMessage(), Toast.LENGTH_LONG).show();
            //return false;
        	return ret;
        }
    }	

	public void VerificarAparelho(Context contexto, String cd_cliente, String ChaveAparelho) 
	{ 			
		
		ArrayList<Aparelho> lapar = new ArrayList<Aparelho>();
		
		String SOAP_ACTION = "http://tempuri.org/Verificar_Aparelho";
		String METHOD_NAME = "Verificar_Aparelho";
		String NAMESPACE = "http://tempuri.org/";
		String URL = "http://www.sistecno.com.br/novodotnet/WSAndroid/Service.asmx";	
		
    	SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME); 
    	request.addProperty("chave", ChaveAparelho);
    	request.addProperty("senhaSeguranca", "s817i625s433t341e05c6n7o8");
    	request.addProperty("cd_cliente", cd_cliente);
    	
        SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11); 
        envelope.dotNet = true; 
        envelope.setOutputSoapObject(request); 
        
        try 
        { 
            HttpTransportSE androidHttpTransport = new HttpTransportSE(URL); 
            androidHttpTransport.call(SOAP_ACTION, envelope); 
            SoapObject obj = (SoapObject)envelope.getResponse();
            
            String[] itens = new String[obj.getPropertyCount()];
            
        	dbSQLite dataBase = new dbSQLite(contexto);
            dataBase.iniciaBanco();

            
            for (int i = 0; i < itens.length; i++) 
	        {
            	SoapObject linha = (SoapObject)obj.getProperty(i);
                Aparelho apar = new Aparelho();
                apar.setChave(linha.getPropertySafelyAsString("Chave").toString());
                apar.setEnviaPosicaoZerada(linha.getPropertySafelyAsString("EnviaPosicaoZerada").toString());
                apar.setIdRastreador(linha.getPropertySafelyAsString("IdRastreador").toString());
                apar.setNome(linha.getPropertySafelyAsString("Nome").toString());
                apar.setTempo(linha.getPropertySafelyAsString("Tempo").toString());
                apar.setCd_cliente(cd_cliente);                
                lapar.add(apar);
	        }           
            
            if(lapar==null || lapar.size()==0)
            {
            	 Aparelho apar = new Aparelho();
                 apar.setChave("");
                 apar.setEnviaPosicaoZerada("S");
                 apar.setIdRastreador("1");
                 apar.setNome("Nao Identificado");
                 apar.setTempo("1");
                 apar.setCd_cliente(cd_cliente);                 
                 lapar.add(apar);
            }
            
            dataBase.GarvarDadosDoAparelho(lapar);           
        } 
        catch (Exception ex) 
        { 
            Toast.makeText(contexto, "Erro: "+ex.getMessage(), Toast.LENGTH_LONG).show();
        }
    }
		
	public String RetornarQtdNotasDt(String placa, String ndt, String cd_cliente) 
	{ 		
		String SOAP_ACTION = "http://tempuri.org/RetornarQtdNotasNoDt";
		String METHOD_NAME = "RetornarQtdNotasNoDt";
		String NAMESPACE = "http://tempuri.org/";
		String URL = "http://www.sistecno.com.br/novodotnet/WSAndroid/Service.asmx";	
		
    	SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME); 
    	request.addProperty("placa", placa);
    	request.addProperty("ndt", ndt);
    	request.addProperty("cd_cliente", cd_cliente);
    	request.addProperty("senhaSeguranca", "s817i625s433t341e05c6n7o8");
    	
    	
        SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11); 
        envelope.dotNet = true; 
        envelope.setOutputSoapObject(request); 
        String ret = "0";
        
        try 
        { 
        	
            HttpTransportSE androidHttpTransport = new HttpTransportSE(URL); 
            androidHttpTransport.call(SOAP_ACTION, envelope);           
            
            SoapObject result = (SoapObject) envelope.bodyIn;
            ret =  result.getPropertyAsString(0).toString();
            return ret;
                       
        } 
        catch (Exception ex) 
        { 
            return ret;
        }
    }
	
	
	public void TempoDeSincronizacao(String chave, String dt, String DataInicial, String DataFinal, String cd_cliente) 
	{ 		
		//string chave, string dt, string DataInicial, string DataFinal, string cd_cliente,
		
		String SOAP_ACTION = "http://tempuri.org/GrarvarTempoSincronizacao";
		String METHOD_NAME = "GrarvarTempoSincronizacao";
		String NAMESPACE = "http://tempuri.org/";
		String URL = "http://www.sistecno.com.br/novodotnet/WSAndroid/Service.asmx";	
		
    	SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME); 
    	request.addProperty("chave", chave);
    	request.addProperty("DataInicial", DataInicial);
    	request.addProperty("DataFinal", DataFinal);
    	request.addProperty("cd_cliente", cd_cliente);
    	request.addProperty("senhaSeguranca", "s817i625s433t341e05c6n7o8");
    	
    	
        SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11); 
        envelope.dotNet = true; 
        envelope.setOutputSoapObject(request); 
        
        
        try 
        { 
        	
        	HttpTransportSE androidHttpTransport = new HttpTransportSE(URL); 
            androidHttpTransport.call(SOAP_ACTION, envelope); 
            SoapObject obj = (SoapObject)envelope.getResponse();
            
            String[] itens = new String[obj.getPropertyCount()];
            
                       
        } 
        catch (Exception ex) 
        { 
            
        }
    }
	
	
	public void UltimoEnvioDedados(String chave, String cd_cliente) 
	{ 		
		//string chave, string dt, string DataInicial, string DataFinal, string cd_cliente,
		
		String SOAP_ACTION = "http://tempuri.org/GrarvarUltimoEnvioDeDados";
		String METHOD_NAME = "GrarvarUltimoEnvioDeDados";
		String NAMESPACE = "http://tempuri.org/";
		String URL = "http://www.sistecno.com.br/novodotnet/WSAndroid/Service.asmx";	
		
    	SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME); 
    	request.addProperty("chave", chave);
    	request.addProperty("cd_cliente", cd_cliente);
    	request.addProperty("senhaSeguranca", "s817i625s433t341e05c6n7o8");
    	
    	
        SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11); 
        envelope.dotNet = true; 
        envelope.setOutputSoapObject(request); 
        
        
        try 
        { 
        	
        	HttpTransportSE androidHttpTransport = new HttpTransportSE(URL); 
            androidHttpTransport.call(SOAP_ACTION, envelope); 
            SoapObject obj = (SoapObject)envelope.getResponse();
            
            String[] itens = new String[obj.getPropertyCount()];
            
                       
        } 
        catch (Exception ex) 
        { 
            
        }
    }
	

}
