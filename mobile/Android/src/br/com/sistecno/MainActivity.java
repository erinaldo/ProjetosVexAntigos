package br.com.sistecno;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.Iterator;
import java.util.List;

import br.com.sistecno.DT.DTO.Conf;
import br.com.sistecno.DT.DTO.DT;
import BD.dbSQLite;
import WSS.ChamadasWebServices;
import android.R.integer;
import android.R.string;
import android.app.Activity;
import android.app.AlarmManager;
import android.app.AlertDialog;
import android.app.PendingIntent;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.net.ConnectivityManager;
import android.net.ParseException;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Environment;
import android.provider.Settings;
import android.provider.Settings.Secure;
import android.telephony.TelephonyManager;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.Toast;



@SuppressWarnings("unused")
public class MainActivity extends Activity implements OnClickListener
{       
	EditText edplaca;
	EditText edDT;
	EditText txtEmpresa;
	LocationManager locationManager;
	ListView listViewStatus;
	AsyncOcorrencia mAsyncTask;
	AsyncDocumentos mAsyncDocs;
	AsyncLimparDocumentos mAsyncDocs1;
	ProgressDialog mDialog;
	String cd_cliente ="";

	
    public void onCreate(Bundle savedInstanceState) 
    {
        super.onCreate(savedInstanceState);
        
        
        
        setContentView(R.layout.login);
        Button btn = (Button) findViewById(R.id.btnConfirmar);
        btn.setOnClickListener(this);    
        //Toast.makeText(getApplicationContext(), "Teste", Toast.LENGTH_LONG).show();
        
        String provider = Settings.Secure.getString(getContentResolver(),  		Settings.Secure.LOCATION_PROVIDERS_ALLOWED);   
        if((provider == null || provider.length()==0)  || !provider.contains("gps"))
        {        
        	//Intent intent = new Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS);   
        	//startActivityForResult(intent, 1);
        }
        	
        /*
        // GPSTracker class
        GPSTracker gps;
        
        gps = new GPSTracker(MainActivity.this);
        
        // check if GPS enabled     
        if(gps.canGetLocation()){
             
            double latitude = gps.getLatitude();
            double longitude = gps.getLongitude();
             
            // \n is for new line
            Toast.makeText(getApplicationContext(), "Sua Localização \nLatitude: " + latitude + "\nLongintude: " + longitude, Toast.LENGTH_LONG).show();    
        }else{
            gps.showSettingsAlert();
        }
        
        */
        dbSQLite db = new dbSQLite(this);
        db.iniciaBanco();        
        ArrayList<Conf> conf = db.retornarConfiguracoes(this);        
        
        if(conf != null)
        {        	
        	Iterator<Conf> itr = conf.iterator();		  
			 
			while (itr.hasNext()) 
			{
				Conf element = itr.next();
				edplaca = (EditText)findViewById(R.id.edPlaca);
				edDT = (EditText)findViewById(R.id.edDT);
				txtEmpresa = (EditText)findViewById(R.id.txtEmpresa);
				
				edplaca.setText(element.getPLACA());
				edDT.setText(element.getNDT());
				txtEmpresa.setText(element.getCD_CLIENTE());
			}
        }             
       
		//PosicaoGPS();		
		
    }
    
	
	private void PosicaoGPS() {
		try 
		{
			Intent it = new Intent("AlarmPosicao");
			PendingIntent p  = PendingIntent.getService(MainActivity.this, 123, it, 0);
			
			String android_id = Secure.getString(getBaseContext().getContentResolver(),Secure.ANDROID_ID);
			String tempo = new dbSQLite(this).RetornarMinutosSincronizacao(this, android_id);
			
			AlarmManager alarme = (AlarmManager)getSystemService(ALARM_SERVICE);
			alarme.cancel(p);
			
			
			if(tempo==null || tempo =="" ||  tempo=="0")
				alarme.setRepeating(AlarmManager.RTC, 60000,300000, p); // pra comecar 1 min e 5 de intervalo
			else
				alarme.setRepeating(AlarmManager.RTC, 60000,(Integer.parseInt(tempo) * 60000 ), p);			
			
     		startActivity(it);
			
        } catch (Exception e) 
        {
            Log.e("Inicial Posicao", e.getMessage());
        }

		
	}
	
