namespace SharpBoot.Common.Interfaces
{
    public interface ITransactionable
    {
        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 提交
        /// </summary>
        void Commit();

        /// <summary>
        /// 事物回滚
        /// </summary>
        void RollBack();
    }
}
