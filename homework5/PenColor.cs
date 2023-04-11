using System.Collections.Generic;
using System.Windows.Media;

namespace homework5
{
    internal class PenColor
    {
        public Brush PenColorValue { get; set; }
        public string PenColorName { get; set; }

        public PenColor()
        {
            PenColorValue = Brushes.White;
            PenColorName = "White";
        }

        public PenColor(Brush brush, string name)
        {
            PenColorValue = brush;
            PenColorName = name;
        }

        public static List<PenColor> CreatePenColorList()
        {
            return new List<PenColor>
            {
                new(Brushes.White, "White"),
                new(Brushes.Yellow, "Yellow"),
                new(Brushes.AliceBlue, "AliceBlue"),
                new(Brushes.AntiqueWhite, "AntiqueWhite"),
                new(Brushes.Aqua, "Aqua"),
                new(Brushes.Red, "Red"),
                new(Brushes.BlueViolet, "BlueViolet"),
                new(Brushes.PaleVioletRed, "PaleVioletRed"),
                new(Brushes.ForestGreen, "ForestGreen"),
            };
        }
    }
}
