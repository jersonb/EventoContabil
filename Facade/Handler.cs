
using System.Collections.Generic;
using System.Text;
using EventoContabil.Models;

namespace EventoContabil.Facade
{
    public class Handler
    {
        private List<EventoConvert> _eventoConvert = new List<EventoConvert>();

        public Handler(object json)
        {
            _eventoConvert = new JsonObjects().ToJson(json);
        }


        public List<Evento> Eventos()
        {
            List<Evento> eventos = new List<Evento>();



            _eventoConvert.ForEach(x =>
                {
                    if (x.Valor < 0)
                    {
                        x.Valor = x.Valor * -1;
                        x.Tipo = "DESPESA";
                    }
                    else
                    {
                        x.Tipo = "RECEITA";
                    }

                    eventos.Add(
                        new Evento
                        {
                            Competencia = x.Competencia,
                            Conta = x.Conta,
                            Tipo = x.Tipo,
                            Valor = x.Valor,
                            Descricao = Convert(x.Descricao)
                        });

                });



            return eventos;
        }

        private string Convert(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            return Encoding.ASCII.GetString(bytes).ToUpper();
        }
    }
}