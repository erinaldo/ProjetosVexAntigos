package br.com.sistecno.DT.Controles;

import java.util.List;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;
import br.com.sistecno.R;
import br.com.sistecno.DT.DTO.DT;

public class ListaGernericaPorStatusAdapter extends BaseAdapter
{
	private Context context;    
	private List<DT> Lista;
	
	public ListaGernericaPorStatusAdapter(Context context, List<DT> ListaGenerica)
	{        
		this.context = context;        
		this.Lista = ListaGenerica;    
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
		 // Recupera o estado da posição atual        
		DT dt = Lista.get(position);
		
		// Cria uma instância do layout XML para os objetos correspondentes        
		// na View        
		LayoutInflater inflater = (LayoutInflater)context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);        
		View view = inflater.inflate(R.layout.listview_por_status, null);
		
		      
		TextView txtid = (TextView)view.findViewById(R.id.txtId);        
		txtid.setText("N°.: " + dt.get_NUMERODOCUMENTO());

		TextView txtremetente = (TextView)view.findViewById(R.id.txtRemetente);        
		txtremetente.setText("REMETENTE: " + dt.get_REMETENTE());
		
		TextView txtdestinatario = (TextView)view.findViewById(R.id.txtDestinatario);        
		txtdestinatario.setText("DESTINATÁRIO: " + dt.get_DESTINATARIO());
		
		
				
		return view;
	}

}
