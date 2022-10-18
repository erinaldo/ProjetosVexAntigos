package br.com.sistecno.DT.DTO;

public class Conf {
	private String _id;
	private String PLACA;
	private String NDT;
	private String CD_CLIENTE;
	
	public String get_Id() {
		return _id;
	}
	public void set_Id(String _Id) {
		this._id = _Id;
	}
	public String getPLACA() {
		return PLACA;
	}
	public void setPLACA(String pLACA) {
		PLACA = pLACA;
	}
	public String getNDT() {
		return NDT;
	}
	public void setNDT(String nDT) {
		NDT = nDT;
	}
	public String getCD_CLIENTE() {
		return CD_CLIENTE;
	}
	public void setCD_CLIENTE(String cD_CLIENTE) {
		CD_CLIENTE = cD_CLIENTE;
	}
}
