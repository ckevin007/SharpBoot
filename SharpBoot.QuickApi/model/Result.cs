
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.QuickApi.model
{
    public class Result
    {
        public string Msg { get; set; }

        public int Code { get; set; }

        public object Data { get; set; }


        public Result() { }
        private Result(int code, string msg = "", object data = null)
        {
            this.Code = code;
            this.Msg = msg;
            this.Data = data;
        }

        public Result(ResultCode rc, string msg = "", object data = null)
        {
            if (string.IsNullOrEmpty(msg)) msg = rc.Msg;
            this.Code = rc.Code;
            this.Msg = msg;
            this.Data = data;
        }

        public static Result Ok(string msg = "")
        {
            return new Result(ResultCode.Ok, msg);
        }

        public static Result Fail(string msg = "操作失败")
        {
            return new Result(ResultCode.Fail, msg);
        }

        public static Result ClosedApi()
        {
            return new Result(ResultCode.ClosedApi);
        }

        public static Result Object(object data, bool autoNull = false)
        {
            if (data == null && autoNull)
            {
                return Result.NotFound();
            }
            return new Result(ResultCode.Ok, "", data);
        }

        public static Result List<T>(List<T> list, bool autoNull = false)
        {
            if ((list == null || list.Count == 0) && autoNull)
            {
                return Result.NotFound();
            }
            return new Result(ResultCode.Ok, "", list);
        }

        public static Result NotLogin()
        {
            return new Result(ResultCode.NotLogin);
        }

        public static Result PowerError()
        {
            return new Result(ResultCode.PowerError);
        }


        public static Result NotFound()
        {
            return new Result(ResultCode.NotFound);
        }


        public T Parse<T>()
        {
            if (Data == null) return default;
            try
            {
                if (Data is T)
                {
                    return (T)Data;
                }
                var json = JsonConvert.SerializeObject(Data);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
