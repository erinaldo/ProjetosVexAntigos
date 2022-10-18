package br.com.sistecno.DT.DTO;

public class Rastreamento implements java.io.Serializable 
{
	private String _Longitude;
	private String _Latitude;
	private String _DataHora;
	private String _PontoDeOcorrencia;
	private String _IdDt;
	private String _Sinc;
	private String _Id;
	
	public String get_Longitude() {
		return _Longitude;
	}
	
	public void set_Longitude(String _Longitude) {
		this._Longitude = _Longitude;
	}
	
	public String get_Latitude() {
		return _Latitude;
	}
	
	public void set_Latitude(String _Latitude) {
		this._Latitude = _Latitude;
	}
	
	public String get_DataHora() {
		return _DataHora;
	}
	
	public void set_DataHora(String _DataHora) {
		this._DataHora = _DataHora;
	}

	public String get_PontoDeOcorrencia() {
		return _PontoDeOcorrencia;
	}

	public void set_PontoDeOcorrencia(String _PontoDeOcorrencia) {
		this._PontoDeOcorrencia = _PontoDeOcorrencia;
	}

	public String get_IdDt() {
		return _IdDt;
	}

	public void set_IdDt(String _IdDt) {
		this._IdDt = _IdDt;
	}

	public String get_Sinc() {
		return _Sinc;
	}

	public void set_Sinc(String _Sinc) {
		this._Sinc = _Sinc;
	}

	public String get_Id() {
		return _Id;
	}

	public void set_Id(String _Id) {
		this._Id = _Id;
	}

	
}
