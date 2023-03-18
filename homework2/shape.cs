using System;

namespace homework2
{
    internal interface IShape
    {
        public double GetArea();
        public bool IsLegal();
    }


    internal class Rectangle : IShape
    {
        private double _width, _length;

        public double Width
        {
            get => _width;
            set => _width = value;
        }

        public double Length
        {
            get => _length;
            set => _length = value;
        }

        public double GetArea()
        {
            return _width * _length;
        }

        public bool IsLegal()
        {
            return _width > 0 && _length > 0;
        }
    }

    internal class Square : IShape
    {
        private double _side;

        public double Side
        {
            get => _side;
            set => _side = value;
        }

        public double GetArea()
        {
            return _side * _side;
        }

        public bool IsLegal()
        {
            return _side > 0;
        }
    }

    internal class Circle : IShape
    {
        private double _radius;

        public double Radius
        {
            get => _radius;
            set => _radius = value;
        }

        public double GetArea()
        {
            return _radius * _radius * Math.PI;
        }

        public bool IsLegal()
        {
            return _radius > 0;
        }
    }
}
