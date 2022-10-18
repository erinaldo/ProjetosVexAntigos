using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.DAL
{
    public class Email
    {
        public string email { get; set; }
        public string smtp { get; set; }
        public int porta { get; set; }
        public string senha { get; set; }
        public string titulo { get; set; }
        public string apelido { get; set; }
        public string corpo { get; set; }
        public string[] destinatarios_email { get; set; }
    }
}