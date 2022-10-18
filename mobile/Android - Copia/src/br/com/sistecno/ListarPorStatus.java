package br.com.sistecno;

import java.util.ArrayList;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;
import br.com.sistecno.DT.Controles.ListaGernericaPorStatusAdapter;
import br.com.sistecno.DT.DTO.DT;

public class ListarPorStatus extends Activity implements OnClickListener 
{	
	
	ListView listViewStatus;
	public void onCreate(Bundle savedInstanceState) 
	    {		 
	        super.onCreate(savedInstanceState);
	        setContentView(R.layout.listaporstatus); 	        
	        listViewStatus = (ListView)findViewById(R.id.listViewStatus);  
	        
	        Button botaoVoltar = (Button)findViewById(R.id.btnVoltar);
	        botaoVoltar.setOnClickListener(this);
	        
	        @SuppressWarnings("unchecked")
			ArrayList<DT> listDt = (ArrayList<DT>)getIntent().getSerializableExtra("litadts"); 	 
	        
	        TextView txtTitulo = (TextView)findViewById(R.id.txtTitulo0);
	        
	        Intent intencao = getIntent();
	        Bundle parametros = intencao.getExtras();
	        
	        if(parametros.getString("tipo").contains("Pendente"))
	             txtTitulo.setText("Documentos Pendentes");
	             
	        if(parametros.getString("tipo").contains("Transmitir"))
	             txtTitulo.setText("Documentos  à Transmitir");
	        
	        if(parametros.getString("tipo").contains("Realizado"))
	             txtTitulo.setText("Documentos  Realizados");
	        
	        
	        ListaGernericaPorStatusAdapter adapter = new ListaGernericaPorStatusAdapter(this,listDt) ;    
	        
	        listViewStatus.setAdapter(adapter);    
	        
	         listViewStatus.setOnItemClickListener(new OnItemClickListener() 
	        {
	        	  public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,  long arg3) 
	        	  {        		  
	        		
	        		 DT dt = (DT)listViewStatus.getAdapter().getItem(arg2);	        		 
      		 
	        		 Intent intencao = new Intent(ListarPorStatus.this, ExibirDetalhe_dt.class);
	        	     intencao.putExtra("dt", dt);
	        	     
	        	     TextView txtTitulo = (TextView)findViewById(R.id.txtTitulo0);
	        	     
	        	     Bundle parametros = new Bundle();
	        	     parametros.putString("tipo", txtTitulo.getText().toString());
	        	     intencao.putExtras(parametros);
	        		 startActivity(intencao);
	        		 finish();
	        	  }  

	       });  
	        
	    }
	 
	public void onClick(View v) {
		
		finish();
		
	}

}
