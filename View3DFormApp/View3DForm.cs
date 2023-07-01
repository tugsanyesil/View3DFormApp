using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace View3DFormApp
{
    public partial class View3DForm : Form
    {
        public View3DForm()
        {
            InitializeComponent();
        }

        int BitmapSize = 300;
        Bitmap Bitmap;
        Point3D Rotation;
        Point Center;
        Point MouseLocation;
        readonly float Cube_Edge = 3f;
        readonly float ZoomStep = 1f;
        float Zoom = 10f;

        private void View3DForm_Load(object sender, EventArgs e)
        {
            BitmapSize = 300;
            Bitmap = new Bitmap(BitmapSize, BitmapSize);
            Rotation = new Point3D(0, 0, 0);
            Center = new Point(BitmapSize / 2, BitmapSize / 2);
            View3DPictureBox.MouseWheel += View3DPictureBox_MouseWheel;
            Bitmap_Refresh();
        }

        private void View3DPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            MouseLocation = e.Location;
            View3DPictureBox.MouseMove += View3DPictureBox_MouseMove;
        }

        private void View3DPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            View3DPictureBox.MouseMove -= View3DPictureBox_MouseMove;
        }

        private void View3DPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                Rotation.Z += e.Location.X - MouseLocation.X;
            }
            else
            {
                Rotation.Y -= e.Location.X - MouseLocation.X;
                Rotation.X += e.Location.Y - MouseLocation.Y;
            }

            MouseLocation = e.Location;

            Bitmap_Refresh();
        }

        private void View3DPictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            Zoom = Math.Max(Zoom + Math.Sign(e.Delta) * ZoomStep, ZoomStep);
            Bitmap_Refresh();
        }

        void InfoLabel_Refresh()
        {
            InfoLabel.Text = $"Rotation\nX = {Rotation.X}\nY = {Rotation.Y}\nZ = {Rotation.Z}\nZoom = {Zoom}";
        }

        void Bitmap_Clear()
        {
            using (Graphics g = Graphics.FromImage(Bitmap))
            {
                g.Clear(Color.White);
            }
        }

        void Bitmap_Paint_Axes()
        {
            PointF fromX = Img2Coord(new Point(0, Center.Y));
            PointF toX = Img2Coord(new Point(Bitmap.Width, Center.Y));

            PointF fromY = Img2Coord(new Point(Center.X, 0));
            PointF toY = Img2Coord(new Point(Center.X, Bitmap.Height));

            var fromX3 = Rot(new Point3D(fromX.X, fromX.Y, 0));
            var toX3 = Rot(new Point3D(toX.X, toX.Y, 0));

            var fromY3 = Rot(new Point3D(fromY.X, fromY.Y, 0));
            var toY3 = Rot(new Point3D(toY.X, toY.Y, 0));

            var fromI = Coord2Img(fromX3.PointF);
            var toI = Coord2Img(toX3.PointF);

            var fromJ = Coord2Img(fromY3.PointF);
            var toJ = Coord2Img(toY3.PointF);

            PointF area = new PointF(3, 3);
            var xi = Rot(new Point3D(1, 0, 0)).X; xi = Math.Abs(xi);
            var yi = Rot(new Point3D(0, 1, 0)).Y; yi = Math.Abs(yi);

            using (Graphics g = Graphics.FromImage(Bitmap))
            {
                g.DrawLine(Pens.Black, fromI, toI);
                g.DrawLine(Pens.Black, fromJ, toJ);

                for (float x = Math.Min(toX3.X, fromX3.X); x <= Math.Max(toX3.X, fromX3.X); x += xi)
                {
                    g.FillEllipse(Brushes.Black, Coord2ImgI(x) - area.X / 2, Coord2ImgJ((toX3.Y - fromX3.Y) * ((x - fromX3.X) / (toX3.X - fromX3.X)) + fromX3.Y) - area.Y / 2, area.X, area.Y);
                }

                for (float y = Math.Min(toY3.Y, fromY3.Y); y <= Math.Max(toY3.Y, fromY3.Y); y += yi)
                {
                    g.FillEllipse(Brushes.Black, Coord2ImgI((toY3.X - fromY3.X) * ((y - fromY3.Y) / (toY3.Y - fromY3.Y)) + fromY3.X) - area.X / 2, Coord2ImgJ(y) - area.Y / 2, area.X, area.Y);
                }
            }
        }

        void Bitmap_Paint_Cube()
        {
            using (Graphics g = Graphics.FromImage(Bitmap))
            {
                g.DrawLines(Pens.Red, Get_Cube_Coordinates(Cube_Edge).ConvertAll(p => Coord2Img(Rot(p).PointF)).ToArray());
            }
        }

        void Bitmap_Refresh()
        {
            Bitmap_Clear();
            Bitmap_Paint_Axes();
            Bitmap_Paint_Cube();
            View3DPictureBox.Image = Bitmap;
            View3DPictureBox.Refresh();
            InfoLabel_Refresh();
        }

        List<Point3D> Get_Cube_Coordinates(float edge) => new List<Point3D> {
                                                                                new Point3D(-edge, -edge, -edge),
                                                                                new Point3D(-edge, -edge, edge),
                                                                                new Point3D(-edge, edge, edge),
                                                                                new Point3D(-edge, edge, -edge),
                                                                                new Point3D(-edge, -edge, -edge),

                                                                                new Point3D(edge, -edge, -edge),
                                                                                new Point3D(edge, -edge, edge),
                                                                                new Point3D(edge, edge, edge),
                                                                                new Point3D(edge, edge, -edge),
                                                                                new Point3D(edge, -edge, -edge),

                                                                                new Point3D(edge, -edge, edge),
                                                                                new Point3D(-edge, -edge, edge),
                                                                                new Point3D(-edge, -edge, -edge),
                                                                                new Point3D(edge, -edge, -edge),
                                                                                new Point3D(edge, edge, -edge),

                                                                                new Point3D(edge, edge, edge),
                                                                                new Point3D(-edge, edge, edge),
                                                                                new Point3D(-edge, edge, -edge),
                                                                                new Point3D(edge, edge, -edge)
                                                                            };

        Point3D Rot(Point3D p)
        {
            float x, y, z;

            x = p.X; y = p.Y;
            p.X = (float)(x * Math.Cos(Rotation.Z * Math.PI / 180) - y * Math.Sin(Rotation.Z * Math.PI / 180));
            p.Y = (float)(y * Math.Cos(Rotation.Z * Math.PI / 180) + x * Math.Sin(Rotation.Z * Math.PI / 180));

            x = p.X; z = p.Z;
            p.X = (float)(x * Math.Cos(Rotation.Y * Math.PI / 180) - z * Math.Sin(Rotation.Y * Math.PI / 180));
            p.Z = (float)(z * Math.Cos(Rotation.Y * Math.PI / 180) + x * Math.Sin(Rotation.Y * Math.PI / 180));

            y = p.Y; z = p.Z;
            p.Y = (float)(y * Math.Cos(Rotation.X * Math.PI / 180) - z * Math.Sin(Rotation.X * Math.PI / 180));
            p.Z = (float)(z * Math.Cos(Rotation.X * Math.PI / 180) + y * Math.Sin(Rotation.X * Math.PI / 180));

            return p;
        }

        PointF Img2Coord(Point bit) => new PointF(Img2CoordX(bit.X), Img2CoordY(bit.Y));

        float Img2CoordX(int i) => (i - Center.X) / Zoom;

        float Img2CoordY(int j) => (Center.Y - j) / Zoom;

        Point Coord2Img(PointF Coord) => new Point(Coord2ImgI(Coord.X), Coord2ImgJ(Coord.Y));

        int Coord2ImgI(float x) => (int)(x * Zoom + Center.X);

        int Coord2ImgJ(float y) => (int)(Center.Y - y * Zoom);
    }
}
