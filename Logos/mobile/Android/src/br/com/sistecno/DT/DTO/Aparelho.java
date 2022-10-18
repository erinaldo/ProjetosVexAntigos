package br.com.sistecno.DT.DTO;

public class Aparelho {

	private String _ID;
	private String IdRastreador;
	private String Chave;
	private String Nome;
	private String Tempo;
	private String EnviaPosicaoZerada;
	private String Cd_cliente;
	
	public String get_ID() {
		return _ID;
	}
	public void set_ID(String _ID) {
		this._ID = _ID;
	}
	public String getIdRastreador() {
		return IdRastreador;
	}
	public void setIdRastreador(String idRastreador) {
		IdRastreador = idRastreador;
	}
	public String getChave() {
		return Chave;
	}
	public void setChave(String chave) {
		Chave = chave;
	}
	public String getNome() {
		return Nome;
	}
	public void setNome(String nome) {
		Nome = nome;
	}
	public String getTempo() {
		return Tempo;
	}
	public void setTempo(String tempo) {
		Tempo = tempo;
	}
	public String getEnviaPosicaoZerada() {
		return EnviaPosicaoZerada;
	}
	public void setEnviaPosicaoZerada(String enviaPosicaoZerada) {
		EnviaPosicaoZerada = enviaPosicaoZerada;
	}
	public String getCd_cliente() {
		return Cd_cliente;
	}
	public void setCd_cliente(String cd_cliente) {
		Cd_cliente = cd_cliente;
	}
	
	
}
