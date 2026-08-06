using static System.Console;
using static System.Convert;
using static System.Math;
using static Gen;
using static Pre;
using static Chall;
using static Program;
using BreakInfinity;

class Upg
{
    public string upgradeName, upgradeDesc;
    public BigDouble upgradeCostP2;
    public bool upgradeStateOfBuy;
    public Action upgradeEffect;
    public static Upg[] p2StaticUpgrades = [];
    public static int timesBoughtP2SU = 0;
    public BigDouble upgradeCostHikeP2;
    public static Upg[] p2Upgrades = [];
    public BigDouble mG = multGain;
    public static BigDouble p2U2Mult = 2;
    public static Upg[] p2ChallengeUpgrades = [];
    public bool upgradeCheckChallengeCompletion;
    public static BigDouble totalGensMultsEight, totalGensMultsNineTen, gensBoughtEight, gensBoughtNineTen, totalGensBought;
    public static Upg[] p2FinalUpgrades = [];
    
    //main upgrades
    public void upgradesP2MainFinal(string name, string desc, BigDouble costP2, Action effect, bool buyState)
    {
        upgradeName = name;
        upgradeDesc = desc;
        upgradeCostP2 = costP2;
        upgradeEffect = effect;
        upgradeStateOfBuy = buyState;
    }

    //static upgrades
    public void upgradesP2Static(string name, string desc, BigDouble costP2, BigDouble costHike,Action effect)
    {
        upgradeName = name;
        upgradeDesc = desc;
        upgradeCostP2 = costP2;
        upgradeEffect = effect;
        upgradeCostHikeP2 = costHike;
    }

    //challenge upgrades
    public void upgradesP2Challenge(string name, string desc, BigDouble costP2, bool checkChallengeCompletion, Action effect, bool buyState)
    {
        upgradeName = name;
        upgradeDesc = desc;
        upgradeCostP2 = costP2;
        upgradeEffect = effect;
        upgradeCheckChallengeCompletion = checkChallengeCompletion;
        upgradeStateOfBuy = buyState;
    }

