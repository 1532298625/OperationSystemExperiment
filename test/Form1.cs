using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace test
{
    public partial class Form1 : Form
    {
        public static int empty = 2;
        public static int applenum = 0;
        public static int orangenum = 0;
        //盘子互斥信号量，最多只有两个水果
        public static Semaphore plate = new Semaphore(2, 2);
        //苹果同步信号量
        public static Semaphore apple = new Semaphore(0, 2);
        //橘子同步信号量
        public static Semaphore orange = new Semaphore(0, 2);
        //对盘子的操作放水果互斥信号量
        public static Semaphore nutexput = new Semaphore(1, 1);
        public static Semaphore nutexout = new Semaphore(1, 1);

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread Father = new Thread(father);
            Thread Mother = new Thread(mother);
            Thread Daughter = new Thread(daughter);
            Thread Son = new Thread(son);
            fatherhand_put.Hide();
            fatherhand_return.Hide();
            motherhand_put.Hide();
            motherhand_return.Hide();
            daughterhand_get.Hide();
            daughterhand_return.Hide();
            sonhand_get.Hide();
            sonhand_return.Hide();
            pbx1.Hide();
            pbx2.Hide();
            pbx3.Hide();
            pbx4.Hide();



            Father.Start();
            Son.Start();
            Mother.Start();
            Daughter.Start();
            
        }

        public void details()
        {
            Console.WriteLine("当前盘中苹果数量：" + applenum + "   橘子数量：" + orangenum);
        }

        public void father()
        {
            while (true)
            {
                plate.WaitOne();
                nutexput.WaitOne();
                Showfather();
                nutexput.Release();
                Thread.Sleep(500);
            }
        }

        private void movedadhandput()
        {
            fatherhand_put.Show();
            int startx = 150;
            int starty = 12;
            while (startx <= 200 && starty <= 62)
            {
                startx = startx + 1;
                starty = starty + 1;
                fatherhand_put.Location = new Point(startx, starty);
                Thread.Sleep(50);
            }
            
            fatherhand_put.Hide();
            fatherhand_put.Location = new Point(150, 12);
        }

        private void movedadhandreturn()
        {
            fatherhand_return.Show();
            int startx = 200;
            int starty = 62;
            while (startx >= 150 && starty >= 12)
            {
                startx = startx - 1;
                starty = starty - 1;
                fatherhand_return.Location = new Point(startx, starty);
                Thread.Sleep(50);
            }

            fatherhand_return.Hide();
            fatherhand_return.Location = new Point(200, 62);
        }

        private void Showfather()
        {
            
            if (empty == 0)
            {
                details();
                Console.WriteLine("父亲无法向盘子里放入苹果");
                if (orangenum == 2)
                {
                    pbx1.Hide();
                    pbx2.Hide();
                    pbx3.Show();
                    pbx4.Show();
                }
                if (orangenum == 1 && applenum == 1)
                {
                    pbx1.Hide();
                    pbx4.Hide();
                    pbx3.Show();
                    pbx2.Show();
                }
                if (applenum == 2)
                {
                    pbx3.Hide();
                    pbx4.Hide();
                    pbx1.Show();
                    pbx2.Show();
                }
                Thread.Sleep(500);
            }

            
            if (empty > 0 && empty <= 2 && applenum < 2)
            {
                movedadhandput();
                if (orangenum == 1 && applenum == 0)
                {
                    pbx1.Hide();
                    pbx4.Hide();
                    pbx2.Show();
                    pbx3.Show();
                    Thread.Sleep(500);
                    applenum++;
                    empty--;
                    Console.WriteLine("父亲放入了一个苹果");
                    details();
                    apple.Release();
                }
                if (applenum == 1 && orangenum == 0)
                {
                    pbx3.Hide();
                    pbx4.Hide();
                    pbx1.Show();
                    pbx2.Show();
                    Thread.Sleep(500);
                    applenum++;
                    empty--;
                    Console.WriteLine("父亲放入了一个苹果");
                    details();
                    apple.Release();
                }
                if (empty == 2)
                {
                    
                    pbx1.Show();
                    pbx2.Hide();
                    pbx3.Hide();
                    pbx4.Hide();
                    Thread.Sleep(500);
                    applenum++;
                    empty--;
                    Console.WriteLine("父亲放入了一个苹果");
                    details();
                    apple.Release();
                }
                movedadhandreturn();
            }
        }

        public void mother()
        {
            while (true)
            {
                plate.WaitOne();
                nutexput.WaitOne();
                Showmother();
                nutexput.Release();
                Thread.Sleep(5000);
            }
        }

        private void Showmother()
        {
            if (empty == 0)
            {
                details();
                Console.WriteLine("母亲无法向盘子里放入橘子");
                if (orangenum == 2 && applenum == 0)
                {
                    pbx1.Hide();
                    pbx2.Hide();
                    pbx3.Show();
                    pbx4.Show();
                }
                if (orangenum == 1 && applenum == 1)
                {
                    pbx1.Hide();
                    pbx2.Show();
                    pbx3.Show();
                    pbx4.Hide();
                }
                if (applenum == 2)
                {
                    pbx1.Show();
                    pbx2.Show();
                    pbx3.Hide();
                    pbx4.Hide();
                }
                Thread.Sleep(500);
            }
            if (empty > 0 && empty <= 2)
            {
                motherhand_put.Show();
                int startx = 150;
                int starty = 240;
                while (startx <= 200 && starty >= 190)
                {
                    startx = startx + 1;
                    starty = starty - 1;
                    motherhand_put.Location = new Point(startx, starty);
                    Thread.Sleep(50);
                }
                motherhand_put.Hide();
                motherhand_put.Location = new Point(150, 240);
                if (orangenum == 1 && applenum == 0)
                {
                    pbx1.Hide();
                    pbx2.Hide();
                    pbx3.Show();
                    pbx4.Show();
                    Thread.Sleep(500);
                    orangenum++;
                    empty--;
                    Console.WriteLine("母亲放入了一个橘子");
                    details();
                    orange.Release();
                }
                if (applenum == 1 && orangenum == 0)
                {
                    pbx1.Hide();
                    pbx2.Show();
                    pbx3.Show();
                    pbx4.Hide();
                    Thread.Sleep(500);
                    orangenum++;
                    empty--;
                    Console.WriteLine("母亲放入了一个橘子");
                    details();
                    orange.Release();
                }

                if (empty == 2)
                {
                    pbx1.Hide();
                    pbx2.Hide();
                    pbx4.Hide();
                    pbx3.Show();
                    Thread.Sleep(500);
                    orangenum++;
                    empty--;
                    Console.WriteLine("母亲放入了一个橘子");
                    details();
                    orange.Release();
                }
                motherhand_return.Show();
                int startx2 = 200;
                int starty2 = 190;
                while (startx2 >= 150 && starty2 <= 240)
                {
                    startx2 = startx2 - 1;
                    starty2 = starty2 + 1;
                    motherhand_return.Location = new Point(startx2, starty2);
                    Thread.Sleep(50);
                }
                
                motherhand_return.Hide();
                motherhand_return.Location = new Point(200, 190);
            }
        }

        public void daughter()
        {
            while (true)
            {
                apple.WaitOne();
                nutexout.WaitOne();
                showdaughter();
                nutexout.Release();
                Thread.Sleep(500);
            }
        }

        public void movedaughterreturn()
        {
            daughterhand_return.Show();
            int startx = 220;
            int starty = 62;
            while (startx <= 270 && starty >= 12)
            {
                startx = startx + 1;
                starty = starty - 1;
                daughterhand_return.Location = new Point(startx, starty);
                Thread.Sleep(50);
            }
            
            daughterhand_return.Hide();
            daughterhand_return.Location = new Point(220, 62);
        }

        private void movedaughterget()
        {
            daughterhand_get.Show();
            int startx = 270;
            int starty = 12;
            while (startx >= 220 && starty <= 62)
            {
                startx = startx - 1;
                starty = starty + 1;
                daughterhand_get.Location = new Point(startx, starty);
                Thread.Sleep(50);
            }
            daughterhand_get.Hide();
            daughterhand_get.Location = new Point(270, 12);
        }

        private void showdaughter()
        {   
            if (applenum >= 1 && applenum <= 2)
            {
                movedaughterget();
                if (applenum == 2)
                {
                    pbx1.Show();
                    pbx2.Hide();
                    pbx3.Hide();
                    pbx4.Hide();
                    Thread.Sleep(500);
                    applenum--;
                    empty++;
                    Console.WriteLine("女儿吃了一个苹果");
                    details();
                    plate.Release();
                }
                if (applenum == 1 && orangenum == 1)
                {
                    pbx1.Hide();
                    pbx2.Hide();
                    pbx3.Show();
                    pbx4.Hide();
                    Thread.Sleep(500);
                    applenum--;
                    empty++;
                    Console.WriteLine("女儿吃了一个苹果");
                    details();
                    plate.Release();
                }
                if (applenum == 1 && orangenum == 0)
                {
                    pbx1.Hide();
                    pbx2.Hide();
                    pbx3.Hide();
                    pbx4.Hide();
                    Thread.Sleep(500);
                    applenum--;
                    empty++;
                    Console.WriteLine("女儿吃了一个苹果");
                    details();
                    plate.Release();
                }
                movedaughterreturn();
            }
            if (applenum == 0)
            {
                details();
                Console.WriteLine("女儿无法从盘子里拿苹果吃");
                if (orangenum == 2)
                {
                    pbx1.Hide();
                    pbx2.Hide();
                    pbx3.Show();
                    pbx4.Show();
                }
                if (orangenum == 1)
                {
                    pbx1.Hide();
                    pbx2.Hide();
                    pbx3.Show();
                    pbx4.Hide();
                }
                if (applenum == 0 && orangenum == 0)
                {
                    pbx1.Hide();
                    pbx2.Hide();
                    pbx3.Hide();
                    pbx4.Hide();
                }
                Thread.Sleep(500);
            }

        }

        public void son()
        {
            while (true)
            {
                orange.WaitOne();
                nutexout.WaitOne();
                showson();
                nutexout.Release();
                Thread.Sleep(500);
            }
        }

        public void movesonreturn()
        {
            sonhand_return.Show();
            int startx = 220;
            int starty = 190;
            while (startx <= 270 && starty <= 240)
            {
                startx = startx + 1;
                starty = starty + 1;
                sonhand_return.Location = new Point(startx, starty);
                Thread.Sleep(50);
            }
            sonhand_return.Hide();
            sonhand_return.Location = new Point(220, 190);
        }

        public void movesonget()
        {
            sonhand_get.Show();
            int startx = 270;
            int starty = 240;
            while (startx >= 220 && starty >= 190)
            {
                startx = startx - 1;
                starty = starty - 1;
                sonhand_get.Location = new Point(startx, starty);
                Thread.Sleep(50);
            }
            sonhand_get.Hide();
            sonhand_get.Location = new Point(270, 240);
            
        }

        private void showson()
        {   
            if (orangenum >= 1 && orangenum <= 2)
            {
                movesonget();
                if (orangenum == 2)
                {
                    pbx1.Hide();
                    pbx2.Hide();
                    pbx3.Show();
                    pbx4.Hide();
                    Thread.Sleep(500);
                    orangenum--;
                    empty++;
                    Console.WriteLine("儿子吃了一个橘子");
                    details();
                    plate.Release();
                }
                if (applenum == 1 && orangenum == 1)
                {
                    pbx1.Hide();
                    pbx2.Show();
                    pbx3.Hide();
                    pbx4.Hide();
                    Thread.Sleep(500);
                    orangenum--;
                    empty++;
                    Console.WriteLine("儿子吃了一个橘子");
                    details();
                    plate.Release();
                }
                if (orangenum == 1 && applenum == 0)
                {
                    pbx1.Hide();
                    pbx2.Hide();
                    pbx3.Hide();
                    pbx4.Hide();
                    Thread.Sleep(500);
                    orangenum--;
                    empty++;
                    Console.WriteLine("儿子吃了一个橘子");
                    details();
                    plate.Release();
                }
                movesonreturn();
            }
            if (orangenum == 0)
            { 
                details();
                Console.WriteLine("儿子无法从盘子里拿橘子吃");
                if (applenum == 2 && orangenum == 0)
                {
                    pbx1.Show();
                    pbx2.Show();
                    pbx3.Hide();
                    pbx4.Hide();
                }
                if (applenum == 1 && orangenum == 0)
                {
                    pbx1.Hide();
                    pbx2.Show();
                    pbx3.Hide();
                    pbx4.Hide();
                }
                if (applenum == 0 && orangenum == 0)
                {
                    pbx1.Hide();
                    pbx2.Hide();
                    pbx3.Hide();
                    pbx4.Hide();
                }
                Thread.Sleep(500);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //退出程序
            System.Environment.Exit(0);
        }
    }
}