	public void onClick(View v) 
	{		
		try
		{		
			edplaca = (EditText)findViewById(R.id.edPlaca);
			edDT = (EditText)findViewById(R.id.edDT);
			txtEmpresa = (EditText)findViewById(R.id.txtEmpresa);
			
			if(edDT.getText().length()==0 || edplaca.getText().length()==0 || txtEmpresa.getText().length()==0)
			{
				CriarMensagem("ATENCAO","Preencha os Campos.");
				txtEmpresa.setFocusable(true);
				return;
			}
		 
			cd_cliente = txtEmpresa.getText().toString();			
			dbSQLite db = new dbSQLite(this);			
			Conf item = new Conf();
			item.setCD_CLIENTE(cd_cliente);
			item.setPLACA(edplaca.getText().toString());
			item.setNDT(edDT.getText().toString());
			
			ArrayList<Conf> ldtos = new ArrayList<Conf>();
			ldtos.add(item);
			db.inserirConf(ldtos);
			
			Intent intencao = new Intent(this, ListarStatus.class);
			Bundle parametros = new Bundle();		 
			parametros.putString("cd_cliente", cd_cliente);
			intencao.putExtras(parametros); 
			
			startActivity(intencao);
	 } 
    catch (Exception ex) 
    { 
    	CriarMensagem("erro", ex.getMessage());
    }
		
	}
	
	public void CriarMensagem(String mensagen, String titulo)
	{
		AlertDialog.Builder dialogo = new AlertDialog.Builder(this);
		dialogo.setMessage(mensagen);
		dialogo.setTitle(titulo);
		dialogo.setIcon(R.drawable.ic_menu_search);
		dialogo.setNeutralButton("OK", null);
		dialogo.show();
	}

    public boolean onCreateOptionsMenu(Menu menu)
    {
    	MenuInflater inflater = getMenuInflater();
    	inflater.inflate(R.menu.menu_sincronizar, menu);
    	return true;
    }
   
	public boolean onOptionsItemSelected(MenuItem Item)
    {
    	try
		{
    		edplaca = (EditText)findViewById(R.id.edPlaca);
			edDT = (EditText)findViewById(R.id.edDT);
			txtEmpresa = (EditText)findViewById(R.id.txtEmpresa);
			cd_cliente = txtEmpresa.getText().toString();
			
			if(edDT.getText().length()==0 || edplaca.getText().length()==0 || txtEmpresa.getText().length()==0)
			{
				CriarMensagem("ATENCAO","Preencha os Campos.");
				txtEmpresa.setFocusable(true);
				return false;
			}
			
			int id = Item.getItemId();
			
			if(id==R.id.mnSair)
			{
				finish();
			}
			else if(id==R.id.mnSincronizarOco)
			{
				try
    			{
    				if(!Conectado(this))
        			{
        				Toast.makeText(this, "Sinal de Internet Indisponível", Toast.LENGTH_LONG).show();
        				return false;
        			}
    				
    				mAsyncTask = new AsyncOcorrencia();
    				mAsyncTask.execute(1);
    				
    				Toast.makeText(this, "Ocorrencias Sincronizadas", Toast.LENGTH_LONG).show();        				
        			
    				return true;
    			}
				catch(Exception ex)
				{
					Toast.makeText(this, ex.getMessage(), Toast.LENGTH_LONG).show();
				}
			}
			else if(id==R.id.mnSincronizarDocumentos)
			{
Date dataInicial = new Date(System.currentTimeMillis());
    			
    			if(!Conectado(this))
    			{
    				Toast.makeText(this, "Sinal de Internet Indisponível", Toast.LENGTH_LONG).show();
    				return false;
    			}
    			
    			WSS.ChamadasWebServices ws = new WSS.ChamadasWebServices();    			
    			String qtdDocs = ws.RetornarQtdNotasDt(edplaca.getText().toString(), edDT.getText().toString(), txtEmpresa.getText().toString());
    			
    			
    			if(qtdDocs=="0")
    			{
    				Toast.makeText(this, "DT/RE Não disponível", Toast.LENGTH_LONG).show();
    				return false;
    			}
    			else
    			{	
    				mAsyncDocs = new AsyncDocumentos();
	    			mAsyncDocs.execute(1);
	    			Toast.makeText(this, qtdDocs+ " - Sincronizados", Toast.LENGTH_LONG).show();	    			
	    			Date dataFinal = new Date(System.currentTimeMillis());
	    			SimpleDateFormat formatarDate = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");  	    			
	    			String android_id = Secure.getString(getBaseContext().getContentResolver(),Secure.ANDROID_ID);			
	    			ws.TempoDeSincronizacao(android_id, edDT.getText().toString(),formatarDate.format(dataInicial), formatarDate.format(dataFinal), txtEmpresa.getText().toString());
	    			return true;    			
	    			
    			}
			}
			
			
			
		return false;
	    } 
	    catch (Exception ex) 
	    { 
	    	CriarMensagem("erro", ex.getMessage());
	    	return false;
	    }
    }
	
