using SharpBoot.Auth.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.QuickApi.model
{
    public class UserVo : IUserVo
    {
        public List<string> Permissions { get; set; }

        public int Id { get; set; }
    }
}
