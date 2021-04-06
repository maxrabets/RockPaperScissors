using System;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            if(!Game.CheckItems(args))
            {
                Console.WriteLine("\r\nNumber of items shoud be odd and grater than 3." +
                    " All items should be unique");
                Console.WriteLine("Example: RockPaperScissors.exe rock paper scissors lizard Spock");
                Console.ReadLine();
                return;
            }

            Game game = new Game(args);
            int computerMove = game.MakeComputerMove();

            Console.WriteLine("HMAC: " + BitConverter.ToString(game.Hash));
            Console.WriteLine("\r\nAvailable moves: ");
            for(int i = 0; i < game.Items.Length; i++)
                Console.WriteLine($"{i+1} - {game.Items[i]}");
            Console.WriteLine("0 - exit");

            while (true)
            {
                Console.Write("\r\nENTER YOUR MOVE: ");
                if (int.TryParse(Console.ReadLine(), out int playerMove))
                {
                    if (playerMove == 0)
                        return;
                    if(playerMove > game.Items.Length || playerMove < 0)
                    {
                        Console.WriteLine("\r\nBad move. Please, choose one of this: ");
                        for (int i = 0; i < game.Items.Length; i++)
                            Console.WriteLine($"{i + 1} - {game.Items[i]}");
                        Console.WriteLine("0 - exit");
                        continue;
                    }

                    playerMove--;
                    Console.Write("YOUR MOVE: " + game.Items[playerMove]);
                    Console.Write("\r\nCOMPUTER MOVE: " + game.Items[computerMove]);
                    Console.WriteLine();
                    int result = game.CompareMoves(playerMove, computerMove);
                    switch (result)
                    {
                        case 1:
                            Console.WriteLine("you win");
                            break;

                        case 2:
                            Console.WriteLine("YOU LOOOOOOOOSE!!!!!!!");
                            break;

                        case 0:
                            Console.WriteLine("Draw");
                            break;
                    }
                    Console.WriteLine("\r\nHMAC key: " + BitConverter.ToString(game.Key));
                    Console.ReadLine();
                    return;
                }
                else
                {
                    Console.WriteLine("\r\nBad move. Please, choose one of this: ");
                    for (int i = 0; i < game.Items.Length; i++)
                        Console.WriteLine($"{i + 1} - {game.Items[i]}");
                    Console.WriteLine("0 - exit");
                }
            } 
        }

    }
}
