using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class EasyUiPage
    {
        private int _page { get; set; }

        private int _rows { get; set; }

        public int Page
        {
            get { if (_page == 0)_page = 1; return _page; }
            set { _page = value; }
        }
        public int Rows {
            get { if (_rows == 0)_rows = 10; return _rows; }
            set { _rows = value; }
        }
    }
}
