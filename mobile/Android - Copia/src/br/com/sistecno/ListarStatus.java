package br.com.sistecno;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Iterator;

import BD.dbSQLite;
import WSS.ChamadasWebServices;
import android.R.array;
import android.R.integer;
import android.R.string;
import android.app.Activity;
import android.app.AlarmManager;
import android.app.PendingIntent;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.database.sqlite.SQLiteDatabase;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.ConnectivityManager;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Environment;
import android.provider.Settings;
import android.provider.Settings.Secure;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Toast;
import br.com.sistecno.DT.DTO.DT;
import br.com.sistecno.DT.DTO.Rastreamento;

public class ListarStatus extends Activity implements OnClickListener
{
  ArrayList<DT> listPendentes;
  ArrayList<DT> listRealizados;
  ArrayList<DT> listATransmitir; 
  int pendentes = 0;
  int atransmitir = 0;
  int realizados = 0;     
  ListView listViewStatus;
  AsyncTarefa mAsyncTask;
  ProgressDialog mDialog;
  
  String cd_cliente ="";
	
	 public void onCreate(Bundle savedInstanceState) 
	    {		 
	        super.onCreate(savedInstanceState);
	        setContentView(R.layout.listarstatus); 	           
	      
	        Intent intencao = getIntent();
	        Bundle parametros = intencao.getExtras();	        
	        cd_cliente =  parametros.getString("cd_cliente").toString();
	        
	        Button btn = (Button) findViewById(R.id.btnVoltar);
	        btn.setOnClickListener(this); 
	        
	        Button btnTeste = (Button) findViewById(R.id.btnTeste);
	        btnTeste.setOnClickListener(this);	        
	        carregarListView();	        
	        
	        String provider = Settings.Secure.getString(getContentResolver(),  		Settings.Secure.LOCATION_PROVIDERS_ALLOWED);   
	        if((provider == null || provider.length()==0)  || !provider.contains("gps"))
	        {        
	        	Intent intent = new Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS);   
	        	startActivityForResult(intent, 1);
	        }
	        	
	        
	        // GPSTracker class
	        GPSTracker gps;
	        
	        gps = new GPSTracker(ListarStatus.this);
	        
