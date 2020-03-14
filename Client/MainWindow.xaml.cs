using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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

            #endregion
        }

        private void initEvent()
        {
            // 输入参数
            this.txtInput.TextChanged += txtInput_TextChanged;
            this.txtOutput_CSharp_1_Arg.TextChanged += txtInput_TextChanged;
            this.txtOutput_SQL_2_Arg1.TextChanged += txtInput_TextChanged;
            this.txtOutput_SQL_2_Arg2.TextChanged += txtInput_TextChanged;

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
        }

        private void tabX_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is TabItem)
            {
                copyToClipboard((sender as TabItem).Content);
            }
        }

        private void copyToClipboard(object sender)
        {
            if (sender is TextBox)
            {
                Clipboard.SetText((sender as TextBox).Text);
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
                return;
            }

            StringBuilder sbInfo = new StringBuilder();
            var matchNewLineSymbol = System.Text.RegularExpressions.Regex.Matches(text, "[\r]?\n");
            sbInfo.Append($"源文件共 {matchNewLineSymbol.Count + 1} 行; ");

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
            this.txtOutputInfo.Text = $"共 {list.Count} 项";


            this.txtOutput0.Text = calc(list);
            this.txtOutput_SQL_0.Text = SQL_IN条件参数(list);
            this.txtOutput_SQL_1.Text = SQL_插入临时表(list);
            this.txtOutput_SQL_2.Text = SQL_Insert语句(list);
            this.txtOutput_CSharp_0.Text = CSharp_插入List(list);
            this.txtOutput_CSharp_1.Text = CSharp_DataTable(list);
            this.txtOutput_CSharp_2.Text = CSharp_rDataTable(list);
            this.txtOutput_CSharp_3.Text = CSharp_rDataTableHowe(list);
            this.txtOutput_CSharp_4.Text = CSharp_CopyModel(list);
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

        private string CSharp_DBReader(List<string> l)
        {
            string tabSymbol = "\t";

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"public System.Data.DataTable CreateDataTable_X()");
            sb.AppendLine("{");

            sb.Append(tabSymbol).AppendLine("System.Data.DataTable dt = new System.Data.DataTable();");

            foreach (var item in l)
            {
                sb.Append(tabSymbol).AppendLine("dt.Columns.Add(\"" + item + "\");");
            }

            sb.Append(tabSymbol).AppendLine("return dt;");
            sb.AppendLine("}");

            sb.AppendLine("public System.Data.DataTable GetDataTable(IEnumerable<XModel> l)");
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

            sb.Append(tabSymbol).AppendLine("return dt;");
            sb.Append(tabSymbol).AppendLine("}");
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
    }
}
