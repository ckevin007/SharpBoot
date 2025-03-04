using SharpBoot.Auth.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.QuickApi.Demo.model
{
    public class UserVoBase : IUserVo
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Name { get; set; }
        public string Usr { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Permissions { get; set; }
        public UsrStatus UsrStatus { get; set; }
        public string Token { get; set; }

        public bool IsAdmin => this.Roles != null && this.Roles.Contains("admin");
    }

    public enum UsrStatus
    {
        AllowLogin = 0,

        NotAllowLogin = 1
    }
}
