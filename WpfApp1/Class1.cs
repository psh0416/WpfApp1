using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp1
{
    class Class1 : Control
    {
        bool TextType = false;
        public Class1()
        {
            preSize.Width = 300;
            preSize.Height = 300;
        }

        Size preSize;
        protected override Size MeasureOverride(Size constraint)
        {
            pt = new Point(constraint.Width / 2.0, constraint.Height / 2.0);
            size = Math.Min(constraint.Width, constraint.Height) / 2.0;

            double ratio = Math.Min(constraint.Width, constraint.Height) / Math.Min(preSize.Width, preSize.Height);
            ptCenter.X = constraint.Width / 2.0 + (ptCenter.X - preSize.Width / 2.0) * ratio;
            ptCenter.Y = constraint.Height / 2.0 + (ptCenter.Y - preSize.Height / 2.0) * ratio;
            System.Diagnostics.Debug.WriteLine(ptCenter);
            preSize = constraint;
            return base.MeasureOverride(constraint);
        }
        Point ptCenter;
        Point pt;
        double size;
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            dc.DrawEllipse(Brushes.Black, new Pen(), pt, size - 30, size - 30);
            dc.DrawEllipse(Brushes.White, new Pen(), ptCenter, 2, 2);
            //dc.DrawEllipse(null, new Pen(Brushes.White,1), pt, size - 15, size - 15);
            var cb = new CombinedGeometry(GeometryCombineMode.Xor,
                                          new EllipseGeometry(pt, size, size),
                                         new EllipseGeometry(pt, size - 30, size - 30));
            dc.PushClip(cb);
            dc.PushTransform(new TranslateTransform(ptCenter.X, ptCenter.Y));
            Pen pen = new Pen(Brushes.White, 1.0);
            double x = ptCenter.X - pt.X, y = pt.Y - ptCenter.Y;
            double angle = Math.Atan2(y, x) * 180 / Math.PI;
            double dist = Math.Sqrt(x * x + y * y);
            double radius = size - 15;
            double[] rArray = new double[72];
            for (int i = 0; i < 72; i++)
            {
                dc.PushTransform(new RotateTransform(5 * i));
                
                double ang = 5 * i;
                double rad = Math.PI / 180.0 * (90 - ang);
                double b = x * Math.Cos(rad) + y * Math.Sin(rad);
                double c = x * x + y * y - radius * radius;
                double r = -b + Math.Sqrt(b * b - c);
                rArray[i] = r;
                // sqrt((r * cos(ang) + x)^2 + (r * sin(ang) + y)^2) = radius^2

                if (TextType)
                {
                    FormattedText fmt = new FormattedText(ang.ToString(), CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight, new Typeface("KaiTi"), 10, Brushes.White);
                    dc.DrawText(fmt, new Point(-fmt.Width / 2.0, -r - fmt.Height));
                }

                dc.DrawLine(pen, new Point(0, 0), new Point(0, -r));
                dc.Pop();
            }

            dc.Pop();
            dc.PushTransform(new TranslateTransform(pt.X, pt.Y));
            if (!TextType)
            {
                for (int i = 0; i < 72; i++)
                {
                    double ang = 5 * i;
                    double rad = Math.PI / 180.0 * (90 - ang);
                    double r = rArray[i];
                    double x2 = x + r * Math.Cos(rad), y2 = y + r * Math.Sin(rad);
                    double ang2 = Math.Atan2(y2, x2) / Math.PI * 180.0;
                    dc.PushTransform(new RotateTransform(90 - ang2));
                    FormattedText fmt = new FormattedText(ang.ToString(), CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight, new Typeface("KaiTi"), 10, Brushes.White);
                    dc.DrawText(fmt, new Point(-fmt.Width / 2.0, -radius - fmt.Height));
                    dc.Pop();
                }
            }
        }

        public void SetCenter(double x, double y)
        {
            ptCenter = new Point(x , y);
            InvalidateVisual();
        }

        public void SetCenter()
        {
            ptCenter = new Point(ActualWidth / 2.0, ActualHeight / 2.0);
            InvalidateVisual();
        }

        public void SetTextType(bool type)
        {
            TextType = type;
            InvalidateVisual();
        }
    }
}
