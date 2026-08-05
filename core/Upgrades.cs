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
    public required string upgradeName, upgradeDesc;
    public BigDouble upgradeCostP2;
    public required bool upgradeStateOfBuy;
    public required Action upgradeEffect;
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
    public void upgradesP2(string name, string desc, BigDouble costP2,Action effect)
    {
        upgradeName = name;
        upgradeDesc = desc;
        upgradeCostP2 = costP2;
        upgradeEffect = effect;
    }

    //static upgrades
    public void upgradesP2(string name, string desc, BigDouble costP2, BigDouble costHike,Action effect)
    {
        upgradeName = name;
        upgradeDesc = desc;
        upgradeCostP2 = costP2;
        upgradeEffect = effect;
        upgradeCostHikeP2 = costHike;
    }

    //challenge upgrades
    public void upgradesP2(string name, string desc, BigDouble costP2, bool checkChallengeCompletion,Action effect)
    {
        upgradeName = name;
        upgradeDesc = desc;
        upgradeCostP2 = costP2;
        upgradeEffect = effect;
        upgradeCheckChallengeCompletion = checkChallengeCompletion;
    }

    public static void addedUpgrades()
    {
        p2StaticUpgrades[0].upgradesP2("P2SU: Static P2 Increase", "Multiply P2 gain by x10 per level", 100, 10, () => { productionP2 *= Pow(10, timesBoughtP2SU); });

        p2Upgrades[0].upgradesP2("P2U1: Production Boost", "Boosts all previous mults and production except that for P2 by 2 times.", 1, () => { multGain *= p2U2Mult; multP1 *= p2U2Mult; anotherMult *= p2U2Mult; foreach (Gen gens in generatorsEight){gens?.mult *= p2U2Mult;} foreach (Gen gens in generatorsNineTen){gens?.mult *= p2U2Mult;} });
        p2Upgrades[1].upgradesP2("P2U2: Gen8 Synergism", "Gen8 boosts Gen8 amounts.", 5 , () => { generatorsEight[7].amount *= 1 + BigDouble.Log10(generatorsEight[7].amount); });
        p2Upgrades[2].upgradesP2("P2U3: More P2", "Multliplies P2 gain by a dynamic amount based on time spent in this P2.", 10, () => { productionP2 *= timeThisP2 / 100; });
        p2Upgrades[3].upgradesP2("P2U4: Cheaper Gen9 and Gen10", "Divides Gen9 and Gen10 costs by 1,000 and 10,000", 1000, () => { generatorsNineTen[0].cost /= 1000; generatorsNineTen[1].cost /= 10000; if(generatorsNineTen[0].cost < 1){generatorsNineTen[0].cost = 1;} if (generatorsNineTen[1].cost < 1){generatorsNineTen[1].cost = 1;} });
        p2Upgrades[4].upgradesP2("P2U5: Overall Synergism", "All generators now boost their amounts.", 1000000, () => { foreach(Gen gens in generatorsEight){if (gens == generatorsEight[7]){continue;} gens.amount += BigDouble.Pow(BigDouble.Log10(gens.amount), 0.1);} });
        p2Upgrades[5].upgradesP2("P2U6: Lower Costs", "Reduces the cost scaling of all gens by 20%", 25000000, () => { foreach(Gen gens in generatorsEight){gens.costHike /= 1.2;} foreach(Gen gens in generatorsNineTen){gens.costHike /= 1.2;} });

        p2ChallengeUpgrades[0].upgradesP2("P2CU1: MORE POINTS!", "Puts a static multiplier on points production based on total generators' multipliers", 1e9, p2Challenges[0].challCompletion, () => { foreach(Gen gens in generatorsEight){totalGensMultsEight += gens.mult - 1;} foreach(Gen gens in generatorsNineTen){totalGensMultsNineTen += gens.mult - 1;} productionP0 *= totalGensMultsEight + totalGensMultsNineTen + 1; });
        p2ChallengeUpgrades[1].upgradesP2("P2CU2: MORE PRESTIGE POINTS!", "Makes the prestige points formula stronger based on points", 1e12, p2Challenges[1].challCompletion, () => { productionP1 += BigDouble.Pow(BigDouble.Log10(pointsP0), 10); });
        p2ChallengeUpgrades[2].upgradesP2("P2CU3: MORE MULTIPLIERS!", "All generators' base multipliers are 900% better", 1e15, p2Challenges[2].challCompletion, () => { foreach(Gen gens in generatorsEight){gens.startMult *= 10;} foreach(Gen gens in generatorsNineTen){gens.startMult *= 10;} });
        p2ChallengeUpgrades[3].upgradesP2("P2CU4: MORE BEYOND POINTS!", "Beyond points are multiplied based on number of challenges completed for Beyond layer", 1e21, p2Challenges[3].challCompletion, () => { productionP2 *= BigDouble.Log10(BigDouble.Pow(BigDouble.Pow(challengesCompleted, 100), 10)); });
        p2ChallengeUpgrades[4].upgradesP2("P2CU5: MORE COST DEDUCION!", "All generators' cost are reduced based on how many have been bought in total", 1e27, p2Challenges[4].challCompletion, () => { boughtGensCalculator(); foreach(Gen gens in generatorsEight){gens.cost /= BigDouble.Log10(totalGensBought);} foreach(Gen gens in generatorsNineTen){gens.cost /= BigDouble.Log10(totalGensBought);} });
        p2ChallengeUpgrades[5].upgradesP2("P2CU6: MORE... something?", "This upgrades seems to boost something 3.14159 times", 1e42, p2Challenges[5].challCompletion, () => { productionP2Amount *= 3.14159 * BigDouble.Pow(BigDouble.Log10(totalSpentTime/100), 0.75); });

        p2FinalUpgrades[0].upgradesP2("P2FUU1: ", "", 1e50, () => {});
        p2FinalUpgrades[1].upgradesP2("P2FUU2: ", "", 1e75, () => {});
        p2FinalUpgrades[2].upgradesP2("P2FUU3: ", "", 1e100, () => {});
        p2FinalUpgrades[3].upgradesP2("P2FLU1: ", "", 1e125, () => {});
        p2FinalUpgrades[4].upgradesP2("P2FLU2: ", "", 1e150, () => {});
        p2FinalUpgrades[5].upgradesP2("P2FLU3: ", "", 1e225, () => {});
        p2FinalUpgrades[6].upgradesP2("P2FMU1: ", "", 1e275, () => {});
        p2FinalUpgrades[7].upgradesP2("P2FMU2: ", "", 1e300, () => {});
        p2FinalUpgrades[8].upgradesP2("P2FU: ", "", 1e308, () => {});
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


    }

    public static void debug()
    {
        WriteLine($"");
    }
}