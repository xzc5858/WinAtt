using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAtt
{
    public partial class CmnLoadDataForm : Form
    {
        private Timer _timer;
        public IAsyncResult AsyncResult;

        public string LoadString;
        public CmnLoadDataForm()
        {
            this.components = null;
            this.LoadString = "";
            this.InitializeComponent();
            if (this.LoadString != "")
            {
                this.lblHint.Text = this.LoadString;
            }
            this.Cursor = Cursors.WaitCursor;
            this._timer = new Timer();
            this._timer.Interval = 50;
            this._timer.Tick += _timer_Tick;

            this._timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (this.AsyncResult.IsCompleted)
            {
                this._timer.Stop();
                this._timer.Dispose();
                base.Close();
            }
            else
            {
                if (progressBar1.Value < 100)
                    progressBar1.Value = progressBar1.Value + 1;
                else progressBar1.Value = 0;
            }
        }

        public CmnLoadDataForm(string LoadString)
        {
            this.components = null;

            this.InitializeComponent();
            this.lblHint.Text = LoadString;
            this.Cursor = Cursors.WaitCursor;
            this._timer = new Timer();
            this._timer.Interval = 50;
            this._timer.Tick += _timer_Tick;
            this._timer.Start();
        }

        ~CmnLoadDataForm()
        {
            if (this._timer != null)
            {
                if (this._timer.Enabled)
                {
                    this._timer.Stop();
                }
                this._timer.Dispose();
                this._timer = null;
            }
        }

    }
}
