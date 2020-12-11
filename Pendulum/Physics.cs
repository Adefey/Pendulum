using SharpGL;
using SharpGL.SceneGraph;
using System;
using System.Diagnostics;
using System.Drawing;

namespace Pendulum
{
    internal class Physics
    {
        private Drawings GL;
        private const float g = 9.8145f;
        private float rot;
        private float A; //В градусах
        private float T;
        private float w; //ммм... Наверное в радианах в секунду.
        private float phi0; //В радианах
        private float t;
        private float length;
        private float w1Coord;
        private float w2Coord;
        private float C; //центр масс
        private float offset; //смещение
        public Stopwatch stopWatch;
        public Physics(OpenGLControl Control, Vertex eyePos, Vertex centrePos, Vertex topPos)
        {
            length = 0.55f;
            w1Coord = 0.4f * length;
            w2Coord = 0.8f * length;
            C = (w1Coord + w2Coord) / 2;
            A = 5;
            //T = 2 * (float)Math.PI * (float)Math.Sqrt(C / g);
            T = 2 * (float)Math.PI * (float)Math.Sqrt(Math.Abs(C) / g);
            w = 0;
            phi0 = A / 2 * (float)Math.PI / 180; //автонахождение начальной фазы. Амплитуду перевести в радианы и делить на 2.
            stopWatch = new Stopwatch();
            GL = new Drawings(Control, eyePos, centrePos, topPos);
        }

        public void SetParams(float w1, float w2, float l, float r, float o)
        {
            A = r;
            length = l / 10;
            offset = o / 10 * length;
            w1Coord = length * w1 / 10 - offset;
            w2Coord = length * w2 / 10 - offset;
            C = (w1Coord + w2Coord) / 2;
            //T = 2 * (float)Math.PI * (float)Math.Sqrt(C / g);
            T = 2 * (float)Math.PI * (float)Math.Sqrt(Math.Abs(C) / g);
            if (T != 0)
            {
                w = 2 * (float)Math.PI / T;
            }
            else
            {
                w = 0;
            }
        }

        public float GetAngle()
        {
            return rot;
        }

        public float GetT()
        {
            return T;
        }

        public float GetC()
        {
            return C;
        }

        public float GetW1()
        {
            return w1Coord;
        }

        public float GetW2()
        {
            return w2Coord;
        }

        public double getL()
        {
            return length;
        }

        public void setW(float W)
        {
            w = W;
        }

        public void ProcessPhysics()
        {
            //Система имеет одну степень свободы (грузы прочно закреплены). Параметр - rot - угол отклонения
            //rot вычисляется по классической формуле гармонических колебаний
            //phi = A * sin(w * t + phi0), где phi - угол отклонения, A - амплитуда, w - угловая частота, t - время в секундах, phi0 - начальное отклонение
            t = (float)stopWatch.ElapsedMilliseconds / 1000;
            rot = A * (float)Math.Sin(w * t + phi0);
            GL.Render();
            GL.DrawStand(new Vertex(0, 0, 0));
            GL.GL.Rotate(0, 0, rot);
            GL.DrawRod(new Vertex(0, offset * 10, 0), new Vertex(0, -length * 10 + offset * 10, 0), 0.2f, Color.Blue);
            GL.DrawRod(new Vertex(0, -w1Coord * 10 - 0.1f, 0), new Vertex(0, -w1Coord * 10 + 0.1f, 0), 0.3f, Color.Red);
            GL.DrawRod(new Vertex(0, -w2Coord * 10 - 0.1f, 0), new Vertex(0, -w2Coord * 10 + 0.1f, 0), 0.3f, Color.Green);
            GL.DrawRod(new Vertex(0, -C * 10 - 0.05f, 0), new Vertex(0, -C * 10 + 0.05f, 0), 0.21f, Color.BlueViolet);
        }
    }
}
