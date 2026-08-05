using static System.Console;
using static System.Convert;
using static System.Math;
using static Pre;
using static Chall;
using static Upg;
using static Program;
using System.Formats.Asn1;
using System.Security.AccessControl;
using BreakInfinity;

public class Gen
{
    public BigDouble amount, mult, cost, costHike, bought;
    public BigDouble gensEightCost, gensEightCostHike;
    public static BigDouble amountBought = 1;
    public static BigDouble multGain = 1;
    public static BigDouble productionP0;
    public static BigDouble pointsP0;
    public static BigDouble anotherMult;
    public BigDouble startAmount, startMult, startCost;
    public BigDouble startGenEightCost;
    public static Gen[] generatorsEight = [];
    public static Gen[] generatorsNineTen = [];

    public void Gens(int id, BigDouble multGain)
    {
        int totalUniqueGens = 0;
        for (int i = 0; i < id; i++)
        {
            totalUniqueGens++;
            multGain *= 1.0000005;

            if (totalUniqueGens > 10 || multGain > Pow(1.0000005, 10))
            {
                totalUniqueGens = 10;
                multGain = Pow(1.0000005, 10);
            }
        }
    }

    public void Generator(BigDouble amount, BigDouble mult , BigDouble cost, BigDouble costHike, BigDouble bought)
    {
        addedGens();

        this.amount = amount;
        this.mult = mult;
        this.cost = cost;
        this.costHike = costHike;
        this.bought = bought;

        startAmount = amount;
        startMult = mult;
        startCost = cost;

        BigDouble gensMult = 1 + (generatorsEight[3].amount + generatorsEight[4].amount + generatorsEight[5].amount + generatorsEight[6].amount) / 10000;
        productionP0 = generatorsEight[0].amount * generatorsEight[0].mult * multGain * (generatorsEight[1].amount + 1) * (generatorsEight[2].amount + 1) * gensMult * multP1;
    }

    public void GeneratorNineTen(BigDouble amount, BigDouble mult, BigDouble gensEightCost, BigDouble gensEightCostHike, BigDouble bought)
    {
        addedGens();

        this.amount = amount;
        this.mult = mult;
        this.gensEightCost = gensEightCost;
        this.gensEightCostHike = gensEightCostHike;
        this.bought = bought;

        startAmount = amount;
        startMult = mult;
        startGenEightCost = gensEightCost;
    }
    
    public static BigDouble recalculateProduction()
    {
        addedGens();

        BigDouble gensMult = 1 + (generatorsEight[3].amount + generatorsEight[4].amount + generatorsEight[5].amount + generatorsEight[6].amount) / 10000;
        return productionP0 = generatorsEight[0].amount * generatorsEight[0].mult * multGain * (generatorsEight[1].amount + 1) * (generatorsEight[2].amount + 1) * gensMult * multP1;
    }

    public static void addedGens()
    {
        if (generatorsEight.Length >= 8 && generatorsNineTen.Length >= 2)
        {
            return;
        }

        generatorsEight = new Gen[8];
        for (int i = 0; i < generatorsEight.Length; i++)
        {
            generatorsEight[i] = new Gen();
        }

        generatorsNineTen = new Gen[2];
        for (int i = 0; i < generatorsNineTen.Length; i++)
        {
            generatorsNineTen[i] = new Gen();
        }

        generatorsEight[0].Generator(1, 1.5, 10, 10, 0); //gens1
        generatorsEight[1].Generator(0, 1, 100, 10, 0); //gens2
        generatorsEight[2].Generator(0, 1, 1000, 10, 0); //gens3
        generatorsEight[3].Generator(0, 1, 10000, 100, 0); //gens4
        generatorsEight[4].Generator(0, 1, 100000, 100, 0); //gens5
        generatorsEight[5].Generator(0, 1, 1000000, 100, 0); //gens6
        generatorsEight[6].Generator(0, 1, 10000000, 1000, 0); //gens7
        generatorsEight[7].Generator(0, 1, 100000000, 1000, 0); //gens8

        generatorsNineTen[0].GeneratorNineTen(0, 1, 10, 10, 0); //gens9
        generatorsNineTen[1].GeneratorNineTen(0, 1, 100, 100, 0); //gens10

        generatorsNineTen[0].cost = 100;
        generatorsNineTen[1].cost = 1000;
        generatorsNineTen[0].costHike = 100;
        generatorsNineTen[1].costHike = 1000;
    }

