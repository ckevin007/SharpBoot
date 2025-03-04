
using log4net;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Enums;
using SharpBoot.Common.Interfaces;
using SharpBoot.Demo.Services.impl;
using SharpBoot.Entity;
using SharpBoot.Repository;
using SharpBoot.Starter.Freesql.Attributes;
using SharpBoot.Starter.Freesql.Inteceptors;
using SharpBoot.Starter.Log4net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IFreesqlDemo.Services
{
    [Component(ComponentLifeTime.Scoped)]
    [Interceptor(typeof(TransactionalInterceptor))]
    public class UserService
    {

        [Autowired]
        UserRepository repo;


        [Transactional]
        public virtual async Task<User> TranscationalTest()
        {
            User user = new User()
            {
                Name = "zed"
            };
            user = await repo.InsertAsync(user);
            Console.WriteLine("start ");
            await Task.Delay(3000);
            Console.WriteLine("end ");
            throw new Exception("cuowu la");
            return user;
        }
    }
}
