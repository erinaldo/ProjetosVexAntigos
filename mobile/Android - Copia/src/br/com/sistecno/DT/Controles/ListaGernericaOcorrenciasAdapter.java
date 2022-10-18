package br.com.sistecno.DT.Controles;

import java.util.List;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;
import android.widget.Toast;
import br.com.sistecno.R;
import br.com.sistecno.DT.DTO.Ocorrencias;

public class ListaGernericaOcorrenciasAdapter extends BaseAdapter
{
	private Context context;    
	private List<Ocorrencias> Lista;
	
	public ListaGernericaOcorrenciasAdapter(Context context, List<Ocorrencias> locorrencias)
	{        
		this.context = context;        
		this.Lista = locorrencias;    
	}
	
	public int getCount() 
	{
		return Lista.size();
	}

	public Object getItem(int arg0) 
	{
		return Lista.get(arg0);
	}

	public long getItemId(int position) {
		return position;
	}

	public View getView(int position, View convertView, ViewGroup parent) 
	{
		try
		{
			// Recupera o estado da posição atual        
			Ocorrencias dt = Lista.get(position);
			
			// Cria uma instância do layout XML para os objetos correspondentes        
			// na View        
			LayoutInflater inflater = (LayoutInflater)context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);        
			View view = inflater.inflate(R.layout.list_view_ocorrencias, null);
			
			String cod = dt.get_CODIGO().toUpperCase().trim();
			
			if(cod.length()==1)
				cod = "00" + cod;
			
			if(cod.length()==2)
				cod = "0" + cod;
			
			String nome = dt.get_NOME().toUpperCase().trim();
			
			/*if(nome.length()>40)
				nome = nome.substring(0, 40) + "...";*/
			      
			TextView txtid = (TextView)view.findViewById(R.id.txtIdOcorrencia);        
			txtid.setText(cod + "|" + nome);
	
			return view;
		 }
	    catch(Exception erro)
	    {	       
	        Toast.makeText(context, erro.toString() ,Toast.LENGTH_SHORT).show();
	        return null;
	    }
	}
}