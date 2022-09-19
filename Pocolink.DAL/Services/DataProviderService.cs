using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Pocolink.Models.Models;

namespace Pocolink.DAL.Services
{
    public class DataProviderService : IDataProviderService
    {
        private readonly IMongoDbClient _mongoDbClient;
        private readonly IKeyVaultService _keyVaultService;

        public DataProviderService(IMongoDbClient mongoDbClient, IKeyVaultService keyVaultService)
        {
            _mongoDbClient = mongoDbClient;
            _keyVaultService = keyVaultService;
        }
        public List<ShortenedUrl> ListDocuments(Expression<Func<ShortenedUrl, bool>> predicate)
        {
            var connectionString = _keyVaultService.RetrieveSecret("mongoDb");
            const string databaseName = "PocofyDb";
            const string collectionName = "Urls";

            var mongoClient = _mongoDbClient.ConnectToAtlasInstance(connectionString);
            var mongoDatabase = _mongoDbClient.ConnectToDb(mongoClient, databaseName);
            var mongoCollection = _mongoDbClient.GetCollection(mongoDatabase, collectionName);
            var mongodocuments = _mongoDbClient.ListCollectionDocuments(mongoCollection, predicate);
            return mongodocuments;
        }

        public async Task AddDocumentToCollection(ShortenedUrl document)
        {

            var connectionString = _keyVaultService.RetrieveSecret("mongoDb");
            const string databaseName = "PocofyDb";
            const string collectionName = "Urls";

            var mongoClient = _mongoDbClient.ConnectToAtlasInstance(connectionString);
            var mongoDatabase = _mongoDbClient.ConnectToDb(mongoClient, databaseName);
            var mongoCollection = _mongoDbClient.GetCollection(mongoDatabase, collectionName);
            await mongoCollection.InsertOneAsync(document);
        }
    }
}
