using System;

namespace WebX.MODEL
{
    [Serializable]
    public class ArticlesMD
    {
        public ArticlesMD()
        {   
        }


        private int lineNO;
        private int type;
        private string title;
        private string content;
        private DateTime fillTime;
        private string fillOper;
        private string fillOperName;
        //private string sortID;
        /// <summary>
        /// 编号，自动增加
        /// </summary>
        public int LineNo
        {
            get { return lineNO; }
            set { lineNO = value; }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set {title=value; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime FillTime
        {
            get { return fillTime; }
            set { fillTime = value; }
        }
        /// <summary>
        /// 创建操作者编号
        /// </summary>
        public string FillOper
        {
            get { return fillOper; }
            set { fillOper = value; }
        }
        /// <summary>
        /// 操作者用户名
        /// </summary>
        public string FillOperName
        {
            get { return fillOperName; }
            set {fillOperName=value; }
        }
        /// <summary>
        /// 排序id
        /// </summary>
        public int SortId { get; set; }
       
    }
}
