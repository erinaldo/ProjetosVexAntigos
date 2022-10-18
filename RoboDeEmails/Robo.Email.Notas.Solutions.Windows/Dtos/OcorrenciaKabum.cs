using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robo.Email.Notas.Solutions.Windows.Testes.Dtos
{
   public  class OcorrenciaKabum
    {
        public class Root
        {
            public string chaveNFe { get; set; }
            public int numeroNFe { get; set; }
            public string serieNFe { get; set; }
            public int numeroPreNota { get; set; }
            public string seriePreNota { get; set; }
            public string cnpjEmissor { get; set; }
            public string cnpjTransportadora { get; set; }
            public string ocorrenciaTransportadora { get; set; }
            public string ocorrenciaEmpresa { get; set; }
            public string mensagem { get; set; }
            public string data { get; set; }
            public string prazo { get; set; }
            public string rastreio { get; set; }
            public int latitude { get; set; }
            public int longitude { get; set; }
        }
    }
}
