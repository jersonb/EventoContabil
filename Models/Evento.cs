using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventoContabil.Models
{
    public class Evento
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Conta { get; set; }
        public string Descricao { get; set; }
        [Column(TypeName = "decimal(20,2)")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public Decimal Valor { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Competencia { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Tipo { get; set; }

    }
}