    public static void lister()
    {
        addedGens();


        WriteLine("---------------------------------------------------");
        WriteLine($"{"Generator",-15} {"Mult",-12} {"Amount",-12} {"Cost",-15}");
        WriteLine("---------------------------------------------------");

        WriteLine();

        WriteLine($"{"Generator 1",-15} {generatorsEight[0].mult,-12} {generatorsEight[0].amount,-12} {generatorsEight[0].cost,-15}");
        WriteLine($"{"Generator 2",-15} {generatorsEight[1].mult,-12} {generatorsEight[1].amount,-12} {generatorsEight[1].cost,-15}");
        WriteLine($"{"Generator 3",-15} {generatorsEight[2].mult,-12} {generatorsEight[2].amount,-12} {generatorsEight[2].cost,-15}");
        WriteLine($"{"Generator 4",-15} {generatorsEight[3].mult,-12} {generatorsEight[3].amount,-12} {generatorsEight[3].cost,-15}");
        WriteLine($"{"Generator 5",-15} {generatorsEight[4].mult,-12} {generatorsEight[4].amount,-12} {generatorsEight[4].cost,-15}");
        WriteLine($"{"Generator 6",-15} {generatorsEight[5].mult,-12} {generatorsEight[5].amount,-12} {generatorsEight[5].cost,-15}");
        WriteLine($"{"Generator 7",-15} {generatorsEight[6].mult,-12} {generatorsEight[6].amount,-12} {generatorsEight[6].cost,-15}");

        WriteLine();

        WriteLine($"{"Generator 8",-15} {generatorsEight[7].mult,-12} {generatorsEight[7].amount,-12} {generatorsEight[7].cost,-15}");

        WriteLine();

        WriteLine("---------------------------------------------------");
        WriteLine($"{"Generator",-15} {"Mult",-12} {"Amount",-12} {"Gen8 Cost",-10}");
        WriteLine("---------------------------------------------------");

        WriteLine();

        WriteLine($"{"Generator 9",-15} {generatorsNineTen[0].mult,-12} {generatorsNineTen[0].amount,-12} {generatorsNineTen[0].gensEightCost,-15}");
        WriteLine($"{"Generator 10",-15} {generatorsNineTen[1].mult,-12} {generatorsNineTen[1].amount,-12} {generatorsNineTen[1].gensEightCost,-15}");

        WriteLine("---------------------------------------------------");
    }
    
    public static BigDouble production()
    {
        addedGens();

        pointsP0 += productionP0;
        return pointsP0;
    }

    public static bool buyer(Gen gensNumber)
    {
        addedGens();

        if (pointsP0 < gensNumber.cost)
        {
            WriteLine("Not enough points!");
            return false;
        }

        if(gensNumber == generatorsEight[0] || gensNumber == generatorsEight[1] || gensNumber == generatorsEight[2] || gensNumber == generatorsEight[3] || gensNumber == generatorsEight[4] || gensNumber == generatorsEight[5] || gensNumber == generatorsEight[6] || gensNumber == generatorsEight[7]){pointsP0 -= gensNumber.cost;}
        else if(gensNumber == generatorsNineTen[0] || gensNumber == generatorsNineTen[1]){generatorsEight[7].amount -= gensNumber.gensEightCost;}

        gensNumber.amount += amountBought;
        gensNumber.bought += 1;

        if(gensNumber == generatorsEight[0] || gensNumber == generatorsEight[1] || gensNumber == generatorsEight[2] || gensNumber == generatorsEight[3] || gensNumber == generatorsEight[4] || gensNumber == generatorsEight[5] || gensNumber == generatorsEight[6] || gensNumber == generatorsEight[7]){gensNumber.cost *= gensNumber.costHike;}
        else if(gensNumber == generatorsNineTen[0] || gensNumber == generatorsNineTen[1]){gensNumber.gensEightCost *= gensNumber.gensEightCostHike;}

        if(gensNumber == generatorsEight[0] || gensNumber == generatorsEight[7]){gensNumber.mult *= 1.5;}
        else if(gensNumber == generatorsEight[1] || gensNumber == generatorsEight[2] || gensNumber == generatorsEight[3]){gensNumber.mult *= 1.05;}
        else if(gensNumber == generatorsEight[4] || gensNumber == generatorsEight[5] || gensNumber == generatorsEight[6] || gensNumber == generatorsNineTen[0] || gensNumber == generatorsNineTen[1]){gensNumber.mult *= 1.005;}

        return true;
    }

