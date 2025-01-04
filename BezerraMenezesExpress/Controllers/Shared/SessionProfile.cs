using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BezerraMenezesExpress.Controllers.Shared
{
    public class SessionProfile
    {

        private enum Key : short
        {
            Email,
            Admin,
            Livraria,
            Financeiro,
            Secretaria,
            PerfisAcesso,
            Logado

        }


        public static bool Admin
        {
            get
            {
                string xx = Key.Admin.ToString();
                if (HttpContext.Current.Session[Key.Admin.ToString()] == null)
                    return false;

                return (bool)HttpContext.Current.Session[Key.Admin.ToString()];
            }
            set
            {
                HttpContext.Current.Session[Key.Admin.ToString()] = value;
            }

        }

        public static bool Secretaria
        {
            get
            {
                string xx = Key.Secretaria.ToString();
                if (HttpContext.Current.Session[Key.Secretaria.ToString()] == null)
                    return false;

                return (bool)HttpContext.Current.Session[Key.Secretaria.ToString()];
            }
            set
            {
                HttpContext.Current.Session[Key.Secretaria.ToString()] = value;
            }

        }

        public static bool Livraria
        {
            get
            {
                if (HttpContext.Current.Session[Key.Livraria.ToString()] == null)
                    return false;

                return (bool)HttpContext.Current.Session[Key.Livraria.ToString()];
            }
            set
            {
                HttpContext.Current.Session[Key.Livraria.ToString()] = value;
            }

        }


        public static bool Financeiro
        {
            get
            {
                if (HttpContext.Current.Session[Key.Financeiro.ToString()] == null)
                    return false;

                return (bool)HttpContext.Current.Session[Key.Financeiro.ToString()];
            }
            set
            {
                HttpContext.Current.Session[Key.Financeiro.ToString()] = value;
            }

        }



        public static bool Logado
        {
            get
            {
                if (HttpContext.Current.Session[Key.Logado.ToString()] == null)
                    return false;

                return (bool)HttpContext.Current.Session[Key.Logado.ToString()];
            }
            set
            {
                HttpContext.Current.Session[Key.Logado.ToString()] = value;
            }

        }


        //public static bool Entrar()
        //{
        //    get{

        //        return (Entrar != 0);

        //    }
        //}

        //public static string Nome
        //{
        //    get
        //    {
        //        return 

        //    }

        //}


    }
}