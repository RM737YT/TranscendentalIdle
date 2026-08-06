using static System.Console;
using static System.Convert;
using System.Diagnostics;
using static Gen;
using static Pre;
using BreakInfinity;
using static Upg;
using static Chall;
using System.Formats.Asn1;

class Program
{
    private static readonly object consoleLock = new object();
    public static BigDouble totalSpentTime = 0, timeThisP2 = 0;
    
    public static void Main(string[] args) 
    {
        Title = "Transcendental Idle";
#pragma warning disable CA1416 // Validate platform compatibility
        WindowWidth = 125;
#pragma warning restore CA1416 // Validate platform compatibility

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

                WriteLine("BUY: Buys generator >> Usage: BUY then, enter [generator number(1 to 10)] to buy on the next line \nBUYMAX: Buys all generators to their max in order >> Usage: BUYMAX \nLIST: Lists all generators and their informations >> Usage: LIST \nPOINTS: Returns how many points P0 you have >> Usage: POINTS \nPRESTIGE: Prestiges once it is unlocked >> Usage: PRESTIGE then specify the prestige by P1 or P2 and confirm. \nUPGRADE: Buys specified upgrade >> Usage: UPGRADE and then enter the tier of upgrade and the respective upgrade number [P2 <enter> FU <enter> 6: will buy P2FLU3] \nENTER CHALLENGE: Enters specified challenge >> Usage: ENTER CHALLENGE and then specify tier and challenge number \nQUIT: Leave the game >> Usage: QUIT then confirm.");

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
                        if(p2Challenges[5].challRunningState == true)
                        {
                            buyerChallSix(genNum);
                        }
                        else
                        {
                            buyer(genNum);
                        }
                    }
                    else if (num == 9 || num == 10)
                    {
                        genNum = generatorsNineTen[num - 9];
                        if(p2Challenges[5].challRunningState == true)
                        {
                            buyerChallSix(genNum);
                        }
                        else
                        {
                            buyer(genNum);
                        }
                    }
                    else
                    {
                        WriteLine("That is an invalid generator number!");
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
                    WriteLine("-----------------------------------------------------");

                    WriteLine("That is either wrong or that layer doesn't exist yet!");

                    WriteLine("-----------------------------------------------------");
                }
            }
            else if (string.Equals(commands, "upgrade", StringComparison.OrdinalIgnoreCase))
            {
                Upg.lister();

                if(p2Amount < 1)
                {
                    Main(args);
                    return;
                }

                WriteLine("--------------------------------------------------");
                try
                {
                    Write("Which tier of upgrades to be bought? [P2]: ");
                    string tier = ReadLine()!;

                    Write("Which upgrade to be bought? [SU/U/CU/FUU/FLU/FMU/FU]: ");
                    string type = ReadLine()!;

                    Write("WHich upgrade to buy? [Write the upgrade number]: ");
                    int num = ToInt32(ReadLine());

                    Upg upgID;

                    if(string.Equals(tier, "p2", StringComparison.OrdinalIgnoreCase))
                    {
                        if(string.Equals(type, "SU", StringComparison.OrdinalIgnoreCase))
                        {
                            upgID = p2StaticUpgrades[num - 1];
                            buyer(upgID);
                        }
                        else if(string.Equals(type, "U", StringComparison.OrdinalIgnoreCase))
                        {
                            upgID = p2Upgrades[num - 1];
                            buyer(upgID);
                        }
                        else if(string.Equals(type, "CU", StringComparison.OrdinalIgnoreCase))
                        {
                            upgID = p2ChallengeUpgrades[num - 1];
                            buyer(upgID);
                        }
                        else if(string.Equals(type, "FU", StringComparison.OrdinalIgnoreCase))
                        {
                            upgID = p2FinalUpgrades[num - 1];
                            buyer(upgID);
                        }
                        else
                        {
                            WriteLine("That doesn't seem to exist!");
                        }
                    }
                }
                catch (FormatException)
                {
                    WriteLine("That's the wrong format!");
                }

                WriteLine("--------------------------------------------------");
            }
            else if (string.Equals(commands, "enter challenge", StringComparison.OrdinalIgnoreCase) || string.Equals(commands, "challenge", StringComparison.OrdinalIgnoreCase))
            {
                Chall.lister();

                if(p2Amount < 1)
                {
                    Main(args);
                }

                WriteLine("-----------------------------------------------");

                Write("Which tier of challenge to be entered? [P2]: ");
                string tier = ReadLine()!;

                WriteLine("-----------------------------------------------");

                tier = tier.ToLower();

                WriteLine("-------------------------------------------------------------------------");

                Write("Which challenge of this tier to be entered? [For P2: 1, 2, 3, 4, 5, 6]: ");
                int id = ToInt32(ReadLine());

                WriteLine("-------------------------------------------------------------------------");

                enterChallenge(tier, id);
            }
            else if (string.Equals(commands, "list", StringComparison.OrdinalIgnoreCase))
            {
                Gen.lister();

                WriteLine("----------------------");

                Write("Points: ");
                WriteLine(pointsP0);

                WriteLine("----------------------");

                Write("Points per second: ");
                WriteLine(productionP0);

                WriteLine("----------------------");
            }
            else if (string.Equals(commands, "points", StringComparison.OrdinalIgnoreCase))
            {
                WriteLine("-------------------------------------------");

                WriteLine($"Points per tick: {productionP0}");

                WriteLine("-------------------------------------------");

                WriteLine($"Points: {pointsP0}");

                WriteLine("-------------------------------------------");

                WriteLine($"Multiplier gain on prestige: {productionP1 - bestP1}");

                WriteLine("-------------------------------------------");

                WriteLine($"Multiplier: {multP1}");

                WriteLine("-------------------------------------------");

                if(p2Amount >= 1)
                {
                    WriteLine($"Beyond points gain on prestige: {productionP2}");

                    WriteLine("-------------------------------------------");

                    WriteLine($"Beyond points: {pointsP2}");

                    WriteLine("-------------------------------------------");

                    WriteLine($"Beyond prestiges: {p2Amount}");

                    WriteLine("-------------------------------------------");
                }
            }
            else if (commands == "dbug")
            {
                Write("PASSWORD: ");
                string pass = ReadLine()!;
                if(pass != "P1PrestigeLayer.Name/dfm.txt"){ Main(args); }

                string debugKind = ReadLine()!;
                switch (debugKind)
                {
                    case "gen()":
                        Gen.debug();
                        break;
                    case "p1()":
                        Pre.debug();
                        break;
                    case "p2()":
                        double nombres = ToDouble(ReadLine());
                        p2Amount = nombres;
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
                    case "challs()":
                        Chall.debug();
                        break;
                    case "upgs()":
                        Upg.debug();
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
        WriteLine(pointsP0);
        Gen.lister();
        SetCursorPosition(0,20);
        WriteLine("--------------------------\n");
    }

    public static void tick()
    {
        addedGens();
        addedChalls();
        addedUpgrades();
        production();
        recalculateProduction();
        produceGenEights();
        produceGenNineTen();
        prestigeP1Gain();
        prestigeP2Gain();
    }
}