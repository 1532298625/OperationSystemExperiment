using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer_rePaint(object sender, EventArgs e)
        {
            //控制车的移动
            int move_x = panel1.Size.Width / 100;
            int move_y = panel1.Size.Height / 100;
            int car_x = pictureBox1.Location.X + move_x;
            int car_y = pictureBox1.Location.Y + move_y;
            //判断车是否超过窗体区域，超过则重新回到左上角
            if (car_x > this.Width - pictureBox1.Size.Width || car_y > this.Height - pictureBox1.Size.Height)
            {
                car_x = 0;
                car_y = 0;
            }
            //重新设置小车的位置
            pictureBox1.Location = new Point(car_x, car_y);

        }

        private void button_start_click(object sender, EventArgs e)
        {
            //开启定时器，开始小车的移动
            timer1.Start();
        }

        private void button_stop_click(object sender, EventArgs e)
        {
            //暂停定时器，暂停小车的移动
            timer1.Stop();
        }

        private void button_speedUp_click(object sender, EventArgs e)
        {   
            //当刷新的时间间隔大于10ms
            if(timer1.Interval > 20)
            {
                timer1.Interval -= 10;
            }
            //当刷新的时间间隔大于5ms小于10ms
            else if(timer1.Interval > 5)
            {
                timer1.Interval -= 1;
            }
            else
            {
                timer1.Interval = 5;
            }
        }

        private void button_decelerate_click(object sender, EventArgs e)
        {
            //当刷新的时间间隔小于10ms
            if (timer1.Interval < 300)
            {
                timer1.Interval += 50;
            }
            else
            {
                timer1.Interval = 350;
            }
        }

        private void button_terminate_click(object sender, EventArgs e)
        {
            //退出程序
            System.Environment.Exit(0);
        }

        private void notifyIcon_mouseDoubleClick(object sender, MouseEventArgs e)
        {
            //显示本Winform程序
            this.Show();
        }

        private void form_mouseClick(object sender, MouseEventArgs e)
        {
            //显示本WinForm程序
            this.Show();
        }

        private void form_load(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(0, 0);//设置初始位置为最左上角
        }

        private void form_formClosing(object sender, FormClosingEventArgs e)
        {
            //设置点击“X”后不会退出程序
            e.Cancel = true;
            //隐藏本WinForm程序
            this.Hide();
        }

        private void toolStripMenuItem_restore(object sender, EventArgs e)
        {
            //显示本WinForm程序
            this.Show();
        }

        private void toolStripMenuItem_exit(object sender, EventArgs e)
        {
            //退出程序
            System.Environment.Exit(0);
        }
    }
}
