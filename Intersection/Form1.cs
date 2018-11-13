using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace affine_transform
{

    public partial class Form1 : Form
    {

        private Graphics g;
        private PointF startPoint, endPoint = PointF.Empty;
        bool allowDrawing = true;

        private Pen penColor = Pens.BlueViolet;
        private Pen penColor2 = Pens.MediumVioletRed;
        private Pen penColor3 = new Pen(Color.Red, 2);

        private LinkedList<PolyNode> polygon1 = new LinkedList<PolyNode>();
        private LinkedList<PolyNode> polygon2 = new LinkedList<PolyNode>();
        private LinkedList<PolyNode> polygon3 = new LinkedList<PolyNode>();

        public class PolyNode
        {
            public PointF p;
            public bool isIntersection;
            public float angle;
            public LinkedListNode<PolyNode> otherNode;

            public PolyNode(PointF _p, bool _is_intersecton = false, float _angle = 0)
            {
                p = _p;
                isIntersection = _is_intersecton;
                angle = _angle;
            }
        }

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            penColor3.Width = 2;
        }

        //INTERFACE

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && allowDrawing)
            {
                if (polyChk.Checked && polygon1.Count == 0)
                {
                    startPoint = e.Location;
                    endPoint = e.Location;
                    polygon1.AddLast(new PolyNode(startPoint));
                }
                else if (poly2Chk.Checked && polygon2.Count == 0)
                {
                    startPoint = e.Location;
                    endPoint = e.Location;
                    polygon2.AddLast(new PolyNode(startPoint));
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && allowDrawing)
            {
                endPoint = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && allowDrawing)
            {
                if (poly2Chk.Checked)
                {
                    polygon2.AddLast(new PolyNode(endPoint));
                    startPoint = endPoint;
                }
                else if (polyChk.Checked)
                {
                    polygon1.AddLast(new PolyNode(endPoint));
                    startPoint = endPoint;
                }
            }

            pictureBox1.Invalidate();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            allowDrawing = true;
            g.Clear(Color.White);
            pictureBox1.Invalidate();
            polygon1.Clear();
            polygon2.Clear();
            polygon3.Clear();
            startPoint = endPoint = Point.Empty;
        }

        private void poly2Chk_CheckedChanged(object sender, EventArgs e)
        {
            if (poly2Chk.Checked)
            {
                polyChk.Checked = false;
                if (polygon2.Count > 0)
                    startPoint = polygon2.Last.Value.p;
            }
        }

        private void polyChk_CheckedChanged(object sender, EventArgs e)
        {
            if (polyChk.Checked)
            {
                poly2Chk.Checked = false;
                if (polygon1.Count > 0)
                    startPoint = polygon1.Last.Value.p;
            }
        }

        private void applyBtn_Click(object sender, EventArgs e)
        {
            allowDrawing = false;

            findIntersections();

            // Find inner point
            var cur_node = polygon1.First;
            for (var test_node = polygon1.First; test_node != null; test_node = test_node.Next)
            {
                if (isInside(polygon2.Select(el => el.p).ToArray(), test_node.Value.p))
                {
                    cur_node = test_node;
                    break;
                }
            }

            PointF start_point = cur_node.Value.p;

            while (true)
            {
                polygon3.AddLast(new PolyNode(cur_node.Value.p));

                var next_node = cur_node.Next ?? cur_node.List.First;

                if (next_node.Value.p == start_point)
                    break;

                if (next_node.Value.isIntersection)
                    cur_node = next_node.Value.otherNode;
                else
                    cur_node = next_node;
            }

            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (polygon1.Count > 1)
                e.Graphics.DrawPolygon(penColor, polygon1.Select(el => el.p).ToArray());

            if (polygon2.Count > 1)
                e.Graphics.DrawPolygon(penColor2, polygon2.Select(el => el.p).ToArray());

            if (polygon3.Count > 1)
                e.Graphics.DrawPolygon(penColor3, polygon3.Select(el => el.p).ToArray());

            if (polyChk.Checked)
                e.Graphics.DrawLine(penColor, startPoint, endPoint);
            else
                e.Graphics.DrawLine(penColor2, startPoint, endPoint);
        }

        

        // ALGORITHMS

        private void findIntersections()
        {
            PointF pol1center, pol2center;
            sortPointsClockwise(ref polygon1, out pol1center);
            sortPointsClockwise(ref polygon1, out pol2center);

            var pol1 = new PolyNode[polygon1.Count + 1];
            var pol2 = new PolyNode[polygon2.Count + 1];

            polygon1.CopyTo(pol1, 0);
            polygon2.CopyTo(pol2, 0);

            pol1[polygon1.Count] = pol1.First();
            pol2[polygon2.Count] = pol2.First();

            for (int i = 0; i < pol1.Count() - 1; ++i)
            {
                for (int j = 0; j < pol2.Count() - 1; ++j)
                {
                    PointF intersection_point = findIntersection(pol1[i].p, pol1[i + 1].p, pol2[j].p, pol2[j + 1].p);
                    if (intersection_point.X != -1)
                    {
                        PolyNode ip1 = new PolyNode(intersection_point, true);
                        PolyNode ip2 = new PolyNode(intersection_point, true);

                        var t1 = polygon1.Find(pol1[i]);
                        while (t1.Next != null && t1.Next.Value.isIntersection && distance(pol1[i + 1].p, t1.Next.Value.p) > distance(pol1[i + 1].p, ip1.p))
                            t1 = t1.Next;
                        var n1 = polygon1.AddAfter(t1, ip1);

                        var t2 = polygon2.Find(pol2[j]);
                        while (t2.Next != null && t2.Next.Value.isIntersection && distance(pol2[j + 1].p, t2.Next.Value.p) > distance(pol2[j + 1].p, ip2.p))
                            t2 = t2.Next;
                        var n2 = polygon2.AddAfter(t2, ip2);

                        n1.Value.otherNode = n2;
                        n2.Value.otherNode = n1;

                    }
                }
            }
        }

        private float distance(PointF p1, PointF p2)
        {
            return (float)Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
        }

        private void sortPointsClockwise(ref LinkedList<PolyNode> pointArr, out PointF avgCenter)
        {
            avgCenter = new PointF((float)pointArr.Average(el => el.p.X), (float)pointArr.Average(el => el.p.Y));

            for (var curNode = pointArr.First; curNode != null; curNode = curNode.Next)
                curNode.Value.angle = (float)Math.Atan2(curNode.Value.p.Y - avgCenter.Y, curNode.Value.p.X - avgCenter.X);

            LinkedList<PolyNode> tempLinkedList = new LinkedList<PolyNode>(pointArr);
            pointArr.Clear();
            IEnumerable<PolyNode> orderedEnumerable = tempLinkedList.OrderByDescending(p => p.angle).AsEnumerable();
            foreach (var oe in orderedEnumerable)
                pointArr.AddLast(oe);
        }

        bool isInside(PointF[] polygon, PointF p)
        {
            int n = polygon.Length;
            if (n < 3) return false;

            PointF extreme = new PointF(pictureBox1.Width, p.Y);

            int count = 0, i = 0;
            do
            {
                int next = (i + 1) % n;
                PointF intersection = findIntersection(polygon[i], polygon[next], p, extreme);
                if (intersection.X != -1)
                {
                    // If the point 'p' is colinear with line segment 'i-next',
                    // then check if it lies on segment. If it lies, return true,
                    // otherwise false
                    if (orientation(polygon[i], p, polygon[next]) == 0)
                        return onSegment(polygon[i], p, polygon[next]);

                    count++;
                }
                i = next;
            } while (i != 0);

            // Return true if count is odd, false otherwise
            return count % 2 == 1;
        }

        // 0 --> p, q and r are colinear
        // 1 --> Clockwise
        // 2 --> Counterclockwise
        int orientation(PointF p, PointF q, PointF r)
        {
            float val = (q.Y - p.Y) * (r.X - q.X) -
                      (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0;  // colinear
            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }

        bool onSegment(PointF p, PointF q, PointF r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                    q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;
            return false;
        }

        PointF findIntersection(PointF p0, PointF p1, PointF p2, PointF p3)
        {
            PointF i = new PointF(-1, -1);
            PointF s1 = new PointF();
            PointF s2 = new PointF();
            s1.X = p1.X - p0.X;
            s1.Y = p1.Y - p0.Y;
            s2.X = p3.X - p2.X;
            s2.Y = p3.Y - p2.Y;
            float s, t;
            s = (-s1.Y * (p0.X - p2.X) + s1.X * (p0.Y - p2.Y)) / (-s2.X * s1.Y + s1.X * s2.Y);
            t = (s2.X * (p0.Y - p2.Y) - s2.Y * (p0.X - p2.X)) / (-s2.X * s1.Y + s1.X * s2.Y);

            if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
            {
                i.X = p0.X + (t * s1.X);
                i.Y = p0.Y + (t * s1.Y);

            }
            return i;
        }

    }
}
