using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SistecnoColetor.Classes.BLL
{
    public class Usuario
    {
        public Classes.DTO.Usuario Logar(Classes.DTO.Usuario usuario, string cnx)
        {
            return new SistecnoColetor.Classes.DAL.Usuario().Logar(usuario, cnx);
        }
    }
}
