using static System.Console;
using static System.Convert;
using System.Diagnostics;
using static Gen;
using static Pre;
using static FLN;
using static Upg;
using static Chall;

class Program
{
    private static readonly object consoleLock = new object();
    public static double totalSpentTime = 0, timeThisP2 = 0;
    
    static void Main(string[] args) 
    {
        Title = "Transcendental Idle";

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

                WriteLine("BUY: Buys generator >> Usage: BUY then, enter [generator number(1 to 10)] to buy on the next line \nBUYMAX: Buys all generators to their max in order >> Usage: BUYMAX \nLIST: Lists all generators and their informations >> Usage: LIST \nPOINTS: Returns how many points P0 you have >> Usage: POINTS \nPRESTIGE: Prestiges once it is unlocked >> Usage: PRESTIGE then specify the prestige by P1 or P2 and confirm. \nQUIT: Leave the game >> Usage: QUIT then confirm.");

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
                    int num = ToInt32(ReadLine());
                    Gen genNum;
                    if (num >= 1 && num <= 8)
                    {
                        genNum = generatorsEight[num - 1];
                        buyer(genNum);
                    }
                    else if (num == 9 || num == 10)
                    {
                        genNum = generatorsNineTen[num - 9];
                        buyer(genNum);
                    }
                    else
                    {
                        WriteLine("That is an invalid gen number man.");
                    }
                }
                catch (FormatException)
                {
                    WriteLine("ERROR: That is not the correct data type!");
                }

                WriteLine("--------------------------------");
            }
            else if (string.Equals(commands, "buymax", StringComparison.OrdinalIgnoreCase))
            {
                buyerMax();
            }
            else if (string.Equals(commands, "prestige", StringComparison.OrdinalIgnoreCase))
            {
                WriteLine("--------------------------------------------");

                Write("Which prestige do you want to do [P1/P2]: ");
                string whichPrestige = ReadLine()!;

                WriteLine("--------------------------------------------");

                if (string.Equals(whichPrestige, "P1", StringComparison.OrdinalIgnoreCase))
                {
                    prestigeP1Function();
                }
                else if (string.Equals(whichPrestige, "P2", StringComparison.OrdinalIgnoreCase))
                {
                    prestigeP2Function();
                }
                else
                {
                    WriteLine("That is either wrong or that layer doesn't exist yet!");
                }
            }
            else if (string.Equals(commands, "upgrade", StringComparison.OrdinalIgnoreCase))
            {
                WriteLine("Not Yet Implemented fully!");
            }
            else if (string.Equals(commands, "list", StringComparison.OrdinalIgnoreCase))
            {
                Gen.lister();
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
                    case "pointsp0":
                        double amt = ToDouble(ReadLine())!;
                        string op = ReadLine()!;
                        switch (op)
                        {
                            case "set":
                                pointsP0 = amt;
                                break;
                            case "*":
                                pointsP0 *= amt;
                                break;
                            case "+":
                                pointsP0 += amt;
                                break;
                            case "/":
                                try
                                {
                                    pointsP0 /= amt;
                                    if (pointsP0 <= 0)
                                    {
                                        pointsP0 = 0;
                                    }
                                }
                                catch(DivideByZeroException)
                                {
                                    WriteLine("You divided by zero man");
                                }
                                break;
                            case "-":
                                pointsP0 -= amt;
                                if (pointsP0 <= 0)
                                {
                                    pointsP0 = 0;
                                }
                                break;
                            default:
                                WriteLine("Nothing to do");
                                break;
                        }
                        break;
                    case "proto()":
                        int num = ToInt32(ReadLine());
                        Gen genNum;
                        if (num >= 1 && num <= 8)
                        {
                            genNum = generatorsEight[num - 1];
                            Prototype.buyerForGensFile(genNum);
                        }
                        else if (num == 9 || num == 10)
                        {
                            genNum = generatorsNineTen[num - 9];
                            Prototype.buyerForGensFile(genNum);
                        }
                        else
                        {
                            WriteLine("That is an invalid gen number man.");
                        }
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
                totalSpentTime += 50;
                timeThisP2 += 50;
                sw.Restart();
            }
        }
    }

    public static void UI()
    {
        SetCursorPosition(0,3);
        WriteLine(fln(pointsP0));
        Gen.lister();
        SetCursorPosition(0,20);
        WriteLine("--------------------------\n");
    }

    public static void tick()
    {
        addedGens();
        production();
        recalculateProduction();
        produceGenEights();
        prestigeP1Gain();
        prestigeP2Gain();
    }
}