    public static void addedUpgrades()
    {
        if(p2StaticUpgrades.Length >= 1 && p2Upgrades.Length >= 6 && p2ChallengeUpgrades.Length >= 6 && p2FinalUpgrades.Length >= 9)
        {
            return;
        }

        p2StaticUpgrades = new Upg[1];
        for(int i = 0; i < p2StaticUpgrades.Length; i++)
        {
            p2StaticUpgrades[i] = new Upg();
        }

        p2Upgrades = new Upg[6];
        for(int i = 0; i < p2Upgrades.Length; i++)
        {
            p2Upgrades[i] = new Upg();
        }

        p2ChallengeUpgrades = new Upg[6];
        for (int i = 0; i < p2ChallengeUpgrades.Length; i++)
        {
            p2ChallengeUpgrades[i] = new Upg();
        }

        p2FinalUpgrades = new Upg[9];
        for (int i = 0; i < p2FinalUpgrades.Length; i++)
        {
            p2FinalUpgrades[i] = new Upg();
        }

        p2StaticUpgrades[0].upgradesP2Static("P2SU: Static P2 Increase", "Multiply P2 gain by x10 per level", 100, 10, () => { timesBoughtP2SU++; productionP2 *= Pow(10, timesBoughtP2SU); });

        p2Upgrades[0].upgradesP2MainFinal("P2U1: Production Boost", "Boosts all previous mults and production except that for P2 by 2 times.", 1, () => { multGain *= p2U2Mult; multP1 *= p2U2Mult; anotherMult *= p2U2Mult; foreach (Gen gens in generatorsEight){gens?.mult *= p2U2Mult;} foreach (Gen gens in generatorsNineTen){gens?.mult *= p2U2Mult;} }, false);
        p2Upgrades[1].upgradesP2MainFinal("P2U2: Gen8 Synergism", "Gen8 boosts Gen8 amounts.", 5 , () => { generatorsEight[7].amount *= 1 + BigDouble.Log10(generatorsEight[7].amount); }, false);
        p2Upgrades[2].upgradesP2MainFinal("P2U3: More P2", "Multliplies P2 gain by a dynamic amount based on time spent in this P2.", 10, () => { productionP2 *= timeThisP2 / 100; }, false);
        p2Upgrades[3].upgradesP2MainFinal("P2U4: Cheaper Gen9 and Gen10", "Divides Gen9 and Gen10 costs by 1,000 and 10,000", 1000, () => { generatorsNineTen[0].cost /= 1000; generatorsNineTen[1].cost /= 10000; if(generatorsNineTen[0].cost < 1){generatorsNineTen[0].cost = 1;} if (generatorsNineTen[1].cost < 1){generatorsNineTen[1].cost = 1;} }, false);
        p2Upgrades[4].upgradesP2MainFinal("P2U5: Overall Synergism", "All generators now boost their amounts.", 1000000, () => { foreach(Gen gens in generatorsEight){if (gens == generatorsEight[7]){continue;} gens.amount += BigDouble.Pow(BigDouble.Log10(gens.amount), 0.1);} }, false);
        p2Upgrades[5].upgradesP2MainFinal("P2U6: Lower Costs", "Reduces the cost scaling of all gens by 20%", 25000000, () => { foreach(Gen gens in generatorsEight){gens.costHike /= 1.2;} foreach(Gen gens in generatorsNineTen){gens.costHike /= 1.2;} }, false);

        p2ChallengeUpgrades[0].upgradesP2Challenge("P2CU1: MORE POINTS!", "Puts a static multiplier on points production based on total generators' multipliers", 1e9, p2Challenges[0].challCompletion, () => { foreach(Gen gens in generatorsEight){totalGensMultsEight += gens.mult - 1;} foreach(Gen gens in generatorsNineTen){totalGensMultsNineTen += gens.mult - 1;} productionP0 *= totalGensMultsEight + totalGensMultsNineTen + 1; }, false);
        p2ChallengeUpgrades[1].upgradesP2Challenge("P2CU2: MORE PRESTIGE POINTS!", "Makes the prestige points formula stronger based on points", 1e12, p2Challenges[1].challCompletion, () => { productionP1 += BigDouble.Pow(BigDouble.Log10(pointsP0), 10); }, false);
        p2ChallengeUpgrades[2].upgradesP2Challenge("P2CU3: MORE MULTIPLIERS!", "All generators' base multipliers are 900% better", 1e15, p2Challenges[2].challCompletion, () => { foreach(Gen gens in generatorsEight){gens.startMult *= 10;} foreach(Gen gens in generatorsNineTen){gens.startMult *= 10;} }, false);
        p2ChallengeUpgrades[3].upgradesP2Challenge("P2CU4: MORE BEYOND POINTS!", "Beyond points are multiplied based on number of challenges completed for Beyond layer", 1e21, p2Challenges[3].challCompletion, () => { productionP2 *= BigDouble.Log10(BigDouble.Pow(BigDouble.Pow(challengesCompleted, 100), 10)); }, false);
        p2ChallengeUpgrades[4].upgradesP2Challenge("P2CU5: MORE COST DEDUCION!", "All generators' cost are reduced based on how many have been bought in total", 1e27, p2Challenges[4].challCompletion, () => { boughtGensCalculator(); foreach(Gen gens in generatorsEight){gens.cost /= BigDouble.Log10(totalGensBought);} foreach(Gen gens in generatorsNineTen){gens.cost /= BigDouble.Log10(totalGensBought);} }, false);
        p2ChallengeUpgrades[5].upgradesP2Challenge("P2CU6: MORE... something?", "This upgrades seems to boost something 3.14159 times", 1e42, p2Challenges[5].challCompletion, () => { productionP2Amount *= 3.14159 * BigDouble.Pow(BigDouble.Log10(totalSpentTime/100), 0.75); }, false);

        p2FinalUpgrades[0].upgradesP2MainFinal("P2FUU1(1): ", "", 1e50, () => {}, false);
        p2FinalUpgrades[1].upgradesP2MainFinal("P2FUU2(2): ", "", 1e75, () => {}, false);
        p2FinalUpgrades[2].upgradesP2MainFinal("P2FUU3(3): ", "", 1e100, () => {}, false);
        p2FinalUpgrades[3].upgradesP2MainFinal("P2FLU1(4): ", "", 1e125, () => {}, false);
        p2FinalUpgrades[4].upgradesP2MainFinal("P2FLU2(5): ", "", 1e150, () => {}, false);
        p2FinalUpgrades[5].upgradesP2MainFinal("P2FLU3(6): ", "", 1e225, () => {}, false);
        p2FinalUpgrades[6].upgradesP2MainFinal("P2FMU1(7): ", "", 1e275, () => {}, false);
        p2FinalUpgrades[7].upgradesP2MainFinal("P2FMU2(8): ", "", 1e300, () => {}, false);
        p2FinalUpgrades[8].upgradesP2MainFinal("P2FU(9): ", "", 1e308, () => {}, false);
    }

