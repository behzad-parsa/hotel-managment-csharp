using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Bunifu.Framework.UI;

namespace HotelManagement
{
    public partial class ActivityItem : UserControl
    {
        public Activity Activity { get; set; } 
        public ActivityItem()
        {
            InitializeComponent();
        }

        public ActivityItem(Activity act)
        {
            InitializeComponent();
            Activity = act;

            lblDate.Text = Theme.LastTime(act.Date);
            lblDes.Text = act.Description;
            lblTitle.Text = act.Title.ToUpper();

        }

        private void ActivityItem_Load(object sender, EventArgs e)
        {
            this.MouseEnter += ActivityItem_MouseEnter;
            this.MouseLeave += ActivityItem_MouseLeave;
        }

        private void LabelColors(int red , int green , int blue )
        {
            foreach (var item in this.Controls)
            {
                if (item is BunifuCustomLabel)
                {
                    var lbl = item as BunifuCustomLabel;
                    lbl.ForeColor = Color.FromArgb(red, green, blue);

                }

            }
        }
        private void ActivityItem_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(255, 255, 255);
            LabelColors(116, 130, 154);
        }

        private void ActivityItem_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(75, 157, 214);
            LabelColors(255, 255, 255);
        }
    }
}
