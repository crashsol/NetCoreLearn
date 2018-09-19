using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoLearn.Model;
using System.Linq;
using System.Threading;
using MongoDB.Driver;

namespace MongoLearn.Data
{
    public class MongoContactBookRepository : IContactBookRepository
    {

        public ContactContext _contactContext;
        public MongoContactBookRepository(ContactContext contactContext)
        {
            _contactContext = contactContext;
        }
        public async Task<bool> AddContactUserAsync(int userId, ContactUser contactUser, CancellationToken cancellationToken)
        {
           if(_contactContext.ContactBooks.Count(b=>b.Id == userId,null,cancellationToken) == 0)
            {
               await CreateContactBookAsync(userId, cancellationToken);
            }
            var filter = Builders<ContactBook>.Filter.Eq(b => b.Id, userId);
            var update = Builders<ContactBook>.Update.AddToSet(b => b.Contacts, contactUser);

            var result =  await _contactContext.ContactBooks.UpdateOneAsync(filter, update, null, cancellationToken);
            return result.MatchedCount == result.ModifiedCount && result.ModifiedCount == 1;
        }

        public async Task<bool> CreateContactBookAsync(int userId, CancellationToken cancellationToken)
        {
            if (_contactContext.ContactBooks.Count(b => b.Id == userId, null, cancellationToken) == 0)
            {
                await _contactContext.ContactBooks.InsertOneAsync(new ContactBook
                {
                    Id = userId,
                }, null, cancellationToken);
            }
            return true;
        }

        public async Task<bool> TagContactUserAsync(int userId, int contactUserId, List<string> tags, CancellationToken cancellationToken)
        {
            if (_contactContext.ContactBooks.Count(b => b.Id == userId, null, cancellationToken) == 0)
            {
                await CreateContactBookAsync(userId, cancellationToken);
            }
            var filter = Builders<ContactBook>.Filter.And(
                         Builders<ContactBook>.Filter.Eq(b=>b.Id,userId),
                         Builders<ContactBook>.Filter.ElemMatch(b=>b.Contacts,contact => contact.Id ==contactUserId)
                        );
            var update = Builders<ContactBook>.Update
                        .Set("Contacts.$.Tags", tags);
            var result = await _contactContext.ContactBooks.UpdateOneAsync(filter, update, null, cancellationToken);
            return result.MatchedCount == result.ModifiedCount && result.ModifiedCount == 1;


        }

    }
}
