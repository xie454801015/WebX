using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace WebX.COMMON
{
    /// <summary>
    /// Dataset对象操作
    /// </summary>

    public  class DataSetHelper
    {   
        /// <summary>
        /// DataSet 转换为实体列表
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <param name="tableIndex">待转数据表索引</param>
        /// <returns></returns>
        public static IList<T> DataSetToEntityList<T>(DataSet dataSet,int tableIndex) where T : new()
        {
            //dataSet 为空或者dataset的表的数量小于0；当不存在表table，或者表第一个表的行数为0是，输出空的list<T>;
            if (dataSet == null || dataSet.Tables.Count < 0)
            {
                return new List<T>();
            }
            if (tableIndex > dataSet.Tables.Count - 1)
            {
                return new List<T>();
            }
            if (tableIndex< 0)  { tableIndex = 0; }
            if (dataSet.Tables[tableIndex].Rows.Count <= 0)
            {
                return new List<T>();
            }

            //定义集合
            List<T> list = new List<T>();
            Type type = typeof(T);
            string tempName = string.Empty;
            DataTable dt = dataSet.Tables[tableIndex];
            //遍历datatable中的所有行
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                //获取属性信息
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    // 检查Datatable 是否包含此列（列名==对象的属性名）
                    if (dt.Columns.Contains(tempName))
                    {
                        if (!pi.CanWrite) continue;//如果该属性不可写，跳过
                        object value = dr[tempName];
                        //如果取值非空
                        if(value != DBNull.Value)
                        {   
                            ///属性的GET方法返回的参数的类型，强制转换是数据类型，int32强制转int32，十进制Decimal强制转10进制Decimal
                            if (pi.GetMethod.ReturnParameter.ParameterType.Name == "Int32")
                            {
                                value = Convert.ToInt32(value);
                            }
                            else if (pi.GetMethod.ReturnParameter.ParameterType.Name == "Decimal")
                            {
                                value = Convert.ToDecimal(value);
                            }
                            pi.SetValue(t, value, null);
                        }

                    }
                }
                list.Add(t);
            }
            return list;
        }
    }

}
