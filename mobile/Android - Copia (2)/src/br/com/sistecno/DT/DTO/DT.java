package br.com.sistecno.DT.DTO;

import br.com.sistecno.GPSTracker;
import android.content.Context;
import android.widget.Toast;

@SuppressWarnings("serial")
public class DT implements java.io.Serializable 
{
	
	private String _NUMERO; 
	private String _IDDOCUMENTOOCORRENCIA;
	private String _NUMERODOCUMENTO;
	private String _IDDOCUMENTO ;
	private String _IDFILIALATUAL;
	private String _VOLUMES;                                
	private String _PESOBRUTO;                              
	private String _PLACA;     
	private String _NUMEROPLACA ;
	private String _IDDT;
	private String _REMETENTE ;
	private String _DESTINATARIO ; 
	private String _CIDADE  ;	
	private String _PENDENTE  ;
	private String _TRANSMITIDO  ;	
	private String _ENDERECO ;
	private String _ESTADO ;
	private String _IDOCORRENCIA ;	
	private String _EMPRESA ;
	
	private String _DATAHORAOCORRENCIA ;
	private String _LATTUDE ;
	private String _LONGITUDE ;
	private String _DATAHORAPOSICAO ;
	
	
	public String get_IDDOCUMENTOOCORRENCIA() {
		return _IDDOCUMENTOOCORRENCIA;
	}
	public void set_IDDOCUMENTOOCORRENCIA(String _IDDOCUMENTOOCORRENCIA) {
		this._IDDOCUMENTOOCORRENCIA = _IDDOCUMENTOOCORRENCIA;
	}
	public String get_NUMERODOCUMENTO() {
		return _NUMERODOCUMENTO;
	}
	public void set_NUMERODOCUMENTO(String _NUMERODOCUMENTO) {
		this._NUMERODOCUMENTO = _NUMERODOCUMENTO;
	}
	public String get_IDFILIALATUAL() {
		return _IDFILIALATUAL;
	}
	public void set_IDFILIALATUAL(String _IDFILIALATUAL) {
		this._IDFILIALATUAL = _IDFILIALATUAL;
	}
	public String get_IDDOCUMENTO() {
		return _IDDOCUMENTO;
	}
	public void set_IDDOCUMENTO(String _IDDOCUMENTO) {
		this._IDDOCUMENTO = _IDDOCUMENTO;
	}
	public String get_VOLUMES() {
		return _VOLUMES;
	}
	public void set_VOLUMES(String _VOLUMES) {
		this._VOLUMES = _VOLUMES;
	}
	public String get_PESOBRUTO() {
		return _PESOBRUTO;
	}
	public void set_PESOBRUTO(String _PESOBRUTO) {
		this._PESOBRUTO = _PESOBRUTO;
	}
	public String get_PLACA() {
		return _PLACA;
	}
	public void set_PLACA(String _PLACA) {
		this._PLACA = _PLACA;
	}
	public String get_NUMEROPLACA() {
		return _NUMEROPLACA;
	}
	public void set_NUMEROPLACA(String _NUMEROPLACA) {
		this._NUMEROPLACA = _NUMEROPLACA;
	}
	public String get_IDDT() {
		return _IDDT;
	}
	public void set_IDDT(String _IDDT) {
		this._IDDT = _IDDT;
	}
	public String get_REMETENTE() {
		return _REMETENTE;
	}
	public void set_REMETENTE(String _REMETENTE) {
		this._REMETENTE = _REMETENTE;
	}
	public String get_CIDADE() {
		return _CIDADE;
	}
	public void set_CIDADE(String _CIDADE) {
		this._CIDADE = _CIDADE;
	}
	
	public String get_NUMERO() {
		return _NUMERO;
	}
	public void set_NUMERO(String _NUMERO) {
		this._NUMERO = _NUMERO;
	}
	public String get_PENDENTE() {
		return _PENDENTE;
	}
	public void set_PENDENTE(String _PENDENTE) {
		this._PENDENTE = _PENDENTE;
	}
	public String get_TRANSMITIDO() {
		return _TRANSMITIDO;
	}
	public void set_TRANSMITIDO(String _TRANSMITIDO) {
		this._TRANSMITIDO = _TRANSMITIDO;
	}
	public String get_DESTINATARIO() {
		return _DESTINATARIO;
	}
	public void set_DESTINATARIO(String _DESTINATARIO) {
		this._DESTINATARIO = _DESTINATARIO;
	}
	public String get_ENDERECO() {
		return _ENDERECO;
	}
	public void set_ENDERECO(String _ENDERECO) {
		this._ENDERECO = _ENDERECO;
	}
	public String get_ESTADO() {
		return _ESTADO;
	}
	public void set_ESTADO(String _ESTADO) {
		this._ESTADO = _ESTADO;
	}     
	
	public void set_IDOCORRENCIA(String _IDOCORRENCIA) {
		this._IDOCORRENCIA = _IDOCORRENCIA;
	}
	
	public String get_IDOCORRENCIA() {
		return _IDOCORRENCIA;
	}
	public String get_EMPRESA() {
		return _EMPRESA;
	}
	public void set_EMPRESA(String _EMPRESA) {
		this._EMPRESA = _EMPRESA;
	}
	public String get_DATAHORAOCORRENCIA() {
		return _DATAHORAOCORRENCIA;
	}
	public void set_DATAHORAOCORRENCIA(String _DATAHORAOCORRENCIA) {
		this._DATAHORAOCORRENCIA = _DATAHORAOCORRENCIA;
	}
	public String get_LATTUDE() {
		return _LATTUDE;
	}
	public void set_LATTUDE(String _LATTUDE) {
		this._LATTUDE = _LATTUDE;
	}
	public String get_LONGITUDE() {
		return _LONGITUDE;
	}
	public void set_LONGITUDE(String _LONGITUDE) {
		this._LONGITUDE = _LONGITUDE;
	}
	public String get_DATAHORAPOSICAO() {
		return _DATAHORAPOSICAO;
	}
	public void set_DATAHORAPOSICAO(String _DATAHORAPOSICAO) {
		this._DATAHORAPOSICAO = _DATAHORAPOSICAO;
	}
	
}
