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

        private void Form1_Resize(object sender, EventArgs e)
        {
            physics.ProcessPhysics();
        }

        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            physics.ProcessPhysics();
            label5.Text = "phi = " + physics.GetAngle().ToString() + " градусов";
            label7.Text = "T = " + physics.GetT().ToString() + " сек";
            label10.Text = "t = " + ((double)physics.stopWath.ElapsedMilliseconds / 1000).ToString() + " сек";
        }

        private void button1_Click(object sender, EventArgs e) //старт
        {
            openGLControl1.FrameRate = 50;
            physics.stopWath.Restart();
        }

        private void button2_Click(object sender, EventArgs e) //стоп
        {
            openGLControl1.FrameRate = 0;
            physics.stopWath.Stop();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            physics.SetParams(trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);
            physics.ProcessPhysics();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            physics.SetParams(trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);
            physics.ProcessPhysics();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            physics.SetParams(trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);
            physics.ProcessPhysics();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            physics.SetParams(trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value);
            physics.ProcessPhysics();
        }
    }
}
