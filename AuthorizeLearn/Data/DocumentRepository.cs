using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AuthorizeLearn.Models;

namespace AuthorizeLearn.Data
{
    public class DocumentRepository : IDocumentRepository
    {


        private List<Document> GetDocuments()
        {
            return new List<Document> {
                new Document
                {
                    ID = 1,
                    Author = "Document",
                    Content = null,
                    Title = "1.doc",
                    Departments = new List<string>{ "办公室","企划部" },
                    AllowUsers = new List<string>{"admin","crashsol"}
                },
                new Document
                {
                    ID = 2,
                    Author = "Document",
                    Content = null,
                    Title = "2.xls",
                    Departments = new List<string>{ "办公室","企划部" },
                    AllowUsers = new List<string>{"admin","渣渣辉"}
                },
                new Document
                {
                    ID = 3,
                    Author = "Document",
                    Content = null,
                    Title = "3.jpg",
                    Departments = new List<string>{ "办公室","企划部","信息中心" },
                    AllowUsers = new List<string>{"admin","古天乐"}
                },
                new Document
                {
                    ID = 4,
                    Author = "Document",
                    Content = null,
                    Title = "4.pdf",
                    Departments = new List<string>{ "办公室","企划部","信息中心" },
                    AllowUsers = new List<string>{"admin"}
                }

            };
        }



        public Document Find(int  documentId)        {

            return GetDocuments().SingleOrDefault(b => b.ID == documentId);

        }

    }
}
