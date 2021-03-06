﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Collections;
using System.IO;
using Newtonsoft.Json.Linq;
namespace Utils.Office
{
    /// <summary>
    /// 导出
    /// </summary>
    public class Exporter
    {

        /// <summary>
        /// EasyUi Columns
        /// </summary>
        private List<List<Column>> _titles;

        private IExport _exprot;
        /// <summary>
        /// 获取数据
        /// </summary>
        private object _dataSource;

        public Column _head;



        public static Exporter Instance()
        {
            var current = HttpContext.Current;
            var exporter = new Exporter();
            if (!current.Request["titles"].IsNullOrEmpty())
            {
                exporter.SetTitle(JsonConvert.DeserializeObject<List<List<Column>>>(current.Request["titles"]));
            }
            exporter.SetExport(new XlsExport());
            exporter.SetDataSource(new ApiData());
            return exporter;

        }
        public Exporter SetTitle(List<List<Column>> titles)
        {
            this._titles = titles;
            return this;
        }
        public Exporter SetExport(IExport export)
        {
            this._exprot = export;
            return this;
        }
        public Exporter SetDataSource(IApiData data)
        {
            this._dataSource = data.GetData(HttpContext.Current);
            return this;
        }


        private Exporter()
        {
        }

        public Exporter Export()
        {
            try
            {
                int currentRowIndex = 0, currentColIndex = 0;
                Dictionary<int, int> currenteHeadRow = new Dictionary<int, int>();//放置单元格所占行数
                Dictionary<string, int> fieldIndex = new Dictionary<string, int>();//获取每列元素所对应的
                Func<int, int> getCurrentHeadRow = cell => currenteHeadRow.ContainsKey(cell) ? currenteHeadRow[cell] : 0;
                #region 设置表头，包含多表头
                for (int i = 0; i < _titles.Count; i++)
                {
                    currentColIndex = 0;//当前列
                    for (int j = 0; j < _titles[i].Count; j++)
                    {
                        var column = _titles[i][j];
                        if (column.hidden) continue;
                        while (currentRowIndex < getCurrentHeadRow(currentColIndex))
                        {
                            currentColIndex++;
                        }

                        _exprot.FillData(currentRowIndex, currentColIndex, column.title);
                        if (column.rowspan + column.colspan > 2)
                        {
                            this._exprot.MergeCell(currentRowIndex, currentRowIndex + column.rowspan - 1, currentColIndex, currentColIndex + column.colspan - 1);
                        }
                        if (column.colspan == 1 && !column.field.IsNullOrEmpty())
                        {
                            fieldIndex[column.field] = currentColIndex;
                        }
                        for (int k = 0; k < column.colspan; k++)
                        {
                            currenteHeadRow[currentColIndex] = getCurrentHeadRow(currentColIndex++) + column.rowspan;
                        }
                    }
                    _exprot.SetHeadStyle(currentRowIndex, 0, currentRowIndex, currentColIndex - 1);

                    currentRowIndex++;
                }
                #endregion
                #region 填充数据
                //填充数据
                var index = 0;
                foreach (var x in (_dataSource as IEnumerable<dynamic>))
                {
                    foreach (var item in fieldIndex)
                    {
                        _exprot.FillData(index + _titles.Count, item.Value, (x as JObject).Property(item.Key.ToLower()).Value.ToString());
                    }
                    _exprot.SetRowsStyle(index + _titles.Count, 0, index + _titles.Count, fieldIndex.Count-1);//设置数据样式
                    index++;
                }
                #endregion
                return this;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DownLoad()
        {
            Utils.HttpHelper.DownloadExcel(_exprot.SaveAsStream(),Path.Combine(DateTime.Now.ToString("yyyyMMdd"),".xls"));
        }
    }
}
