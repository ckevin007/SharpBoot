using FreeSql;
using SharpBoot.Common.Attributes;
using SharpBoot.Common.Enums;
using SharpBoot.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBoot.Demo.Config
{
    [Component]
    public class FreesqlConfig
    {
        [Value("ConnectionStrings:MysqlConnection")]
        string mysqlConn;

        [Bean]
        public IFreeSql FreeSql()
        {
            IFreeSql sql = new FreeSqlBuilder()
                .UseConnectionString(DataType.MySql, mysqlConn)
                .UseAutoSyncStructure(true)
                .Build();

            return sql;
        }


    }
}
