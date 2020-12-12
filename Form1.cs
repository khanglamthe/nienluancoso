﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FordBell
{
    public  partial class Form1 : Form
    {
        const int vocung = 9999999;
        public Form1()
        {
            InitializeComponent();
        }


        FB a;
       

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                openFileDialog1.Filter = "Tập tin đồ thị | *.txt";
                openFileDialog1.ShowDialog();
               
                a = new FB(openFileDialog1.FileName);

                if (a.KiemTraVoHuong())
                {
                    start.Items.Clear();
                    end.Items.Clear();
                    for (int i = 1; i <= a.SoDinh; i++)
                    {
                        start.Items.Add(i);
                        end.Items.Add(i);
                    }
                    end.Enabled = start.Enabled = true;
                    button2.Enabled = true;
                    end.Text = a.SoDinh.ToString();
                    start.Text = "1";


                    pb.Image = a.Paint();
                    if (a.DFS(1))
                    {
                        lienthong.Text = "Đồ thị Liên thông";

                    }
                    else
                        lienthong.Text = "Đồ thị Không Liên thông";
                    tam.matran = a.ToMaTrix;
                    tam.sodinh = a.SoDinh;
                    this.Width = 953;
                    button5.Location = new Point(854, 503);
                    this.Text = "Tìm đường đi ngắn nhất bằng Bellman-Ford";
                    ToListView();
                }
                else
                {
                    MessageBox.Show("Đồ thị nhập vào không phải đồ thị Vô Hướng!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     button1_Click(sender, e);
                    
                }

                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Đồ thị nhập vào không hợp lê!!! \r\n"+ex.Message, "Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                
            }

            this.Height = 578;
           

            button4.Enabled = contextMenuStrip1.Enabled = true;
          
                

        }


        void ToListView()
        {
           if(a.SoDinh>0)
               {
                dt.Height = 50;
                dt.Columns.Add("Begin", "Begin");
                dt.Columns.Add("End", "End");
                dt.Columns.Add("Distance", "Distance");
                dt.Rows.Clear();

                dt.Columns[0].Width = 40;
                dt.Columns[1].Width = 40;
                dt.Columns[2].Width = 60;

                int row = 0;
                int[,] danhdau = new int[a.SoDinh, a.SoDinh];

                for (int i = 0; i < a.SoDinh; i++)
                {

                    for (int j = 0; j < a.SoDinh; j++)
                    {
                        if (a.ToMaTrix[i, j] != vocung && danhdau[i, j] == 0)
                        {
                            dt.Rows.Add();
                            if (dt.Height<250)
                            {
                                dt.Height += 20;
                            }
                           
                            
                            dt[0, row].Value = i + 1;
                            dt[1, row].Value = j + 1;
                            dt[2, row].Value = a.ToMaTrix[i, j];
                            row++;
                            danhdau[i, j] = danhdau[j, i] = 1;

                        }
                    }
                
            }

               }
           
        }
      

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
            if(start.Text!=end.Text)
            {
                pb.Image = a.Paint();


                string st = a.XuaKQ(int.Parse(start.Text) - 1, int.Parse(end.Text) - 1, pb.Image);



                st = "Đường đi ngắn nhất từ " + start.Text + " đến " + end.Text + " : " + st + "  ||  ";
                if (a.KhoangCach[Convert.ToInt32(end.Text) - 1] == vocung)
                {
                    st += "Độ dài: ∞";
                }
                else
                {
                    st += "Độ dài: " + a.KhoangCach[Convert.ToInt32(end.Text) - 1].ToString();
                    pb.Image = a.DuongDiPic(int.Parse(start.Text) - 1, int.Parse(end.Text) - 1, pb.Image);
                }
                tb.Text = st;
            }
            else
            {
                MessageBox.Show("Bạn đang ở tại đó", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
    }
           
          
        }
        //lưu ảnh của đồ thị
        void SaveImage(System.Drawing.Imaging.ImageFormat format)
        {
            try
            {
                
                saveFileDialog1.Filter = "Ảnh đồ thị | *.png" ;
                saveFileDialog1.ShowDialog();
                
                pb.Image.Save(saveFileDialog1.FileName ,format);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Width = 953;
            this.Text = "Tìm đường đi ngắn nhất bằng Bellman-Ford";
            button5.Location = new Point(854, 503);
            this.Height = 578;
            Nhapmatran a = new Nhapmatran();
            button4.Enabled = contextMenuStrip1.Enabled = true;
            a.ShowDialog();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button4.Enabled = contextMenuStrip1.Enabled = false;
            
        }

        public void button4_Click(object sender, EventArgs e)
        {
            a = new FB(tam.sodinh, tam.matran);

            start.Items.Clear();
            end.Items.Clear();
            for (int i = 1; i <= a.SoDinh; i++)
            {
                start.Items.Add(i);
                end.Items.Add(i);
            }
            end.Enabled = start.Enabled = true;
            button2.Enabled = true;
            end.Text = a.SoDinh.ToString();
            start.Text = "1";


            pb.Image = a.Paint();
            if (a.DFS(1))
            {
                lienthong.Text = "Đồ thị Liên thông";

            }
            else
                lienthong.Text = "Đồ thị Không Liên thông";
            a.Paint();
            ToListView();
        }
        //sửa dữ liệu ma trận 
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tam.matran = a.ToMaTrix;
            tam.sodinh = a.SoDinh;
            suamatran sua = new suamatran();
            sua.ShowDialog();
        }
        //cập nhật lại đồ thị
        private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }


        private void dt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pb.Image = a.Paint();
            
            int tam=e.RowIndex;
            PointF p1 = a.ViTriVe[Convert.ToInt32(dt[0, tam].Value)-1];
            PointF p2 = a.ViTriVe[Convert.ToInt32(dt[1, tam].Value)-1];
            int ts = a.ToMaTrix[Convert.ToInt32(dt[0, tam].Value)-1, Convert.ToInt32(dt[1, tam].Value)-1];
            pb.Image=a.LineTo(p1, p2, pb.Image, ts, Color.Red);
            
        }

       
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //lưu lại hình ảnh đồ thị
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveImage(System.Drawing.Imaging.ImageFormat.Png);
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tam.matran = a.ToMaTrix;
            tam.sodinh = a.SoDinh;
            suamatran sua = new suamatran();
            sua.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            
            SaveImage(System.Drawing.Imaging.ImageFormat.Png);

        }
    }
}
