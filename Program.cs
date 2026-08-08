using static System.Console;
using static System.Convert;
using static System.ConsoleColor;
using System.Diagnostics;
using static Gen;
using static Pre;
using BreakInfinity;
using static Upg;
using static Chall;
using static Save;
using static SaveCustom;

class Program
{
    private static readonly object consoleLock = new object();
    public static BigDouble totalSpentTime = 0, timeThisP2 = 0, nextAutoSave = 0;
    public static string saveName;
    
    public static void Main(string[] args)
    {
        Title = "Transcendental Idle";
#pragma warning disable CA1416 // Validate platform compatibility
        WindowWidth = 125;
#pragma warning restore CA1416 // Validate platform compatibility
        WriteLine("This is a simple console incremental game! \nFor all the commands, type 'All'");

        Thread thisThread = Thread.CurrentThread;
        Thread tickThread = new Thread(tickCaller);
        Thread drawthread = new Thread(UI);

        bool loadedAuto = loadFunction();
        if(loadedAuto == true)
        {
            WriteLine("-----------------------------------------------------");

            WriteLine("Loaded last available default save file successfully!");

            WriteLine("-----------------------------------------------------");
        }
        else
        {
            WriteLine("-----------------------------------------------");

            WriteLine("No default save file found. Started a new game!");

            WriteLine("-----------------------------------------------");
        }

        Thread.Sleep(100);

        tickThread.Start();

        string commands;
        bool keepPlaying = true;

        while (keepPlaying)
        {
            Write("COMMANDS> ");
            commands = ReadLine()!;

            if (string.Equals(commands, "all", StringComparison.OrdinalIgnoreCase))
            {
                WriteLine("--------------------------------------------------------------------------------------------------");

                WriteLine($"BUY: Buys generator >> Usage: BUY then, enter [generator number(1 to 10)] to buy on the next line \nBUYMAX: Buys all generators to their max in order >> Usage: BUYMAX \nLIST: Lists all generators and their informations >> Usage: LIST \nPOINTS: Returns how many points P0 you have >> Usage: POINTS \nPRESTIGE: Used to once it is unlocked >> Usage: PRESTIGE then specify the prestige by P1 or P2 and confirm. \nUPGRADE: Buys specified upgrade >> Usage: UPGRADE and then enter the tier of upgrade and the respective upgrade number [P2 <enter> FU <enter> 6: will buy P2FLU3] \nENTER CHALLENGE: Enters specified challenge >> Usage: ENTER CHALLENGE and then specify tier and challenge number \nSAVE: Saves the game onto default save file >> Usage: SAVE \nSAVE CUSTOM: Saves the game onto a custom named save file >> Usage: SAVE CUSTOM then write the name of the save file \nLOAD: Loads the default save file >> Usage: LOAD \nLOAD CUSTOM: Loads a custom named save file >> Usage: LOAD CUSTOM then write the name of the save file \nQUIT: Saves and quits the game >> Usage: QUIT");

                WriteLine("--------------------------------------------------------------------------------------------------");
            }
            else if (string.Equals(commands, "quit", StringComparison.OrdinalIgnoreCase))
            {
                WriteLine("----------------------------");

                keepPlaying = false;
                
                Random random = new Random();
                int quitTime = random.Next(400, 1001);

                WriteLine("Wait while the game saves...");
                saveFunction();
                Thread.Sleep(quitTime);

                WriteLine("\nThanks for playing!");

                WriteLine("----------------------------");

                Thread.Sleep(quitTime);
                Environment.Exit(0);
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
                WriteLine("-------------------------------------");

                Write("What should be done? [list/buy]: ");
                string listerQuestion = ReadLine()!;

                WriteLine("-------------------------------------");

                if(string.Equals(listerQuestion, "list", StringComparison.OrdinalIgnoreCase))
                {
                    if(p2Amount < 1)
                    {
                        WriteLine("-------------------------------------------");

                        WriteLine("Get one Beyond prestige first to see these!");

                        WriteLine("-------------------------------------------");

                        keepPlaying = true;
                        continue;
                    }
                    else
                    {
                        Upg.lister();  
                    }
                }
                else
                {
                    if(p2Amount < 1)
                    {
                        WriteLine("-------------------------------------------");

                        WriteLine("Get one Beyond prestige first to see these!");

                        WriteLine("-------------------------------------------");

                        keepPlaying = true;
                        continue;
                    }
                    else
                    {
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
                }
            }
            else if (string.Equals(commands, "enter challenge", StringComparison.OrdinalIgnoreCase) || string.Equals(commands, "challenge", StringComparison.OrdinalIgnoreCase))
            {
                WriteLine("-------------------------------------");

                Write("What should be done? [list/start]: ");
                string listerQuestion = ReadLine()!;

                WriteLine("-------------------------------------");

                if(string.Equals(listerQuestion, "list", StringComparison.OrdinalIgnoreCase))
                {
                    if(p2Amount < 1)
                    {
                        WriteLine("-------------------------------------------");

                        WriteLine("Get one Beyond prestige first to see these!");

                        WriteLine("-------------------------------------------");

                        keepPlaying = true;
                        continue;
                    }
                    else
                    {
                        Chall.lister();  
                    }
                }
                else
                {
                    if(p2Amount < 1)
                    {
                        WriteLine("-------------------------------------------");

                        WriteLine("Get one Beyond prestige first to see these!");

                        WriteLine("-------------------------------------------");

                        keepPlaying = true;
                        continue;
                    }
                    else
                    {
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
                }
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

                WriteLine($"Multiplier gain on prestige: {productionP1}");

                WriteLine("-------------------------------------------");

                WriteLine($"Multiplier: {multP1}");

                WriteLine("-------------------------------------------");

                WriteLine($"Beyond points gain on prestige: {productionP2}");

                WriteLine("-------------------------------------------");

                if(p2Amount >= 1)
                {
                    WriteLine($"Beyond points: {pointsP2}");

                    WriteLine("-------------------------------------------");

                    WriteLine($"Beyond prestiges: {p2Amount}");

                    WriteLine("-------------------------------------------");
                }
            }
            else if (string.Equals(commands, "save", StringComparison.OrdinalIgnoreCase))
            {
                bool save = saveFunction();
                if(save == true)
                {
                    WriteLine("------");

                    WriteLine("Saved!");

                    WriteLine("------");
                }
                else
                {
                    WriteLine("--------------------------------------------");

                    WriteLine("File could not be saved due to some problem!");

                    WriteLine("--------------------------------------------");
                }
            }
            else if (string.Equals(commands, "save custom", StringComparison.OrdinalIgnoreCase))
            {
                Write("What will be the name of the save file?: ");
                saveName = ReadLine()!;
                bool saveManualCustom = saveFunctionCustom(saveName);
                if(saveManualCustom == true)
                {
                    WriteLine("------");

                    WriteLine("Saved!");

                    WriteLine("------");
                }
                else
                {
                    WriteLine("--------------------------------------------");

                    WriteLine("File could not be saved due to some problem!");

                    WriteLine("--------------------------------------------");
                }
            }
            else if (string.Equals(commands, "load", StringComparison.OrdinalIgnoreCase))
            {
                bool loadedManual = loadFunction();
                if(loadedManual == true)
                {
                    WriteLine("-----------------------------------------------------");

                    WriteLine("Loaded last available default save file successfully!");

                    WriteLine("-----------------------------------------------------");
                }
                else
                {
                    WriteLine("-----------------------------------------------");

                    WriteLine("No default save file found. Started a new game!");

                    WriteLine("-----------------------------------------------");
                }
                Thread.Sleep(50);
            }
            else if (string.Equals(commands, "load custom", StringComparison.OrdinalIgnoreCase))
            {
                Write("Which save to be loaded?: ");
                saveName = ReadLine()!;
                bool loadedManualCustom = loadFunctionCustom(saveName);
                if(loadedManualCustom == true)
                {
                    WriteLine("----------------------------------");

                    WriteLine("Loaded the save file successfully!");


                    WriteLine("----------------------------------");
                }
                else
                {
                    WriteLine("----------------------------------------------------------");

                    WriteLine("No such save file found. Continued with default save file!");

                    WriteLine("----------------------------------------------------------");
                }
            }
            else if (commands == "dbug")
            {
                Write("PASSWORD: ");
                string pass = ReadLine()!;
                if(pass != "P1PrestigeLayer.Name/dfm.txt"){ Main(args); }
                ForegroundColor = DarkRed; WriteLine("WARNING: THIS AREA IS USED FOR TESTING PURPOSES AND MAY CONTAIN UNFINISHED THINGS, USE CAREFULLY AT YOUR OWN RISK!"); ForegroundColor = Gray;

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
                    case "save()":
                        Debug.debug();
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
            if (sw.ElapsedMilliseconds >= 16)
            {
                BigDouble deltaTime = sw.Elapsed.TotalMilliseconds;

                tick();

                totalSpentTime += deltaTime;
                timeThisP2 += deltaTime;

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
        if(p2FinalUpgrades[6].upgradeStateOfBuy == true)
        {
            produceBeyondPrestige();
        }

        if(totalSpentTime / 1000 >= nextAutoSave)
        {
            bool save = saveFunction();
            if(save == true)
            {
                WriteLine("-------------");

                WriteLine("Saved game...");
                WriteLine("COMMANDS> ");

                WriteLine("-------------");
            }
            else
            {
                WriteLine("---------------------------------------------------------");

                WriteLine("Game could not be auto saved! Try saving manually please.");

                WriteLine("---------------------------------------------------------");
            }
            nextAutoSave += 3000;
        }
    }
}