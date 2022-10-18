package br.com.sistecno.DT.Controles;

import java.util.ArrayList;

import BD.dbSQLite;
import android.app.ListActivity;
import android.os.Bundle;
import br.com.sistecno.R;
import br.com.sistecno.DT.DTO.Ocorrencias;

public class ListaGenericaOcorrencias extends ListActivity
{
	 @Override
	public void onCreate(Bundle savedInstanceState) 
	{		
		super.onCreate(savedInstanceState);
		setContentView(R.layout.list_view_ocorrencias); 
		
		ArrayList<Ocorrencias> locorrencias = new dbSQLite(this).ListarOcorrencias(this);
		setListAdapter(new ListaGernericaOcorrenciasAdapter(this, locorrencias));
	}
}

