package br.com.sistecno;


import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Iterator;
import java.util.concurrent.ExecutionException;

import br.com.sistecno.DT.DTO.DT;
import br.com.sistecno.DT.DTO.Rastreamento;

import BD.dbSQLite;
import WSS.ChamadasWebServices;
import android.R.string;
import android.annotation.SuppressLint;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.location.Criteria;
import android.location.Location;
import android.location.LocationManager;
import android.net.ConnectivityManager;
import android.os.Bundle;
import android.os.Debug;
import android.os.IBinder;
import android.provider.Settings.Secure;
import android.util.Log;
import android.widget.Toast;


public class AlarmPosicao extends Service implements Runnable
{
	@Override
	public IBinder onBind(Intent intent) {
		// TODO Auto-generated method stub
		return null;
	}
	
	private double la;
	private double lo;	
	String cd_cliente ="";
	
	public final static String NomeBanco = "TRANSPORTE";
	
	public static boolean Conectado(Context context) {
        try {
            ConnectivityManager cm = (ConnectivityManager)
            context.getSystemService(Context.CONNECTIVITY_SERVICE);
            if (cm.getNetworkInfo(ConnectivityManager.TYPE_MOBILE).isConnected()) {
                    //handler.sendEmptyMessage(0);
                    Log.d("conexaoTipo:","Status de conexão 3G: "+cm.getNetworkInfo(ConnectivityManager.TYPE_MOBILE).isConnected());
                    return true;
            } else if(cm.getNetworkInfo(ConnectivityManager.TYPE_WIFI).isConnected()){
                    //handler.sendEmptyMessage(0);
                    Log.d("conexaoTipo:","Status de conexão Wifi: "+cm.getNetworkInfo(ConnectivityManager.TYPE_WIFI).isConnected());
                    return true;
            } else {
                    //handler.sendEmptyMessage(0);
                    Log.e("conexaoTipo:","Status de conexão Wifi: "+cm.getNetworkInfo(ConnectivityManager.TYPE_WIFI).isConnected());
                    Log.e("conexaoTipo:","Status de conexão 3G: "+cm.getNetworkInfo(ConnectivityManager.TYPE_MOBILE).isConnected());
                    return false;
            }
        } catch (Exception e) {
                Log.e("conexaoTipo:",e.getMessage());
                return false;
        }
    }
	
	public int onStartCommand(Intent intent, int flags, int startId)
	{	
		try
		{
			runPegarPosicao();
			new Thread(AlarmPosicao.this).start();
			
		}
		catch (Exception e)
		{
			String m = "";
			m=e.getMessage();
			Log.e("onStartCommand", e.getMessage());
		}
		return 1;
	}
	GPSTracker gps;
	
	public void run()
	{
		try
		{
			ChamadasWebServices call = new ChamadasWebServices();
			ArrayList<Rastreamento> listRast = new ArrayList<Rastreamento>();
	    	
			SQLiteDatabase	dataBase = openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
	    	String query = "SELECT * FROM RASTREAMENTO WHERE SINC='N'"; 
	        Cursor c =  dataBase.rawQuery(query, null);
	        
	        
	        c.moveToFirst();        
	        if(c.getCount()>0)
	        {               
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
		        	
		        	listRast.add(oRast);	        	
		        } 
		        while (c.moveToNext());
		        c.close();
	        }	
			
			Iterator<Rastreamento> itr2 = listRast.iterator();
				
				while (itr2.hasNext()) 
				{
					try
					{
						Rastreamento element = itr2.next();
						String android_id = Secure.getString(getBaseContext().getContentResolver(),Secure.ANDROID_ID);
						call.UltimoEnvioDedados(android_id, cd_cliente);
						
						
						if(call.GravPosicao(element.get_IdDt().toString(), element.get_Latitude().toString(), element.get_Longitude().toString(), element.get_DataHora().toString(), cd_cliente, element.get_PontoDeOcorrencia() , this))
							{
								String sql = "DELETE FROM RASTREAMENTO where _id=" + element.get_Id();
								dataBase.execSQL(sql);
								
							}
					}
					catch (Exception e) {
						Log.e("Nao Encoutrou WebService", e.getMessage());
					}
					
				}
				dataBase.close();
		}
		catch (Exception e) {
		Log.e("Threding Sincronizar Rastreamento",e.getMessage());
		}
	}
	
	public void runPegarPosicao() 
	{
		la = 0;
		lo = 0;
		
		//pega a posição do gps
		gps = new GPSTracker(this.getApplicationContext());
		
        la = gps.getLatitude();
        lo = gps.getLongitude();
        

		SQLiteDatabase	dataBase = openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
    	String query = "SELECT IDDT FROM DOCUMENTO ";  
    	String ret ="0";
    	
        Cursor c =  dataBase.rawQuery(query, null);
        
        if(c==null)
        	return;
        
	
	    SimpleDateFormat dateFormat = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
		Date date = new Date();
		   
		try
		{
        
        c.moveToFirst();
        if(c.getCount()>0)
        {       
	        	 	        
	        ret = c.getString(c.getColumnIndex("IDDT"));	 	                
	        c.close();	  
	        
	        /////retorna IDCLiente
	        query = "SELECT CD_CLIENTE FROM APARELHO ";      	
						
			Cursor c1 =  dataBase.rawQuery(query, null);
		        
		        if(c1==null)
		        	return ;
		        c1.moveToFirst();
		        cd_cliente =String.valueOf( c1.getInt(c1.getColumnIndex("CD_CLIENTE")));	 	                
		        c1.close();

		        
				String sql = "INSERT INTO RASTREAMENTO (LATITUDE,LONGITUDE, DATAHORA, PONTODEOCORRENCIA, IDDT, SINC) VALUES ('"+String.valueOf(la)+"','"+String.valueOf(lo)+"', '"+ dateFormat.format(date) +"', 'NAO', '"+ret+"', 'N')";
				dataBase.execSQL(sql);
        }
			
			
	}
	catch(Exception ee)
	{
			String sql = "INSERT INTO RASTREAMENTO (LATITUDE,LONGITUDE, DATAHORA, PONTODEOCORRENCIA, IDDT, SINC) VALUES ('"+String.valueOf(la)+"','"+String.valueOf(lo)+"', '"+ dateFormat.format(date) +"', 'NAO', '"+ret+"', 'N')";
			dataBase.execSQL(sql);
			
			String erro= ee.getMessage();
			erro="";
			Log.e("RUN THreding", erro);
	}
	finally
	{
			dataBase.close();
	}
		
	}	
}
