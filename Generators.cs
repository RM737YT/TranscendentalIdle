using static System.Console;
using static System.Convert;
using static System.Math;

public class Gen
{
    public double amount, mult, cost, costHike;
    public static double multGain = 1;
    public static double productionP0;
    public static double pointsP0;
    public static Gen[] generators;

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

        double genMult = 1 + (generators[3].amount + generators[4].amount + generators[5].amount + generators[6].amount) / 2;

        productionP0 = generators[0].amount * generators[0].mult * multGain * (generators[1].amount + 1) * (generators[2].amount + 1) * genMult;
    }
    
    public static void recalculateProduction()
    {
        addedGens();
        double genMult = 1 + (generators[3].amount + generators[4].amount + generators[5].amount + generators[6].amount) / 2;

        productionP0 = generators[0].amount * generators[0].mult * multGain * (generators[1].amount + 1) * (generators[2].amount + 1) * genMult;
    }

    public static void addedGens()
    {
        if (generators != null)
        {
            return;
        }

        generators = new Gen[10];
        for (int i = 0; i < generators.Length; i++)
        {
            generators[i] = new Gen();
        }

        generators[0].Generator(1, 1.5, 10, 10);
        generators[1].Generator(0, 1, 100, 10);
        generators[2].Generator(0, 1, 1000, 10);
        generators[3].Generator(0, 1, 10000, 100);
        generators[4].Generator(0, 1, 100000, 100);
        generators[5].Generator(0, 1, 1000000, 100);
        generators[6].Generator(0, 1, 10000000, 1000);
        generators[7].Generator(0, 1, 100000000, 1000);
        generators[8].Generator(0, 1, 1000000000, 1000);
        generators[9].Generator(0, 1, 10000000000, 10000);
    }

    public static void lister()
    {
        addedGens();
        WriteLine($"{"Generator",-15} {"Mult",-12} {"Amount",-12} {"Cost",-15}");

        WriteLine($"{"Generator 1",-15} {FLN.fln(generators[0].mult),-12} {FLN.fln(generators[0].amount),-12} {FLN.fln(generators[0].cost),-15}");
        WriteLine($"{"Generator 2",-15} {FLN.fln(generators[1].mult),-12} {FLN.fln(generators[1].amount),-12} {FLN.fln(generators[1].cost),-15}");
        WriteLine($"{"Generator 3",-15} {FLN.fln(generators[2].mult),-12} {FLN.fln(generators[2].amount),-12} {FLN.fln(generators[2].cost),-15}");
        WriteLine($"{"Generator 4",-15} {FLN.fln(generators[3].mult),-12} {FLN.fln(generators[3].amount),-12} {FLN.fln(generators[3].cost),-15}");
        WriteLine($"{"Generator 5",-15} {FLN.fln(generators[4].mult),-12} {FLN.fln(generators[4].amount),-12} {FLN.fln(generators[4].cost),-15}");
        WriteLine($"{"Generator 6",-15} {FLN.fln(generators[5].mult),-12} {FLN.fln(generators[5].amount),-12} {FLN.fln(generators[5].cost),-15}");
        WriteLine($"{"Generator 7",-15} {FLN.fln(generators[6].mult),-12} {FLN.fln(generators[6].amount),-12} {FLN.fln(generators[6].cost),-15}");
        WriteLine($"{"Generator 8",-15} {FLN.fln(generators[7].mult),-12} {FLN.fln(generators[7].amount),-12} {FLN.fln(generators[7].cost),-15}");
        WriteLine($"{"Generator 9",-15} {FLN.fln(generators[8].mult),-12} {FLN.fln(generators[8].amount),-12} {FLN.fln(generators[8].cost),-15}");
        WriteLine($"{"Generator 10",-15} {FLN.fln(generators[9].mult),-12} {FLN.fln(generators[9].amount),-12} {FLN.fln(generators[9].cost),-15}");
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
                if (pointsP0 >= generators[0].cost)
                {
                    pointsP0 -= generators[0].cost;
                    generators[0].amount++;
                    generators[0].mult *= 1.5;
                    generators[0].cost *= generators[0].costHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 2:
                if (pointsP0 >= generators[1].cost)
                {
                    pointsP0 -= generators[1].cost;
                    generators[1].amount++;
                    generators[1].mult *= 1.05;
                    generators[1].cost *= generators[1].costHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 3:
                if (pointsP0 >= generators[2].cost)
                {
                    pointsP0 -= generators[2].cost;
                    generators[2].amount++;
                    generators[2].mult *= 1.05;
                    generators[2].cost *= generators[2].costHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 4:
                if (pointsP0 >= generators[3].cost)
                {
                    pointsP0 -= generators[3].cost;
                    generators[3].amount++;
                    generators[3].mult *= 1.05;
                    generators[3].cost *= generators[3].costHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 5:
                if (pointsP0 >= generators[4].cost)
                {
                    pointsP0 -= generators[4].cost;
                    generators[4].amount++;
                    generators[4].mult *= 1.005;
                    generators[4].cost *= generators[4].costHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 6:
                if (pointsP0 >= generators[5].cost)
                {
                    pointsP0 -= generators[5].cost;
                    generators[5].amount++;
                    generators[5].mult *= 1.005;
                    generators[5].cost *= generators[5].costHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 7:
                if (pointsP0 >= generators[6].cost)
                {
                    pointsP0 -= generators[6].cost;
                    generators[6].amount++;
                    generators[6].mult *= 1.005;
                    generators[6].cost *= generators[6].costHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 8:
                if (pointsP0 >= generators[7].cost)
                {
                    pointsP0 -= generators[7].cost;
                    generators[7].amount++;
                    generators[7].mult *= 1.0005;
                    generators[7].cost *= generators[7].costHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 9:
                if (pointsP0 >= generators[8].cost)
                {
                    pointsP0 -= generators[8].cost;
                    generators[8].amount++;
                    generators[8].mult *= 1.0005;
                    generators[8].cost *= generators[8].costHike;
                }
                else
                {
                    WriteLine("Not enough points!");
                }
                break;
            case 10:
                if (pointsP0 >= generators[9].cost)
                {
                    pointsP0 -= generators[9].cost;
                    generators[9].amount++;
                    generators[9].mult *= 1.0005;
                    generators[9].cost *= generators[9].costHike;
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

    public static void debug()
    {
        WriteLine($"multGain={multGain}");
    }
}