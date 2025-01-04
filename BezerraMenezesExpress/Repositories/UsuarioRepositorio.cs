using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BezerraMenezesExpress.Models;

namespace BezerraMenezesExpress.Repositories
{
    public class UsuarioRepositorio
    {

        public static bool AutenticarUsuario(string Login, string Senha)
        {
         
            
          db_BezerraMenezesEntities db = new db_BezerraMenezesEntities();

            var Query = ( from u in db.tblUsuario
                              where u.Email == Login &&
                              u.Senha == Senha
                              select u).SingleOrDefault();

            if (Query == null)
                return false;

            // ira setar um cookie encriptado com Login do usuario autenticado
            return true;
        
        }
    }
}