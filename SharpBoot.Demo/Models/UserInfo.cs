using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Demo.Models
{

    public class UserInfo
    {
        public UserInfo()
        {

        }
        public UserInfo(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public UserInfo User { get; set; }

        public override string ToString()
        {
            return Id + ";" + Name;
        }

    }
}
