using System;
using apiDio.Data.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace apiDio.Data
{
    public class MongoDB
    {
        public IMongoDatabase DB { get; }

        public MongoDB(IConfiguration configuration)
        {
            try
            {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
                var client = new MongoClient(settings);
                DB = client.GetDatabase(configuration["NomeBanco"]);
                MapClasses();
            }
            catch (Exception ex)
            {
                throw new MongoException("Não foi possível se conectar ao MongoDB", ex);
            }
        }

        private void MapClasses()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            if (!BsonClassMap.IsClassMapRegistered(typeof(Paciente)))
            {
                BsonClassMap.RegisterClassMap<Paciente>(i =>
                {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}