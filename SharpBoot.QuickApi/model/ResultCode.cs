using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.QuickApi.model
{
    public class ResultCode
    {
        public string Msg { get; set; }

        public int Code { get; set; }
        public ResultCode(int code, string msg)
        {
            Msg = msg;
            Code = code;
        }

        public static ResultCode Fail = new ResultCode(-1, "操作失败");
        public static ResultCode ClosedApi = new ResultCode(400, "接口已关闭");

        public static ResultCode Ok = new ResultCode(0, "操作成功");

        public static ResultCode NotLogin = new ResultCode(401, "请登录");
        public static ResultCode PowerError = new ResultCode(403, "权限不足");
        public static ResultCode NotFound = new ResultCode(404, "资源不存在");

        public static ResultCode LoginError = new ResultCode(505, "账号密码错误");
        public static ResultCode UsrIsDel = new ResultCode(506, "账号状态异常");


    }
}
