using System;
using apiDio.Data.Collections;
using apiDio.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace apiDio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PacienteController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Paciente> _pacientesCollection;

        public PacienteController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _pacientesCollection = _mongoDB.DB.GetCollection<Paciente>(typeof(Paciente).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarPaciente([FromBody] PacienteDto dto)
        {
            var paciente = new Paciente(dto.Nome, dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _pacientesCollection.InsertOne(paciente);
            
            return StatusCode(201, "Paciente com o vírus foi adicionado ao bd");
        }

        [HttpGet]
        public ActionResult ObterPacientes()
        {
            var pacientes = _pacientesCollection.Find(Builders<Paciente>.Filter.Empty).ToList();
            
            return Ok(pacientes);
        }

        [HttpPut]
        public ActionResult AtualizarPaciente([FromBody] Paciente dto)
        {
            _pacientesCollection.UpdateOne(Builders<Paciente>.Filter.Where(_ => _.DataNascimento == dto.DataNascimento), Builders<Paciente>.Update.Set("sexo", dto.Sexo));
                       
            return Ok("Atualizado com sucesso!");
        }

        [HttpDelete("{dataNasc}")]
          public ActionResult DeletarPaciente(string dataNasc)
        {
            _pacientesCollection.DeleteOne(Builders<Paciente>.Filter.Where(_ => _.DataNascimento == Convert.ToDateTime(dataNasc)));
                       
            return Ok("Deletado com sucesso!");
        }
        
    }
}