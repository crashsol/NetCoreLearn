using System;
using System.Collections.Generic;
using System.Text;
using DB.Interface;

namespace DB.Sqlserver
{
    public class SqlserverHelper : IDBHelper
    {

        public SqlserverHelper()
        {
            Console.WriteLine("创建SqlServer Helper");
        }
        public void Query()
        {
            Console.WriteLine("SQLServerHelper 查询");
        }
    }
}
