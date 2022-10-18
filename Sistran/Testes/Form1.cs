using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Testes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            br.com.vexlogistica.www2.OrderServices o = new br.com.vexlogistica.www2.OrderServices();

            o.integrateOrder(new br.com.vexlogistica.www2.integrateOrderRequest());
        }
    }
}
