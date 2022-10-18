using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SistecnoColetor.Classes;

namespace SistecnoColetor
{
    public partial class CLW00019 : Form
    {
        public CLW00019()
        {
            InitializeComponent();
        }

        private void CLW00019_Load(object sender, EventArgs e)
        {
            this.Text = this.Name;
            statusBar1.Text = "FILIAL: " + VarGlobal.Usuario.NomeFilial;
            lblTitulo.Text = VarGlobal.NomePrograma;
            PesquisarRomaneiosPendentes();
        }

        private void PesquisarRomaneiosPendentes()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string sql = "SELECT C.IDROMANEIO, C.SITUACAO, ISNULL(CAST(R.OBSERVACAO1 AS VARCHAR(MAX)), '') + ISNULL(CAST(R.OBSERVACAO2 AS VARCHAR(MAX)), '') OBS  FROM CONFERENCIA C INNER JOIN ROMANEIO R ON R.IDROMANEIO = C.IDROMANEIO WHERE C.SITUACAO IN('CONFERIDA', 'EMBARCADA')";
                dataGrid1.DataSource = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                if (((DataTable)dataGrid1.DataSource).Rows.Count > 0)
                {
                    idRomaneio = dataGrid1[0, 0].ToString();
                    dataGrid1.Select(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema ao Carregar Romaneios Pendentes. " + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void dataGrid1_Click(object sender, EventArgs e)
        {
            SetarVariaveisGrid();
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            SetarVariaveisGrid();
        }

        string idRomaneio = "";
        int? linhaClicada = null;
        private void SetarVariaveisGrid()
        {
            linhaClicada = dataGrid1.CurrentCell.RowNumber;
            idRomaneio = dataGrid1[(int)linhaClicada, 0].ToString();
            dataGrid1.Select((int)linhaClicada);
            lblInfoRomaneio.Text ="ROMANEIO:"+dataGrid1[(int)linhaClicada, 0].ToString() + "- OBS: " + dataGrid1[(int)linhaClicada, 2].ToString();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                DialogResult ret = MessageBox.Show("Tem certeza que deseja fechar o carregamento do Romaneio: " + idRomaneio + "?", "Sistecno Coletor", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (ret == DialogResult.Yes)
                {
                    #region Verifica se as todas as notas estao relacionadas com os pedidos

                    string sqlAux = " SELECT DPED.IDDOCUMENTO IDDOCUMENTO_PEDIDO, ISNULL(DNF.IDDOCUMENTO, 0) IDDOCUMENTO_NF  ";
                    sqlAux += " FROM ROMANEIODOCUMENTO RD  ";
                    sqlAux += " LEFT JOIN DOCUMENTO DPED ON  DPED.IDDOCUMENTO = RD.IDDOCUMENTO ";
                    sqlAux += " LEFT JOIN DOCUMENTORELACIONADO DR ON DR.IDDOCUMENTOPAI = DPED.IDDOCUMENTO ";
                    sqlAux += " LEFT JOIN DOCUMENTO DNF ON DNF.IDDOCUMENTO = DR.IDDOCUMENTOFILHO ";

                    sqlAux += " WHERE DPED.TIPODEDOCUMENTO = 'PEDIDO' ";
                    sqlAux += " AND RD.IDROMANEIO = " + idRomaneio;
                    sqlAux += " AND DPED.ATIVO = 'SIM' ";
                    sqlAux += " AND  ISNULL(DNF.IDDOCUMENTO, 0) =0 ";



                    if (int.Parse(Classes.BdExterno.RetornarDT(sqlAux, VarGlobal.Conexao).Rows[0][0].ToString()) > 0)
                    {
                        MessageBox.Show("As Notas Fiscais não estao relacionadas aos pedidos.", "Sistecno Coletor");
                        return;
                    }
                    #endregion



                    //verifica se todas as uas foram lidas
                    string sql = " SELECT COUNT(*) ";
                    sql += " FROM CONFERENCIA C   INNER JOIN CONFERENCIAPALLET CP ON CP.IDCONFERENCIA = C.IDCONFERENCIA   ";
                    sql += " INNER JOIN ROMANEIO R ON R.IDROMANEIO = C.IDROMANEIO   INNER JOIN UNIDADEDEARMAZENAGEM UA ON UA.IDUNIDADEDEARMAZENAGEM = CP.IDPALLET   ";
                    sql += " WHERE   R.IDROMANEIO = " + idRomaneio;
                    sql += " AND C.SITUACAO IN('CONFERIDA', 'EMBARCADA')";
                    sql += " AND (EMBARCADO IS NULL  OR EMBARCADO='NAO')";


                    if (int.Parse(Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao).Rows[0][0].ToString()) > 0)
                    {
                        MessageBox.Show("Esta carga possui pallets ainda não conferidos. Favor verificar", "Sistecno Coletor");
                        return;
                    }
                    else
                    {

                        sql = "UPDATE CONFERENCIA SET SITUACAO='EMBARCADA' WHERE IDROMANEIO=" + idRomaneio;
                        Classes.BdExterno.Executar(sql, VarGlobal.Conexao);


                        //pega o peso e o valor das notas


                        sql = " SELECT SUM(DNF.VALORDANOTA) VALORDANOTA , SUM(DNF.PESOBRUTO  ) PESOBRUTO, sum( DNF.Volumes) Volumes";
                        sql += " FROM ROMANEIODOCUMENTO RD  ";
                        sql += " LEFT JOIN DOCUMENTO DPED ON  DPED.IDDOCUMENTO = RD.IDDOCUMENTO ";
                        sql += " LEFT JOIN DOCUMENTORELACIONADO DR ON DR.IDDOCUMENTOPAI = DPED.IDDOCUMENTO ";
                        sql += " LEFT JOIN DOCUMENTO DNF ON DNF.IDDOCUMENTO = DR.IDDOCUMENTOFILHO ";
                        sql += " WHERE DPED.TIPODEDOCUMENTO = 'PEDIDO' ";
                        sql += " AND RD.IDROMANEIO = " + idRomaneio;
                        sql += " AND DPED.ATIVO = 'SIM' ";

                        DataTable dtAux = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);


                        string idRomTransporte = BdExterno.RetornarIDTabela("ROMANEIO").ToString();
                        sql = "INSERT  INTO ROMANEIO ";
                        sql += " (IDROMANEIO, IDFILIAL,IDUSUARIO, IDDEPOSITOPLANTALOCALIZACAO, EMISSAO, TIPO, DIVISAO, CONFERENCIA, OBSERVACAO1, SITUACAO, VALORDANOTA, VOLUMES, PESOBRUTO, ENTREGA, SEPARADOPOR) ";
                        sql += " VALUES ";
                        sql += " (" + idRomTransporte + ", " + VarGlobal.Usuario.UltimaFilial + "," + VarGlobal.Usuario.IDUsuario + ", 5, GETDATE(), 'ENTREGA', 'TRANSPORTE', 'NOTA FISCAL', '" + lblInfoRomaneio.Text + "', 'ATIVO', " + dtAux.Rows[0]["ValorDaNota"].ToString().Replace(",", ".") + ", " + int.Parse(dtAux.Rows[0]["VOLUMES"].ToString()) + ", " + dtAux.Rows[0]["PesoBruto"].ToString().Replace(",", ".") + ", 1, 'DOCUMENTO') ; ";

                        string iddt = BdExterno.RetornarIDTabela("DT").ToString();
                        sql += " INSERT INTO DT  (IDDT, IDFILIAL, NUMERO, IDDTTIPO, ATIVO, SITUACAO, ANDAMENTO) ";
                        sql += " VALUES (" + iddt + ", " + VarGlobal.Usuario.UltimaFilial + ", (SELECT PROXIMONUMERO FROM NUMERADOR WHERE IDFILAIL=" + VarGlobal.Usuario.UltimaFilial + " AND NOME='DT'), 1, 'SIM', 'EM ABERTO', 'AGUARDANDO CARREGAMENTO') ; ";

                        sql += "UPDATE NUMERADOR SET  PROXIMONUMERO=PROXIMONUMERO+1  WHERE IDFILAIL=" + VarGlobal.Usuario.UltimaFilial + " AND NOME='DT'; ";

                        string idRomDoc = BdExterno.RetornarIDTabela("ROMANEIO").ToString();
                        sql += "INSERT INTO DTROMANEIO VALUES (" + idRomDoc + ", " + iddt + ", " + idRomTransporte + ")";

                        Classes.BdExterno.Executar(sql, VarGlobal.Conexao);
                        tabControl1.SelectedIndex = 0;


                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema ao Finalizar o Carregamento. " + ex.Message);               
            }
        }

        private void txtCodConferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return && txtCodConferencia.Text != "")
                {
                    Cursor.Current = Cursors.WaitCursor;
                    e.Handled = true;

                    string sql = "SELECT COUNT(*) FROM Conferencia WHERE IdConferencia = " + txtCodConferencia.Text + " AND IdRomaneio = " + idRomaneio;


                    if (Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao).Rows[0][0].ToString() == "0")
                    {
                        MessageBox.Show("O Romaneio: " + idRomaneio + " Não pertence a Conferencia: " + txtCodConferencia.Text, "SISTECNO COLETOR");
                        txtCodConferencia.SelectAll();
                        return;
                    }

                    txtUa.Enabled = true;
                    txtUa.Focus();

                }
            }
            catch (Exception)
            {

            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void txtUa_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblIdConferenciaPallet.Text = "";
            try
            {
                if (e.KeyChar == (char)Keys.Return && txtCodConferencia.Text != "" && txtUa.Text != "")
                {
                    Cursor.Current = Cursors.WaitCursor;
                    e.Handled = true;

                    string sql = "";
                    sql += " SELECT C.IDCONFERENCIA, C.IDROMANEIO, CP.IDPALLET, UA.DIGITO, CP.IDCONFERENCIAPALLET ";
                    sql += " FROM CONFERENCIA C  ";
                    sql += " INNER JOIN CONFERENCIAPALLET CP ON CP.IDCONFERENCIA = C.IDCONFERENCIA ";
                    sql += " INNER JOIN ROMANEIO R ON R.IDROMANEIO = C.IDROMANEIO  ";
                    sql += " INNER JOIN UNIDADEDEARMAZENAGEM UA ON UA.IDUNIDADEDEARMAZENAGEM = CP.IDPALLET ";
                    sql += " WHERE  ";
                    sql += " R.IDROMANEIO = "+idRomaneio;
                    sql += " AND C.SITUACAO IN('CONFERIDA', 'EMBARCADA') ";
                    sql += " AND C.IdConferencia = "+ txtCodConferencia.Text;
                    sql += " AND CAST(UA.IDUnidadeDeArmazenagem AS nvarchar(50)) + Digito = '"+txtUa.Text+"' ";

                    DataTable dt = Classes.BdExterno.RetornarDT(sql, VarGlobal.Conexao);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("A UA: " + Helpers.RetirarDigitoUA(txtUa.Text) + " Não pertence ao Romaneio: " + idRomaneio, "SISTECNO COLETOR");
                        txtUa.Text = "";
                        txtUa.Enabled = false;
                        txtCodConferencia.Text = "";
                        txtCodConferencia.Focus();
                        return;
                    }
                    else
                    {
                        lblIdConferenciaPallet.Text = dt.Rows[0]["IdCONFERENCIAPALLET"].ToString();
                    }


                    if(dataGrid1[(int)linhaClicada, 1].ToString() == "EMBARCADA")
                    {
                        MessageBox.Show("Carregamento Completo, Favor Concluir o Carregamento.");
                    }

                    btnConfirmar.Enabled = true;
                    btnConfirmar.Focus();

                }
            }
            catch (Exception)
            {

            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
                txtCodConferencia.Focus();
            else if (tabControl1.SelectedIndex == 0)
            {
                PesquisarRomaneiosPendentes();
                lblIdConferenciaPallet.Text = "";
            }

        }

        private void dataGrid1_DoubleClick(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            txtCodConferencia.Focus();
        }

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Classes.BdExterno.Executar("UPDATE CONFERENCIAPALLET SET EMBARCADO='SIM' WHERE IDCONFERENCIAPALLET = " + lblIdConferenciaPallet.Text, VarGlobal.Conexao);
                lblIdConferenciaPallet.Text = "";
                txtUa.Text = "";
                txtUa.Enabled = false;
                txtCodConferencia.Text = "";
                txtCodConferencia.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema ao Confirmar O Pallet. " + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}