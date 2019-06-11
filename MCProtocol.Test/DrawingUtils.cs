using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public static class DrawingUtils
    {
        public static int DefaultMargin { get; set; } = 10;

        public static Point GetMiddleLocation(Rectangle bounds)
        {
            return new Point(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
        }


        public static Point GetDirectionValue(PlaceDirection direction)
        {
            int v = (int)direction;
            int col = v % 3;
            int row = v / 3;

            return new Point(col, row);
        }

        public static double GetLevelValue(PlaceLevel level)
        {
            return ((int)level - 1) / 2.0D;
        }

        public static Point PlaceByDirection(Rectangle master, Size slave, PlaceDirection direction)
        {
            return PlaceByDirection(master, slave, direction, PlaceLevel.Zero);
        }

        public static Point PlaceByDirection(Rectangle master, Size slave, PlaceDirection direction, int margin)
        {
            return PlaceByDirection(master, slave, direction, PlaceLevel.Zero, margin);
        }

        public static Point PlaceByDirection(Rectangle master, Size slave, PlaceDirection direction, PlaceLevel level)
        {
            return PlaceByDirection(master, slave, direction, level, DefaultMargin);
        }

        public static Point PlaceByDirection(Rectangle master, Size slave, PlaceDirection direction, PlaceLevel level, int margin)
        {
            Point directionValue = GetDirectionValue(direction);
            int[] xs = new int[3] { master.Left - slave.Width - margin, master.Left, master.Right + margin };
            int[] ys = new int[3] { master.Top - slave.Height - margin, master.Top, master.Bottom + margin };

            double levelValue = GetLevelValue(level);

            int x = xs[directionValue.X] + (directionValue.X == 1 ? (int)((master.Width - slave.Width) * levelValue) : 0);
            int y = ys[directionValue.Y] + (directionValue.Y == 1 ? (int)((master.Height - slave.Height) * levelValue) : 0);


            return new Point(x, y);
        }

        public static Point PlaceByReference(Rectangle xMaster, PlaceDirection xDirection, Rectangle yMaster, PlaceDirection yDirection)
        {
            int dm = DefaultMargin;
            return PlaceByReference(xMaster, xDirection, dm, yMaster, yDirection, dm);
        }

        public static Point PlaceByReference(Rectangle xMaster, PlaceDirection xDirection, int xMargin, Rectangle yMaster, PlaceDirection yDirection)
        {
            int dm = DefaultMargin;
            return PlaceByReference(xMaster, xDirection, xMargin, yMaster, yDirection, dm);
        }

        public static Point PlaceByReference(Rectangle xMaster, PlaceDirection xDirection, Rectangle yMaster, PlaceDirection yDirection, int yMargin)
        {
            int dm = DefaultMargin;
            return PlaceByReference(xMaster, xDirection, dm, yMaster, yDirection, yMargin);
        }

        public static Point PlaceByReference(Rectangle xMaster, PlaceDirection xDirection, int xMargin, Rectangle yMaster, PlaceDirection yDirection, int yMargin)
        {
            int x = 0;
            int y = 0;

            if (xDirection == PlaceDirection.Left)
            {
                x = xMaster.Left - xMargin;
            }
            else if (xDirection == PlaceDirection.Right)
            {
                x = xMaster.Right + xMargin;
            }
            else
            {
                throw new ArgumentException("xDirection must be Left or Right");
            }

            if (yDirection == PlaceDirection.Top)
            {
                y = yMaster.Top - yMargin;
            }
            else if (yDirection == PlaceDirection.Bottom)
            {
                y = yMaster.Bottom + yMargin;
            }
            else
            {
                throw new ArgumentException("yDirection must be Top or Bottom");
            }

            return new Point(x, y);
        }

        public static Rectangle PlaceByDirection2(Rectangle master, Size slave, PlaceDirection direction)
        {
            return new Rectangle(PlaceByDirection(master, slave, direction), slave);
        }

        public static Rectangle PlaceByDirection2(Rectangle master, Size slave, PlaceDirection direction, int margin)
        {
            return new Rectangle(PlaceByDirection(master, slave, direction, margin), slave);
        }

        public static Rectangle PlaceByDirection2(Rectangle master, Size slave, PlaceDirection direction, PlaceLevel level)
        {
            return new Rectangle(PlaceByDirection(master, slave, direction, level), slave);
        }

        public static Rectangle PlaceByDirection2(Rectangle master, Size slave, PlaceDirection direction, PlaceLevel level, int margin)
        {
            return new Rectangle(PlaceByDirection(master, slave, direction, level, margin), slave);
        }

    }

}