    public static BigDouble boughtGensCalculator()
    {
        foreach(Gen gens in generatorsEight)
        {
            gensBoughtEight += gens.bought;
        }
        foreach(Gen gens in generatorsNineTen)
        {
            gensBoughtNineTen += gens.bought;
        }

        return totalGensBought = gensBoughtEight + gensBoughtNineTen;
    }

    public static void lister()
    {
        addedUpgrades();

        if(p2Amount < 1)
        {
            WriteLine("Get one Beyond prestige first to see these!");
            return;
        }
        
        WriteLine("---------------------------------------------------------------------------------------------------");

        WriteLine($"Name:         {p2StaticUpgrades[0].upgradeName}");
        WriteLine($"Description:  {p2StaticUpgrades[0].upgradeDesc}");
        WriteLine($"Cost:         {p2StaticUpgrades[0].upgradeCostP2}");
        WriteLine($"Times Bought: {timesBoughtP2SU}");

        WriteLine("---------------------------------------------------------------------------------------------------");

        foreach(Upg upgs in p2Upgrades)
        {
            WriteLine($"Name:         {upgs.upgradeName}");
            WriteLine($"Description:  {upgs.upgradeDesc}");
            WriteLine($"Cost:         {upgs.upgradeCostP2}");

            WriteLine("---------------------------------------------------------------------------------------------------");
        }

        foreach(Upg upgs in p2ChallengeUpgrades)
        {
            WriteLine($"Name:         {upgs.upgradeName}");
            WriteLine($"Description:  {upgs.upgradeDesc}");
            WriteLine($"Cost:         {upgs.upgradeCostP2}");

            WriteLine("---------------------------------------------------------------------------------------------------");
        }

        /*foreach(Upg upgs in p2FinalUpgrades)
        {
            WriteLine($"Name:         {upgs.upgradeName}");
            WriteLine($"Description:  {upgs.upgradeDesc}");
            WriteLine($"Cost:         {upgs.upgradeCostP2}");

            WriteLine("---------------------------------------------------------------------------------------------------");
        }*/
    }

    public static bool buyer(Upg upgsID)
    {
        addedUpgrades();

        if(p2Amount < 1)
        {
            WriteLine("Get one Beyond prestige first to buy these!");
            return false;
        }

        if(upgsID.upgradeStateOfBuy == true)
        {
            WriteLine("That upgrade has already been bought once!");
            return false;
        }

        if(pointsP2 < upgsID.upgradeCostP2)
        {
            WriteLine("Not enough beyond points!");
            return false;
        }
        
        if(upgsID == p2FinalUpgrades[0] || upgsID == p2FinalUpgrades[1] || upgsID == p2FinalUpgrades[2] || upgsID == p2FinalUpgrades[3] || upgsID == p2FinalUpgrades[4] || upgsID == p2FinalUpgrades[5] || upgsID == p2FinalUpgrades[6] || upgsID == p2FinalUpgrades[7] || upgsID == p2FinalUpgrades[8])
        {
            WriteLine("Those upgrades have not been implemented yet!");
            return false;
        }

        pointsP2 -= upgsID.upgradeCostP2;
        upgsID.upgradeStateOfBuy = true;
        upgsID.upgradeEffect();

        return true;
    }

    public static void debug()
    {
        WriteLine($"");
    }
}