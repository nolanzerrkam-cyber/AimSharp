// TODO : Timer on the screen : use the code I have left in the example codes : timer class.
// TODO : Combo mechanic 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AimTrainer
{
    internal class Game
    {
        private static Stopwatch stopwatch = new Stopwatch();
        private int score = 0;
        private int nombreDeTirTouche = 0;
        private int nombreDeTirLoupe = 0;
        private int nombreDeTirs;
        private Target target;
        private Target biggerTarget;
        private Random rnd = new Random();

        public int Score
        {
            get { return score; }
            set
            {
                score = value;
            }
        }

        public int NombreDeTirLoupe
        {
            get { return nombreDeTirLoupe; }
            set
            {
                nombreDeTirLoupe = value;
            }
        }

        public int NombreDeTirTouche
        {
            get { return nombreDeTirTouche; }
            set { nombreDeTirTouche = value; }
        }

        public int NombreDeTirs
        {
            get { return nombreDeTirs; }
            set { nombreDeTirs = value; }
        }

        public Game()
        {
            target = new Target(3);
            biggerTarget = new Target(5);
        }

        public void Start()
        {
            Console.Clear();
            Console.CursorVisible = false;
            ConsoleMouse.EnableMouse();

            target.DrawTarget();
            biggerTarget.DrawTarget();

            // Chrono
            Stopwatch chrono = Stopwatch.StartNew();
            stopwatch.Restart();
            int tempsLimiteSec = 30;

            AfficherInterface();


            while (chrono.Elapsed.TotalSeconds < tempsLimiteSec)
            {
                //var (clickX, clickY) = ConsoleMouse.GetClickPosition();

                var click = ConsoleMouse.TryGetClickPosition();

                if(click == null)
                {
                    AfficherInterface();
                    Thread.Sleep(5);
                    continue;
                }

                var (clickX, clickY) = click.Value;

                bool hitSmallTarget = target.IsHit(clickX, clickY);
                bool hitBigTarget = biggerTarget.IsHit(clickX, clickY);

                if(hitSmallTarget)
                {
                    TargetDestroyed();
                    target.Erase();

                    target.CoordX = rnd.Next(2, Console.WindowWidth - 3);
                    target.CoordY = rnd.Next(2, Console.WindowHeight - 5);
                    target.DrawTarget();
                }

                if (hitBigTarget)
                {
                    TargetDestroyed();
                    biggerTarget.Erase();

                    biggerTarget.CoordX = rnd.Next(2, Console.WindowWidth - 3);
                    biggerTarget.CoordY = rnd.Next(2, Console.WindowHeight - 5);

                    while(target.CoordY == biggerTarget.CoordY || target.CoordX == biggerTarget.CoordX)
                    {
                        biggerTarget.CoordX = rnd.Next(2, Console.WindowWidth - 3);
                        biggerTarget.CoordY = rnd.Next(2, Console.WindowHeight - 5);
                    }

                    biggerTarget.DrawTarget();
                }

                if (!hitSmallTarget && !hitBigTarget)
                {
                    TargetMissed();
                }

                AfficherInterface();
            }

            stopwatch.Stop();

            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("------- F I N I S H E D -------");
            StatsAim();
            Console.ReadKey();
        }

        private void AfficherInterface()
        {
            Console.SetCursorPosition(0, 0);
            string test = ($"Score : {this.Score} | Cible touchés : {this.NombreDeTirTouche} | Cible Ratés : {this.NombreDeTirLoupe} | Temps : {stopwatch.Elapsed:ss\\.f} s");
            Console.Write(test.PadRight(Console.WindowWidth));
        }

        public void InformationEcran()
        {
            Console.WriteLine($"Votre fenêtre de jeu est actuellement a {Console.WindowWidth} sur du {Console.WindowHeight}");
            Console.WriteLine($"La longeur MAX est de {Console.LargestWindowWidth} et la hauteur, de {Console.LargestWindowHeight}");
            Console.ReadKey();
        }

        public void StatsAim()
        {
            double accuracy = nombreDeTirs > 0 ? ((double)nombreDeTirTouche / nombreDeTirs) * 100 : 0;

            Console.WriteLine("Total de tirs ---- {0}", this.NombreDeTirs);
            Console.WriteLine("----> Touché  ---- {0}", this.NombreDeTirTouche);
            Console.WriteLine("----> Loupé   ---- {0}", this.NombreDeTirLoupe);
            Console.WriteLine($"Votre précision est de {accuracy:F1} %");

            Console.WriteLine($"Le score est de {score} pts !");
        }



        public void TargetDestroyed()
        {
            this.NombreDeTirs++;
            this.Score += 10;
            this.NombreDeTirTouche++;
        }

        public void TargetMissed()
        {
            this.NombreDeTirs++;
            this.Score -= 5;
            this.NombreDeTirLoupe++;
        }

        public override string ToString()
        {
            return $"Game actuelle : \n{this.StatsAim}";
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
