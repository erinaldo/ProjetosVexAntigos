package br.com.sistecno;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Iterator;

import org.w3c.dom.CDATASection;

import br.com.sistecno.DT.DTO.DT;
import BD.dbSQLite;
import WSS.ChamadasWebServices;
import android.app.Service;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Environment;
import android.os.IBinder;
import android.provider.Settings.Secure;
import android.widget.ArrayAdapter;
import android.widget.Toast;

public class AlarmReceiver2 extends Service
{
	@Override
	public IBinder onBind(Intent intent) {
		// TODO Auto-generated method stub
		return null;
	}
	
	@SuppressWarnings("finally")
	public int onStartCommand(Intent intent, int flags, int startId)
	{
		try {
			
		dbSQLite dataBase = new dbSQLite(this);
		dataBase.iniciaBanco();
		
		ArrayList<DT> dtSincronizar = dataBase.ListarDocumentosPorCondicao(this, " TRANSMITIDO='N' AND PENDENTE='N' ");	
		ChamadasWebServices call = new ChamadasWebServices();
		boolean gravou = false;
		if(dtSincronizar!=null)
		{
			Iterator<DT> itr = dtSincronizar.iterator();
			  
			Toast.makeText(this, "Iniciou Threading", Toast.LENGTH_SHORT).show();
			 
			while (itr.hasNext()) 
			{
				DT element = itr.next();						
				
				
				byte[] buffer =null;						
				
				File sdCard = Environment.getExternalStorageDirectory();
				File dir = new File (sdCard.getAbsolutePath());  
				dir.mkdirs(); 
				File file = new File(dir, element.get_IDDOCUMENTO()+".jpg"); 
				File imagefile = null;

				if(file.exists())					
					{
						String filepath =sdCard.getAbsolutePath();								
						imagefile = new File(filepath + "/" + element.get_IDDOCUMENTO()+".jpg");								
						FileInputStream fis = null;
					    fis = new FileInputStream(imagefile);
						Bitmap bi = BitmapFactory.decodeStream(fis);
						ByteArrayOutputStream baos = new ByteArrayOutputStream();
						bi.compress(Bitmap.CompressFormat.JPEG,10, baos);
						buffer = baos.toByteArray();
						//imagefile.delete();
					} 						
				
				if(buffer !=null &&
						buffer.length>0)
				{
					/*
					gravou = call.GravarOcorrencias(
							element.get_IDDOCUMENTO(), 
							element.get_IDOCORRENCIA(), 
							"EFETUADO PELO ANDROID", 
							"2", 
							buffer, 
							this,
							element.get_LONGITUDE(),
							element.get_LATTUDE(),
							element.get_IDDT(),
							element.get_DATAHORAOCORRENCIA(),
							element.get_DATAHORAOCORRENCIA()
							);
						*/
					//imagefile.delete();
				}
				else
				{
					//call.GravarOcorrenciasSemImagem(idDocumento, idOcorrencia, descricaoOcorrencia, IDFilial, contexto, longitude, latitude, idDt, DataHoraPosicao, DataHoraOcorrencia, cd_cliente)
					/*
					 * gravou = call.GravarOcorrenciasSemImagem(element.get_IDDOCUMENTO(), element.get_IDOCORRENCIA(), "EFETUADO PELO ANDROID", "2", this,
					 
							element.get_LONGITUDE(),
							element.get_LATTUDE(),
							element.get_IDDT(),
							element.get_DATAHORAOCORRENCIA(),
							element.get_DATAHORAOCORRENCIA() 
							);
					*/
				}
				
				if(gravou)
					dataBase.AlterarStatusDocumento(element.get_IDDOCUMENTO());		
									
			}  		
		}
		
		
		String mensagem ="Nota Fiscal Transmitida";

		if(gravou)
			Toast.makeText(this, mensagem, Toast.LENGTH_SHORT).show();
 
		
		} 
		catch (FileNotFoundException e) 
		{			
			Toast.makeText(this, e.getMessage(), Toast.LENGTH_LONG).show();
		}
		finally
		{
			return 0;
		}

	}

}

