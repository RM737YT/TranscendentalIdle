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
    public static int timesBoughtP2SU1 = 0, timesBoughtP2SU2 = 0;
    public BigDouble upgradeCostHikeP2;
    public Action upgradeBuyAdd;
    public static Upg[] p2Upgrades = [];
    public BigDouble mG = multGain;
    public static BigDouble p2U2Mult = 2;
    public static Upg[] p2ChallengeUpgrades = [];
    public bool upgradeCheckChallengeCompletion;
    public static BigDouble totalGensMultsEight, totalGensMultsNineTen, gensBoughtEight, gensBoughtNineTen, totalGensBought;
    public static Upg[] p2FinalUpgrades = [];
    public static int uniqueUpgradesBought;
    public static BigDouble totalGensMult, totalGensAmount;

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
    public void upgradesP2Static(string name, string desc, BigDouble costP2, BigDouble costHike, Action buyAdd, Action effect)
    {
        upgradeName = name;
        upgradeDesc = desc;
        upgradeCostP2 = costP2;
        upgradeEffect = effect;
        upgradeCostHikeP2 = costHike;
        upgradeBuyAdd = buyAdd;
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

        p2StaticUpgrades = new Upg[2];
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

        p2StaticUpgrades[0].upgradesP2Static("P2SU1: Static Beyond point Increase", "Multiply Beyond point gain by x10 per level", 100, 10, () => { timesBoughtP2SU1++; } ,() => { productionP2 *= BigDouble.Pow(10, timesBoughtP2SU1); });
        p2StaticUpgrades[1].upgradesP2Static("P2SU2: Static Beyone prestige Increase", "Multiply Beyond prestige gain by 2x per level", 1e45, 1e15, () => { timesBoughtP2SU2++; }, () => { productionP2Amount *= BigDouble.Pow(2, timesBoughtP2SU2); });

        p2Upgrades[0].upgradesP2MainFinal("P2U1: Production Boost", "Boosts all previous mults and production except that for P2 by 2 times.", 1, () => { multGain *= p2U2Mult; multP1 *= p2U2Mult; anotherMult *= p2U2Mult; foreach (Gen gens in generatorsEight){gens?.mult *= p2U2Mult;} foreach (Gen gens in generatorsNineTen){gens?.mult *= p2U2Mult;} }, false);
        p2Upgrades[1].upgradesP2MainFinal("P2U2: Synergism of Gen8 Synergism", "Gen8 boosts Gen8 amount even more.", 5 , () => { generatorsEight[7].amount *= 1 + BigDouble.Log10(generatorsEight[7].amount); }, false);
        p2Upgrades[2].upgradesP2MainFinal("P2U3: More P2", "Multliplies P2 gain by a dynamic amount based on time spent in this P2.", 10, () => { productionP2 *= timeThisP2 / 1000; }, false);
        p2Upgrades[3].upgradesP2MainFinal("P2U4: Cheaper Gen9 and Gen10", "Divides Gen9 and Gen10 costs by 1,000 and 10,000", 1000, () => { generatorsNineTen[0].cost /= 1000; generatorsNineTen[1].cost /= 10000; if(generatorsNineTen[0].cost < 1){generatorsNineTen[0].cost = 1;} if (generatorsNineTen[1].cost < 1){generatorsNineTen[1].cost = 1;} }, false);
        p2Upgrades[4].upgradesP2MainFinal("P2U5: Overall Synergism", "All generators now boost their amounts.", 1000000, () => { foreach(Gen gens in generatorsEight){if (gens == generatorsEight[7]){continue;} gens.amount += BigDouble.Pow(BigDouble.Log10(gens.amount), 0.1);} }, false);
        p2Upgrades[5].upgradesP2MainFinal("P2U6: Lesser Scalings", "Reduces the cost scaling of all gens by 20%", 25000000, () => { foreach(Gen gens in generatorsEight){gens.costHike /= 1.2;} foreach(Gen gens in generatorsNineTen){gens.costHike /= 1.2;} }, false);

        p2ChallengeUpgrades[0].upgradesP2Challenge("P2CU1: MORE POINTS!", "Puts a static multiplier on points production based on total generators' multipliers", 1e9, p2Challenges[0].challCompletion, () => { foreach(Gen gens in generatorsEight){totalGensMultsEight += gens.mult - 1;} foreach(Gen gens in generatorsNineTen){totalGensMultsNineTen += gens.mult - 1;} productionP0 *= totalGensMultsEight + totalGensMultsNineTen + 1; }, false);
        p2ChallengeUpgrades[1].upgradesP2Challenge("P2CU2: MORE PRESTIGE POINTS!", "Makes the prestige points formula stronger based on points", 1e12, p2Challenges[1].challCompletion, () => { productionP1 += BigDouble.Pow(BigDouble.Log10(pointsP0), 10); }, false);
        p2ChallengeUpgrades[2].upgradesP2Challenge("P2CU3: MORE MULTIPLIERS!", "All generators' base multipliers are 900% better", 1e15, p2Challenges[2].challCompletion, () => { foreach(Gen gens in generatorsEight){gens.startMult *= 10;} foreach(Gen gens in generatorsNineTen){gens.startMult *= 10;} }, false);
        p2ChallengeUpgrades[3].upgradesP2Challenge("P2CU4: MORE BEYOND POINTS!", "Beyond points are multiplied based on number of challenges completed for Beyond layer", 1e21, p2Challenges[3].challCompletion, () => { productionP2 *= BigDouble.Log10(BigDouble.Pow(BigDouble.Pow(challengesCompleted, 100), 10)); }, false);
        p2ChallengeUpgrades[4].upgradesP2Challenge("P2CU5: MORE COST DEDUCION!", "All generators' cost are reduced based on how many have been bought in total", 1e27, p2Challenges[4].challCompletion, () => { boughtGensCalculator(); foreach(Gen gens in generatorsEight){gens.cost /= BigDouble.Log10(totalGensBought);} foreach(Gen gens in generatorsNineTen){gens.cost /= BigDouble.Log10(totalGensBought);} }, false);
        p2ChallengeUpgrades[5].upgradesP2Challenge("P2CU6: MORE... something?", "This upgrades seems to boost something 3.14159 times", 1e42, p2Challenges[5].challCompletion, () => { productionP2Amount *= 3.14159 * BigDouble.Pow(BigDouble.Log10(totalSpentTime / 1000), 0.75); }, false);

        p2FinalUpgrades[0].upgradesP2MainFinal("P2FUU1(1): Multiply prestige Booster", "Multiply prestige gain is boosted by number of unique upgrades bought (P2SU's not counted)", 1e50, () => { productionP1 *= Pow(uniqueUpgradesBought, 12); }, false);
        p2FinalUpgrades[1].upgradesP2MainFinal("P2FUU2(2): Beyond to Normal", "Amount of Beyond prestiges done boosts Points gain", 1e75, () => { productionP0 *= BigDouble.Pow(BigDouble.Pow(BigDouble.Log10(p2Amount), 10), 1.5); }, false);
        p2FinalUpgrades[2].upgradesP2MainFinal("P2FUU3(3): Cost Boosts Nine and Ten", "9th and 10th generators are boosted by their costs", 1e100, () => { foreach(Gen gens in generatorsNineTen){gens.mult *= BigDouble.Pow(BigDouble.Log10(gens.gensEightCost), 1.1);} }, false);
        p2FinalUpgrades[3].upgradesP2MainFinal("P2FLU1(4): Beyond prestige Synergism", "Beyond prestige boosts it's own gain", 1e125, () => { productionP2Amount *= BigDouble.Pow(BigDouble.Log10(BigDouble.Pow(p2Amount, 10)), 2); }, false);
        p2FinalUpgrades[4].upgradesP2MainFinal("P2FLU2(5): Less Gen8 Costs", "Decreases 8th generator's cost step from its multiplier", 1e150, () => { generatorsEight[7].costHike /= BigDouble.Log10(generatorsEight[7].mult); }, false);
        p2FinalUpgrades[5].upgradesP2MainFinal("P2FLU3(6): Better Gen4-7", "4th, 5th, 6th and 7th generators' multipliers are multiplied by time spent in this Beyond prestige", 1e225, () => { foreach(Gen gens in generatorsEight){if(gens == generatorsEight[0] || gens == generatorsEight[1] || gens == generatorsEight[2]){continue;} gens.mult *= BigDouble.Log10(BigDouble.Pow(timeThisP2 / 1000, 15));} }, false);
        p2FinalUpgrades[6].upgradesP2MainFinal("P2FMU1(7): Beyond prestige Auto Gain", "Every tick, your Beyond prestige is added by the amount you would have gotten on prestige", 1e275, () => { /*see tick() for this implementation*/ }, false);
        p2FinalUpgrades[7].upgradesP2MainFinal("P2FMU2(8): MORE... TIME?!?!", "Multiplies the time this Beyond prestige by a factor of all the generators' multipliers and amounts combined", 1e300, () => { timeThisP2 *= gensAmountMultCalculator(); }, false);
        p2FinalUpgrades[8].upgradesP2MainFinal("P2FU(9): ???", "Unlocks something in future...", 1e308, () => {}, false);
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

    public static BigDouble gensAmountMultCalculator()
    {
        foreach(Gen gens in generatorsEight)
        {
            totalGensMult += gens.mult;
        }
        foreach(Gen gens in generatorsEight)
        {
            totalGensAmount += gens.amount;
        }

        foreach(Gen gens in generatorsNineTen)
        {
            totalGensMult += gens.mult;
        }
        foreach(Gen gens in generatorsNineTen)
        {
            totalGensAmount += gens.amount;
        }

        return BigDouble.Log10(BigDouble.Pow(totalGensAmount/1e308 * totalGensMult, 10.5)) * BigDouble.Pow10(PI) / 50000000;
    }

    public static void lister()
    {
        addedUpgrades();

        if(p2Amount < 1)
        {
            WriteLine("-------------------------------------------");

            WriteLine("Get one Beyond prestige first to see these!");

            WriteLine("-------------------------------------------");
            return;
        }
        
        WriteLine("-------------------------------------------------------------------------------------------------------");

        foreach(Upg upgs in p2StaticUpgrades)
        {
            WriteLine($"Name:         {upgs.upgradeName}");
            WriteLine($"Description:  {upgs.upgradeDesc}");
            WriteLine($"Cost:         {upgs.upgradeCostP2}");
            WriteLine($"Times Bought: {upgs.upgradeBuyAdd}");
        }

        WriteLine("-------------------------------------------------------------------------------------------------------");

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

        foreach(Upg upgs in p2FinalUpgrades)
        {
            WriteLine($"Name:         {upgs.upgradeName}");
            WriteLine($"Description:  {upgs.upgradeDesc}");
            WriteLine($"Cost:         {upgs.upgradeCostP2}");

            WriteLine("---------------------------------------------------------------------------------------------------");
        }
    }

    public static bool buyer(Upg upgsID)
    {
        addedUpgrades();

        if(p2Amount < 1)
        {
            WriteLine("-------------------------------------------");

            WriteLine("Get one Beyond prestige first to buy these!");

            WriteLine("-------------------------------------------");
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

        pointsP2 -= upgsID.upgradeCostP2;
        upgsID.upgradeStateOfBuy = true;
        upgsID.upgradeEffect();
        if(upgsID != p2StaticUpgrades[0] || upgsID != p2StaticUpgrades[1])
        {
            uniqueUpgradesBought++;
        }

        return true;
    }

    public static BigDouble produceBeyondPrestige()
    {
        return p2Amount += productionP2Amount;
    }

    public static void debug()
    {
        WriteLine($"");
    }
}