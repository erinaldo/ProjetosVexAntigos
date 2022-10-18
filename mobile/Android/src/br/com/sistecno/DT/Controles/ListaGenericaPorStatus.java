package br.com.sistecno.DT.Controles;

import java.util.ArrayList;

import android.app.ListActivity;
import android.os.Bundle;
import br.com.sistecno.R;
import br.com.sistecno.DT.DTO.DT;

public class ListaGenericaPorStatus extends ListActivity
{
	 @Override
	public void onCreate(Bundle savedInstanceState) 
	{		
		super.onCreate(savedInstanceState);
		setContentView(R.layout.listview_por_status); 
		
		@SuppressWarnings("unchecked")
		ArrayList<DT> listDt = (ArrayList<DT>)getIntent().getSerializableExtra("litadts");
		setListAdapter(new ListaGernericaPorStatusAdapter(this, listDt));
	}
}

