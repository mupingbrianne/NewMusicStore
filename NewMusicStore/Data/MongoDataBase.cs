using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Driver;
using NewMusicStore.Models;

namespace NewMusicStore.Data
{
    public class MongoDBContext
    {
        //public IMongoDatabase mongoDatabase;
        public IMongoCollection<SportsMusic> mongoCollection;
        public MongoDBContext()
        {
            var server = new MongoClient("mongodb://newmusicstorederick:tfqIiFj2kdEmfVVdRGrjG6VqB1geH9eaOigceSTHgGOun0dk0p5sncaCpmFHCMENEnatxpJyyZVFa28NDvnjfQ==@newmusicstorederick.documents.azure.com:10255/?ssl=true&replicaSet=globaldb");
            mongoCollection = server.GetDatabase("SportsMusic-Derick").GetCollection<SportsMusic>("First Collection-Derick");
            
            
        }
        
        
    }
}
