using SharpGL.SceneGraph;
using System;
using System.Windows.Forms;

namespace Pendulum
{
    public partial class Form1 : Form
    {
        private Physics physics;
        public Form1()
        {
            InitializeComponent();
            physics = new Physics(openGLControl1, new Vertex(0, -2, 9), new Vertex(0, -2, 0), new Vertex(0, 1, 0));
            physics.ProcessPhysics();
        }
        private void SyncLabels()
        {
            physics.SetParams(trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);
            label5.Text = "phi = " + Math.Round(physics.GetAngle(), 3).ToString() + " градусов";
            label7.Text = "T = " + Math.Round(physics.GetT(), 3).ToString() + " сек";
            label10.Text = "t = " + Math.Round((double)physics.stopWatch.ElapsedMilliseconds / 1000, 3).ToString() + " сек";
            label11.Text = trackBar4.Value.ToString() + " градусов";
            label12.Text = Math.Round(((double)trackBar3.Value / 10), 3).ToString() + " метров";
            label13.Text = "Координаты грузов: " + "\r\n" + "Первый: " + physics.GetW1().ToString() + " метров" + "\r\n" + "Второй: " + physics.GetW2().ToString() + " метров";
            label14.Text = "Центр масс: " + Math.Round(physics.GetC(), 3).ToString() + " метров";
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            physics.SetParams(trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);
            SyncLabels();
            physics.ProcessPhysics();
        }

        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        { 
            SyncLabels();
            physics.ProcessPhysics();
        }

        private void button1_Click(object sender, EventArgs e) //старт
        {
            physics.stopWatch.Restart();
        }

        private void button2_Click(object sender, EventArgs e) //стоп
        {
            physics.setW(0);
            physics.stopWatch.Stop();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            SyncLabels();
            physics.ProcessPhysics();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            SyncLabels();
            physics.ProcessPhysics();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            SyncLabels();
            physics.ProcessPhysics();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            SyncLabels();
            physics.ProcessPhysics();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Красный цилиндр - первый груз" + "\r\n" + "Зеленый цилиндр - второй груз" + "\r\n" + "Фиолетовая отметка - центр масс", "Инфо");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            physics.stopWatch.Stop();
            physics.stopWatch.Reset();
            trackBar4.Value = 5;
            trackBar3.Value = 5;
            trackBar2.Value = 8;
            trackBar1.Value = 4;
            SyncLabels();
            physics.ProcessPhysics();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new AboutBox1().Show();
        }
    }
}
