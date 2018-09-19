using System;
using System.Collections.Generic;
using System.Text;

namespace MongoLearn.Model
{
    /// <summary>
    /// 通讯录
    /// </summary>
    public   class ContactBook
    {
        public ContactBook()
        {

            Contacts = new List<ContactUser>();
        }
        public int Id { get; set; }

       public   List<ContactUser> Contacts { get; set; }
    }
}
