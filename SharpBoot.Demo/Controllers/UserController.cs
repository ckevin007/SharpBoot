using Microsoft.AspNetCore.Mvc;
using SharpBoot.Common.Attributes;
using SharpBoot.Demo.Controller_Service.impl;
using SharpBoot.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Demo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        [Autowired] UserControllerService service;


    }
}
