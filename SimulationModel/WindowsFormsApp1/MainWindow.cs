using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainWindow : Form
    {
        List<Pipeline> lisrR;
        public Queue<Job> queue;
        Graphics gPanel;
        public MainWindow()
        {
            InitializeComponent();
            gPanel = pictureBox1.CreateGraphics();
            lisrR = null;
            queue = new Queue<Job>();
            textBox1.Text = "700";
            textBox2.Text = "233";
            textBox3.Text = "8";
            textBox4.Text = "1";

            //textBox6.Text = "1000 сек";

            label5.BackColor = Color.FromArgb(100,Color.White);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int int1) && int.TryParse(textBox2.Text, out int int2) 
                                                          && int.TryParse(textBox3.Text, out int int3) && int.TryParse(textBox4.Text, out int int4)&& queue!=null)
            {
                var copyQueue = new Queue<Job>(queue);
                lisrR = Computer.StartConveer(copyQueue, int4, int3, int1/int2);
            }
            pictureBox1.Invalidate();

            textBox6.Text = Math.Round((AddTask.commads / AddTask.taks /** 0.000001)*/), 2).ToString();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (lisrR != null)
            {
                Pen p1 = new Pen(Color.Gray, 1);
                Pen p2 = new Pen(Color.Gray, 2);
                Pen p3 = new Pen(Color.Black, 3);
                Pen p4 = new Pen(Color.Black, 3);
                p4.StartCap = LineCap.ArrowAnchor;
                Pipeline LasrR = new Pipeline() { Cash = CashParameter.OutCash, CashN = 0, 
                    Conveer = ConveerParameter.NoOperation, ConveerN = 0 };
                pictureBox1.Width = 20 * lisrR.Count + 40;
                gPanel = e.Graphics;
                gPanel.Clear(Color.White);
                gPanel.DrawLine(p1, new Point(0, 0), new Point(0, 250));
                gPanel.DrawLine(p1, new Point(20 , 0), new Point(20 , 250));
                gPanel.DrawLine(p2, new Point(0, 100), new Point(20 , 100));
                gPanel.DrawLine(p1, new Point(0, 200), new Point(20 , 200));
                var i = 2;
                var hBrush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Gray, Color.White);
                foreach (var item in lisrR)
                {
                    gPanel.DrawLine(p1, new Point(20 * i, 0), new Point(20 * i, 250));
                    gPanel.DrawLine(p2, new Point(20 * (i - 1), 100), new Point(20 * i, 100));
                    gPanel.DrawLine(p1, new Point(20 * (i - 1), 200), new Point(20 * i, 200));
                    if (LasrR.Conveer != item.Conveer || LasrR.ConveerN != item.ConveerN)
                    {
                        if (LasrR.ConveerN != 0)
                        {
                            if (LasrR.Conveer == ConveerParameter.Request)
                            {
                                gPanel.DrawLine(p3, new Point(20 * (i - 1), 100), new Point(20 * (i - 1), 80));
                            }
                            else if (LasrR.Conveer != ConveerParameter.NoOperation)
                            {
                                gPanel.DrawLine(p3, new Point(20 * (i - 1), 100), new Point(20 * (i - 1), 120));
                            }
                        }
                        if (item.ConveerN != 0)
                        {
                            if (item.Conveer == ConveerParameter.Request)
                            {
                                gPanel.DrawString(item.ConveerN.ToString(), this.Font, Brushes.Black, new Point(20 * (i - 1), 80));
                                gPanel.DrawLine(p3, new Point(20 * (i - 1), 100), new Point(20 * (i - 1), 80));
                            }
                            else if (item.Conveer != ConveerParameter.NoOperation)
                            {
                                gPanel.DrawString(item.ConveerN.ToString(), this.Font, Brushes.Black, new Point(20 * (i - 1), 80));
                                gPanel.DrawLine(p3, new Point(20 * (i - 1), 100), new Point(20 * (i - 1), 120));
                            }
                        }
                    }
                    if (item.ConveerN != 0)
                    {

                        if (item.Conveer == ConveerParameter.Request)
                        {
                            gPanel.DrawLine(p3, new Point(20 * (i - 1), 80), new Point(20 * (i), 80));
                        }
                        else if (item.Conveer != ConveerParameter.NoOperation)
                        {
                            if (item.Conveer == ConveerParameter.ControlOperation)
                            {
                                gPanel.FillClosedCurve(hBrush, new PointF[] { new PointF(20 * (i - 1) + 3, 103),
                                    new PointF(20 * (i) - 3, 103), new PointF(20 * (i) - 3, 117), new PointF(20 * (i - 1) + 3, 117) });
                            }
                            gPanel.DrawLine(p3, new Point(20 * (i - 1), 120), new Point(20 * (i), 120));
                        }
                    }
                    if (LasrR.CashN != item.CashN)
                    {
                        if (LasrR.Cash == CashParameter.InCash || item.Cash == CashParameter.InCash)
                        {
                            if (item.Cash == CashParameter.InCash)
                            {
                                gPanel.DrawString(item.CashN.ToString(), this.Font, Brushes.Black, new Point(20 * (i - 1), 160));
                            }
                            gPanel.DrawLine(p3, new Point(20 * (i - 1), 200), new Point(20 * (i - 1), 180));
                        }
                    }
                    if (item.CashN != 0)
                    {
                        gPanel.FillClosedCurve(hBrush, new PointF[] { new PointF(20 * (i - 1) + 3, 183), 
                            new PointF(20 * (i) - 3, 183), new PointF(20 * (i) - 3, 197), new PointF(20 * (i - 1) + 3, 197) });
                        gPanel.DrawLine(p3, new Point(20 * (i - 1), 180), new Point(20 * (i), 180));
                    }
                    if (item.ListQuest.Count != 0)
                    {
                        gPanel.DrawLine(p4, new Point(20 * (i - 1), 50), new Point(20 * (i - 1), 100));
                        gPanel.DrawString(item.ListQuest[0].ToString(), this.Font, Brushes.Black, new Point(20 * (i - 1), 50));
                        var i2 = 1;
                        for (var i1 = 1;i1< item.ListQuest.Count; i1++)
                        {
                            if (i1 < 4)
                            {
                                gPanel.DrawLine(p4, new Point(20 * (i - 1), 50 - i1 * 10), new Point(20 * (i - 1), 60 - i1 * 10));
                                gPanel.DrawString(item.ListQuest[i1].ToString(), this.Font, Brushes.Black, new Point(20 * (i - 1), 50 - i1 * 10));
                            }
                            else
                            {
                                gPanel.DrawString(item.ListQuest[i1].ToString(), this.Font, Brushes.Black, new Point(20 * (i - 1) + i2 * 10, 10));
                                i2++;
                            }
                        }
                    }
                    i++;
                    LasrR = item;
                }
                if (LasrR.ConveerN != 0)
                {
                    if (LasrR.Conveer == ConveerParameter.Request)
                    {
                        gPanel.DrawLine(p3, new Point(20 * (i - 1), 100), new Point(20 * (i - 1), 80));
                    }
                    else if (LasrR.Conveer != ConveerParameter.NoOperation)
                    {
                        gPanel.DrawLine(p3, new Point(20 * (i - 1), 100), new Point(20 * (i - 1), 120));
                    }
                }
                if (LasrR.CashN != 0)
                {
                    gPanel.DrawLine(p3, new Point(20 * (i - 1), 200), new Point(20 * (i - 1), 180));
                }
                gPanel.DrawLine(p2, new Point(20 * (i - 1), 100), new Point(20 * i, 100));
                gPanel.DrawLine(p1, new Point(20 * (i - 1), 200), new Point(20 * i, 200));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new AddTask(this);
            form.ShowDialog();
        }
    }
}
