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

            this.Title = $"{this.Title} - {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()}";

            initEvent();
        }

        private void initEvent()
        {
            this.txtInput.TextChanged += txtInput_TextChanged;

            // CheckBox
            this.cbTabNewLine.Click += checkBox_Click;
            this.cbDistinct.Click += checkBox_Click;

            // RadioButton
            // 除空行
            this.rbNotRemove.Checked += radioButton_Checked;
            this.rbRemoveNullLine.Checked += radioButton_Checked;
            this.rbRemoveWhiteSpace.Checked += radioButton_Checked;

            // 除内容空格
            // this.rbNotTrim.Click += radioButton_Click; // 先命中 Checked, 再命中 Click, 故只注册 Check 就好了
            this.rbNotTrim.Checked += radioButton_Checked;
            this.rbTrimStart.Checked += radioButton_Checked;
            this.rbTrimEnd.Checked += radioButton_Checked;
            this.rbTrim.Checked += radioButton_Checked;
            this.rbTrimAdv.Checked += radioButton_Checked;
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
                txtInputInfo.Text = "空值";
                this.txtOutputInfo.Text = string.Empty;
                return;
            }
            
            
            StringBuilder sbInfo = new StringBuilder();           

            var query = text.Split('\n').AsQueryable<string>();
            sbInfo.Append($"源文件共 {query.Count()} 行; ");



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
            this.txtOutput1.Text = SQL_IN条件参数(list);
            this.txtOutput2.Text = SQL_插入临时表(list);
            this.txtOutput3.Text = CSharp_插入List(list);
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
    }
}
