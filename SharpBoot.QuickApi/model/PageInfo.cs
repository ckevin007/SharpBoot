using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace SharpBoot.QuickApi.model
{
    public class PageInfo
    {

        public PageInfo Result()
        {
            return new PageInfo
            {
                Page = this.Page,
                Count = this.Count,
                TotalPage = this.TotalPage,
                TotalCount = this.TotalCount,
                Data = this.Data
            };
        }

        [Range(1, int.MaxValue, ErrorMessage = "页码必须大于0")]
        public int Page { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "每页数量必须大于0")]
        public int Count { get; set; }

        public int TotalPage { get; set; }


        private long _totalCount;

        public long TotalCount
        {
            get { return _totalCount; }
            set
            {
                _totalCount = value;
                if (Count > 0) TotalPage = (int)Math.Ceiling((double)_totalCount / (double)Count);
            }
        }

        public object Data { get; set; }
    }
}
