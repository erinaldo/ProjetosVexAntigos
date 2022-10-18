using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;

namespace SistranBLL
{
    public class Deposito
    {
        public DropDownList CarregarCboDeposiito(int idFilial, DropDownList cbo)
        {
            cbo.Items.Clear();
            cbo.DataSource = new SistranDAO.Deposito().CarregarCboDeposiito(idFilial);
            cbo.DataTextField = "DEPOSITO";
            cbo.DataValueField = "IDDEPOSITO";
            cbo.DataBind();
            cbo.Enabled = true;
            cbo.Items.Insert(0, new ListItem("Selecione", "0"));
            return cbo;
        }

        public DropDownList CarregarCboPlanta(int idDeposito, DropDownList cbo)
        {
            cbo.Items.Clear();
            cbo.DataSource = new SistranDAO.Deposito().CarregarCboPlanta(idDeposito);
            cbo.DataTextField = "PLANTA";
            cbo.DataValueField = "IDDEPOSITOPLANTA";
            cbo.DataBind();
            cbo.Enabled = true;
            cbo.Items.Insert(0, new ListItem("Selecione", "0"));
            return cbo;
        }

        public DropDownList CarregarCboRua(int idDepositoPlanta, DropDownList cbo)
        {
            cbo.Items.Clear();
            cbo.DataSource = new SistranDAO.Deposito().CarregarCboRua(idDepositoPlanta);
            cbo.DataTextField = "RUA";
            cbo.DataValueField = "RUA";
            cbo.DataBind();
            cbo.Enabled = true;
            //cbo.Items.Insert(0, new ListItem("TODOS", "TODOS"));
            cbo.Items.Insert(0, new ListItem("Selecione", "0"));      
            return cbo;
        }

        public DropDownList CarregarCboIventario(DropDownList cbo, string IdFilial, string IDCLIENTE)
        {
            cbo.Items.Clear();
            cbo.DataSource = new SistranDAO.Deposito().CarregarCboIventario(IdFilial, IDCLIENTE);
            cbo.DataTextField = "INVENTARIO";
            cbo.DataValueField = "IDINVENTARIO";
            cbo.DataBind();
            cbo.Enabled = true;
            cbo.Items.Insert(0, new ListItem("Selecione", "0"));
            return cbo;
        }

        public DropDownList CarregarCboContagem(DropDownList cbo, string idfilial, string clientes, string idinventario)
        {
            cbo.Items.Clear();
            cbo.DataSource = new SistranDAO.Deposito().CarregarCboIventarioContagem(idfilial, clientes, idinventario);
            cbo.DataTextField = "DESCRICAO";
            cbo.DataValueField = "VALOR";
            cbo.DataBind();
            cbo.Enabled = true;
            cbo.Items.Insert(0, new ListItem("Selecione", "0"));
            return cbo;
        }

        public DataTable CarregarIventario(int IdInventarioContagem, bool ConsiderarRuas, string RuaIni, string RuaFinal, string colunas, string andades, string idInventario)
        {
            return new SistranDAO.Deposito().CarregarIventario(IdInventarioContagem, ConsiderarRuas, RuaIni, RuaFinal, colunas, andades, idInventario);
        }

        public DataTable CarregarIventario(int IdInventarioContagem, bool ConsiderarRuas, string RuaIni, string RuaFinal, string idinventario)
        {
            return new SistranDAO.Deposito().CarregarIventario(IdInventarioContagem, ConsiderarRuas, RuaIni, RuaFinal, idinventario);
        }

        public DataTable RakingUsers(int IdInventarioContagem)
        {
            return new SistranDAO.Deposito().RakingUsers(IdInventarioContagem);
        }

        public DataTable RetornarMaxMinRuas(string IDINVENTARIO)
        {
            return new SistranDAO.Deposito().RetornarMaxMinRuas(IDINVENTARIO);
        }

        public DataTable NaoContados(string IDDEPOSITOPLANTA, string end)
        {
            return new SistranDAO.Deposito().NaoContados(IDDEPOSITOPLANTA, end);

        }

        public DataTable Layout(string IdDepositoPlanta, string Rua)
        {
            return new SistranDAO.Deposito().Layout(IdDepositoPlanta, Rua);
        }

        public DataTable RetornarProdutosEndereco(string endereco)
        {
            return new SistranDAO.Deposito().RetornarProdutosEndereco(endereco);
        }
    }
}