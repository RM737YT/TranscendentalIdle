using static System.Console;
using static System.Math;
using static System.Convert;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Diagnostics;

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
                WriteLine("BUY: Buys generator >> Usage: BUY then, enter [generator number(1 to 10)] to buy on the next line. \nLIST: Lists all generators and their inflnions >> Usage: LIST \nPOINTS: Returns how many points P0 you have >> Usage: POINTS \nPRESTIGE: Prestiges once it is unlocked >> Usage: PRESTIGE. \nQUIT: Leave the game >> Usage: QUIT.");
            }
            else if (string.Equals(commands, "quit", StringComparison.OrdinalIgnoreCase))
            {
                Write("Currently, there is no save system sadly, so quiting will delete the save, do you wish to continue (Y/N)? ");
                string confirmation = ReadLine()!;
                confirmation = confirmation.ToUpper();

                if (string.Equals(confirmation, "Y", StringComparison.OrdinalIgnoreCase) || string.Equals(confirmation, "yes", StringComparison.OrdinalIgnoreCase))
                {
                    keepPlaying = false;
                    WriteLine("Thanks for playing!");
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
                Write("Which generator to be bought?: ");
                try
                {
                    int genNumber = ToInt32(ReadLine())!;
                    Gen.buyer(genNumber);
                }
                catch (FormatException e)
                {
                    WriteLine("ERROR: That is not the correct data type!");
                }
            }
            else if (string.Equals(commands, "prestige", StringComparison.OrdinalIgnoreCase))
            {
                WriteLine("No such system yet! Don't trust the all commands, it can be a lie sometimes!");
            }
            else if (string.Equals(commands, "list", StringComparison.OrdinalIgnoreCase))
            {
                Gen.lister();
            }
            else if (string.Equals(commands, "points", StringComparison.OrdinalIgnoreCase))
            {
                WriteLine(FLN.fln(Gen.pointsP0));
            }
            else if (commands == "prouction")
            {
                WriteLine(FLN.fln(Gen.productionP0));
            }
            else if (commands == "dbug")
            {
                Gen.debug();
            }
            else 
            {
                WriteLine("That is an invalid command!");
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
        WriteLine(FLN.fln(Gen.pointsP0));
        Gen.lister();
        SetCursorPosition(0,20);
        WriteLine("-----------------------------------------------------------------------------------------\n");
    }

    public static void tick()
    {
        Gen.recalculateProduction();
        Gen.addedGens();
        Gen.production();
    }
}