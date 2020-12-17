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
            KeyPreview = true;
            physics = new Physics(openGLControl1, new Vertex(0, -2, 9), new Vertex(0, -2, 0), new Vertex(0, 1, 0));
            physics.ProcessPhysics();
        }
        private void Reset()
        {
            physics.stopWatch.Stop();
            physics.stopWatch.Reset();
            trackBar1.Value = 4;
            trackBar2.Value = 8;
            trackBar3.Value = 1;
            trackBar4.Value = 5;
            trackBar5.Value = 0;
            SyncLabels();
            textBox1.Text = "";
            physics.ProcessPhysics();
        }

        private void SyncLabels()
        {
            physics.SetParams(trackBar1.Value, trackBar2.Value, 5f + 0.5f * trackBar3.Value, trackBar4.Value, trackBar5.Value);
            label5.Text = "phi = " + Math.Round(physics.GetAngle(), 3).ToString() + " градусов";
            //textBox1.Text = "T = " + Math.Round(physics.GetT(), 3).ToString() + " сек"; раскомментируйте чтобы период выводился
            label10.Text = "t = " + Math.Round((double)physics.stopWatch.ElapsedMilliseconds / 1000, 3).ToString() + " сек";
            label11.Text = trackBar4.Value.ToString() + " градусов";
            label12.Text = Math.Round(physics.getL(), 3).ToString() + " метров";
            label13.Text = "Координаты грузов:\r\nПервый: " + Math.Round(physics.GetW1(), 3).ToString() + " метров\r\nВторой: " + Math.Round(physics.GetW2(), 3).ToString() + " метров";
            label14.Text = "Центр масс: " + Math.Round(physics.GetC(), 3).ToString() + " метров";
            label15.Text = "Смещение вверх на " + Math.Round((physics.getL() * (float)trackBar5.Value / 10), 5).ToString() + " метров";
            if (physics.GetC() <= 0)
            {
                Reset();
                MessageBox.Show("Нельзя, чтобы центр масс был выше точки закрепа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
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

        private void button3_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Красный цилиндр - первый груз" + "\r\n" + "Зеленый цилиндр - второй груз" + "\r\n" + "Фиолетовая отметка - центр масс", "Инфо");
            try
            {
                float userInput = float.Parse(textBox1.Text);
                if (Math.Abs(physics.GetT() - userInput) < 0.1)
                {
                    MessageBox.Show("Правильно!", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("Неравильно, пересчитайте ответ. Должно было получиться " + Math.Round(physics.GetT(), 4).ToString(), "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка ввода!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e) //сброс
        {
            Reset();
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

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            SyncLabels();
            physics.ProcessPhysics();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e) //о программе
        {
            new AboutBox1().Show();
        }

        private void начальныеУсловияToolStripMenuItem_Click(object sender, EventArgs e) //начальные условия
        {
            MessageBox.Show("Примите g = 9,8145 м/c^2 и pi = 3,1415", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                MessageBox.Show("Adefe ninekeem ARM\r\nbeyond expectations\r\nvk.com/adefe vk.com/ninekeem", "Авторы", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
