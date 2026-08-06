using static System.Console;
using static System.Convert;
using static System.Math;
using System.Linq;
using static Pre;
using static Gen;
using static Upg;
using static Program;
using BreakInfinity;

class Chall
{
    public string challName, challDesc, challGoal, challRewardDesc;
    public bool challCompletion, challRunningState;
    public Action challModifier, challReward, challInternalGoal = () => {};
    public static Chall[] p2Challenges = [];
    public static bool challengeCheckCompletion;
    public static int challengesCompleted;

    public void challengesP2(string name, string desc, string goal, string rewardDesc, bool runState, bool completion, Action modifier, Action reward)
    {
        challName = name;
        challDesc = desc;
        challCompletion = completion;
        challModifier = modifier;
        challReward = reward;
        challGoal = goal;
        challRewardDesc = rewardDesc;
        challRunningState = runState;
    }

    public static void addedChalls()
    {
        if (p2Challenges.Length >= 6)
        {
            return;
        }

        p2Challenges = new Chall[6];
        for (int i = 0; i < p2Challenges.Length; i++)
        {
            p2Challenges[i] = new Chall();
        }

        // challPreset.challengesP2("id: name", "desc", "goal", "reward desc", running or not(bool), completed or not(bool)), () => { code for modification }, () => { rewward after completion });

        p2Challenges[0].challengesP2("C1: Timed division", "FASTER THE 10 SECOND WINDOW IS CLOSING (Every 10 seconds, your PointsP0 are divided by 10)", "Reach 1 Beyond points produced as next prestige's production to complete challenge.", "Inputs a dynamic multiplier based on time spent on this prestige to P2U2's multiplier.", false, false, () => { if(timeThisP2/100 == 10){pointsP0 /= 10;} }, () => { p2U2Mult *= BigDouble.Log10(timeThisP2/100); });
        p2Challenges[1].challengesP2("C2: Lag-", "Not good enough specs (Buying generators only works at half efficiency)", "Reach 1e30 points to complete", "Generator multipliers are multiplied by 1.001", false, false, () => { foreach(Gen gens in generatorsEight){gens.mult /= 2;} foreach(Gen gens in generatorsNineTen){gens.mult /= 2;} }, () => { foreach(Gen gens in generatorsEight){gens.mult *= 1.001;} foreach(Gen gens in generatorsNineTen){gens.mult *= 1.001;} });
        p2Challenges[2].challengesP2("C3: Jamming out", "Your printers are trying to eat you (all generator randomly restart from 0 amount and 1 mult)", "Reach all 10 generators' amount 1+ to complete", "You can now buy 2 generators at once instead of one at the same cost of buying one", false, false, () => { Random random = new Random(); int eight = random.Next(0, 8); BigDouble nextAttack = 120; if(totalSpentTime/100 >= nextAttack){generatorsEight[eight].amount = 0; generatorsEight[eight].mult = 1; nextAttack = 0; nextAttack = totalSpentTime/100 + random.Next(1, 120); } }, () => { amountBought = 2; });
        p2Challenges[3].challengesP2("C4: Explosive", "P1 is in a bad spot right now (P1 production is divided by 1,000,000,000,000)", "Reach 1,000,000 P1 to complete", "P1 production is multiplied by 1,000,000", false, false, () => { productionP1 /= 1000000000000; }, () => { productionP1 *= 1000000; });
        p2Challenges[4].challengesP2("C5: Lazy workers", "Working from home I guess (9th and 10th generators' multipliers are divided by 10)", "Reach 20 Beyond points as next prestige's production to complete", "9th and 10th generator now start off with one bought already wih base cost and it increases based on Points", false, false, () => { foreach(Gen gens in generatorsNineTen){gens.mult /= 10;} }, () => { foreach(Gen gens in generatorsNineTen){gens.startAmount = 1;} });
        p2Challenges[5].challengesP2("C6: Work leave (paid)", "Why Gen8 no work? :( (You can't buy 8th generators)", "Reach 1,000,000 Beyond points as next prestige's production to complete", "9th and 10th generators are boosted", false, false, () => {  }, () => { foreach(Gen gens in generatorsNineTen){gens.mult *= 1.005;} });
    }

