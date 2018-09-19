using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizeLearn.Models
{
    public class Document
    {

        public int ID { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public byte[] Content { get; set; }      

        /// <summary>
        /// 可以访问该资源的 部门名称
        /// </summary>
        public List<string> Departments { get; set; }


        /// <summary>
        /// 可以访问该资源的 所有用户列表
        /// </summary>
        public List<string> AllowUsers { get; set; }

    }
}
