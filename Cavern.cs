//
using System;

namespace Caveroute
{
    public class Cavern
    {
        private int id;
        private int x;
        private int y;
        private double g;
        private double h;
        private double f;
        private Cavern parent = null;

        public Cavern()
        {

        }
        public Cavern(int ID, int X, int Y)
        {
            id = ID;
            x = X;
            y = Y;
        }
        public Cavern(int X, int Y, double G, double H)
        {
            x = X;
            y = Y;
            g = G;
            h = H;
            f = G + H;

        }
        public Cavern(int ID, int X, int Y, double G, double H)
        {
            id = ID;
            x = X;
            y = Y;
            g = G;
            h = H;
            f = G + H;

        }
        public int getID()
        {
            return id;
        }
        public void setID(int ID)
        {
            this.id = ID;
        }
        public int getX()
        {
            return x;
        }
        public void setX(int X)
        {
            this.x = X;
        }
        public int getY()
        {
            return y;
        }
        public void setY(int Y)
        {
            this.y = Y;
        }
        public double getG()
        {
            return g;
        }
        public void setG(double G)
        {
            this.g = G;
        }
        public double getH()
        {
            return h;
        }
        public void setH(double H)
        {
            this.h = H;
        }
        public double getF()
        {
            return f;
        }
        public void setF(double G, double H)
        {
            this.f = G + H;
        }
        public Cavern getParent()
        {
            return parent;
        }
        public void setParent(Cavern parent)
        {
            this.parent = parent;
        }

        public double CalculateH(Cavern current, Cavern goal)
        {
            int x = goal.getX() - current.getX();
            x = Math.Abs(x);
            x = x * x;
            int y = goal.getY() - current.getY();
            y = Math.Abs(y);
            y = y * y;
            int dist = x + y;
            dist = Math.Abs(dist);
            double distance = Math.Sqrt(dist);

            return distance;
        }

        public double CalculateG(Cavern current, Cavern child, double currentG)
        {
            int x = child.getX() - current.getX();
            x = Math.Abs(x);
            x = x * x;
            int y = child.getY() - current.getY();
            y = Math.Abs(y);
            y = y * y;
            int dist = x + y;
            dist = Math.Abs(dist);
            double distance = Math.Sqrt(dist);
            distance += currentG;

            return distance;
        }
    }
}
