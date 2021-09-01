using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Extensions;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            initUI();
            initEvent();
        }

        private void initUI()
        {
            this.Title = $"{this.Title} - {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()}";

            #region 设置 Fira Code 字体

            var fontFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources", "TTF", "FiraCode-Regular.ttf");
            var fontFamily_Fira_Code = Util.FontFamilyUtils.GetFontFamily_TypeOf_System_Windows_Media(fontFile);

            txtInput.FontFamily = fontFamily_Fira_Code;
            txtOutput0.FontFamily = fontFamily_Fira_Code;
            txtOutput_SQL_0.FontFamily = fontFamily_Fira_Code;
            txtOutput_SQL_1.FontFamily = fontFamily_Fira_Code;
            txtOutput_SQL_2.FontFamily = fontFamily_Fira_Code; txtOutput_SQL_2_Arg1.FontFamily = fontFamily_Fira_Code; txtOutput_SQL_2_Arg2.FontFamily = fontFamily_Fira_Code;

            txtOutput_CSharp_0.FontFamily = fontFamily_Fira_Code;
            txtOutput_CSharp_1.FontFamily = fontFamily_Fira_Code; txtOutput_CSharp_1_Arg.FontFamily = fontFamily_Fira_Code;
            txtOutput_CSharp_2.FontFamily = fontFamily_Fira_Code;
            txtOutput_CSharp_3.FontFamily = fontFamily_Fira_Code;
            txtOutput_CSharp_4.FontFamily = fontFamily_Fira_Code;
            txtOutput_CSharp_5.FontFamily = fontFamily_Fira_Code; txtOutput_CSharp_5_Arg.FontFamily = fontFamily_Fira_Code;
            txtOutput_CSharp_6.FontFamily = fontFamily_Fira_Code; txtOutput_CSharp_6_Arg1.FontFamily = fontFamily_Fira_Code; txtOutput_CSharp_6_Arg2.FontFamily = fontFamily_Fira_Code;
            txtOutput_CSharp_7.FontFamily = fontFamily_Fira_Code; txtOutput_CSharp_7_Arg1.FontFamily = fontFamily_Fira_Code; txtOutput_CSharp_7_Arg2.FontFamily = fontFamily_Fira_Code;
            txtOutput_CSharp_8.FontFamily = fontFamily_Fira_Code; cbOutput_CSharp_8_Arg1.FontFamily = fontFamily_Fira_Code; cbOutput_CSharp_8_Arg2.FontFamily = fontFamily_Fira_Code; cbOutput_CSharp_8_Arg3.FontFamily = fontFamily_Fira_Code;
            txtOutput_CSharp_9.FontFamily = fontFamily_Fira_Code; txtOutput_CSharp_9_Arg1.FontFamily = fontFamily_Fira_Code; txtOutput_CSharp_9_Arg2.FontFamily = fontFamily_Fira_Code; cbOutput_CSharp_9_Arg3.FontFamily = fontFamily_Fira_Code; cbOutput_CSharp_9_Arg4.FontFamily = fontFamily_Fira_Code; cbOutput_CSharp_9_Arg4_0.FontFamily = fontFamily_Fira_Code; cbOutput_CSharp_9_Arg4_1.FontFamily = fontFamily_Fira_Code; cbOutput_CSharp_9_Arg4_10.FontFamily = fontFamily_Fira_Code; cbOutput_CSharp_9_Arg4_11.FontFamily = fontFamily_Fira_Code;

            #endregion
        }

        private void initEvent()
        {
            this.btnClearContent.Click += (s,e) => { this.txtInput.Text = string.Empty; };

            // 输入参数
            this.txtInput.TextChanged += txtInput_TextChanged;
            this.txtOutput_CSharp_1_Arg.TextChanged += txtInput_TextChanged;
            this.txtOutput_SQL_2_Arg1.TextChanged += txtInput_TextChanged; this.txtOutput_SQL_2_Arg2.TextChanged += txtInput_TextChanged;
            this.txtOutput_CSharp_5_Arg.TextChanged += txtInput_TextChanged;
            this.txtOutput_CSharp_6_Arg1.TextChanged += txtInput_TextChanged; this.txtOutput_CSharp_6_Arg2.TextChanged += txtInput_TextChanged;
            this.txtOutput_CSharp_7_Arg1.TextChanged += txtInput_TextChanged; this.txtOutput_CSharp_7_Arg2.TextChanged += txtInput_TextChanged;
            this.cbOutput_CSharp_8_Arg1.Checked += radioButton_Click; this.cbOutput_CSharp_8_Arg2.Checked += radioButton_Click; this.cbOutput_CSharp_8_Arg3.Checked += radioButton_Click;
            this.txtOutput_CSharp_9_Arg1.TextChanged += txtInput_TextChanged; this.txtOutput_CSharp_9_Arg2.TextChanged += txtInput_TextChanged; cbOutput_CSharp_9_Arg3.Click += checkBox_Click; cbOutput_CSharp_9_Arg4.Click += checkBox_Click; cbOutput_CSharp_9_Arg4_0.Checked += checkBox_Click; cbOutput_CSharp_9_Arg4_1.Checked += checkBox_Click; cbOutput_CSharp_9_Arg4_10.Checked += checkBox_Click; cbOutput_CSharp_9_Arg4_11.Checked += checkBox_Click;

            // CheckBox
            this.cbTabNewLine.Click += checkBox_Click;
            this.cbDistinct.Click += checkBox_Click;

            // RadioButton
            // 除空行
            this.rbNotRemove.Checked += radioButton_Checked;
            this.rbRemoveNullLine.Checked += radioButton_Checked;
            this.rbRemoveWhiteSpace.Checked += radioButton_Checked;

            // 除内容空格
            this.rbNotTrim.Checked += radioButton_Checked;
            this.rbTrimStart.Checked += radioButton_Checked;
            this.rbTrimEnd.Checked += radioButton_Checked;
            this.rbTrim.Checked += radioButton_Checked;
            this.rbTrimAdv.Checked += radioButton_Checked;

            // 双击复制到粘贴板
            tab0.MouseDoubleClick += tabX_MouseDoubleClick;
            tab_SQL_0.MouseDoubleClick += tabX_MouseDoubleClick;
            tab_SQL_1.MouseDoubleClick += tabX_MouseDoubleClick;
            tab_SQL_2.MouseDoubleClick += tabX_MouseDoubleClick;
            tab_CSharp_0.MouseDoubleClick += tabX_MouseDoubleClick;
            tab_CSharp_1.MouseDoubleClick += tabX_MouseDoubleClick;
            tab_CSharp_2.MouseDoubleClick += tabX_MouseDoubleClick;
            tab_CSharp_3.MouseDoubleClick += tabX_MouseDoubleClick;
            tab_CSharp_4.MouseDoubleClick += tabX_MouseDoubleClick;
            tab_CSharp_5.MouseDoubleClick += tabX_MouseDoubleClick;
            tab_CSharp_6.MouseDoubleClick += tabX_MouseDoubleClick;
            tab_CSharp_7.MouseDoubleClick += tabX_MouseDoubleClick;
            tab_CSharp_8.MouseDoubleClick += tabX_MouseDoubleClick;
            tab_CSharp_9.MouseDoubleClick += tabX_MouseDoubleClick;
        }

        private void tabX_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is TabItem)
            {
                if ((sender as TabItem).Content is TextBox)
                {
                    copyToClipboard((sender as TabItem).Content);
                }
                else if ((sender as TabItem).Content is Grid)
                {
                    Grid grid = (sender as TabItem).Content as Grid;
                    copyToClipboard(grid.Children[0]);
                }
            }
        }

        private void copyToClipboard(object o)
        {
            if (o is TextBox)
            {
                Clipboard.SetText((o as TextBox).Text);
            }
        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            execute();
        }


        private void radioButton_Click(object sender, RoutedEventArgs e) // 先命中 Checked, 再命中 Click
        {
            execute();
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            execute();
        }

        Util.ActionUtils.DebounceAction mDebounceAction { get; set; } = new Util.ActionUtils.DebounceAction();

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            mDebounceAction.Debounce(TimeSpan.FromSeconds(1).TotalMilliseconds, execute, this.Dispatcher);
        }

        private void execute()
        {
            string text = this.txtInput.Text;

            if (text.IsNullOrWhiteSpace() == true)
            {
                this.txtInputInfo.Text = "空值";

                this.txtOutputInfo.Text = string.Empty;
                this.txtOutput0.Text = string.Empty;
                this.txtOutput_SQL_0.Text = string.Empty;
                this.txtOutput_SQL_1.Text = string.Empty;
                this.txtOutput_SQL_2.Text = string.Empty;
                this.txtOutput_CSharp_0.Text = string.Empty;
                this.txtOutput_CSharp_1.Text = string.Empty;
                this.txtOutput_CSharp_2.Text = string.Empty;
                this.txtOutput_CSharp_3.Text = string.Empty;
                this.txtOutput_CSharp_4.Text = string.Empty;
                this.txtOutput_CSharp_5.Text = string.Empty;
                this.txtOutput_CSharp_6.Text = string.Empty;
                this.txtOutput_CSharp_7.Text = string.Empty;
                this.txtOutput_CSharp_8.Text = string.Empty;
                this.txtOutput_CSharp_9.Text = string.Empty;
                return;
            }

            StringBuilder sbInfo = new StringBuilder();
            var matchNewLineSymbol = System.Text.RegularExpressions.Regex.Matches(text, "[\r]?\n");
            sbInfo.Append($"源文件 - 共 {matchNewLineSymbol.Count + 1} 行; ");

            var query = text.Split('\r', '\n').AsQueryable<string>();
            if (cbTabNewLine.IsChecked.Value)
            {
                List<string> temp = new List<string>();
                foreach (var item in query)
                {
                    temp.AddRange(item.Split('\t'));
                }

                query = temp.AsQueryable<string>();
            }

            // RadioButton
            // 除空行
            if (rbRemoveNullLine.IsChecked.HasValue && rbRemoveNullLine.IsChecked.Value == true)
            {
                query = query.Where(i => i.IsNullOrEmpty() == false);
            }
            else if (rbRemoveWhiteSpace.IsChecked.HasValue && rbRemoveWhiteSpace.IsChecked.Value == true)
            {
                query = query.Where(i => i.IsNullOrWhiteSpace() == false);
            }

            // 除内容空格
            if (rbTrim.IsChecked.HasValue && rbTrim.IsChecked.Value == true)
            {
                query = query.Select(i => i.Trim());
            }
            else if (rbNotTrim.IsChecked.HasValue && rbNotTrim.IsChecked.Value == true)
            {
                // Do Nothing
            }
            else if (rbTrimStart.IsChecked.HasValue && rbTrimStart.IsChecked.Value == true)
            {
                query = query.Select(i => i.TrimStart());
            }
            else if (rbTrimEnd.IsChecked.HasValue && rbTrimEnd.IsChecked.Value == true)
            {
                query = query.Select(i => i.TrimEnd());
            }
            else if (rbTrimAdv.IsChecked.HasValue && rbTrimAdv.IsChecked.Value == true)
            {
                query = query.Select(i => i.TrimAdv());
            }

            if (cbDistinct.IsChecked.HasValue && cbDistinct.IsChecked.Value)
            {
                query = query.Distinct();
            }

            var list = query.ToList();
            this.txtInputInfo.Text = sbInfo.ToString();
            this.txtOutputInfo.Text = $"结果 - 共 {list.Count} 项";


            this.txtOutput0.Text = calc(list);
            this.txtOutput_SQL_0.Text = SQL_IN条件参数(list);
            this.txtOutput_SQL_1.Text = SQL_插入临时表(list);
            this.txtOutput_SQL_2.Text = SQL_Insert语句(list);
            this.txtOutput_CSharp_0.Text = CSharp_插入List(list);
            this.txtOutput_CSharp_1.Text = CSharp_DataTable(list);
            this.txtOutput_CSharp_2.Text = CSharp_rDataTable(list);
            this.txtOutput_CSharp_3.Text = CSharp_rDataTableHowe(list);
            this.txtOutput_CSharp_4.Text = CSharp_CopyModel(list);
            this.txtOutput_CSharp_5.Text = CSharp_SqlParameter(list);
            this.txtOutput_CSharp_6.Text = CSharp_NewModelList(list);
            this.txtOutput_CSharp_7.Text = CSharp_NewModelListHowe(list);
            this.txtOutput_CSharp_8.Text = CSharp_FilePath(list);
            this.txtOutput_CSharp_9.Text = CSharp_Json2CSV(list);
        }



        private string calc(List<string> l)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < l.Count; i++)
            {
                if (i < l.Count - 1)
                {
                    sb.AppendLine(l[i]);
                }
                else
                {
                    sb.Append(l[i]);
                }
            }
            return sb.ToString();
        }

        private string SQL_IN条件参数(List<string> l)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in l)
            {
                sb.Append("'").Append(item).Append("', ");
            }

            var r = sb.ToString();
            return r.Substring(0, r.Length - 2); // 去掉最后的两个符号
        }

        private string SQL_插入临时表(List<string> l)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in l)
            {
                sb.Append("''").Append(item).Append("'', ");
            }

            var r = sb.ToString();
            return r.Substring(0, r.Length - 2); // 去掉最后的两个符号
        }

        private string SQL_Insert语句(List<string> l)
        {
            string insertTableName = this.txtOutput_SQL_2_Arg1.Text;
            if (insertTableName.IsNullOrWhiteSpace())
            {
                insertTableName = "@tmp";
            }

            IEnumerable<string> m = this.txtOutput_SQL_2_Arg2.Text.Split(',', ' ', '\t').Where(i => i.IsNullOrWhiteSpace() == false);
            List<int> ignoreIndexList = new List<int>();
            foreach (var item in m)
            {
                if (int.TryParse(item, out int i))
                {
                    ignoreIndexList.Add(i);
                }
            }

            StringBuilder sb = new StringBuilder();

            foreach (var oneline in l)
            {
                sb.Append($"insert into {insertTableName} values ( ");
                var splitedTabArr = oneline.Split('\t');
                for (int index = 0; index < splitedTabArr.Length; index++)
                {
                    string item = splitedTabArr[index];
                    if (item.Equals("null", StringComparison.CurrentCultureIgnoreCase))
                    {
                        sb.Append(item);
                    }
                    else if (ignoreIndexList.Any(j => j == index)) // 忽略用户手输的
                    {
                        sb.Append(item);
                    }
                    else
                    {
                        sb.Append("'").Append(item).Append("'");
                    }

                    if (index + 1 < splitedTabArr.Length)
                    {
                        sb.Append(", ");
                    }
                }
                sb.AppendLine($" );");
            }

            var r = sb.ToString();
            return r.Substring(0, r.Length - 2); // 去掉最后的两个符号
        }

        private string CSharp_插入List(List<string> l)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in l)
            {
                sb.Append("\"").Append(item).Append("\", ");
            }

            var r = sb.ToString();
            return r.Substring(0, r.Length - 2); // 去掉最后的两个符号
        }

        private string CSharp_DataTable(List<string> l)
        {
            string tabSymbol = "\t";

            StringBuilder sb = new StringBuilder();

            #region 创建表

            sb.AppendLine($"public System.Data.DataTable CreateDataTable_X()");
            sb.AppendLine("{");

            sb.Append(tabSymbol).AppendLine("System.Data.DataTable dt = new System.Data.DataTable();");

            foreach (var item in l)
            {
                sb.Append(tabSymbol).AppendLine("dt.Columns.Add(\"" + item + "\");");
            }

            sb.Append(tabSymbol).AppendLine("return dt;");
            sb.AppendLine("}");

            #endregion

            sb.AppendLine("");

            #region 表赋值

            string typeName = this.txtOutput_CSharp_1_Arg.Text;
            if (typeName.IsNullOrWhiteSpace() == true) { typeName = "XModel"; }

            sb.AppendLine($"public System.Data.DataTable GetDataTable(System.Collections.Generic.IEnumerable<{typeName}> l)");
            sb.AppendLine("{");

            sb.Append(tabSymbol).AppendLine("var dt = CreateDataTable_X();");
            sb.Append(tabSymbol).AppendLine("foreach (var item in l)");
            sb.Append(tabSymbol).AppendLine("{");
            sb.Append(tabSymbol).Append(tabSymbol).AppendLine("DataRow dr = dt.NewRow();");
            sb.Append(tabSymbol).Append(tabSymbol).AppendLine("dt.Rows.Add(dr);");

            foreach (var item in l)
            {
                sb.Append(tabSymbol).Append(tabSymbol).AppendLine($"dr[\"{item}\"] = item.{item};");
            }

            sb.Append(tabSymbol).AppendLine("}");
            sb.Append(tabSymbol).AppendLine("return dt;");
            sb.AppendLine("}");

            #endregion

            return sb.ToString();
        }

        private string CSharp_rDataTable(List<string> l)
        {
            string tabSymbol = "\t";

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("foreach (DataRow dr in dt.Rows)");
            sb.AppendLine("{");
            sb.Append(tabSymbol).AppendLine("CA toAdd = new CA();");
            sb.Append(tabSymbol).AppendLine("l.Add(toAdd);");

            foreach (var item in l)
            {
                sb.Append(tabSymbol).AppendLine($"toAdd.{item} = Util.CommonDal.ReadString(dr[\"{item}\"]);");
            }

            sb.AppendLine("}");
            sb.AppendLine("return dt;");

            return sb.ToString();
        }

        private string CSharp_rDataTableHowe(List<string> l)
        {
            string tabSymbol = "\t";

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("foreach (DataRow dr in dt.Rows)");
            sb.AppendLine("{");
            sb.Append(tabSymbol).AppendLine("CA toAdd = new CA();");
            sb.Append(tabSymbol).AppendLine("l.Add(toAdd);");

            foreach (var item in l)
            {
                if (item.Equals("ID", StringComparison.CurrentCultureIgnoreCase))
                {
                    sb.Append(tabSymbol).AppendLine($"toAdd.{item} = Util.CommonDal.ReadGuid(dr[\"{item}\"]);");
                }
                else if (item.EndsWith("OrderID", StringComparison.CurrentCultureIgnoreCase))
                {
                    sb.Append(tabSymbol).AppendLine($"toAdd.{item} = Util.CommonDal.ReadGuid(dr[\"{item}\"]);");
                }
                else if (item.EndsWith("OrderItemID", StringComparison.CurrentCultureIgnoreCase))
                {
                    sb.Append(tabSymbol).AppendLine($"toAdd.{item} = Util.CommonDal.ReadGuid(dr[\"{item}\"]);");
                }
                else if (item.EndsWith("Time", StringComparison.CurrentCultureIgnoreCase))
                {
                    sb.Append(tabSymbol).AppendLine($"toAdd.{item} = Util.CommonDal.ReadDateTime(dr[\"{item}\"]);");
                }
                else if (item.EndsWith("Date", StringComparison.CurrentCultureIgnoreCase))
                {
                    sb.Append(tabSymbol).AppendLine($"toAdd.{item} = Util.CommonDal.ReadDateTime(dr[\"{item}\"]);");
                }
                else if (item.EndsWith("Qty", StringComparison.CurrentCultureIgnoreCase))
                {
                    sb.Append(tabSymbol).AppendLine($"toAdd.{item} = Util.CommonDal.ReadDecimal(dr[\"{item}\"]);");
                }
                else if (item.EndsWith("SNP", StringComparison.CurrentCultureIgnoreCase))
                {
                    sb.Append(tabSymbol).AppendLine($"toAdd.{item} = Util.CommonDal.ReadDecimal(dr[\"{item}\"]);");
                }
                else
                {
                    sb.Append(tabSymbol).AppendLine($"toAdd.{item} = Util.CommonDal.ReadString(dr[\"{item}\"]);");
                }
            }

            sb.AppendLine("}");
            sb.AppendLine("return dt;");

            return sb.ToString();
        }

        private string CSharp_SqlParameter(List<string> l)
        {
            string argsName = this.txtOutput_CSharp_5_Arg.Text;
            if (argsName.IsNullOrWhiteSpace())
            {
                argsName = "searchArgs";
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"var paramList = new List<System.Data.SqlClient.SqlParameter>();");

            foreach (var item in l)
            {
                sb.AppendLine($"new System.Data.SqlClient.SqlParameter(\"@{item}\", {argsName}.{item}));");
            }

            sb.AppendLine("}");

            return sb.ToString();
        }

        private string CSharp_CopyModel(List<string> l)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"public void UpdateData(XModel o, XModel t)");
            sb.AppendLine("{");

            string tabSymbol = "\t";

            foreach (var item in l)
            {
                sb.Append(tabSymbol).AppendLine($"o.{item} = t.{item};");
            }

            sb.AppendLine("}");

            return sb.ToString();
        }

        private string CSharp_NewModelList(List<string> l)
        {
            string className = this.txtOutput_CSharp_6_Arg1.Text;
            if (className.IsNullOrWhiteSpace())
            {
                className = "XMdoel";
            }

            IEnumerable<string> m = this.txtOutput_CSharp_6_Arg2.Text.Split(',', ' ', '\t').Where(i => i.IsNullOrWhiteSpace() == false);
            List<int> ignoreIndexList = new List<int>();
            foreach (var item in m)
            {
                if (int.TryParse(item, out int i))
                {
                    ignoreIndexList.Add(i);
                }
            }

            var propNameList = l[0].Split('\t').Select(i => i.Trim()).Where(i => i.IsNullOrWhiteSpace() == false).ToList();

            if (l.Count <= 1)
            {
                return "不满足转换要求: 至少需要两行数据";
            }

            StringBuilder sb = new StringBuilder();

            for (int rowIndex = 1; rowIndex < l.Count; rowIndex++)
            {
                sb.Append("l.Add(new ").Append(className).Append("() { ");

                List<string> valuesList = l[rowIndex].Split('\t').Select(i => i.Trim()).Where(i => i.IsNullOrWhiteSpace() == false).ToList();

                if (propNameList.Count < valuesList.Count)
                {
                    return "不满足转换要求: propList.Count < valueList.Count"; ;
                }

                for (int columnIndex = 0; columnIndex < valuesList.Count; columnIndex++)
                {
                    string item = valuesList[columnIndex];

                    if (item.Equals("null", StringComparison.CurrentCultureIgnoreCase))
                    {
                        // sb.Append($"{propNameList[columnIndex]} = {valuesList[columnIndex]}");
                        sb.Append($"{propNameList[columnIndex]} = null");
                    }
                    else if (ignoreIndexList.Any(j => j == columnIndex)) // 忽略用户手输的
                    {
                        sb.Append($"{propNameList[columnIndex]} = {valuesList[columnIndex]}");
                    }
                    else
                    {
                        sb.Append($"{propNameList[columnIndex]} = \"{valuesList[columnIndex]}\"");
                    }

                    if (columnIndex + 1 < propNameList.Count)
                    {
                        sb.Append(", ");
                    }
                }

                if (rowIndex + 1 < l.Count)
                {
                    sb.AppendLine(" });");
                }
                else
                {
                    sb.Append(" });");
                }
            }

            return sb.ToString();
        }

        private string CSharp_NewModelListHowe(List<string> l)
        {
            string className = this.txtOutput_CSharp_7_Arg1.Text;
            if (className.IsNullOrWhiteSpace())
            {
                className = "XMdoel";
            }

            IEnumerable<string> m = this.txtOutput_CSharp_7_Arg2.Text.Split(',', ' ', '\t').Where(i => i.IsNullOrWhiteSpace() == false);
            List<int> ignoreIndexList = new List<int>();
            foreach (var item in m)
            {
                if (int.TryParse(item, out int i))
                {
                    ignoreIndexList.Add(i);
                }
            }

            var propNameList = l[0].Split('\t').Select(i => i.Trim()).Where(i => i.IsNullOrWhiteSpace() == false).ToList();

            if (l.Count <= 1)
            {
                return "不满足转换要求: 至少需要两行数据";
            }

            StringBuilder sb = new StringBuilder();

            for (int rowIndex = 1; rowIndex < l.Count; rowIndex++)
            {
                sb.Append("l.Add(new ").Append(className).Append("() { ");

                List<string> valuesList = l[rowIndex].Split('\t').Select(i => i.Trim()).Where(i => i.IsNullOrWhiteSpace() == false).ToList();

                if (propNameList.Count < valuesList.Count)
                {
                    return "不满足转换要求: propList.Count < valueList.Count"; ;
                }

                for (int columnIndex = 0; columnIndex < valuesList.Count; columnIndex++)
                {
                    string item = valuesList[columnIndex];

                    if (item.Equals("null", StringComparison.CurrentCultureIgnoreCase))
                    {
                        sb.Append($"{propNameList[columnIndex]} = null");
                    }
                    else if (ignoreIndexList.Any(j => j == columnIndex)) // 忽略用户手输的
                    {
                        sb.Append($"{propNameList[columnIndex]} = {valuesList[columnIndex]}");
                    }
                    else
                    {
                        string columnName = propNameList[columnIndex];

                        if (columnName.Equals("ID", StringComparison.CurrentCultureIgnoreCase))
                        {
                            sb.Append($"{propNameList[columnIndex]} = Guid.Parse(\"{valuesList[columnIndex]}\")");
                        }
                        else if (columnName.EndsWith("OrderID", StringComparison.CurrentCultureIgnoreCase))
                        {
                            sb.Append($"{propNameList[columnIndex]} = Guid.Parse(\"{valuesList[columnIndex]}\")");
                        }
                        else if (columnName.EndsWith("OrderItemID", StringComparison.CurrentCultureIgnoreCase))
                        {
                            sb.Append($"{propNameList[columnIndex]} = Guid.Parse(\"{valuesList[columnIndex]}\")");
                        }
                        else if (columnName.EndsWith("Time", StringComparison.CurrentCultureIgnoreCase))
                        {
                            sb.Append($"{propNameList[columnIndex]} = DateTime.Parse(\"{valuesList[columnIndex]}\")");
                        }
                        else if (columnName.EndsWith("Date", StringComparison.CurrentCultureIgnoreCase))
                        {
                            sb.Append($"{propNameList[columnIndex]} = DateTime.Parse(\"{valuesList[columnIndex]}\")");
                        }
                        else if (columnName.EndsWith("Qty", StringComparison.CurrentCultureIgnoreCase))
                        {
                            sb.Append($"{propNameList[columnIndex]} = Decimal.Parse(\"{valuesList[columnIndex]}\")");
                        }
                        else if (columnName.EndsWith("SNP", StringComparison.CurrentCultureIgnoreCase))
                        {
                            sb.Append($"{propNameList[columnIndex]} = Decimal.Parse(\"{valuesList[columnIndex]}\")");
                        }
                        else
                        {
                            sb.Append($"{propNameList[columnIndex]} = \"{valuesList[columnIndex]}\"");
                        }
                    }

                    if (columnIndex + 1 < propNameList.Count)
                    {
                        sb.Append(", ");
                    }
                }

                if (rowIndex + 1 < l.Count)
                {
                    sb.AppendLine(" });");
                }
                else
                {
                    sb.Append(" });");
                }

            }

            return sb.ToString();
        }

        private string CSharp_FilePath(List<string> l)
        {
            StringBuilder sb = new StringBuilder();

            string combineSymbol = string.Empty;
            if (cbOutput_CSharp_8_Arg1.IsChecked.HasValue == true && cbOutput_CSharp_8_Arg1.IsChecked.Value == true)
            {
                combineSymbol = @"\";
            }
            else if (cbOutput_CSharp_8_Arg2.IsChecked.HasValue == true && cbOutput_CSharp_8_Arg2.IsChecked.Value == true)
            {
                combineSymbol = @"\\";
            }
            else if (cbOutput_CSharp_8_Arg3.IsChecked.HasValue == true && cbOutput_CSharp_8_Arg3.IsChecked.Value == true)
            {
                combineSymbol = @"/";
            }

            foreach (var item in l)
            {
                var toAdd = item.Split('\\', '/').Where(i => i.IsNullOrWhiteSpace() == false).CombineString(combineSymbol);
                sb.AppendLine(toAdd);
            }

            var r = sb.ToString();
            return r.Substring(0, r.Length - 2); // 去掉最后的两个符号
        }

        private string CSharp_Json2CSV(List<string> l)
        {
            string symbol = string.Empty;
            if (this.txtOutput_CSharp_9_Arg1.Text.IsNullOrWhiteSpace() == false)
            {
                symbol = this.txtOutput_CSharp_9_Arg1.Text;
            }

            if (symbol.IsNullOrWhiteSpace() == true)
            {
                tab_CSharp_9_txtOutputInfo.Text = string.Empty;
                return "未输入CSV文件分割符号";
            }

            if (this.txtOutput_CSharp_9_Arg2.Text.IsNullOrWhiteSpace() == true)
            {
                tab_CSharp_9_txtOutputInfo.Text = string.Empty;
                return "未输入提取属性";
            }

            List<string> propList = // 提取属性集合
            this.txtOutput_CSharp_9_Arg2.Text
                .Split(',', '|', ';')
                .Select(i => i.Trim())
                .Where(i => i.IsNullOrWhiteSpace() == false)
                .ToList();

            StringBuilder sb = new StringBuilder();

            List<JObject> dl = new List<JObject>();

            string jsonStr = string.Empty;
            List<char> charList = new List<char>() { ']', '}' }; // 若以 ] } 结尾, 则进行JSON字符串的解释, 否则继续累加
            foreach (var item in l)
            {
                jsonStr = jsonStr + item; // 累加成为完整的Json字符串

                if (charList.Contains(jsonStr[jsonStr.Length - 1])) // 若以 ] } 结尾, 则进行JSON字符串的解释
                {
                    if (jsonStr[0] == '[')
                    {
                        dl.AddRange(Util.JsonUtils.DeserializeObject<List<JObject>>(jsonStr));
                    }
                    else
                    {
                        dl.Add(Util.JsonUtils.DeserializeObject<JObject>(jsonStr));
                    }

                    jsonStr = string.Empty;
                }
            }

            foreach (JObject item in dl)
            {
                string toAddSB = string.Empty;
                foreach (string key in propList)
                {
                    if (item.TryGetValue(propertyName: key, StringComparison.CurrentCultureIgnoreCase, out JToken value))
                    {
                        if (toAddSB.IsNullOrEmpty() == true)
                        {
                            toAddSB = value.ToString();
                        }
                        else
                        {
                            toAddSB = $"{toAddSB}{symbol}{value.ToString()}";
                        }
                    }
                }
                sb.AppendLine(toAddSB);
            }

            string r = sb.ToString();

            if (r.IsNullOrWhiteSpace() == true)
            {
                return r;
            }

            var query = sb.ToString()
                      .Split(new string[] { "\r\n" }, StringSplitOptions.None)
                      .AsQueryable<string>()
                      ;

            if (cbOutput_CSharp_9_Arg3.IsChecked == true) // 去重复
            {
                query = query.Distinct();
            }

            tab_CSharp_9_txtOutputInfo.Text = $"Json2CSV 结果 - 共 {query.Count() - 1} 项"; // - 1 原因是 最后有一个 \r\n 多计算了

            if (cbOutput_CSharp_9_Arg4.IsChecked.Value) // 原顺序
            {
                // Do nothing
            }
            else if (cbOutput_CSharp_9_Arg4_10.IsChecked.Value) // Windows 正序
            {
                var stringComparer = new StrLogicalComparer();
                query = query.OrderBy(keySelector: (i => i), comparer: stringComparer);
            }
            else if (cbOutput_CSharp_9_Arg4_11.IsChecked.Value) // Windows 逆序
            {
                var stringComparer = new StrLogicalComparer();
                query = query.OrderByDescending(keySelector: (i => i), comparer: stringComparer);
            }
            else if (cbOutput_CSharp_9_Arg4_0.IsChecked.Value) // C# 正序
            {
                query = query.OrderBy(i => i);
            }
            else if (cbOutput_CSharp_9_Arg4_1.IsChecked.Value) // C# 逆序
            {
                query = query.OrderByDescending(i => i);
            }

            r = query.CombineString("\r\n");

            return r; // 去掉最后的两个符号            
        }
    }
}