using static System.Console;
using static System.Convert;
using static System.Math;
using static Pre;
using static FLN;
using static Chall;
using static Upg;
using static Program;

public class Gen
{
    public double amount, mult, cost, costHike;
    public double genEightCost, genEightCostHike;
    public static double multGain = 1;
    public static double productionP0;
    public static double pointsP0;
    public static double anotherMult;
    public double startAmount, startMult, startCost;
    public double startGenEightCost;
    public static Gen[] generatorsEight = [];
    public static Gen[] generatorsNineTen = [];

    public void Gens(int id, double multGain)
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

    public void Generator(double amount, double mult , double cost, double costHike)
    {
        addedGens();

        this.amount = amount;
        this.mult = mult;
        this.cost = cost;
        this.costHike = costHike;

        startAmount = amount;
        startMult = mult;
        startCost = cost;

        double genMult = 1 + (generatorsEight[3].amount + generatorsEight[4].amount + generatorsEight[5].amount + generatorsEight[6].amount) / 10000;
        productionP0 = generatorsEight[0].amount * generatorsEight[0].mult * multGain * (generatorsEight[1].amount + 1) * (generatorsEight[2].amount + 1) * genMult * multP1;
    }

    public void GeneratorNineTen(double amount, double mult, double genEightCost, double genEightCostHike)
    {
        addedGens();

        this.amount = amount;
        this.mult = mult;
        this.genEightCost = genEightCost;
        this.genEightCostHike = genEightCostHike;

        startAmount = amount;
        startMult = mult;
        startGenEightCost = genEightCost;
    }
    
    public static void recalculateProduction()
    {
        addedGens();

        double genMult = 1 + (generatorsEight[3].amount + generatorsEight[4].amount + generatorsEight[5].amount + generatorsEight[6].amount) / 10000;
        productionP0 = generatorsEight[0].amount * generatorsEight[0].mult * multGain * (generatorsEight[1].amount + 1) * (generatorsEight[2].amount + 1) * genMult * multP1;
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

        generatorsEight[0].Generator(1, 1.5, 10, 10); //gen1
        generatorsEight[1].Generator(0, 1, 100, 10); //gen2
        generatorsEight[2].Generator(0, 1, 1000, 10); //gen3
        generatorsEight[3].Generator(0, 1, 10000, 100); //gen4
        generatorsEight[4].Generator(0, 1, 100000, 100); //gen5
        generatorsEight[5].Generator(0, 1, 1000000, 100); //gen6
        generatorsEight[6].Generator(0, 1, 10000000, 1000); //gen7
        generatorsEight[7].Generator(0, 1, 100000000, 1000); //gen8

        generatorsNineTen[0].GeneratorNineTen(0, 1, 10, 10); //gen9
        generatorsNineTen[1].GeneratorNineTen(0, 1, 100, 100); //gen10
    }

    public static void lister()
    {
        addedGens();


        WriteLine("---------------------------------------------------");
        WriteLine($"{"Generator",-15} {"Mult",-12} {"Amount",-12} {"Cost",-15}");
        WriteLine("---------------------------------------------------");

        WriteLine();

        WriteLine($"{"Generator 1",-15} {fln(generatorsEight[0].mult),-12} {fln(generatorsEight[0].amount),-12} {fln(generatorsEight[0].cost),-15}");
        WriteLine($"{"Generator 2",-15} {fln(generatorsEight[1].mult),-12} {fln(generatorsEight[1].amount),-12} {fln(generatorsEight[1].cost),-15}");
        WriteLine($"{"Generator 3",-15} {fln(generatorsEight[2].mult),-12} {fln(generatorsEight[2].amount),-12} {fln(generatorsEight[2].cost),-15}");
        WriteLine($"{"Generator 4",-15} {fln(generatorsEight[3].mult),-12} {fln(generatorsEight[3].amount),-12} {fln(generatorsEight[3].cost),-15}");
        WriteLine($"{"Generator 5",-15} {fln(generatorsEight[4].mult),-12} {fln(generatorsEight[4].amount),-12} {fln(generatorsEight[4].cost),-15}");
        WriteLine($"{"Generator 6",-15} {fln(generatorsEight[5].mult),-12} {fln(generatorsEight[5].amount),-12} {fln(generatorsEight[5].cost),-15}");
        WriteLine($"{"Generator 7",-15} {fln(generatorsEight[6].mult),-12} {fln(generatorsEight[6].amount),-12} {fln(generatorsEight[6].cost),-15}");

        WriteLine();

        WriteLine($"{"Generator 8",-15} {fln(generatorsEight[7].mult),-12} {fln(generatorsEight[7].amount),-12} {fln(generatorsEight[7].cost),-15}");

        WriteLine();

        WriteLine("---------------------------------------------------");
        WriteLine($"{"Generator",-15} {"Mult",-12} {"Amount",-12} {"Gen8 Cost",-10}");
        WriteLine("---------------------------------------------------");

        WriteLine();

        WriteLine($"{"Generator 9",-15} {fln(generatorsNineTen[0].mult),-12} {fln(generatorsNineTen[0].amount),-12} {fln(generatorsNineTen[0].genEightCost),-15}");
        WriteLine($"{"Generator 10",-15} {fln(generatorsNineTen[1].mult),-12} {fln(generatorsNineTen[1].amount),-12} {fln(generatorsNineTen[1].genEightCost),-15}");

        WriteLine("---------------------------------------------------");
    }
    
