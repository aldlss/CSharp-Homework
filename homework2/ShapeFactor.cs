using System;

namespace homework2
{
    internal class ShapeFactor
    {
        public enum ShapeType
        {
            Rectangle = 0,
            Square = 1,
            Circle = 2,
        }
        public static readonly string[] ShapeName = { "Rectangle", "Square", "Circle" };
        private static readonly Random Random = new();
        public static IShape CreateRandomShape(ShapeType shapeType)
        {
            
            if (shapeType == ShapeType.Rectangle)
            {
                return new Rectangle()
                {
                    Width = Random.NextDouble()*Random.Next(0,100),
                    Length = Random.NextDouble() * Random.Next(0, 100),
                };
            }
            if (shapeType == ShapeType.Square)
            {
                return new Square()
                {
                    Side = Random.NextDouble() * Random.Next(0, 100)
                };
            }
            if (shapeType == ShapeType.Circle)
            {
                return new Circle()
                {
                    Radius = Random.NextDouble() * Random.Next(0, 100)
                };
            }
            throw new ArgumentException("Invalid shape type");
        }
    }
}
