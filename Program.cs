namespace AimTrainer
{
    internal class Program
    {

        static void Main(string[] args)
        {
            int choice = 10000;
            Menu menu = new Menu();
            Game g = new Game();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            try
            {
                Console.WriteLine("Bienvenue sur AimTrainer");
                while (choice != 0)
                {
                    menu.ClearConsole();
                    Console.WriteLine($"Le numéro de choice est {choice}\n");
                    Console.WriteLine("Que souhaitez-vous faire ?");
                    Console.WriteLine("----> 1 : Choisir la couleur de la police d'écriture.");
                    Console.WriteLine("----> 2 : Choisir la couleur du fond.");
                    Console.WriteLine("----> 3 : Lancer le aimtrainer.");
                    Console.WriteLine("----> 4 : Voir les couleurs disponibles.");
                    Console.WriteLine("----> 5 : Voir les stats.");
                    Console.WriteLine("----> 0 : Quitter l'application.");
                    choice = menu.ReadingChoiceUser();

                    switch (choice)
                    {
                        case 0:
                            menu.LeaveTheApp();
                            break;
                        case 1:
                            menu.ChangeFontColor();
                            break;
                        case 2:
                            menu.ChangeBackgroundColor();
                            break;
                        case 3:
                            menu.ClearConsole();
                            g.Start();
                            break;
                        case 4:
                            menu.ShowAllColors();
                            break;
                        case 5:
                            g.StatsAim();
                            menu.ReadKey();
                            break;
                        case 99999:
                            menu.ShowCurrentColors();
                            break;
                        default:
                            Console.WriteLine("Il n'y a pas de fonction attribué a ce chiffre.");
                            menu.ReadKey();
                            break;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"ERROR : {e.Message}");
                Console.WriteLine($"Veuillez appuyer sur la barre d'espace pour quitter.");
                do
                {
                    
                }
                while (Console.ReadKey(true).Key != ConsoleKey.Spacebar);              
            }        
        }
    }
}
