using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Extensions.Eppx
{
    public static class EppExcelRangeExtensions
    {
        public static string EppNullEmpty(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return obj.ToString();
        }

        public static bool OfType(this ExcelRange er, Type type)
        {
            var castable = true;
            if (er != null || er.Value == null || string.IsNullOrEmpty(er.Value.EppNullEmpty()))
            {
                string typeName = type.ToString().ToLower();

                switch (typeName)
                {
                    case "double":
                        castable = Double.TryParse(er.Value.ToString(), out double a);
                        break;
                    case "int32":
                        castable = Int32.TryParse(er.Value.ToString(), out int b);
                        break;
                    case "int64":
                        castable = Int64.TryParse(er.Value.ToString(), out long c);
                        break;
                    case "datetime":
                        castable = DateTime.TryParse(er.Value.ToString(), out DateTime d);
                        break;
                    case "decimal":
                        castable = Decimal.TryParse(er.Value.ToString(), out decimal f);
                        break;
                    default:
                        break;
                }
            }

            return castable;
        }

    }
}
