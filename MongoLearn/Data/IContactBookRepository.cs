using MongoLearn.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MongoLearn.Data
{
   public  interface  IContactBookRepository
    {

        Task<bool> CreateContactBookAsync(int userId,CancellationToken cancellationToken);

        Task<bool> AddContactUserAsync(int userId, ContactUser contactUser,CancellationToken cancellationToken);

        Task<bool> TagContactUserAsync(int userId, int contactUserId, List<string> tags, CancellationToken cancellationToken);
    }
}