	private void SincronizarDocumentosEntrega() 
	{
		try
		{
			if(!Conectado(this))
			{
				Toast.makeText(this.getApplicationContext(), "Internet não Disponível.", Toast.LENGTH_SHORT).show();
				return;
			}
			
			dbSQLite dataBase = new dbSQLite(this);
			dataBase.iniciaBanco();
			
			ArrayList<DT> dtSincronizar = dataBase.ListarDocumentosPorCondicao(this, " TRANSMITIDO='N' AND PENDENTE='N' ");	
			ChamadasWebServices call = new ChamadasWebServices();
			
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
	
					if(file.exists())						
						{
							String filepath =sdCard.getAbsolutePath();								
							File imagefile = new File(filepath + "/" + element.get_IDDOCUMENTO()+".jpg");								
							FileInputStream fis = new FileInputStream(imagefile);
							Bitmap bi = BitmapFactory.decodeStream(fis);
							ByteArrayOutputStream baos = new ByteArrayOutputStream();
							bi.compress(Bitmap.CompressFormat.JPEG,30, baos);
							buffer = baos.toByteArray();
							//imagefile.delete();
						} 						
					
					 				
					if(buffer != null && buffer.length>0)
					{
						call.GravarOcorrencias(
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
								element.get_DATAHORAOCORRENCIA(), 
								cd_cliente
								);
						
					}
					else
					{
						call.GravarOcorrenciasSemImagem(
								element.get_IDDOCUMENTO(), 
								element.get_IDOCORRENCIA(), 
								"EFETUADO PELO ANDROID", 
								"2", 
								this,
								element.get_LONGITUDE(),
								element.get_LATTUDE(),
								element.get_IDDT(),
								element.get_DATAHORAOCORRENCIA(),
								element.get_DATAHORAOCORRENCIA(),
								cd_cliente
								);
					}			
					
				}  		
			}
		
			String android_id = Secure.getString(getBaseContext().getContentResolver(),Secure.ANDROID_ID);
			
			if(android_id==null)
				android_id = "Emulador";
			
			ArrayList<DT>  notas = call.Listar_Documentos(edplaca.getText().toString(), edDT.getText().toString(),txtEmpresa.getText().toString(),true,this, android_id ,cd_cliente);
			
			/*if(notas.size()==0)
			{
				Toast.makeText(this.getApplicationContext(), "DT Não Disponível.", Toast.LENGTH_SHORT).show();
				return;
			}
			else
			{
				Toast.makeText(this.getApplicationContext(),  "Notas Sincronizadas", Toast.LENGTH_SHORT).show();
			}*/
			
			
			