    public static void production()
    {
        addedGens();

        pointsP0 += productionP0;
    }

    public static bool buyer(Gen genNumber)
    {
        addedGens();

        if (pointsP0 < genNumber.cost)
        {
            WriteLine("Not enough points!");
            return false;
        }

        if(genNumber == generatorsEight[0] || genNumber == generatorsEight[1] || genNumber == generatorsEight[2] || genNumber == generatorsEight[3] || genNumber == generatorsEight[4] || genNumber == generatorsEight[5] || genNumber == generatorsEight[6] || genNumber == generatorsEight[7]){pointsP0 -= genNumber.cost;}
        else if(genNumber == generatorsNineTen[0] || genNumber == generatorsNineTen[1]){generatorsEight[7].amount -= genNumber.cost;}
        genNumber.amount++;
        genNumber.cost *= genNumber.costHike;
        if(genNumber == generatorsEight[0] || genNumber == generatorsEight[7]){genNumber.mult *= 1.5;}
        else if(genNumber == generatorsEight[1] || genNumber == generatorsEight[2] || genNumber == generatorsEight[3]){genNumber.mult *= 1.05;}
        else if(genNumber == generatorsEight[4] || genNumber == generatorsEight[5] || genNumber == generatorsEight[6] || genNumber == generatorsNineTen[0] || genNumber == generatorsNineTen[1]){genNumber.mult *= 1.005;}

        return true;
    }

    public static void buyerMax()
    {
        WriteLine("----------------------------------------");

        WriteLine("Trying to buy all generators in order...");

        for(int i = 0; i < generatorsEight.Length; i++)
        {
            while(pointsP0 >= generatorsEight[i].cost)
            {
                Gen genNum = generatorsEight[i];
                buyer(genNum);
            }
        }

        for(int i = 0; i < generatorsNineTen.Length; i++)
        {
            while(generatorsEight[7].amount >= generatorsNineTen[i].genEightCost)
            {
                Gen genNum = generatorsNineTen[i];
                buyer(genNum);
                WriteLine($"Gen8 amount: {generatorsEight[7].amount}");
                WriteLine($"Gen9 cost: {generatorsNineTen[0].genEightCost}");
                WriteLine($"Gen10 cost: {generatorsNineTen[1].genEightCost}");
            }
        }

        WriteLine("All possible generators bought!");

        WriteLine("----------------------------------------");
    }

    public static double produceGenEights()
    {
        anotherMult = generatorsEight[7].amount / 1000000;

        return generatorsEight[7].amount += generatorsEight[7].mult * anotherMult;
    }

    public static void preReset()
    {
        addedGens();

        pointsP0 = 0;
        foreach (Gen gen in generatorsEight)
        {
            if (gen != null)
            {
                gen.amount = gen.startAmount;
                gen.mult = gen.startMult;
                gen.cost = gen.startCost;
            }
        }
    }

    public static void debug()
    {
        WriteLine($"multGain={multGain}");
    }
}