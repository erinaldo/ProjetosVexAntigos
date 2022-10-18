using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using MigracaoOcorrenciasClassLibrary;
//using System.Timers;
using System.Threading;

namespace MigracaoOcorrencias.WindService
{
    public partial class MigracaoOcorrencias : ServiceBase
    {
        public static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        public static Timer timer = null;
      
        public MigracaoOcorrencias()
        {
            InitializeComponent();
        }

        public class State
        {
            public int count = 0;
        }

        static void CallBack(Object stateObject)
        {
            State state = (State)stateObject;
            string BaseOrigem = System.Configuration.ConfigurationSettings.AppSettings["BaseOrigem"];
            string BaseDestino = System.Configuration.ConfigurationSettings.AppSettings["BaseDestino"];
            new ClassLibrary.Iniciar().iniciarProcessoImportacao(BaseOrigem, BaseDestino);            
        }

        protected override void OnStart(string[] args)
        {
            ClassLibrary.Iniciar.EscreverLog("Servico Iniciado()");

            timer = new Timer(new TimerCallback(CallBack), new State(), 360000, 120000);
            
            autoResetEvent.WaitOne();
        }

        protected override void OnStop()
        {
            ClassLibrary.Iniciar.EscreverLog("Servico Finalizado()");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                ClassLibrary.Iniciar.EscreverLog("()");
                timer1.Enabled = false;
                ClassLibrary.Iniciar startServices = new ClassLibrary.Iniciar();

                string BaseOrigem = System.Configuration.ConfigurationSettings.AppSettings["BaseOrigem"];
            string BaseDestino = System.Configuration.ConfigurationSettings.AppSettings["BaseDestino"];
            startServices.iniciarProcessoImportacao(BaseOrigem, BaseDestino);
            }
            catch (Exception ex)
            {
                ClassLibrary.Iniciar.EscreverLog(ex.Message);
            }
            finally
            {
                timer1.Enabled = true;
            }
        }

        private void timer1_Tick()
        {

        }
    }
}
