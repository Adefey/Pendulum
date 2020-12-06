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
            physics = new Physics(openGLControl1, new Vertex(0, -2, 10), new Vertex(0, -2, 0), new Vertex(0, 1, 0));
            physics.ProcessPhysics();
        }

        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            physics.ProcessPhysics();
        }

        private void button1_Click(object sender, EventArgs e) //старт
        {
            openGLControl1.FrameRate = 40;
            physics.stopWath.Start();
        }

        private void button2_Click(object sender, EventArgs e) //стоп
        {
            openGLControl1.FrameRate = 0;
            physics.stopWath.Stop();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            physics.SetWeightCoords(trackBar1.Value, trackBar2.Value);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            physics.SetWeightCoords(trackBar1.Value, trackBar2.Value);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            physics.ProcessPhysics();
        }
    }
}
