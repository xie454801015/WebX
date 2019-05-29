using System;
using System.Diagnostics;

namespace WebX.Utility
{
    public class Pagination
    {
        #region 构造函数
        public Pagination()
        {
            stopWatch.Start();
            orderType = "asc";
            PageIndex = 1;
            PageRows = int.MaxValue;


        }
        #endregion

        #region 私有成员
        /// <summary>
        /// 总记录数
        /// </summary>
        private int recordCount { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        private int pageIndex { get; set; }

        /// <summary>
        /// 每页行数
        /// </summary>
        private int pageRows { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        private string orderField { get; set; }

        /// <summary>
        /// 排序类型
        /// </summary>
        private string orderType { get; set; }

        /// <summary>
        /// 计算总页数
        /// </summary>
        private int pageCount
        {
            get
            {
                int pages = recordCount / pageRows;
                int pageCount = recordCount % pageRows == 0 ? pages : pages + 1;
                return pages;
            }
        }

        /// <summary>
        /// 一个计时器
        /// </summary>
        private Stopwatch stopWatch { get; set; } = new Stopwatch();

        #endregion

        #region 默认方案
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get => pageIndex; set => pageIndex = value; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageRows { get => pageRows; set => pageRows = value; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderField { get => orderField; set => orderField = value; }
        /// <summary>
        /// 排序类型
        /// </summary>
        public string OrderType { get => orderType; set => orderType = value; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Records { get => recordCount; set => recordCount = value; }
        /// <summary>
        /// 页面总数读取
        /// </summary>
        public int PageCount { get=> pageCount; }
        #endregion

        #region DataTable方案
        /// <summary>
        /// 匹配每页行数
        /// </summary>
        public int length { get => pageRows; set => pageRows = value; }
        /// <summary>
        /// 匹配当前页
        /// </summary>
        public int start { get => pageIndex; set => pageIndex = value; }

        public int draw { get; set; }

        public object BuildTablerResult_DataTable(object dataList)
        {
            stopWatch.Stop();
            var resData = new
            {
                data = dataList,
                recordsTotal = recordCount,
                recordsFilterd = recordCount,
                draw = draw,
            };
            return resData;
        }
        #endregion


    }
}

