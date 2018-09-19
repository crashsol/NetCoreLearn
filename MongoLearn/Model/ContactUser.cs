using System;
using System.Collections.Generic;
using System.Text;

namespace MongoLearn.Model
{
    /// <summary>
    /// 联系人信息
    /// </summary>
    public class ContactUser
    {

        public ContactUser()
        {

            Tags = new List<string>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Company { get; set; }

        public string Title { get; set; }

        public List<string> Tags { get; set; }
    }
}
