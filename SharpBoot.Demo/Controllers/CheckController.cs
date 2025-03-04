using IFreesqlDemo.Services;
using Microsoft.AspNetCore.Mvc;
using SharpBoot.Common.Attributes;
using SharpBoot.Demo.Controller_Service;
using SharpBoot.Demo.Models;
using SharpBoot.Demo.Services.impl;
using SharpBoot.Entity;
using SharpBoot.Repository;
using System.Threading.Tasks;

namespace SharpBoot.Demo.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CheckController : ControllerBase
    {
        [Autowired]
        public ICheckControllerService Service { get; set; }

        [Autowired]
        private UserService userService;

        [Autowired]
        UserRepository useRepo;

        [Autowired]
        public BeanDemo Bean { get; set; }

        [Autowired]
        private BeanDemo beanField;


        [Autowired]
        private UserInfo user1;

        [Autowired]
        private UserInfo user2;

        [HttpGet]
        public IActionResult user()
        {
            return Ok((user1 == user2) + "   " + user1.GetHashCode());
        }

        [HttpGet]
        public IActionResult Get()
        {
            var obj = Service.Get();
            beanField?.Say();
            return Ok(obj);
        }

        [HttpGet]
        public Task<User> Test()
        {
            return userService.TranscationalTest();

        }
    }
}