    public static bool buyerChallSix(Gen gensNumber)
    {
        addedGens();

        if(gensNumber == generatorsEight[7])
        {
            WriteLine("Generator 8 is currently on a paid leave!");
            return false;
        }

        if (pointsP0 < gensNumber.cost)
        {
            WriteLine("Not enough points!");
            return false;
        }

        if(gensNumber == generatorsEight[0] || gensNumber == generatorsEight[1] || gensNumber == generatorsEight[2] || gensNumber == generatorsEight[3] || gensNumber == generatorsEight[4] || gensNumber == generatorsEight[5] || gensNumber == generatorsEight[6] || gensNumber == generatorsEight[7]){pointsP0 -= gensNumber.cost;}
        else if(gensNumber == generatorsNineTen[0] || gensNumber == generatorsNineTen[1]){pointsP0 -= gensNumber.cost;}

        if(gensNumber == generatorsEight[7]){gensNumber.amount = 0;}
        else{gensNumber.amount += amountBought;}

        gensNumber.bought += 1;
        gensNumber.cost *= gensNumber.costHike;
        
        if(gensNumber == generatorsEight[7]){gensNumber.mult = 1;}
        else if(gensNumber == generatorsEight[0]){gensNumber.mult *= 1.5;}
        else if(gensNumber == generatorsEight[1] || gensNumber == generatorsEight[2] || gensNumber == generatorsEight[3]){gensNumber.mult *= 1.05;}
        else if(gensNumber == generatorsEight[4] || gensNumber == generatorsEight[5] || gensNumber == generatorsEight[6] || gensNumber == generatorsNineTen[0] || gensNumber == generatorsNineTen[1]){gensNumber.mult *= 1.005;}

        return true;
    }

    public static void buyerMax()
    {
        if(p2Challenges[5].challRunningState == true)
        {
            WriteLine("----------------------------------------");

            WriteLine("Trying to buy all generators in order...");

            for(int i = 0; i < generatorsEight.Length - 1; i++)
            {
                while(pointsP0 >= generatorsEight[i].cost)
                {
                    Gen gensNum = generatorsEight[i];
                    buyerChallSix(gensNum);
                }
            }

            for(int i = 0; i < generatorsNineTen.Length; i++)
            {
                while(generatorsEight[7].amount >= generatorsNineTen[i].gensEightCost)
                {
                    Gen gensNum = generatorsNineTen[i];
                    buyerChallSix(gensNum);
                }
            }

            WriteLine("All possible generators bought!");

            WriteLine("----------------------------------------");
        }
        else
        {
            WriteLine("----------------------------------------");

            WriteLine("Trying to buy all generators in order...");

            for(int i = 0; i < generatorsEight.Length; i++)
            {
                while(pointsP0 >= generatorsEight[i].cost)
                {
                    Gen gensNum = generatorsEight[i];
                    buyer(gensNum);
                }
            }

            for(int i = 0; i < generatorsNineTen.Length; i++)
            {
                while(generatorsEight[7].amount >= generatorsNineTen[i].gensEightCost)
                {
                    Gen gensNum = generatorsNineTen[i];
                    buyer(gensNum);
                }
            }

            WriteLine("All possible generators bought!");

            WriteLine("----------------------------------------");
        }
    }

    public static BigDouble produceGenEights()
    {
        anotherMult = generatorsEight[7].amount / 1000;
        
        return generatorsEight[7].amount += generatorsEight[7].mult * anotherMult;
    }

    public static void produceGenNineTen()
    {
        if(p2Challenges[4].challCompletion == true)
        {
            foreach(Gen genss in generatorsNineTen)
            {
                genss.amount += BigDouble.Pow(BigDouble.Pow(BigDouble.Log10(1 + pointsP0), 0.1), 0.60002);
            } 
        }
    }

    public static void preReset()
    {
        addedGens();

        pointsP0 = 0;
        foreach (Gen gens in generatorsEight)
        {
            if (gens != null)
            {
                gens.amount = gens.startAmount;
                gens.mult = gens.startMult;
                gens.cost = gens.startCost;
                gens.bought = 0;
            }
        }

        foreach (Gen gens in generatorsNineTen)
        {
            if (gens != null)
            {
                gens.amount = gens.startAmount;
                gens.mult = gens.startMult;
                gens.gensEightCost = gens.startGenEightCost;
                gens.bought = 0;
            }
        }
    }

    public static void debug()
    {
        WriteLine($"multGain={multGain}");
    }
}