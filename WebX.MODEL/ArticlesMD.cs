using System;

namespace WebX.MODEL
{
    [Serializable]
    public class ArticlesMD
    {
        public ArticlesMD()
        { }

        /// <summary>
        /// 流水号
        /// </summary>
        public int LineNo { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime FillTime { get; set; }
        /// <summary>
        /// 创建操作者编号
        /// </summary>
        public string FillOper { get; set; }
        /// <summary>
        /// 操作者用户名
        /// </summary>
        public string FillOperName { get; set; }
        /// <summary>
        /// 排序id
        /// </summary>
        public int SortId { get; set; }
       
    }
}
