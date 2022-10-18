package br.com.sistecno;

import java.text.SimpleDateFormat;
import java.util.Date;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;
import br.com.sistecno.DT.DTO.DT;


public class ExibirDetalhe_dt extends Activity implements OnClickListener
{
	DT dt;
	 public void onCreate(Bundle savedInstanceState) 
	    {		 
	        super.onCreate(savedInstanceState);
	        setContentView(R.layout.exibir_detalhe_dt); 	        
	          
	        
	         dt= (DT)getIntent().getSerializableExtra("dt");
	        
	        TextView txtnumero = (TextView)findViewById(R.id.txtNumero);
	        txtnumero.setText(dt.get_NUMERODOCUMENTO());
	        
	        TextView txtendereco = (TextView)findViewById(R.id.txtEndereco);
	        txtendereco.setText(dt.get_ENDERECO() + ", " + dt.get_NUMERO());	        
	        
	        TextView txtdestinatario = (TextView)findViewById(R.id.txtDestinatario);
	        txtdestinatario.setText(dt.get_DESTINATARIO());
	        
	        TextView txtremetente = (TextView)findViewById(R.id.txtRemetente);
	        txtremetente.setText(dt.get_REMETENTE());
	        
	        TextView txtcidade = (TextView)findViewById(R.id.txtCidade);
	        txtcidade.setText(dt.get_CIDADE() + " - " + dt.get_ESTADO());
	        
	        TextView txtpeso = (TextView)findViewById(R.id.txtPeso);
	        txtpeso.setText(dt.get_PESOBRUTO());
	        
	        TextView txtvolumes = (TextView)findViewById(R.id.txtVolumes);
	        txtvolumes.setText(dt.get_VOLUMES());
	        
	        TextView txtOcorrencias = (TextView)findViewById(R.id.txtOcorrencias);
	        txtOcorrencias.setText(dt.get_IDOCORRENCIA());
	        
	        
	        Button btnVoltar = (Button)findViewById(R.id.btnVoltar);
	        btnVoltar.setOnClickListener(this);
	        
	        Button btnRealizarColeta = (Button)findViewById(R.id.btnRealizarColeta);
	        btnRealizarColeta.setOnClickListener(this);
	        
	        Button btnRealizar = (Button)findViewById(R.id.btnRealizar);
	        btnRealizar.setOnClickListener(this);
	        
	        Intent intencao = getIntent();
	        Bundle parametros = intencao.getExtras();
	        
    		if(parametros.getString("tipo").contains("Pendente"))
    		{
    			btnRealizar.setOnClickListener(this);	        			
    		}
    		else
    		{
    			Toast.makeText(this, "Documento já Realizado. " +parametros.getString("tipo").toString() , Toast.LENGTH_LONG).show();
    			btnRealizar.setEnabled(false);
    			btnRealizar.setText("Documento já Realizado. ");
    			
    		}
	        
	    }

	public void onClick(View v) {
		// TODO Auto-generated method stub
		try
		{
			
			int idControle = v.getId();
			
			if(idControle==R.id.btnVoltar)
				finish();
				else if (idControle==R.id.btnRealizar)
				{
					Intent intencao = new Intent(ExibirDetalhe_dt.this, SetarOcorrencias.class);
	        	     intencao.putExtra("dt", dt);
	        		 startActivity(intencao);	
	        		 finish();
				}
				else if (idControle==R.id.btnRealizarColeta)
				{
					try
					{
					
						String NomeBanco = "TRANSPORTE";
						SQLiteDatabase	dataBase = openOrCreateDatabase(NomeBanco,Context.MODE_WORLD_READABLE,null);
					
					
			    		String query = "SELECT IDDT FROM DOCUMENTO ";  
			    		String ret ="0";
			    	
			        	Cursor c =  dataBase.rawQuery(query, null);
			        
			        	if(c==null)
			        		return;
			        
			        	c.moveToFirst();
			            if(c.getCount()>0)
			            {       
				        	ret = c.getString(c.getColumnIndex("IDDT"));	 	                
				        	c.close();	
						
							GPSTracker gps;					
							gps = new GPSTracker(this.getApplicationContext());
						
					    	SimpleDateFormat dateFormat = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
							Date date = new Date();
						
						
							String sql = "INSERT INTO RASTREAMENTO (LATITUDE,LONGITUDE, DATAHORA, PONTODEOCORRENCIA, IDDT, SINC) VALUES ('"+String.valueOf(gps.getLatitude())+"','"+String.valueOf(gps.getLongitude())+"', '"+ dateFormat.format(date) +"', 'COL', '"+ret+"', 'N')";
							dataBase.execSQL(sql);
							
							Toast.makeText(this.getApplicationContext(), "Local de Coleta Realizado com sucesso!!", Toast.LENGTH_SHORT).show();
							finish();
							
			            }
					}
					catch (Exception e) {
						
					}
				}
				
			
			
		 }
	    catch(Exception erro)
	    {	       
	        Toast.makeText(this, erro.toString() ,Toast.LENGTH_SHORT).show();	        
	    }
	}


	 	 
}
