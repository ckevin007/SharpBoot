using SharpBoot.QuickApi.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.QuickApi.exceptions
{
    public class MsgException : Exception
    {
        public ResultCode ResultCode { get; set; }
        public new object Data { get; set; }

        public MsgException(string msg, object data = null) : base(msg)
        {

            this.ResultCode = new ResultCode(ResultCode.Fail.Code, msg);
            this.Data = data;
        }

        public MsgException(ResultCode rc, string msg = "", object data = null)
        {
            this.ResultCode = rc;
            if (!string.IsNullOrEmpty(msg)) this.ResultCode.Msg = msg;
            this.Data = data;
        }
    }
}
