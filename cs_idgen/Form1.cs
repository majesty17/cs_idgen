using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cs_idgen
{
    public partial class Form1 : Form
    {
        private Image image_man = null;
        public Form1()
        {
            InitializeComponent();
        }


        //生成
        private void button_gen_Click(object sender, EventArgs e)
        {
            //1，看看照片有没有载入
            if (image_man == null) {
                MessageBox.Show("请先载入照片", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //2，拿到各项信息&验证是否ok
            string name = textBox_name.Text.Trim();
            string gender = textBox_gender.Text.Trim();
            string ethnic = textBox_ethnic.Text.Trim();
            string birthday = textBox_birth.Text.Trim();
            string address = textBox_address.Text.Trim();
            string id = textBox_id.Text.Trim();
            string sign = textBox_sign.Text.Trim();
            string date_from = textBox_date_from.Text.Trim();
            string date_to = textBox_date_to.Text.Trim();

            
            if (id.Length != 18 || birthday.Length != 8 || date_from.Length != 8 || date_to.Length != 8) {
                MessageBox.Show("日期8位，身份证号18位！");
                return;
            }
            if (!Tools.dateVerify(birthday) || !Tools.dateVerify(date_to) || !Tools.dateVerify(date_from)) {
                MessageBox.Show("日期不合法");
                return;
            }
            if (id.Substring(6, 8).Equals(birthday) == false) {
                MessageBox.Show("身份证号与生日信息不符！");
                return;
            }
            if (Tools.idVerify(id) == false) {
                MessageBox.Show("身份证信息不合法");
                return;
            }

            //3,开始画图
        }

        //点击添加照片
        private void pictureBox_who_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledia = new OpenFileDialog();
            if (openfiledia.ShowDialog() == DialogResult.OK) {
                image_man=Image.FromFile(openfiledia.FileName);
                if (image_man == null) {
                    MessageBox.Show("打开文件失败！");
                    return;
                }
                pictureBox_who.Image = image_man;
            }
        }


        //保存
    }


    static class Tools {
        //看看id是不是合法的身份证id
        public static bool idVerify(string id) {
            if(id.Length!=18)
                return false;
            for (int i = 0; i < id.Length - 1; i++) {
                if (Char.IsNumber(id[i]) == false)
                    return false;
            }
            char last_char=id[17];
            if(Char.IsNumber(last_char)==false && last_char!='X' & last_char!='x')
                return false;
            //TODO:校验码
            return true;
        }
        //看看日期是否合法
        public static bool dateVerify(string date) {
            if (date.Length != 8)
                return false;
            for (int i = 0; i < date.Length; i++) {
                if (Char.IsNumber(date[i]) == false)
                    return false;
            }
            int year = Int32.Parse(date.Substring(0, 4));
            int month = Int32.Parse(date.Substring(4, 2));
            int day = Int32.Parse(date.Substring(6, 2));
            if ( month < 1 || month > 12 || day < 1 || day > 31)
                return false;


            return true;
        }
    }
}
