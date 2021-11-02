﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.Framework.UI;
using System.Drawing.Imaging;
using System.IO;



namespace HotelManagement
{
    public partial class NewBranch : UserControl
    {
        Dictionary<BunifuMetroTextbox, string> txtBoxList = new Dictionary<BunifuMetroTextbox, string>();
        public NewBranch()
        {
            InitializeComponent();
        }

        private void NewBranch_Load(object sender, EventArgs e)
        {

        }

        private void TextBoxEnter(object sender, EventArgs e)
        {
            var txtBox = sender as BunifuMetroTextbox;
            txtBox.BorderColorIdle = Color.FromArgb(231, 228, 228);
            txtBox.ForeColor = Color.Black;
            if (!txtBoxList.ContainsKey(txtBox))
            {
                txtBoxList.Add(txtBox, txtBox.Text);
            }
            txtBoxList.TryGetValue(txtBox, out string defualtText);
            if (txtBox.Text == defualtText)
            {
                txtBox.Text = null;
            }


        }
        private void TextBoxLeave(object sender, EventArgs e)
        {
            var txtBox = sender as BunifuMetroTextbox;

            if (txtBox.Text == null || txtBox.Text == "")
            {
                txtBoxList.TryGetValue(txtBox, out string defualtText);
                txtBox.Text = defualtText;
                txtBox.ForeColor = Color.DarkGray;
            }


        }
        private enum Status
        {
            Green,
            Red,
            blue
        };
        private static int txtCount = 0;
        private bool ValidationFlag = false;
        private void PanelStatus(Control Panel, string text, Status status)
        {
            BunifuCircleProgressbar prgb = null;
            BunifuCustomLabel lbl = null;
            Panel.Visible = true;
            foreach (Control item in Panel.Controls)
            {
                if (item is BunifuCustomLabel)
                {
                    lbl = item as BunifuCustomLabel;

                }
                else
                {
                    prgb = item as BunifuCircleProgressbar;

                }

            }

            lbl.Text = text;
            if (status == Status.Red)
            {
                prgb.ProgressColor = Color.Red;
                lbl.ForeColor = Color.Red;


            }
            else if (status == Status.Green)
            {

                prgb.ProgressColor = Color.Green;
                lbl.ForeColor = Color.Green;


            }
            else
            {
                prgb.ProgressColor = Color.Blue;
                lbl.ForeColor = Color.Blue;

            }




        }
        //bool selectPic = false;

        // Image logo;
        //ImageFormat logoFormat = ImageFormat.Jpeg;
        private void btnLogo_Click(object sender, EventArgs e)
        {
            fileDialog.Filter = "Image Files (jpg , png , bmp ) | *.jpg;*.png;*.bmp |All Files |*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK) 
            {
                //selectPic = true;
                picLogo.Image = Image.FromFile(fileDialog.FileName);
          

            }



        }
        private byte[] ConvertPicToByte(Image img )
        {

            //logo = picLogo.Image;
            //logoFormat = logo.RawFormat;
            // selectPic = false;
            MemoryStream Ms = new MemoryStream();
            img.Save(Ms, img.RawFormat);

            //logo.Save(Ms, logoFormat);

            return Ms.GetBuffer();

        }
        private void TextBoxColor(BunifuMetroTextbox txtBox, Status status)
        {
            if (status == Status.Red)
            {
                txtBox.BorderColorIdle = Color.Red;


            }
            else if (status == Status.Green)
            {

                txtBox.BorderColorIdle = Color.FromArgb(231, 228, 228);
            }
            else
            {
                txtBox.BorderColorIdle = Color.FromArgb(128, 128, 128);
            }

        }
        private bool TextBoxCheck(BunifuMetroTextbox txtBox, string txt)
        {
            if (txtBox.Text == txt || txtBox.Text == "")
            {
                TextBoxColor(txtBox, Status.Red);
                return false;
            }
            else if (txt == "National Code")
            {
                TextBoxColor(txtBox, Status.blue);
                txtCount++;
                return true;
            }
            else
            {
                TextBoxColor(txtBox, Status.Green);
                txtCount++;
                return true;

            }
        }
        private void TextBoxClearText()
        {


            txtCode.Text = "Code";
            txtCode.ForeColor = Color.Gray;


            txtBranchName.Text = "Branch Name";
            txtBranchName.ForeColor = Color.Gray;



            txtOwner.Text = "Owner";
            txtOwner.ForeColor = Color.Gray;

            txtRate.Text = "Rate";
            txtRate.ForeColor = Color.Gray;

            txtTel.Text = "Tel";
            txtTel.ForeColor = Color.Gray;

            txtCity.Text = "City";
            txtCity.ForeColor = Color.Gray;

            txtAddress.Text = "Address";
            txtAddress.ForeColor = Color.Gray;


        }
        private void TextBoxClearBorderColor()
        {

            TextBoxColor(txtCode, Status.Green);
            TextBoxColor(txtBranchName, Status.Green);
            TextBoxColor(txtOwner, Status.Green);
            TextBoxColor(txtRate, Status.blue);
            TextBoxColor(txtCity, Status.Green);

            TextBoxColor(txtAddress, Status.Green);
            TextBoxColor(txtTel, Status.Green);

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
           // panelConfigStatus.Visible = true;

            //if (HotelDatabase.Branch.Insert(txtCode.Text, txtOwner.Text, txtBranchName.Text, txtRate.Text, ConvertPicToByte(picLogo.Image), txtTel.Text, cmbState.SelectedItem.ToString(), txtCity.Text, txtAddress.Text))
            //{
            //    MessageBox.Show("Added");
            //}
            //else
            //{
            //    panelConfigError.Visible = true;

            //}




            //-----------   Validation   -------------

            TextBoxCheck(txtCode, "Code");
            TextBoxCheck(txtBranchName, "Branch Name");
            TextBoxCheck(txtOwner, "Owner");
            TextBoxCheck(txtRate, "Rate");
            TextBoxCheck(txtTel, "Tel");
            TextBoxCheck(txtCity, "City");
            TextBoxCheck(txtAddress, "Address");

            if (txtCount == 7)
            {
                ValidationFlag = true;
                //if (txtPassword.Text != txtConPass.Text)
                //{
                //    PanelStatus(panelStatusUserInfo, "Password Not Match", Status.Red);

                //    TextBoxColor(txtPassword, Status.Red);
                //    TextBoxColor(txtConPass, Status.Red);



                //}
                //else
                //{
                //    ValidationFlag = true;
                //    TextBoxColor(txtPassword, Status.Green);
                //    TextBoxColor(txtConPass, Status.Green);
                //}


            }
            else
            {

                PanelStatus(panelConfigError, "Please Fill The Blank", Status.Red);
            }
            txtCount = 0;


            if (ValidationFlag)
            {
                ValidationFlag = false;
                var res = HotelDatabase.Branch.Insert(txtCode.Text, txtOwner.Text, txtBranchName.Text, txtRate.Text, ConvertPicToByte(picLogo.Image), txtTel.Text, cmbState.SelectedItem.ToString(), txtCity.Text, txtAddress.Text);
                if (res>0)
                {
                    PanelStatus(panelConfigError, "Action Completed Successfully", Status.Green);
                }
                else
                {
                    PanelStatus(panelConfigError, "Unable To Complete Action", Status.Red);

                }

            }


            //panelConfigStatus.Visible = false;
        }

      
    }
}
