using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace apiDio.Data.Collections
{
    public class Paciente
    {
         public Paciente(string nome, DateTime dataNascimento, string sexo, double latitude, double longitude)
        {
            this.Nome = nome;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Localizacao = new GeoJson2DGeographicCoordinates(longitude, latitude);
        }
            
            public string Nome {get; set; }
            public DateTime DataNascimento { get; set; }
            public string Sexo { get; set; }
            public GeoJson2DGeographicCoordinates Localizacao { get; set; }
    }
}