using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Abbott.Tips.Framework.Util
{
    public sealed class ExpressionUtil
    {

        public static Expression<Func<T, bool>> True<T>() { return t => true; }
        public static Expression<Func<T, bool>> False<T>() { return t => false; }

        public static Expression<Func<T, string>> OrderByPredicate<T>(string propName)
        {
            //var props = typeof(T).GetProperties();

            return t => propName;// props.FirstOrDefault(prop => prop.Name == propName).Name;
        }

        public static Expression<Func<TElement, bool>> BuildWhereInExpression<TElement, TValue>(Expression<Func<TElement, TValue>> propertySelector, IEnumerable<TValue> values)
        {
            ParameterExpression p = propertySelector.Parameters.Single();
            if (!values.Any())
                return e => false;

            var equals = values.Select(value => (Expression)Expression.Equal(propertySelector.Body,
                Expression.Constant(value, typeof(TValue))));
            var body = equals.Aggregate<Expression>((accumulate, equal) => Expression.Or(accumulate, equal));

            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }

        public static Expression<Func<T1, bool>> In<T1, T2>(string propertyName, IEnumerable<T2> options) where T1 : class
        {
            ParameterExpression left = Expression.Parameter(typeof(T1), "c");
            Expression expression = Expression.Constant(false);
            foreach (var optionName in options)
            {
                Expression right = Expression.Call
                       (
                          Expression.Property(left, typeof(T1).GetProperty(propertyName)),  //c.DataSourceName
                          typeof(T2).GetMethod("Contains", new Type[] { typeof(T2) }),// 反射使用.Contains()方法                         
                         Expression.Constant(optionName)           // .Contains(optionName)
                       );
                expression = Expression.Or(right, expression);//c.DataSourceName.contain("") || c.DataSourceName.contain("") 
            }

            Expression<Func<T1, bool>> finalExpression
                = Expression.Lambda<Func<T1, bool>>(expression, new ParameterExpression[] { left });
            return finalExpression;
        }



        //public Expression<Func<T, bool>> ChangeFuncToExpress<T>(Func<T, bool> boolMethod)
        //{
        //    //x=> return boolMethod(x)就是一个Expression表达式
        //    return x => return boolMethod(x);
        //}


    }

    /// <summary>
    /// 自定义 Expression 类，用于表达式生成
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RcExpression<T> where T : class
    {
        private ParameterExpression parameterExp;
        private BinaryExpression binaryExp;

        public RcExpression(string instanceName = "ins")
        {
            parameterExp = Expression.Parameter(typeof(T), instanceName);

            //1 == 1，默认为 true
            Expression left = Expression.Constant(1);
            binaryExp = Expression.Equal(left, left);
        }

        public Expression<Func<T, bool>> ActualExpression
        {
            get
            {
                return Expression.Lambda<Func<T, bool>>(binaryExp, parameterExp);
            }
        }

        /// <summary>
        /// 值比较
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="value">待比较值</param>
        /// <param name="mode">比较方法</param>
        public void Compare(string propertyName, object value, ExpressionCompareMode mode = ExpressionCompareMode.EQUAL)
        {

            Expression left = Expression.Property(parameterExp, typeof(T).GetProperty(propertyName));
            Expression right = Expression.Convert(Expression.Constant(value, value.GetType()), left.Type);

            Expression result = null;

            switch (mode)
            {
                case ExpressionCompareMode.LESS_THAN:
                    result = Expression.LessThan(left, right);
                    break;
                case ExpressionCompareMode.LESS_THAN_OR_EQUAL:
                    result = Expression.LessThanOrEqual(left, right);
                    break;
                case ExpressionCompareMode.GREATER_THAN:
                    result = Expression.GreaterThan(left, right);
                    break;
                case ExpressionCompareMode.GREATER_OR_EQUAL:
                    result = Expression.GreaterThanOrEqual(left, right);
                    break;
                case ExpressionCompareMode.NOT_EQUAL:
                    result = Expression.NotEqual(left, right);
                    break;
                case ExpressionCompareMode.EQUAL:
                default:
                    result = Expression.Equal(left, right);
                    break;
            }

            if (result != null)
            {
                binaryExp = Expression.And(binaryExp, result);
            }

        }

        /// <summary>
        /// 值比较
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="value">待比较值</param>
        /// <param name="mode">比较方法</param>
        public void CompareOr(string propertyName, object value, ExpressionCompareMode mode = ExpressionCompareMode.EQUAL)
        {

            Expression left = Expression.Property(parameterExp, typeof(T).GetProperty(propertyName));
            Expression right = Expression.Convert(Expression.Constant(value, value.GetType()), left.Type);

            Expression result = null;

            switch (mode)
            {
                case ExpressionCompareMode.LESS_THAN:
                    result = Expression.LessThan(left, right);
                    break;
                case ExpressionCompareMode.LESS_THAN_OR_EQUAL:
                    result = Expression.LessThanOrEqual(left, right);
                    break;
                case ExpressionCompareMode.GREATER_THAN:
                    result = Expression.GreaterThan(left, right);
                    break;
                case ExpressionCompareMode.GREATER_OR_EQUAL:
                    result = Expression.GreaterThanOrEqual(left, right);
                    break;
                case ExpressionCompareMode.NOT_EQUAL:
                    result = Expression.NotEqual(left, right);
                    break;
                case ExpressionCompareMode.EQUAL:
                default:
                    result = Expression.Equal(left, right);
                    break;
            }

            if (result != null)
            {
                binaryExp = Expression.Or(binaryExp, result);
            }

        }

        /// <summary>
        /// 值比较
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="value">待比较值</param>
        /// <param name="mode">比较方法</param>
        public void CompareNot(string propertyName, object value, ExpressionCompareMode mode = ExpressionCompareMode.EQUAL)
        {

            Expression left = Expression.Property(parameterExp, typeof(T).GetProperty(propertyName));
            Expression right = Expression.Convert(Expression.Constant(value, value.GetType()), left.Type);

            Expression result = null;

            switch (mode)
            {
                case ExpressionCompareMode.LESS_THAN:
                    result = Expression.LessThan(left, right);
                    break;
                case ExpressionCompareMode.LESS_THAN_OR_EQUAL:
                    result = Expression.LessThanOrEqual(left, right);
                    break;
                case ExpressionCompareMode.GREATER_THAN:
                    result = Expression.GreaterThan(left, right);
                    break;
                case ExpressionCompareMode.GREATER_OR_EQUAL:
                    result = Expression.GreaterThanOrEqual(left, right);
                    break;
                case ExpressionCompareMode.NOT_EQUAL:
                    result = Expression.NotEqual(left, right);
                    break;
                case ExpressionCompareMode.EQUAL:
                default:
                    result = Expression.Equal(left, right);
                    break;
            }

            if (result != null)
            {
                binaryExp = Expression.Or(binaryExp, result);
            }

        }

        /// <summary>
        /// 包含
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public void Contains(string propertyName, string value)
        {

            Expression left = Expression.Property(parameterExp, typeof(T).GetProperty(propertyName));
            Expression right = Expression.Constant(value, value.GetType());
            Expression result = Expression.Call(left, typeof(string).GetMethod("Contains"), right);

            binaryExp = Expression.And(binaryExp, result);
        }


    }

    public enum ExpressionCompareMode
    {
        EQUAL,
        NOT_EQUAL,
        LESS_THAN,
        LESS_THAN_OR_EQUAL,
        GREATER_THAN,
        GREATER_OR_EQUAL,
    }
}
