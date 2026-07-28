using static System.Console;
using static System.Math;
using static System.Convert;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Diagnostics;
using static Gen;
using static Pre;
using static FLN;

class Program
{
    private static readonly object consoleLock = new object();
    
    static void Main(string[] args) 
    {
        Thread thisThread = Thread.CurrentThread;
        Thread tickThread = new Thread(tickCaller);
        Thread drawthread = new Thread(UI);
        tickThread.Start();

        string commands;
        bool keepPlaying = true;

        WriteLine("This is a simple console incremental game! \nFor all the commands, type 'All'");

        while (keepPlaying)
        {
            Write("COMMANDS> ");
            commands = ReadLine()!;

            if (string.Equals(commands, "all", StringComparison.OrdinalIgnoreCase))
            {
                WriteLine("--------------------------------------------------------------------------------------------------");

                WriteLine("BUY: Buys generator >> Usage: BUY then, enter [generator number(1 to 10)] to buy on the next line. \nLIST: Lists all generators and their informations >> Usage: LIST \nPOINTS: Returns how many points P0 you have >> Usage: POINTS \nPRESTIGE: Prestiges once it is unlocked >> Usage: PRESTIGE. \nQUIT: Leave the game >> Usage: QUIT.");

                WriteLine("--------------------------------------------------------------------------------------------------");
            }
            else if (string.Equals(commands, "quit", StringComparison.OrdinalIgnoreCase))
            {
                WriteLine("------------------------------------------------------------------------------------------------------------");

                Write("Currently, there is no save system sadly, so quitting will delete the save, do you wish to continue (Y/N)? ");
                string confirmation = ReadLine()!;
                confirmation = confirmation.ToUpper();

                WriteLine("------------------------------------------------------------------------------------------------------------");

                if (string.Equals(confirmation, "Y", StringComparison.OrdinalIgnoreCase) || string.Equals(confirmation, "yes", StringComparison.OrdinalIgnoreCase))
                {
                    keepPlaying = false;
                    WriteLine("-------------------");

                    WriteLine("Thanks for playing!");

                    WriteLine("-------------------");
                    Thread.Sleep(400);
                    Environment.Exit(0);
                    
                }
                else if (string.Equals(confirmation, "N", StringComparison.OrdinalIgnoreCase) || string.Equals(confirmation, "no", StringComparison.OrdinalIgnoreCase))
                {
                    keepPlaying = true;
                }
            }
            else if (string.Equals(commands, "buy", StringComparison.OrdinalIgnoreCase))
            {
                WriteLine("--------------------------------");

                Write("Which generator to be bought?: ");
                try
                {
                    int genNumber = ToInt32(ReadLine())!;
                    buyer(genNumber);
                }
                catch (FormatException e)
                {
                    WriteLine("ERROR: That is not the correct data type!");
                }

                WriteLine("--------------------------------");
            }
            else if (string.Equals(commands, "prestige", StringComparison.OrdinalIgnoreCase))
            {
                prestigeP1Function();
            }
            else if (string.Equals(commands, "list", StringComparison.OrdinalIgnoreCase))
            {
                lister();
            }
            else if (string.Equals(commands, "points", StringComparison.OrdinalIgnoreCase))
            {
                WriteLine("----------------------");

                Write("Points: ");
                WriteLine(fln(pointsP0));

                WriteLine("----------------------");

                Write("Points per second: ");
                WriteLine(fln(productionP0));

                WriteLine("----------------------");
            }
            else if (commands == "dbug")
            {
                string debugKind = ReadLine()!;
                switch (debugKind)
                {
                    case "debug()":
                        Gen.debug();
                        break;
                    case "p1()":
                        Pre.debug();
                        break;
                    default:
                        WriteLine("Wrong dbug man");
                        break;
                }
            }
            else 
            {
                WriteLine("---------------------------");

                WriteLine("That is an invalid command!");

                WriteLine("---------------------------");
            }
        }

        ReadKey();
    }

    public static void tickCaller()
    {
        Stopwatch sw = Stopwatch.StartNew();
        while (true)
        {
            if (sw.ElapsedMilliseconds >= 50)
            {
                tick();
                sw.Restart();
            }
        }
    }

    public static void UI()
    {
        SetCursorPosition(0,3);
        WriteLine(fln(pointsP0));
        lister();
        SetCursorPosition(0,20);
        WriteLine("-----------------------------------------------------------------------------------------\n");
    }

    public static void tick()
    {
        addedGens();
        production();
        recalculateProduction();
        prestigeP1Gain();
    }
}