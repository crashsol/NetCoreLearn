using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using System.Linq;
using MongoLearn.Model;

namespace MongoLearn.Data
{
    public class ContactContext
    {
        private IMongoDatabase _database;

        public ContactContext(string connectionString,string databaseName)
        {
            var client = new MongoClient(connectionString);
            if (client != null)
            {
                _database = client.GetDatabase(databaseName);
            }
        }

        /// <summary>
        /// 检查 表是否创建
        /// </summary>
        /// <param name="name"></param>
        private void CheckAndCreateCollection(string collectionName)
        {
            var collectionList = _database.ListCollections().ToList();
            var collectionNames = new List<string>();
            collectionList.ForEach(b => collectionNames.Add(b["name"].AsString));

            if(!collectionNames.Contains(collectionName))
            {
                //如果没有创建表，进行表的创建
                _database.CreateCollection(collectionName);
            }
        }

        /// <summary>
        /// 通讯录集合
        /// </summary>
        public IMongoCollection<ContactBook> ContactBooks
        {
            get
            {
                CheckAndCreateCollection("ContactBook");
                return _database.GetCollection<ContactBook>("ContactBook");
            }
        }

    }
}
