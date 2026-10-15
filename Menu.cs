using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimTrainer
{
    internal class Menu
    {
        private string colorFont = Console.ForegroundColor.ToString();
        private string colorBckgrnd = Console.BackgroundColor.ToString();
        private Dictionary<string, string> traductColor = new Dictionary<string, string>()
        {
            {"Gray", "Gris"},
            {"Black", "Noir"},
            {"DarkBlue", "Blue foncé"},
            {"DarkGreen", "Vert foncé"},
            {"DarkCyan", "Cyan foncé"},
            {"DarkRed", "Rouge foncé"},
            {"DarkMagenta", "Magenta foncé"},
            {"DarkYellow", "Jaune foncé"},
            {"DarkGray", "Gris foncé"},
            {"Blue", "Bleu"},
            {"Green", "Vert"},
            {"Cyan", "Cyan"},
            {"Red", "Rouge"},
            {"Magenta", "Magenta"},
            {"Yellow", "Jaune"},
            {"White", "Blanc"}
        };

        public void ClearConsole()
        {
            Console.Clear();
        }
        public void LeaveTheApp()
        {
            ClearConsole();
            Console.WriteLine("Merci d'avoir utilisé notre application !\n");
            ReadKey();
        }

        public int ReadingChoiceUser()
        {
            if(!int.TryParse(Console.ReadLine()!, out int variableChoice))
            {
                throw new ArgumentException("Le menu accepte aucune entrée de lettre.");
            }
            return variableChoice;
        }

        public void ShowAllColors()
        {
            ClearConsole();
            int countColor = 0;
            Console.WriteLine("Voici toutes les couleurs disponibles :\n");
            foreach(KeyValuePair<string, string> t in traductColor)
            {
                countColor++;
                Console.ForegroundColor = Enum.Parse<ConsoleColor>(t.Key);
                if(Console.ForegroundColor == ConsoleColor.Black)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine($"EN : {t.Key} | FR : {t.Value}");
                    Console.BackgroundColor = Enum.Parse<ConsoleColor>(colorBckgrnd);
                }
                else
                {
                    Console.WriteLine($"EN : {t.Key} | FR : {t.Value}");
                }
            }
            ResetToCurrentColor();
            Console.WriteLine($"\n\nPour un total de {countColor} couleurs disponibles !");
            ReadKey();
        }

        public void ShowCurrentColors()
        {
            ClearConsole();
            Console.WriteLine("Votre couleur de texte est actuellement {0}.", traductColor[this.colorFont].ToLower());
            Console.WriteLine("Votre couleur de fond est actuellement {0}.", traductColor[this.colorBckgrnd].ToLower());
            ReadKey();
        }

        public void ChangeFontColor()
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            ConsoleColor oldColorBck = Console.BackgroundColor;
            Console.WriteLine("Voulez-vous voir les couleurs disponibles (ATTENTION : pour changer la couleur, elle faut qu'elle soit inscrite correctement dans le format EN !");
            Console.WriteLine("1 - Oui | 0 - Non");
            int seeColors = Convert.ToInt32(Console.ReadLine());
            if(seeColors == 1)
            {
                this.ShowAllColors();
            }
            int confirmFontColor = 0;
            while(confirmFontColor == 0)
            {
                ResetToCurrentColor(oldColor, oldColorBck);
                Console.WriteLine("Quelle couleur voulez-vous pour le texte ?");
                string input = Console.ReadLine()!;
                if(!int.TryParse(input, out _) && Enum.TryParse<ConsoleColor>(input, true, out ConsoleColor result))
                {
                    if(result == Console.BackgroundColor)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Attention : la couleur de fond et de police sera la même. Veuillez recommencer.");
                        ReadKey();
                        Console.ForegroundColor = oldColor;
                    }
                    else if(result == oldColor)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Vous avez déjà sélectionné cette couleur.");
                        ReadKey();
                        Console.ForegroundColor = oldColor;
                    }
                    else
                    {
                        Console.ForegroundColor = result;
                        ClearConsole();
                        Console.WriteLine("La couleur que vous avez choisie est {0}. L'ancienne était {1}.\n Confirmez-vous votre choix ?\n1 - Oui | 0 - Non", traductColor[result.ToString()], traductColor[oldColor.ToString()]);
                        confirmFontColor = Convert.ToInt32(Console.ReadLine());
                        if (confirmFontColor == 1)
                        {
                            colorFont = result.ToString();
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("La couleur inscrite est mal écrite (doit être dans le format EN) ou alors inexistante.");
                    ReadKey();
                    Console.ForegroundColor = oldColor;
                }
            }
        }

        public void ChangeBackgroundColor()
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            ConsoleColor oldColorBck = Console.BackgroundColor;
            Console.WriteLine("Voulez-vous voir les couleurs disponibles (ATTENTION : pour changer la couleur, elle faut qu'elle soit inscrite correctement dans le format EN !");
            Console.WriteLine("1 - Oui | 0 - Non");
            int seeColors = Convert.ToInt32(Console.ReadLine());
            if (seeColors == 1)
            {
                this.ShowAllColors();
            }
            int confirmFontColor = 0;
            while (confirmFontColor == 0)
            {
                ResetToCurrentColor(oldColor, oldColorBck);
                Console.WriteLine("Quelle couleur voulez-vous pour le fond ? ?");
                string input = Console.ReadLine()!;
                if (!int.TryParse(input, out _) && Enum.TryParse<ConsoleColor>(input, true, out ConsoleColor result))
                {
                    if (result == Console.ForegroundColor)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Attention : la couleur de fond et de police sera la même. Veuillez recommencer.");
                        ReadKey();
                        Console.ForegroundColor = oldColor;
                    }
                    else if (result == oldColorBck)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Vous avez déjà sélectionné cette couleur.");
                        ReadKey();
                        Console.ForegroundColor = oldColor;
                    }
                    else
                    {
                        Console.BackgroundColor = result;
                        ClearConsole();
                        Console.WriteLine("La couleur que vous avez choisie est {0}. L'ancienne était {1}.\n Confirmez-vous votre choix ?", traductColor[result.ToString()], traductColor[oldColorBck.ToString()]);
                        Console.WriteLine("1 - Oui | 0 - Non");
                        confirmFontColor = Convert.ToInt32(Console.ReadLine());
                        if (confirmFontColor == 1)
                        {
                            colorBckgrnd = result.ToString();
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("La couleur inscrite est mal écrite (doit être dans le format EN) ou alors inexistante.");
                    ReadKey();
                    Console.ForegroundColor = oldColor;
                }
            }
        }

        public void ReadKey()
        {
            Console.WriteLine("Appuyez sur n'importe quel touche pour pouvoir continuer...\n");
            Console.ReadKey();
        }

        public void ResetToCurrentColor()
        {
            Console.ForegroundColor = Enum.Parse<ConsoleColor>(colorFont);
            Console.BackgroundColor = Enum.Parse<ConsoleColor>(colorBckgrnd);
        }

        public void ResetToCurrentColor(ConsoleColor font, ConsoleColor backgrnd)
        {
            Console.ForegroundColor = font;
            Console.BackgroundColor = backgrnd;
        }

        public void TestToSeefontandbck()
        {
            Console.WriteLine("{0} and {1}", this.colorFont, this.colorBckgrnd);
        }

        public override string ToString()
        {
            return base.ToString()!;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
    }
}
