using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EventoContabil.Models
{

    public class EventoConvert
    {

        public string Conta { get; set; }


        public string Descricao { get; set; }

        public Decimal Valor { get; set; }


        public DateTime Competencia { get; set; }

        public string Tipo { get; set; }


    }
}