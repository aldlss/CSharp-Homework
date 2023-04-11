using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace homework5
{
    internal class CayleyTree: INotifyPropertyChanged
    {
        private static readonly GeometryDrawing AGeometryDrawing = new ()
        {
            Geometry = Geometry.Empty,
        };

        private ImageSource _anImageSource = new DrawingImage(AGeometryDrawing);
        
        private double _th1;
        private double _th2;
        private double _per1;
        private double _per2;
        private int _n;
        private double _length;
        private double _th; // 角度

        public double Th1
        {
            get => _th1;
            set
            {
                _th1 = value;
                OnPropertyChanged();
            }
        }

        public double Th2
        {
            get => _th2;
            set
            {
                _th2 = value;
                OnPropertyChanged();
            }
        }

        public double Per1
        {
            get => _per1;
            set
            {
                _per1 = value;
                OnPropertyChanged();
            }
        }

        public double Per2
        {
            get => _per2;
            set
            {
                _per2 = value;
                OnPropertyChanged();
            }
        }

        public int N
        {
            get => _n;
            set
            {
                _n = value;
                OnPropertyChanged();
            }
        }

        public double Length
        {
            get => _length;
            set
            {
                _length = value;
                OnPropertyChanged();
            }
        }

        public double Th
        {
            get => _th;
            set
            {
                _th = value;
                OnPropertyChanged();
            }
        }

        public Brush Pen
        {
            get => AGeometryDrawing.Pen.Brush;
            set
            {
                AGeometryDrawing.Pen = new Pen(value, 2);
                OnPropertyChanged();
            }
        }

        public ImageSource AnImageSource
        {
            get => _anImageSource;
            set
            {
                _anImageSource = value;
                OnPropertyChanged();
            }
        }

        private GeometryGroup _geometryGroup = new();

        public CayleyTree()
        {
            _n = 8;
            _length = 100;
            _per1 = 0.6;
            _per2 = 0.6;
            _th = 270;
            _th1 = 30;
            _th2 = 60;
        }

        public void StartDrawCayleyTree(double x0, double y0)
        {
            _geometryGroup = new();
            DrawCayleyTree(_n, x0, y0, _length, _th);
            AGeometryDrawing.Geometry = _geometryGroup;
            DrawingImage drawingImage = new(AGeometryDrawing);
            // drawingImage.Freeze();
            AnImageSource = drawingImage;
        }

        private void DrawCayleyTree(int n, double x0, double y0, double length, double angle)
        {
            if (n == 0)
            {
                return;
            }

            double x1 = x0 + length * Math.Cos(angle * Math.PI / 180);
            double y1 = y0 + length * Math.Sin(angle * Math.PI / 180);

            _geometryGroup.Children.Add(
                new LineGeometry(new Point(x0, y0), new Point(x1, y1)));

            DrawCayleyTree(n-1, x1, y1, _per1 * length, angle + _th1);
            DrawCayleyTree(n-1, x1, y1, _per2 * length, angle - _th2);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
