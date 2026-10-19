using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimTrainer
{
    internal class Target
    {
        Random rnd = new Random();
        private int coordX;
        private int coordY;
        private int size;

        public int CoordX
        {
            get { return coordX; }
            set
            {
                if (value > Console.WindowWidth || value < 0)
                {
                    throw new ArgumentException("La cible doit bien être dans les coordonnées respectant la taille de l'écran.");
                }

                coordX = value;
            }
        }

        public int CoordY
        {
            get { return coordY; }
            set
            {
                if (value > Console.WindowHeight || value < 0)
                {
                    throw new ArgumentException("La cible doit bien être dans les coordonnées respectant la taille de l'écran.");
                }

                coordY = value;
            }
        }

        public int Size
        {
            get { return size;}
            set
            {
                size = value;
            }
        }

        public Target(int size)
        {
            coordX = rnd.Next(1, (Console.WindowWidth - 3));
            coordY = rnd.Next(1, (Console.WindowHeight - 3));
            this.size = size;
        }

        public void DrawTarget()
        {
            Console.SetCursorPosition(this.coordX, this.coordY);
            if(this.size == 3)
            {
                Console.Write("[◎]");
            }
            else if(this.size == 5)
            {
                Console.Write("[-◎-]");
            }
            else if(this.size == 7)
            {
                Console.Write("[==◎==]");
            }
        }

        public void Erase()
        {
            Console.SetCursorPosition(this.CoordX, this.coordY);
            if(this.size == 3)
            {
                Console.Write("   ");
            }
            else if(this.size == 5)
            {
                Console.Write("     ");
            }
            else if(this.size == 7)
            {
                Console.Write("       ");
            }

        }

        public bool IsHit(int clickX, int clickY)
        {
            return clickY == coordY && clickX >= coordX && clickX < coordX + size;
        }

        //public void ShowCoordTarget()
        //{
        //    Console.WriteLine("La cible était sur {0}x, {1}y", this.coordX, this.coordY);
        //}

        public override string ToString()
        {
            return $"Votre cible {this} est sur les coordonnées x -> {CoordX}, y --> {CoordY} et de taille {Size}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return obj.ToString() == this.ToString();
        }
    }
}
