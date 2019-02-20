using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace COD03 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e) {
            SqlConnection cn = new SqlConnection();
            //TODO 完成連線字串，開啟資料庫，並進行資料錄設定

        }
        //@防止後面的'/'變成轉義字元
        DataTable dt = new DataTable();

        public static String con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|datadirectory|TestDB.mdf;Integrated Security=True;Connect Timeout=30";
        SqlConnection cn = new SqlConnection(con);
        SqlCommand cmd;
        SqlDataReader read;
        String sql;
        SqlDataAdapter da = new SqlDataAdapter("Select * from Salary", con);


        private void btnClean_Click(object sender, EventArgs e) {

            txtId.Text = "";
            txtName.Text = "";
            txtBasesalary.Text = "";
            txtBonus.Text = "";
            txtDeduct.Text = "";
            txtId.Enabled = true;
        }

        void Edit(string sqlstr) {
            cmd = new SqlCommand(sqlstr, cn);
            //TODO 完成連線字串，開啟資料庫，執行傳入的SQL指令

        }



        private void btnAdd_Click(object sender, EventArgs e) {
            //TODO 新增資料
            String readtxtid = txtId.Text;
            String str1 = readtxtid.Substring(0, 1);
            bool boo_confirm = confirm(readtxtid);
            bool boo2 = id_contains(readtxtid);

            if (!( str1.Equals("A") || str1.Equals("B") || str1.Equals("C") || str1.Equals("D") || str1.Equals("E") || str1.Equals("F") )) {
                MessageBox.Show("Id格式錯誤");
            } else if (readtxtid.Length != 6) {
                MessageBox.Show("Id格式錯誤");
            } else if (boo_confirm = false) {
                MessageBox.Show("Id格式錯誤");
            } else if (boo2 == false) {
                MessageBox.Show("Id格式錯誤");
            } else {
                sql = @"INSERT INTO SALARY (Id,name,basesalary,bonus,deduct,subtotal,status,picture)" +
                "VALUES (" + txtId.Text + "," + txtName.Text + "," + txtBasesalary.Text + "," + txtBonus.Text + "," + txtDeduct.Text + ","
                + ( int.Parse(txtBasesalary.Text) + int.Parse(txtBonus.Text) - int.Parse(txtDeduct.Text) ) + ",NULL ," + txtName + ")";
                Edit(sql);
            }


        }

        private bool confirm(String text) {
            bool boo = true;
            int total = 0;
            int ten_digits = 0;

            switch (text.Substring(0, 1)) {
                case "A":
                    total += 1;
                    break;
                case "B":
                    total += 2;
                    break;
                case "C":
                    total += 3;
                    break;
                case "D":
                    total += 4;
                    break;
                case "E":
                    total += 5;
                    break;
                case "F":
                    total += 6;
                    break;
            }

            int k = 0;
            for (int i = 1; i < 5; i++) {
                k = int.Parse(text.Substring(i, 1));
                k = k * ( i + 1 );
                total += k;
            }

            ten_digits = total / 10;
            total = total % 10;
            total += ten_digits;

            if (total == int.Parse(text.Substring(5, 1))) {
                return true;
            } else {
                return false;
            }
        }

        private bool id_contains(String id) {

            da.Fill(dt);
            txtId.Text = dt.Rows[1][0].ToString();

            for (int i = 0; i < dt.Rows.Count; i++) {
                if (dt.Rows[i][0].ToString().Contains(id)) {
                    return false;
                }
            }

            return true;
        }


        private void btnUpdate_Click(object sender, EventArgs e) {
            //TODO 更新資料





        }

        private void btnDelete_Click(object sender, EventArgs e) {
            //TODO 刪除資料





        }

        private void btnShow_Click(object sender, EventArgs e) {
            //TODO 開啟Form2表單
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
            }
            pictureBox1.ImageLocation = openFileDialog.FileName;
        }
    }
}
