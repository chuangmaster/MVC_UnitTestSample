using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_UnitTestSample.Models
{
    public class NameRepository
    {
        MyData data;

        public NameRepository()
        {
            data = new MyData()
            {
                User = new List<string>() { "Tom","Steven","John"}
            };
        }

        //public MemberRepository(string connstr)
        //{
        //    db = new MyDataContext(connstr);
        //}

        public string GetNameByData(string Name)
        {
            var q = data.User.FirstOrDefault(x=>x==Name);

            return q;
        }
    }

    public class MyData
    {
        public List<string> User { get; set; }
    }
}