using Luna.Core.Internal.Lib.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luna.Core.Internal.Lib
{
    public  class User
    {
        public string AskInput(string Question)
        {
            var dlg = new InputDialog();
            dlg.Qeustion = Question;
            if(dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return dlg.Data;
            }


            return "";
        }

    }
}
