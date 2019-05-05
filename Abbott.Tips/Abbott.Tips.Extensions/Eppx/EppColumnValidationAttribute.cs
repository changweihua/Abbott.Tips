using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Extensions.Eppx
{
    /// <summary>
    /// Epplus 列导入时单元格校验模型
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class EppColumnValidationAttribute : Attribute
    {
        public EppColumnValidationAttribute(bool isRequired = false, EppColumnDataType dataType = EppColumnDataType.STRING, bool isUnique = false)
        {
            IsRequired = isRequired;
            DataType = dataType;
            IsUnique = isUnique;
        }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 是否唯一
        /// </summary>
        public bool IsUnique { get; set; }

        /// <summary>
        /// 单元格列对应数据类型
        /// </summary>
        public EppColumnDataType DataType { get; set; }

        /// <summary>
        /// 单元格校验正则表达式
        /// </summary>
        public string DataValidationRegex { get; set; }

        /// <summary>
        /// 单元格校验委托名称，后期用于动态校验
        /// 暂时用不上
        /// </summary>
        public string DataValidationFunctionName { get; set; }

        public bool IsInValidSource(string source, object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return !IsRequired;
            }
            var sources = source.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return sources.Any(s => s.Trim() == value.ToString());
        }

        /// <summary>
        /// 检查数据是否能够成功转换至目标类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsValidDataType(object value)
        {
            //值为空，表示可以转换
            var castable = value == null || string.IsNullOrEmpty(value.ToString());

            if (!castable)
            {
                switch (DataType)
                {
                    case EppColumnDataType.NUMBER:
                        castable = Double.TryParse(value.ToString(), out double a) && a >= 0;
                        break;
                    case EppColumnDataType.INTEGER:
                        castable = Int32.TryParse(value.ToString(), out int b);
                        break;
                    case EppColumnDataType.DATE:
                        castable = DateTime.TryParse(value.ToString(), out DateTime c);
                        break;
                    case EppColumnDataType.DATETIME:
                        castable = DateTime.TryParse(value.ToString(), out DateTime d);
                        break;
                    case EppColumnDataType.STRING:
                    default:
                        castable = true;
                        break;
                }
            }
            return castable;
        }

    }
}
