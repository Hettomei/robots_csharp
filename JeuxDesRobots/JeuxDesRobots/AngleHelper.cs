using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JeuxDesRobots
{
    static class AngleHelper
    {

        /// <summary>
        /// Renvoie un vecteur transormé
        /// </summary>
        /// <param name="angle">en degré</param>
        /// <param name="pointInitial"></param>
        /// <param name="centreDeRotation"></param>
        /// <returns></returns>
        public static Vector2 getPointAfterRotate(float angle, Vector2 pointInitial, Vector2 centreDeRotation)
        {
            float theta = MathHelper.ToRadians(angle);

            return new Vector2(
                (float)(Math.Cos(theta) * (pointInitial.X - centreDeRotation.X) - Math.Sin(theta) * (pointInitial.Y - centreDeRotation.Y) + centreDeRotation.X),
                (float)(Math.Sin(theta) * (pointInitial.X - centreDeRotation.X) + Math.Cos(theta) * (pointInitial.Y - centreDeRotation.Y) + centreDeRotation.Y)
                );
        }


        public static double AngleOfView(Vector2 pointCentral, Vector2 pointRobotNez, Vector2 pointBrique)
        {
            double a1, b1, a2, b2, a, b, t, cosinus;

            a1 = pointRobotNez.X - pointCentral.X;
            a2 = pointRobotNez.Y - pointCentral.Y;

            b1 = pointBrique.X - pointCentral.X;
            b2 = pointBrique.Y - pointCentral.Y;

            a = Math.Sqrt((a1 * a1) + (a2 * a2));
            b = Math.Sqrt((b1 * b1) + (b2 * b2));

            if ((a == 0.0) || (b == 0.0))
                return (0.0);

            cosinus = (a1 * b1 + a2 * b2) / (a * b);

            t = Math.Acos(cosinus);
            t = MathHelper.ToDegrees((float)t);

            return CounterClockWise(pointCentral, pointRobotNez, pointBrique) * t;
        }

        public static int CounterClockWise(Vector2 pointDeRotation, Vector2 point1, Vector2 point2)
        {
            double dx1, dx2, dy1, dy2;

            dx1 = point1.X - pointDeRotation.X;
            dy1 = point1.Y - pointDeRotation.Y;

            dx2 = point2.X - pointDeRotation.X;
            dy2 = point2.Y - pointDeRotation.Y;

            if ((dx1 * dy2) > (dy1 * dx2))
                return (+1);

            if ((dx1 * dy2) < (dy1 * dx2))
                return (-1);

            if (((dx1 * dx2) < 0.0) || ((dy1 * dy2) < 0.0))
                return (-1);

            if ((dx1 * dx1 + dy1 * dy1) < (dx2 * dx2 + dy2 * dy2))
                return (+1);

            return (0);
        }
    }
}
