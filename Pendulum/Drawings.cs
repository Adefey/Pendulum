using SharpGL;
using SharpGL.SceneGraph;
using System;
using System.Drawing;

namespace Pendulum
{
    internal class Drawings
    {
        private OpenGL GL;
        private int ScreenWidth;
        private int ScreenHeight;
        private Vertex eyePos, centrePos, topPos;

        public Drawings(OpenGLControl Control, Vertex eyePos, Vertex centrePos, Vertex topPos)
        {
            GL = Control.OpenGL;
            ScreenWidth = Control.Width;
            ScreenHeight = Control.Height;
            GL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            GL.ClearColor(1f, 1f, 1f, 1f);
            GL.Enable(OpenGL.GL_DEPTH_TEST);
            //Initlight();
            this.eyePos = eyePos;
            this.centrePos = centrePos;
            this.topPos = topPos;
            InitCamera(eyePos, centrePos, topPos);
        }

        private void Initlight()
        {
            float[] ambientColor = new float[4];
            ambientColor[0] = 0.5f;
            ambientColor[1] = 0.5f;
            ambientColor[2] = 0.5f;
            ambientColor[3] = 1f;
            float[] sourceColor = new float[4];
            sourceColor[0] = 1f;
            sourceColor[1] = 1f;
            sourceColor[2] = 1f;
            sourceColor[3] = 1f;
            float[] position = new float[4];
            position[0] = 0;
            position[1] = 0;
            position[2] = 50;
            position[3] = 1f;
            float[] dir = { 0, 0, 0 };

            GL.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, ambientColor);
            GL.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, sourceColor);
            GL.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, sourceColor);
            GL.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, position);
            GL.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPOT_DIRECTION, dir);
            GL.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, dir);

            GL.Enable(OpenGL.GL_LIGHTING);
            GL.Enable(OpenGL.GL_LIGHT0);

            GL.ShadeModel(OpenGL.GL_SMOOTH);
        }

        public void InitCamera(Vertex eyePos, Vertex centrePos, Vertex topPos)
        {
            GL.MatrixMode(OpenGL.GL_PROJECTION);
            GL.LoadIdentity();
            GL.Perspective(60, ScreenWidth / (float)ScreenHeight, 1, 200);

            GL.MatrixMode(OpenGL.GL_MODELVIEW);
            GL.LoadIdentity();
            GL.LookAt(eyePos.X, eyePos.Y, eyePos.Z,
                        centrePos.X, centrePos.Y, centrePos.Z,
                        topPos.X, topPos.Y, topPos.Z);
        }

        public void Render(float rot)
        {
            GL.Flush();
            GL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            GL.LoadIdentity();
            InitCamera(eyePos, centrePos, topPos);
            GL.Rotate(0, 0, rot);

        }

        public void DrawRod(Vertex UpCoord, Vertex LowCoord, double radius, Color color)
        {
            GL.Begin(OpenGL.GL_QUAD_STRIP);
            GL.Color(color);
            for (double i = 0; i <= 2.1 * Math.PI; i += 0.2)
            {
                GL.Vertex(radius * Math.Cos(i) + UpCoord.X, UpCoord.Y, radius * Math.Sin(i) + UpCoord.Z);
                GL.Vertex(radius * Math.Cos(i) + LowCoord.X, LowCoord.Y, radius * Math.Sin(i) + LowCoord.Z);
            }
            GL.End();
        }
    }
}
