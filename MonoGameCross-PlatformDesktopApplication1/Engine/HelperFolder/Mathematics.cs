using System;
using Microsoft.Xna.Framework;

namespace MonoGameCross_PlatformDesktopApplication1; 

public static class Mathematics {

    public static double DistanceBetween(Vector2 point1, Vector2 point2) {
        return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
    }

    public static Vector2 PointOnLine(Vector2 point1, Vector2 point2, double t) {
        return new Vector2((float)((1 - t) * point1.X + t * point2.X), (float)((1 - t) * point1.Y + t * point2.Y));
    }

    public static Rectangle MakeRectangle(int point1X, int point1Y,int point2X,int point2Y) {
        return new Rectangle(point1X, point1Y, Math.Abs(point2X - point1X), Math.Abs(point2Y - point1Y));
    }

    public static int[] RandomPosition() {
        int[] bla = new int[2];
        Random random = new Random();
        bla[0] = random.Next(-1020, 1020);
        bla[1] = random.Next(0, 1020);
        return bla;
    }

    public static int RandomNumber(int min, int max) {
        Random random = new Random();
        return random.Next(min, max);
    }
}