using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;
using Pocolink.Models.Models;

namespace Pocolink.DAL
{
    public interface IMongoDbClient
    {
        public MongoClient ConnectToAtlasInstance(string connectionString);
        public IMongoDatabase ConnectToDb(MongoClient client, string dBName);
        public IMongoCollection<ShortenedUrl> GetCollection(IMongoDatabase database, string collectionName);
        public List<ShortenedUrl> ListCollectionDocuments(IMongoCollection<ShortenedUrl> collection, Expression<Func<ShortenedUrl, bool>> filter);
    }
}