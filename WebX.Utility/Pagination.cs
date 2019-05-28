using System;

namespace WebX.Utility
{
    public class Pagination
    {
        #region 构造函数
        public Pagination()
        {
            OrderType = "asc";
        }
        #endregion

        #region 私有成员
        /// <summary>
        /// 总记录数
        /// </summary>
        private int RecordCount { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        private int PageIndex { get; set; }

        /// <summary>
        /// 每页行数
        /// </summary>
        private int PageRows { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        private string OrderFiled { get; set; }

        private string OrderType { get; set; }
        #endregion

    }
}
