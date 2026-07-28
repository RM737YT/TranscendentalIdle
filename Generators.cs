using static System.Console;
using static System.Convert;
using static System.Math;
using static Pre;
using static FLN;

public class Gen
{
    public double amount, mult, cost, costHike;
    public double genEightCost, genEightCostHike;
    public static double multGain = 1;
    public static double productionP0, productionP2;
    public static double pointsP0;
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

    public static void buyer(int genNumber)
    {
        addedGens();

        switch (genNumber)
        {
            case 1:
                if (pointsP0 >= generatorsEight[0].cost)
                {
                    pointsP0 -= generatorsEight[0].cost;
                    generatorsEight[0].amount++;
                    generatorsEight[0].mult *= 1.5;
                    generatorsEight[0].cost *= generatorsEight[0].costHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 2:
                if (pointsP0 >= generatorsEight[1].cost)
                {
                    pointsP0 -= generatorsEight[1].cost;
                    generatorsEight[1].amount++;
                    generatorsEight[1].mult *= 1.05;
                    generatorsEight[1].cost *= generatorsEight[1].costHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 3:
                if (pointsP0 >= generatorsEight[2].cost)
                {
                    pointsP0 -= generatorsEight[2].cost;
                    generatorsEight[2].amount++;
                    generatorsEight[2].mult *= 1.05;
                    generatorsEight[2].cost *= generatorsEight[2].costHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 4:
                if (pointsP0 >= generatorsEight[3].cost)
                {
                    pointsP0 -= generatorsEight[3].cost;
                    generatorsEight[3].amount++;
                    generatorsEight[3].mult *= 1.05;
                    generatorsEight[3].cost *= generatorsEight[3].costHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 5:
                if (pointsP0 >= generatorsEight[4].cost)
                {
                    pointsP0 -= generatorsEight[4].cost;
                    generatorsEight[4].amount++;
                    generatorsEight[4].mult *= 1.005;
                    generatorsEight[4].cost *= generatorsEight[4].costHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 6:
                if (pointsP0 >= generatorsEight[5].cost)
                {
                    pointsP0 -= generatorsEight[5].cost;
                    generatorsEight[5].amount++;
                    generatorsEight[5].mult *= 1.005;
                    generatorsEight[5].cost *= generatorsEight[5].costHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 7:
                if (pointsP0 >= generatorsEight[6].cost)
                {
                    pointsP0 -= generatorsEight[6].cost;
                    generatorsEight[6].amount++;
                    generatorsEight[6].mult *= 1.005;
                    generatorsEight[6].cost *= generatorsEight[6].costHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 8:
                if (pointsP0 >= generatorsEight[7].cost)
                {
                    pointsP0 -= generatorsEight[7].cost;
                    generatorsEight[7].amount++;
                    generatorsEight[7].mult *= 1.5;
                    generatorsEight[7].cost *= generatorsEight[7].costHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 9:
                if (generatorsEight[7].amount >= generatorsNineTen[0].genEightCost)
                {
                    generatorsEight[7].amount -= generatorsNineTen[0].genEightCost;
                    generatorsNineTen[0].amount++;
                    generatorsNineTen[0].mult *= 1.05;
                    generatorsNineTen[0].genEightCost *= generatorsNineTen[0].genEightCostHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 10:
                if (generatorsEight[7].amount >= generatorsNineTen[1].genEightCost)
                {
                    generatorsEight[7].amount -= generatorsNineTen[1].genEightCost;
                    generatorsNineTen[1].amount++;
                    generatorsNineTen[1].mult *= 1.05;
                    generatorsNineTen[1].genEightCost *= generatorsNineTen[1].genEightCostHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            default:
                WriteLine("Invalid generator!");
                break;
        }
    }

    public static void produceGenEights()
    {
        double anotherMult = generatorsEight[7].amount / 1000000;

        generatorsEight[7].amount += generatorsEight[7].mult * anotherMult;
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