	        // check if GPS enabled     
	        if(gps.canGetLocation()){
	             
	            double latitude = gps.getLatitude();
	            double longitude = gps.getLongitude();
	             
	            // \n is for new line
	            Toast.makeText(getApplicationContext(), "Sua Localização \nLatitude: " + latitude + "\nLongintude: " + longitude, Toast.LENGTH_LONG).show();    
	        }else{
	            // can't get location
	            // GPS or Network is not enabled
	            // Ask user to enable GPS/network in settings
	            gps.showSettingsAlert();
	        }
	   
	    }
	 	 
	 protected void onResume()
	 {
		 super.onResume();		 
		 
			dbSQLite dataBase = new dbSQLite(ListarStatus.this);
			dataBase.iniciaBanco();
			listViewStatus = (ListView)findViewById(R.id.listView1);
			listPendentes = new dbSQLite(this).ListarDocumentosPorCondicao(this, " PENDENTE='S'");
	        listRealizados = new dbSQLite(this).ListarDocumentosPorCondicao(this, " PENDENTE='N'");
	        listATransmitir = new dbSQLite(this).ListarDocumentosPorCondicao(this, " TRANSMITIDO='N' AND PENDENTE='N' ");        
		    
	        String posPend =  dataBase.qtdPosicoesPend(this);
		        
	        pendentes = 0;
			atransmitir = 0;
			realizados = 0;
			  
	         if(listPendentes!=null)
	         	pendentes = listPendentes.size();
	         
	         if(listRealizados!=null)
	         	realizados = listRealizados.size();
	         
	         if(listATransmitir!=null)
	           	atransmitir = listATransmitir.size();
	         
		        String[] lv_arr={"Pendentes: " + pendentes,"A Transmitir: " + atransmitir,"Realizados: " + realizados};	  	        
		        listViewStatus.setAdapter(new ArrayAdapter<String>(this,android.R.layout.simple_list_item_1 , lv_arr));  
	         
		 if(atransmitir > 0 && Conectado(ListarStatus.this))
         {         	
		        mAsyncTask = new AsyncTarefa();
		        mAsyncTask.execute(1);
         }
		 else
		 {
		 
			 if(posPend != "" && posPend !="0")
			 {
				   mAsyncTask = new AsyncTarefa();
			       mAsyncTask.execute(2);
			 }
		 
		 }
		 if(! Conectado(ListarStatus.this))
		 {
			 Toast.makeText(ListarStatus.this, "Sinal de Telefonia ou Wi-FI Indisponível" , Toast.LENGTH_SHORT).show();
		 }
		 
	 }
	 

		public void SincronizacaoAutomatica()
		{
			
			ChamadasWebServices call = new ChamadasWebServices();
			try 
			{				
				dbSQLite dataBase = new dbSQLite(this);
				dataBase.iniciaBanco();		
				
				ArrayList<DT> dtSincronizar = dataBase.ListarDocumentosPorCondicao(this, " TRANSMITIDO='N' AND PENDENTE='N' ");	
				
				boolean gravou = false;
				
				if(dtSincronizar!=null)
				{
					Iterator<DT> itr = dtSincronizar.iterator();

					 
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
							    try {
									fis = new FileInputStream(imagefile);
								} catch (FileNotFoundException e) {
									// TODO Auto-generated catch block
									e.printStackTrace();
								}
								Bitmap bi = BitmapFactory.decodeStream(fis);
								ByteArrayOutputStream baos = new ByteArrayOutputStream();
								bi.compress(Bitmap.CompressFormat.JPEG,10, baos);
								buffer = baos.toByteArray();
								//imagefile.delete();
							} 						
						
						if(buffer !=null &&	buffer.length>0)
						{
							if(cd_cliente.toString()=="2") // teste
								gravou = call.GravarOcorrencias(element.get_IDDOCUMENTO(),element.get_IDOCORRENCIA(),"EFETUADO PELO ANDROID","6", buffer, this,element.get_LONGITUDE(),element.get_LATTUDE(),	element.get_IDDT(),	element.get_DATAHORAOCORRENCIA(),element.get_DATAHORAOCORRENCIA(), cd_cliente);							
							else
							{
								gravou = call.GravarOcorrencias(element.get_IDDOCUMENTO(),element.get_IDOCORRENCIA(),"EFETUADO PELO ANDROID","6", buffer, this,element.get_LONGITUDE(),element.get_LATTUDE(),	element.get_IDDT(),	element.get_DATAHORAOCORRENCIA(),element.get_DATAHORAOCORRENCIA(), cd_cliente);
							
								if(gravou)
								{
									try
									{
										String filepath =sdCard.getAbsolutePath();								
										imagefile = new File(filepath + "/" + element.get_IDDOCUMENTO()+".jpg");								
										imagefile.delete();
									}
									catch (Exception e) 
									{
										Log.e("Apagar Imagem ", e.getMessage());
									}
								}
							}
						}
						else
						{
							if(cd_cliente.toString()=="2") // teste
								gravou = call.GravarOcorrenciasSemImagem(element.get_IDDOCUMENTO(), element.get_IDOCORRENCIA(), "EFETUADO PELO ANDROID", "6", this,	element.get_LONGITUDE(),element.get_LATTUDE(),	element.get_IDDT(),	element.get_DATAHORAOCORRENCIA(),element.get_DATAHORAOCORRENCIA(), cd_cliente);
							else
								gravou = call.GravarOcorrenciasSemImagem(element.get_IDDOCUMENTO(), element.get_IDOCORRENCIA(), "EFETUADO PELO ANDROID", "6", this,	element.get_LONGITUDE(),element.get_LATTUDE(),	element.get_IDDT(),	element.get_DATAHORAOCORRENCIA(),element.get_DATAHORAOCORRENCIA(), cd_cliente);
							
						}
						
						if(gravou)
						{
							dataBase.AlterarStatusDocumento(element.get_IDDOCUMENTO());
						}
						else
						{
							erro = true;
						}
									
					}  		
				}
				} 
				catch (Exception e) 
				{				
					Log.e("erro:", e.getMessage());
				}
			}

	 private void ApagarGravados(String Id) {
		SQLiteDatabase dataBase = openOrCreateDatabase("TRANSPORTE",Context.MODE_WORLD_READABLE,null);
		String sql = "DELETE FROM RASTREAMENTO where _id=" + Id;
		dataBase.close();
			
		}

	private void carregarListView() 
	 {
		listViewStatus = null;
		dbSQLite dataBase = new dbSQLite(ListarStatus.this);
		dataBase.iniciaBanco();
		listViewStatus = (ListView)findViewById(R.id.listView1);
		listPendentes = new dbSQLite(this).ListarDocumentosPorCondicao(this, " PENDENTE='S'");
        listRealizados = new dbSQLite(this).ListarDocumentosPorCondicao(this, " PENDENTE='N'");
        listATransmitir = new dbSQLite(this).ListarDocumentosPorCondicao(this, " TRANSMITIDO='N' AND PENDENTE='N' ");        
	        
	        
        pendentes = 0;
		atransmitir = 0;
		realizados = 0;
		  
         if(listPendentes!=null)
         	pendentes = listPendentes.size();
         
         if(listRealizados!=null)
         	realizados = listRealizados.size();
         
         if(listATransmitir!=null)
           	atransmitir = listATransmitir.size();           
         
         
	        String[] lv_arr={"Pendentes: " + pendentes,"A Transmitir: " + atransmitir,"Realizados: " + realizados};	  	        
	        listViewStatus.setAdapter(new ArrayAdapter<String>(this,android.R.layout.simple_list_item_1 , lv_arr));    	        
	        
	        listViewStatus.setOnItemClickListener(new OnItemClickListener() 
	        {
	        	  public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,  long arg3) 
	        	  { 	        		
	          		 Intent intencao = new Intent(ListarStatus.this, ListarPorStatus.class);
	          		 Bundle parametros = new Bundle();
	          		
	          		 if(arg2==0 && listPendentes!=null)
	          		 {
	          			intencao.putExtra("litadts", listPendentes);
	          			parametros.putString("tipo", "Pendentes");
	          			intencao.putExtras(parametros);          			
	 	        		startActivity(intencao);
	 	        		//finish();
	          		 }
	          		 
	          		 if(arg2==1 && listATransmitir!=null)
	          		 {
	          			intencao.putExtra("litadts", listATransmitir);
	          			parametros.putString("tipo", "ATransmitir");
	          			intencao.putExtras(parametros);
	 	        		startActivity(intencao);
	 	        		//finish();
	          		 }
	          		 
	          		 if(arg2==2 && listRealizados!=null)
	          		 {
	          			intencao.putExtra("litadts", listRealizados);
	          			parametros.putString("tipo", "Realizados");
	          			intencao.putExtras(parametros);
	 	        		startActivity(intencao);
	 	        		//finish();
	          		 }
          		
	              }  

	       });  	
		
	}

	public void onClick(View v) 
		{	
		
		switch(v.getId())
		{
			case R.id.btnVoltar:
				cancelarServ();
				break;
		    
			case R.id.btnTeste:
				Toast.makeText(this, "Text", Toast.LENGTH_SHORT).show();
		        String[] lv_arr={"Pendentes: z" ,"A Transmitir: y" ,"Realizados: x"};	  	        
		        listViewStatus.setAdapter(new ArrayAdapter<String>(this,android.R.layout.simple_list_item_1 , lv_arr));    	
				break;
		}		
	
		}

	private void cancelarServ() {
		finish();		 	
		
	}
	
	boolean erro=false;
	private class AsyncTarefa extends AsyncTask<Integer, Void, Integer> {
		 
        protected void onPreExecute() {
        	Toast.makeText(ListarStatus.this, "Iniciou a Transmissão", Toast.LENGTH_SHORT).show();
        }
        
        
        
        protected Integer doInBackground(Integer... progress) 
        {
        	erro = false;
        	try
        	{
        		SincronizacaoAutomatica();
        		
        	}
        	catch (ExceptionInInitializerError e) 
        	{
        		erro=true;
        		throw e;
			}
        	finally
        	{
        		        			
        	}
            return (Integer)0;
         }
        
        protected void onPostExecute(Integer result) 
        {
        	if(erro==false)
    			Toast.makeText(ListarStatus.this, "Fim da Transmissão", Toast.LENGTH_SHORT).show();
    		else
    			Toast.makeText(ListarStatus.this, "Sinal de Telefonia Indisponível...", Toast.LENGTH_SHORT).show();
        	carregarListView();
        }
    }
	
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

}