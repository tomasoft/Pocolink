using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using MongoDB.Driver;
using Pocolink.Models.Models;

namespace Pocolink.DAL.Client
{
    public class MongoDbClient : IMongoDbClient
    {
        public IKeyVaultService KeyVaultService { get; }

        public MongoDbClient(IKeyVaultService keyVaultService)
        {
            KeyVaultService = keyVaultService;
        }

        public MongoClient ConnectToAtlasInstance(string connectionString)
        {
            var settings = MongoClientSettings.FromConnectionString(KeyVaultService.RetrieveSecret("mongoDb"));
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            return client;
        }

        public IMongoDatabase ConnectToDb(MongoClient client, string dBName)
        {
            var database = client.GetDatabase(dBName);
            return database;
        }

        public IMongoCollection<ShortenedUrl> GetCollection(IMongoDatabase database, string collectionName)
        {
            var collection = database.GetCollection<ShortenedUrl>(collectionName);
            return collection;
        }

        public List<ShortenedUrl> ListCollectionDocuments(IMongoCollection<ShortenedUrl> collection, Expression<Func<ShortenedUrl, bool>> filter)
        {
            var documents = collection.Find(filter).ToList();
            return documents;
        }
    }
}
