using AuthorizeLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizeLearn.Data
{
    public interface IDocumentRepository
    {

        Document Find(int documentId);
    }
}
