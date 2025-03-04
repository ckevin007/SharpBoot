using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Auth.model
{
    public interface IUserVo
    {
        int Id { get; set; }
        List<string> Permissions { get; set; }
    }
}