			dataBase.CarregarDadosAparelho(android_id, cd_cliente);
		}			
		catch (Exception e) 
		{
			Log.e("Sincrinização de Entregas", e.getMessage());
		}
		
	}
	
	private void SincronizarOcorrencias() 
	{
		try
		{		
			if(Conectado(this))
			{
				ChamadasWebServices call = new ChamadasWebServices();
				call.SincronizarOcorrencias(this,cd_cliente);
			}
			
			
		 } 
	    catch (Exception ex) 
	    { 
	    	//CriarMensagem("erro", ex.getMessage());
	    	Log.e("Sincronizar Ocorrencias", ex.getMessage());
	    	//Toast.makeText(this.getApplicationContext(), "Sem Sinal de Telefonia, tente mais tarde.", Toast.LENGTH_SHORT).show();
	    }
	}

	/*private void Apaga_Recria() 
	{
		try
		{		
			//ChamadasWebServices call = new ChamadasWebServices();
			//call.Limpar(edplaca.getText().toString(), edDT.getText().toString(), cd_cliente, this);
			
			//SincronizarDocumentosEntrega();
		 } 
	    catch (Exception ex) 
	    { 
	    	CriarMensagem("erro", ex.getMessage());
	    }
	}*/

	
	private class AsyncOcorrencia extends AsyncTask<Integer, Void, Integer> {
		 
	    protected void onPreExecute() {
	        mDialog = ProgressDialog.show(MainActivity.this, "Aguarde...", "Sincronizando Ocorrências...", false, false);
	    	//Toast.makeText(MainActivity.this, "Iniciou a Transmissão", Toast.LENGTH_SHORT).show();
	    }
	    
	    protected Integer doInBackground(Integer... progress) 
	    {
	    	SincronizarOcorrencias();
	        int i = 0;
	        do {
	            try { 
	                Thread.sleep(1000);
	                i++;
	            } catch (InterruptedException e) { 
	            	
	            }
	        } while (i<progress[0]);
	        
	        
	        return (Integer) i;
	     }
	    
	    protected void onPostExecute(Integer result) {
	        mDialog.dismiss();
	        }
	}
	
	private class AsyncDocumentos extends AsyncTask<Integer, Void, Integer> {
		 
	    protected void onPreExecute() {
	        mDialog = ProgressDialog.show(MainActivity.this, "Aguarde...", "Sincronizando Entregas...", false, false);
	    	 }
	    
	    protected Integer doInBackground(Integer... progress) 
	    {
	    	SincronizarDocumentosEntrega();
	        int i = 0;
	        do {
	            try { 
	                Thread.sleep(1000);
	                i++;
	            } catch (InterruptedException e) {  }
	        } while (i<progress[0]);
	        
	        
	        return (Integer) i;
	     }
	    
	    protected void onPostExecute(Integer result) {
	        mDialog.dismiss();       
	    	
	    }
	}
		
	private class AsyncLimparDocumentos extends AsyncTask<Integer, Void, Integer> {
		 
	    protected void onPreExecute() {
	        mDialog = ProgressDialog.show(MainActivity.this, "Aguarde...", "Limpando e Carregando Novamente...", false, false);
	    	 }
	    
	    protected Integer doInBackground(Integer... progress) 
	    {
	    	//Apaga_Recria();
	    	
	        int i = 0;
	        do {
	            try { 
	                Thread.sleep(1000);
	                i++;
	            } catch (InterruptedException e) {  }
	        } while (i<progress[0]);
	        
	        
	        return (Integer) i;
	     }
	    
	    protected void onPostExecute(Integer result) {
	        mDialog.dismiss();       
	    	
	    }
	}

	private LocationListener myLocationListener = new LocationListener() 
	{
	        public void onLocationChanged(Location location) 
	        {
	            Location mLocal = new Location(location);
	            atualizaPosicao(mLocal); 
	        }
	        public void onProviderDisabled(String provider) 
	        {
	            
	        }
	        public void onProviderEnabled(String provider) 
	        {
	            
	        }
	
	
	public void atualizaPosicao(Location location)
	{       
	    if (location != null) 
	    {

			//pega a precisão
			double precisaoGPS = location.getAccuracy();
			double latitude = (location.getLatitude());
			double longitude = (location.getLongitude());
			double data_hora = location.getTime();

	    }
	}

	public void startColeta() 
	    {           
	        locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, 0, 0, myLocationListener);
	        
	    }
	    
	    public void stopColeta() 
	    {        
	        locationManager.removeUpdates(myLocationListener);        
	    }
		public void onStatusChanged(String provider, int status, Bundle extras) {
			// TODO Auto-generated method stub
			
		}
	};

	public static boolean Conectado(Context context) {
        try {
            ConnectivityManager cm = (ConnectivityManager)
            context.getSystemService(Context.CONNECTIVITY_SERVICE);
            if (cm.getNetworkInfo(ConnectivityManager.TYPE_MOBILE).isConnected()) {
                    //handler.sendEmptyMessage(0);
                    //Log.d("conexaoTipo:","Status de conexão 3G: "+cm.getNetworkInfo(ConnectivityManager.TYPE_MOBILE).isConnected());
                    return true;
            } else if(cm.getNetworkInfo(ConnectivityManager.TYPE_WIFI).isConnected()){
                    //handler.sendEmptyMessage(0);
                    //Log.d("conexaoTipo:","Status de conexão Wifi: "+cm.getNetworkInfo(ConnectivityManager.TYPE_WIFI).isConnected());
                    return true;
            } else {
                    //handler.sendEmptyMessage(0);
                   // Log.e("conexaoTipo:","Status de conexão Wifi: "+cm.getNetworkInfo(ConnectivityManager.TYPE_WIFI).isConnected());
                   // Log.e("conexaoTipo:","Status de conexão 3G: "+cm.getNetworkInfo(ConnectivityManager.TYPE_MOBILE).isConnected());
                    return false;
            }
        } catch (Exception e) {
                Log.e("conexaoTipo:",e.getMessage());
                return false;
        }
    }
}