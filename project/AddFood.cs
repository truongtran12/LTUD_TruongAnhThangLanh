﻿/*
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace project
{
    public partial class frmAddFood : Form
    {
        bool isFood = false;
        public frmAddFood()
        {
            InitializeComponent();
        }
        public frmAddFood(string nameT, string nameF, string sttT)
            : this()
        {
            loadFood();
            //chon gia tri goi y
            txtBan.Text = nameT;
            cbbFood.Text = nameF;
            txtSTT.Text = sttT;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //ham load thong tin
        private void loadFood()
        {
            DataProvider provider = new DataProvider();
            DataTable table = provider.loadAllFood();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                cbbFood.Items.Add(table.Rows[i][0].ToString());
            }
        }

        //Ham Mở bàn
        public void openTable()
        {
            DataProvider provider = new DataProvider();
            provider.ResetTable(txtBan.Text);
        }

        //Them mon moi
        public void addFood()
        {
            DataProvider provider = new DataProvider();
            provider.ThemMon(txtBan.Text,cbbFood.Text,Int16.Parse(cbbCount.Value.ToString()));
            //dong thoi tang total
            setTotal();
        }
        //Tang so luong mon len
        public void addCountFood()
        {
            DataProvider provider = new DataProvider();
            provider.TangSLMon(txtBan.Text, cbbFood.Text, Int16.Parse(cbbCount.Value.ToString()));
            //dong thoi tang total
            setTotal();
        }

        //khi nhan nut chap nhan them mon
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //Kiem tra ban trong k
                if (txtSTT.Text == "TRONG")
                {
                    //Neu ban trong thi mo ban moi va them mon
                    openTable();
                    addFood();
                    this.Close();
                }
                else if (txtSTT.Text == "ONLINE")
                {
                    //Ban dang hoat dong. chi them mon
                    isCountFood() ;
                    if (isFood == false)
                    {
                        //Neu mon chua co thi them mon
                        addFood();
                        this.Close();
                    }
                    else
                    {
                        //Neu mon co roi thi tang so luong
                        addCountFood();
                        this.Close();
                    }
                }
                MessageBox.Show("Thêm món thành công!", "Đã thêm",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch 
            {
                MessageBox.Show("Thêm món không thành công!", "Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        //Kiem tra xem mon da ton tai chua || neu co: true. khong co false
        public void isCountFood()
        {
            try
            {
                DataProvider provider = new DataProvider();
                DataTable table = provider.checkFoodTable(txtBan.Text, cbbFood.Text);
                if (Int16.Parse(table.Rows[0][3].ToString()) > 0)
                {
                    isFood = true;
                }
            }
            catch
            {
                isFood = false;
            }
        }

        //Ham tra ve don gia mon hien tai
        private float getDonGia()
        {
            DataProvider provider = new DataProvider();
            DataTable table = provider.getPrice(cbbFood.Text);
            return Int16.Parse(table.Rows[0][0].ToString());
        }

        //Set Tong tien
        private void setTotal()
        {
            float total = getDonGia() * (float)cbbCount.Value;
            DataProvider provider = new DataProvider();
            provider.setTotal(txtBan.Text, float.Parse(total.ToString()));
        }

        private void lbl1_Click(object sender, EventArgs e)
        {

        }
    }
}
