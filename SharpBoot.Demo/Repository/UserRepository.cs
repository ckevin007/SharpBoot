using FreeSql;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Enums;
using SharpBoot.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SharpBoot.Repository
{
    [Component(ComponentLifeTime.Transient)]
    public class UserRepository : BaseRepository<User>
    {
        public UnitOfWorkManager Uowm { get; set; }
        public UserRepository(IFreeSql fsql, UnitOfWorkManager uowm) : base(fsql, null, null)
        {
            uowm?.Binding(this);
            this.Uowm = uowm;
            Console.WriteLine("UserRepository ctor");
        }

        public override User Insert(User entity)
        {
            entity.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return base.Insert(entity);
        }

        public override List<User> Insert(IEnumerable<User> entitys)
        {
            return base.Insert(entitys);
        }
    }
}
