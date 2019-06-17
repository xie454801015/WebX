using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace WebX.Utility
{
    public class FilterObj
    {   
        
        public string Key { get; set; }
        public string Value { get; set; }
        public string Contract { get; set; }
    }

    //生成
    public static class DynamicLinq
    {

        /// <summary>
        /// 创建lambda表达式参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ParameterExpression CreateLambdaParam<T>(string name)
        {
            return Expression.Parameter(typeof(T), name);
        }

        /// <summary>
        /// 创建lambda表达式主体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <param name="filterObj"></param>
        /// <returns></returns>
        public static Expression GenerateBody<T>(this ParameterExpression param, FilterObj filterObj)
        {   
            //获取模型T中对应key的属性
            PropertyInfo property = typeof(T).GetProperty(filterObj.Key);
            
            Expression left = Expression.Property(param, property);
            Expression right = null;
            if (property.PropertyType == typeof(int))
            {
                right = Expression.Constant(int.Parse(filterObj.Value));
            }
            else if (property.PropertyType == typeof(DateTime))
            {
                right = Expression.Constant(DateTime.Parse(filterObj.Value));
            }
            else if (property.PropertyType == typeof(string))
            {
                right = Expression.Constant((filterObj.Value));
            }
            else if (property.PropertyType == typeof(decimal))
            {
                right = Expression.Constant(decimal.Parse(filterObj.Value));
            }
            else if (property.PropertyType == typeof(Guid))
            {
                right = Expression.Constant(Guid.Parse(filterObj.Value));
            }
            else if (property.PropertyType == typeof(bool))
            {
                right = Expression.Constant(filterObj.Value.Equals("1"));
            }
            else
            {
                throw new Exception("暂不能解析该Key的类型");
            }

            Expression filter = Expression.Equal(left, right);
            //根据字段拓展比较
            switch (filterObj.Contract)
            {
                case "<=":
                    filter = Expression.LessThanOrEqual(left, right);
                    break;

                case "<":
                    filter = Expression.LessThan(left, right);
                    break;

                case ">":
                    filter = Expression.GreaterThan(left, right);
                    break;

                case ">=":
                    filter = Expression.GreaterThanOrEqual(left, right);
                    break;

                case "like":
                    filter = Expression.Call(left, typeof(string).GetMethod("Contains", new[] { typeof(string) }),Expression.Constant(filterObj.Value));
                    break;
            }

            return filter;
        }

        /// <summary>
        /// 创建完整的lambda
        /// </summary>
        public static LambdaExpression GenerateLambda(this ParameterExpression param, Expression body)
        {
            return Expression.Lambda(body, param);
        }

        public static Expression<Func<T, bool>> GenerateTypeLambda<T>(this ParameterExpression param, Expression body)
        {
            return (Expression<Func<T, bool>>)(param.GenerateLambda(body));
        }
        public static Expression AndAlso(this Expression expression, Expression expressionRight)
        {
            return Expression.AndAlso(expression, expressionRight);
        }

        public static Expression Or(this Expression expression, Expression expressionRight)
        {
            return Expression.Or(expression, expressionRight);
        }

        public static Expression And(this Expression expression, Expression expressionRight)
        {
            return Expression.And(expression, expressionRight);
        }
    }

    public static class DynamicExtention
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, FilterObj[] filters)
        {
            var param = DynamicLinq.CreateLambdaParam<T>("c");
            Expression body = Expression.Constant(true); //初始默认一个true
            foreach (var filter in filters)
            {
                body = body.AndAlso(param.GenerateBody<T>(filter)); 
            }
            var lambda = param.GenerateTypeLambda<T>(body); //最终组成lambda
            return query.Where(lambda);
        }
    }
}
