package br.com.sistecno;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import BD.dbSQLite;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.location.Criteria;
import android.location.Location;
import android.location.LocationManager;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.provider.MediaStore;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;
import br.com.sistecno.DT.Controles.ListaGernericaOcorrenciasAdapter;
import br.com.sistecno.DT.DTO.DT;
import br.com.sistecno.DT.DTO.Ocorrencias;

public class SetarOcorrencias extends Activity implements OnClickListener 
{
	ListView listv;
	DT dt;
	Ocorrencias oco;
	 
	public void onCreate(Bundle savedInstanceState) 
    {		 
        super.onCreate(savedInstanceState);
        setContentView(R.layout.setarocorrencias); 	  
          
	   	 TextView txtEscolha = (TextView)findViewById(R.id.txtEscolha);
		 txtEscolha.setText("Selecione uma Ocorrência");
		 txtEscolha.setTextColor(Color.RED);
		 txtEscolha.setTextSize(15);
	 
        dt= (DT)getIntent().getSerializableExtra("dt");
        
        listv = (ListView)findViewById(R.id.listView1);
		ArrayList<Ocorrencias> locorrencias =  new dbSQLite(this).ListarOcorrencias(this);
		
		ListaGernericaOcorrenciasAdapter adapter = new ListaGernericaOcorrenciasAdapter(this, locorrencias);
		listv.setAdapter(adapter);
		
		listv.setOnItemClickListener(new OnItemClickListener() 
	        {
	        	  public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,  long arg3) 
	        	  {        		  
	        		
	        		 oco = (Ocorrencias)listv.getAdapter().getItem(arg2);	        		 
	        		 Toast.makeText(SetarOcorrencias.this, "Você selecionou : " + oco.get_NOME().toUpperCase().trim(), Toast.LENGTH_SHORT).show();	        		 
	        		 
	        		 TextView txtnumero = (TextView)findViewById(R.id.txtNumero);
	        		 txtnumero.setText(dt.get_NUMERODOCUMENTO());
	        		 txtnumero.setTextColor(Color.BLACK);     		 
	        		 
	        		 TextView txtEscolha = (TextView)findViewById(R.id.txtEscolha);
	        		 txtEscolha.setText("Você selecionou:\n" + oco.get_NOME().toUpperCase().trim());
	        		 txtEscolha.setTextColor(Color.GREEN);
	        		 txtEscolha.setTextSize(12);
	        		
	        	  }  

	       });  
		
		Button btnVoltar= (Button)findViewById(R.id.btnVoltar);
		btnVoltar.setOnClickListener(this);
		
		Button btnConfirmarOcorrencia= (Button)findViewById(R.id.btnConfirmarOcorrencia);
		btnConfirmarOcorrencia.setOnClickListener(this);		
		
    }
	
public void onClick(View arg0) 
	{
	
	int id = arg0.getId();
	
	if(R.id.btnVoltar==id)
		finish();
		else if(R.id.btnConfirmarOcorrencia==id)
		{
			TextView txtEscolha = (TextView)findViewById(R.id.txtEscolha);
			
			if(txtEscolha.getText().length()>10 && txtEscolha.getText()!="Selecione uma Ocorrência")
			{				
				dbSQLite dataBase = new dbSQLite(this);
	            dataBase.iniciaBanco();
	            
/*		            double lat = 0;
            	double lon = 0;
            	
	            LocationManager LM = (LocationManager)getSystemService(Context.LOCATION_SERVICE);  
	            String bestProvider = LM.getBestProvider(new Criteria(),true); 
	            
	            Location l = LM.getLastKnownLocation(bestProvider);  
	            		            

	            if(l!=null)
	            {
	            	lat = l.getLatitude();
	            	lon = l.getLongitude();
	            }
	            */
	            
	            // GPSTracker class
		        GPSTracker gps;
		        
		        gps = new GPSTracker(SetarOcorrencias.this);
		        
		        double latitude =0 ;
		        double longitude = 0;
		        
		        // check if GPS enabled     
		        if(gps.canGetLocation()){
		             
		            latitude = gps.getLatitude();
		            longitude = gps.getLongitude();
		        }
	            
	            dataBase.SetarOcorrenciaDocumento(oco.get_IDOCORRENCIA(), dt.get_IDDOCUMENTO(), String.valueOf(latitude), String.valueOf(longitude));
				CriarMensagem("Ocorrencia Gravada com Sucesso", "Confirmação...");		
				showCustomDialog();
			}			
			
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
	
	private void showCustomDialog()
	{

    	final Dialog dialog = new Dialog(this);
    	dialog.setContentView(R.layout.custon_dialog);//carregando o layout do dialog do xml
    	dialog.setTitle("Atenção");//título do dialog

    	final Button ok = (Button) dialog.findViewById(R.id.bt_ok);//se atentem ao dialog.
    	final Button cancelar = (Button) dialog.findViewById(R.id.bt_cancel);
 	

    	ok.setOnClickListener(new View.OnClickListener() {
    		public void onClick(View v) 
    		{
    			//ação do botão ok    			
    			Intent i = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);    			
    			//o Environment.getExternalStorageDirectory() retorna a string da localizaÃ§Ã£o do sdcard (geralmente retorna sdcard mesmo)
    			//a imagem vc pode substituir pela pk do registro.
    			File f = new File(Environment.getExternalStorageDirectory(), dt.get_IDDOCUMENTO()+".jpg");   			
    	
    			
    			Uri end = Uri.fromFile(f);    			
    			//passa a informaÃ§Ã£o da URI para a intent
    			i.putExtra(MediaStore.EXTRA_OUTPUT, end);    			
    			startActivity(i);
    			dialog.dismiss();  	
    			//voltarTelaInicial();
    			finish();
    		}
    	});

    	cancelar.setOnClickListener(new View.OnClickListener() 
    	{
    		public void onClick(View v) 
    		{
    			//ação do botão cancelar    			
				//voltarTelaInicial();
    			finish();
    			dialog.dismiss();//encerra o dialog
    		}			
    	});

    dialog.show();//mostra o dialog

    }
	
	private void voltarTelaInicial() 
	{
		Intent intencao = new Intent(this, ListarStatus.class);				
		intencao.putExtra("dt", dt);
		startActivity(intencao);
		finish();
		
	}
	
	
}
