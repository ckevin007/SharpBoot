
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharpBoot.Entity
{
    [Table("user")]
    public class User
    {
        [FreeSql.DataAnnotations.Column(Name = "id", IsIdentity = true)]
        public int Id { get; set; }

        [Column("time")]
        public string Time { get; set; }

        [Column("name")]
        public string Name { get; set; }

    }
}
