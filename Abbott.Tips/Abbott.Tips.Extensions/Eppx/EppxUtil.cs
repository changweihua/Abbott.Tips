using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Abbott.Tips.Extensions.Eppx
{
    public class EppxUtil
    {
        public Func<string, string> L = (key) => key;

        private bool HasL(string key)
        {
            Regex regex = new Regex(@"^\[[\S\s]*\]$");
            return !regex.IsMatch(L(key).EppNullEmpty());
        }

        #region 生成Excel的方法

        public byte[] GenerateExcelStream<T>(List<T> items, string sheetName, int rowIndex = 1)
        {
            var package = GenerateExcel(items, sheetName, rowIndex);
            byte[] bytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                package.SaveAs(ms);
                //转化为byte[]
                bytes = ms.ToArray();
                ms.Seek(0, SeekOrigin.Begin);
            }

            return bytes;
        }

        protected ExcelPackage GenerateExcel<T>(List<T> items, string sheetName, int rowIndex = 1)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(sheetName);

                #region 生成表头

                Type type = typeof(T);
                PropertyInfo[] properties = type.GetProperties();

                int rowHeight = 15;
                int columWidth = 20;

                foreach (PropertyInfo prop in properties)
                {
                    var attr = prop.GetCustomAttribute<EppColumnAttribute>();
                    if (attr != null)
                    {
                        if (!HasL(prop.Name))
                        {
                            worksheet.Cells[rowIndex, attr.ColumnIndex].Value = attr.ColumnDescription;
                        }
                        else
                        {
                            worksheet.Cells[rowIndex, attr.ColumnIndex].Value = L(prop.Name);
                        }
                        worksheet.Column(attr.ColumnIndex).Width = columWidth;
                    }
                }
                worksheet.Row(rowIndex).Height = rowHeight;//设置行高

                #endregion

                rowIndex++;

                #region 写入表格

                foreach (var item in items)
                {
                    foreach (PropertyInfo prop in properties)
                    {
                        var attr = prop.GetCustomAttribute<EppColumnAttribute>();
                        if (attr != null)
                        {
                            if (!string.IsNullOrEmpty(attr.ColumnCellDataValidationSource))
                            {
                                var listValidation = worksheet.Cells[rowIndex, attr.ColumnIndex].DataValidation.AddListDataValidation();
                                listValidation.AllowBlank = false;
                                attr.ColumnCellDataValidationSource.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(_ =>
                                {
                                    listValidation.Formula.Values.Add(_.Trim());
                                });
                            }
                            worksheet.Cells[rowIndex, attr.ColumnIndex].Value = prop.GetValue(item);
                            if (!string.IsNullOrEmpty(attr.ColumnNumberFormat) && !string.IsNullOrEmpty(prop.GetValue(item).EppNullEmpty()))
                            {
                                switch (attr.ColumnDataType)
                                {
                                    case EppColumnDataType.NUMBER:
                                    case EppColumnDataType.INTEGER:
                                        worksheet.Cells[rowIndex, attr.ColumnIndex].Value = Convert.ChangeType(prop.GetValue(item).EppNullEmpty(), typeof(decimal));
                                        break;
                                    case EppColumnDataType.PERCENTAGE:
                                        worksheet.Cells[rowIndex, attr.ColumnIndex].Value = Convert.ChangeType(prop.GetValue(item).EppNullEmpty(), typeof(decimal));
                                        break;
                                    case EppColumnDataType.DATE:
                                    case EppColumnDataType.DATETIME:
                                        worksheet.Cells[rowIndex, attr.ColumnIndex].Value = Convert.ChangeType(prop.GetValue(item).EppNullEmpty(), typeof(DateTime));
                                        break;
                                    default:
                                    case EppColumnDataType.STRING:
                                        break;
                                }
                                worksheet.Cells[rowIndex, attr.ColumnIndex].Style.Numberformat.Format = attr.ColumnNumberFormat;
                            }
                        }
                    }
                    worksheet.Row(rowIndex).Height = rowHeight;//设置行高
                    rowIndex++;
                }

                #endregion

                worksheet.Cells.Style.WrapText = true;//自动换行

                #region 统一返回结果方法

                return package;

                #endregion
            }
        }

        #endregion

        #region 加载Excel数据

        private List<T> LoadExcel<T>(string filePath, int rowStart = 1, Func<List<T>, List<T>> callback = null) where T : EppRowModel, new()
        {
            var list = new List<T>();
            FileInfo file = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                int rowCount = worksheet.Dimension.Rows;

                Type type = typeof(T);
                var properties = type.GetProperties();

                for (int row = rowStart; row <= rowCount; row++)
                {
                    var item = new T();
                    item.RowIndex = row;
                    item.CellErrors = new List<EppCellErrorModel>();
                    //反射读取单元格
                    foreach (PropertyInfo prop in properties)
                    {
                        var attr = prop.GetCustomAttribute<EppColumnAttribute>();
                        if (attr != null)
                        {
                            var cell = worksheet.Cells[row, attr.ColumnIndex];

                            var validationAttr = prop.GetCustomAttribute<EppColumnValidationAttribute>();
                            if (validationAttr != null)
                            {
                                if (!validationAttr.IsValidDataType(cell.Value))
                                {
                                    item.CellErrors.Add(new EppCellErrorModel { RowIndex = row, ColumnIndex = attr.ColumnIndex, ErrorItem = prop.Name, ErrorMessage = "类型错误" });
                                }
                                if (!string.IsNullOrEmpty(attr.ColumnCellDataValidationSource) && !validationAttr.IsInValidSource(attr.ColumnCellDataValidationSource, cell.Value))
                                {
                                    item.CellErrors.Add(new EppCellErrorModel { RowIndex = row, ColumnIndex = attr.ColumnIndex, ErrorItem = prop.Name, ErrorMessage = "值不在范围" });
                                }
                            }
                            if (item.CellErrors.Where(err => err.ErrorItem == prop.Name).Count() == 0)
                            {
                                prop.SetValue(item, Convert.ChangeType(cell.Value.EppNullEmpty(), prop.PropertyType));
                            }
                        }
                    }

                    list.Add(item);
                }

                list = callback?.Invoke(list);

            }
            return list;
        }

        #endregion
    }
}
