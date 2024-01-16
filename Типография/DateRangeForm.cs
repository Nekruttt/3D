using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Типография
{
    public partial class DateRangeForm : Form
    {
        public DateRangeForm()
        {
            InitializeComponent();
        }

        private void DateRangeForm_Load(object sender, EventArgs e)
        {

        }

        public DateTime StartDate => dateTimePickerStartDate.Value;
        public DateTime EndDate => dateTimePickerEndDate.Value;

        private void btnOK_Click(object sender, EventArgs e)
        {
        }

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
