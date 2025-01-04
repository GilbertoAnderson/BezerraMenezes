using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;


namespace BezerraMenezesExpress.Controllers.Shared
{
    public static class Helper
    {

        #region Geral

        public static SelectList EstadosBrasil()
        {
            var selectItems = new Dictionary<string, string>
            {
                {"AC","Acre"},
                {"AL","Alagoas"},
                {"AP","Amapá"},
                {"AM","Amazonas"},
                {"BA","Bahia"},
                {"CE","Ceará"},
                {"DF","Distrito Federal"},
                {"ES","Espírito Santo"},
                {"GO","Goiás"},
                {"MA","Maranhão"},
                {"MT","Mato Grosso"},
                {"MS","Mato Grosso do Sul"},
                {"MG","Minas Gerais"},
                {"PA","Pará"},
                {"PB","Paraíba"},
                {"PR","Paraná"},
                {"PE","Pernambuco"},
                {"PI","Piauí"},
                {"RJ","Rio de Janeiro"},
                {"RN","Rio Grande do Norte"},
                {"RS","Rio Grande do Sul"},
                {"RO","Rondônia"},
                {"RR","Roraima"},
                {"SC","Santa Catarina"},
                {"SP","São Paulo"},
                {"SE","Sergipe"},
                {"TO","Tocantins"},
            };

            return new SelectList(selectItems, "Key", "Value");

        }

        public static SelectList TipoResidencia()
        {
            var selectItems = new Dictionary<string, string>
            {
                {"propria","propria"},
                {"alugada","alugada"},
                {"parentes","parentes"},
                {"outros","outros"},
            };

            return new SelectList(selectItems, "Key", "Value");
        }

        public static SelectList EstadoCivil()
        {
            var selectItems = new Dictionary<string, string>
            {
                {"Casado","Casado"},
                {"Solteiro","Solteiro"},
                {"Separado","Separado"},
                {"Divorciado","Divorciado"},
                {"Viuvo","Viuvo"},
            };

            return new SelectList(selectItems, "Key", "Value");
        }

        public static SelectList Sexo()
        {
            var selectItems = new Dictionary<string, string>
            {
                {"F","Feminino"},
                {"M","Masculino"},
            };

            return new SelectList(selectItems, "Key", "Value");

        }

        public static SelectList ListaCategoria()
        {
            var selectItems = new Dictionary<string, string>
            {
                {"Colaborador","Colaborador"},
                {"Efetivo","Efetivo"},
                {"Tarefeiro","Tarefeiro"},
            };

            return new SelectList(selectItems, "Key", "Value");

        }

        public static SelectList ListaAnos()
        {
            var selectItems = new Dictionary<string, string>
            {
                {"2015","2015"},
                {"2016","2016"},
                {"2017","2017"},
                {"2018","2018"},
                {"2019","2019"},
                {"2020","2020"},
                {"2021","2021"},
                {"2022","2022"},
                {"2023","2023"},
                {"2024","2024"},
                {"2025","2025"},
                {"2026","2026"},
                {"2027","2027"},
                {"2028","2028"},
                {"2029","2029"},
                {"2030","2030"},
                {"2031","2031"},
                {"2032","2032"},
                {"2033","2033"},
                {"2034","2034"},
                {"2035","2035"},
                {"2036","2036"},
                {"2037","2037"},
                {"2038","2038"},
                {"2039","2039"},
                {"2040","2040"},
            };

            return new SelectList(selectItems, "Key", "Value");
        }


        public static SelectList ListaNrParcela()
        {
            var selectItems = new Dictionary<string, string>
            {
                {"01","01"},
                {"02","02"},
                {"03","03"},
                {"04","04"},
                {"05","05"},
                {"06","06"},
                {"07","07"},
                {"08","08"},
                {"09","09"},
                {"10","10"},
                {"11","11"},
                {"12","12"},
                {"13","13"},
                {"14","14"},
            };

            return new SelectList(selectItems, "Key", "Value");
        }

        #endregion Geral
    }
}