    public static void enterChallenge(string tier, int id)
    {
        if(p2Amount < 1)
        {
            WriteLine("Get one Beyond prestige first to enter these!");
            return;
        }
        
        switch (tier)
        {
            case "p2":
                switch (id)
                {
                    case 1:
                        p2Challenges[0].challRunningState = true;
                        startChallenge("p2-1");
                        break;
                    case 2:
                        p2Challenges[1].challRunningState = true;
                        startChallenge("p2-2");
                        break;
                    case 3:
                        p2Challenges[2].challRunningState = true;
                        startChallenge("p2-3");
                        break;
                    case 4:
                        p2Challenges[3].challRunningState = true;
                        startChallenge("p2-4");
                        break;
                    case 5:
                        p2Challenges[4].challRunningState = true;
                        startChallenge("p2-5");
                        break;
                    case 6:
                        p2Challenges[5].challRunningState = true;
                        startChallenge("p2-6");
                        break;
                    default:
                        WriteLine("ERROR: That id doesn't exist!");
                        break;
                }
                break;
            default:
                WriteLine("ERROR: That tier is not present!");
                break;
        }
    }

    public static void startChallenge(string challInternalID)
    {
        switch (challInternalID)
        {
            case "p2-1":
                p2Challenges[0].challModifier();
                exitChallenge("p2-1");
                break;
            case "p2-2":
                p2Challenges[1].challModifier();
                exitChallenge("p2-2");
                break;
            case "p2-3":
                p2Challenges[2].challModifier();
                exitChallenge("p2-3");
                break;
            case "p2-4":
                p2Challenges[3].challModifier();
                exitChallenge("p2-4");
                break;
            case "p2-5":
                p2Challenges[4].challModifier();
                exitChallenge("p2-5");
                break;
            case "p2-6":
                p2Challenges[5].challModifier();
                exitChallenge("p2-6");
                break;
            default:
                WriteLine("// There seems to be some problem! \nError.InternalID.NotFound");
                break;
        }
    }

    public static void exitChallenge(string challID)
    {
        switch (challID)
        {
            case "p2-1":
                if(productionP2 >= 1)
                {
                    WriteLine("P2-1 has successfully beeen completed! \nP2CU1 has been unlocked!");
                    p2Challenges[0].challReward();
                    p2Challenges[0].challCompletion = true;
                    p2Challenges[0].challRunningState = false;
                    challengesCompleted += 1;
                }
                break;
            case "p2-2":
                if(pointsP0 >= 1e30)
                {
                    WriteLine("P2-2 has successfully beeen completed! \nP2CU2 has been unlocked!");
                    p2Challenges[1].challReward();
                    p2Challenges[1].challCompletion = true;
                    p2Challenges[1].challRunningState = false;
                    challengesCompleted += 1;
                }
                break;
            case "p2-3":
                if(generatorsEight.All(gens => gens.amount >= 1) && generatorsNineTen.All(gens => gens.amount >= 1))
                {
                    WriteLine("P2-3 has successfully beeen completed! \nP2CU3 has been unlocked!");
                    p2Challenges[2].challReward();
                    p2Challenges[2].challCompletion = true;
                    p2Challenges[2].challRunningState = false;
                    challengesCompleted += 1;
                }
                break;
            case "p2-4":
                if(multP1 >= 1000000)
                {
                    WriteLine("P2-4 has successfully beeen completed! \nP2CU4 has been unlocked!");
                    p2Challenges[3].challReward();
                    p2Challenges[3].challCompletion = true;
                    p2Challenges[3].challRunningState = false;
                    challengesCompleted += 1;
                }
                break;
            case "p2-5":
                if(productionP2 >= 20)
                {
                    WriteLine("P2-5 has successfully beeen completed! \nP2CU5 has been unlocked!");
                    p2Challenges[4].challReward();
                    p2Challenges[4].challCompletion = true;
                    p2Challenges[4].challRunningState = false;
                    challengesCompleted += 1;
                }
                break;
            case "p2-6":
                if(productionP2 >= 1000000)
                {
                    WriteLine("P2-6 has successfully beeen completed! \nP2CU6 has been unlocked!");
                    p2Challenges[5].challReward();
                    p2Challenges[5].challCompletion = true;
                    p2Challenges[5].challRunningState = false;
                    challengesCompleted += 1;
                }
                break;
            case "exitAll":
                foreach(Chall challs in p2Challenges)
                {
                    challs.challCompletion = false;
                    challs.challRunningState = false;
                }
                break;
            default:
                break;
        }
    }

    public static void lister()
    {
        addedChalls();

        if(p2Amount < 1)
        {
            WriteLine("Get one Beyond prestige first to see these!");
            return;
        }

        WriteLine("---------------------------------------------------------------------------------------------------------------------------");

        foreach(Chall challs in p2Challenges)
        {
            WriteLine($"Name:              {challs.challName}");
            WriteLine($"Description:       {challs.challDesc}");
            WriteLine($"Goal:              {challs.challGoal}");
            WriteLine($"Reward Description:{challs.challRewardDesc}");
            WriteLine($"Completion:        {Convert.ToString(challs.challCompletion)}");
            WriteLine("------------------------------------------------------------------------------------------------------------------------");
        }
    }

    public static void debug()
    {
        lister();
    }
}