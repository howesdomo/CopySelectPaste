using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Windows.Controls.Extensions
{
    public class StrLogicalComparer : Comparer<object>
    {
        [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string x, string y);

        public override int Compare(object x, object y)
        {
            return StrCmpLogicalW(x.ToString(), y.ToString());
        }
    }
}