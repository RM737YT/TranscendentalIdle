using static System.Console;
using static System.Math;
using static Gen;
using static Chall;
using static Upg;
using static Program;
using BreakInfinity;
class Pre
{
    public static BigDouble productionP1 = 0, productionP2 = 0, productionP2Amount = 1;
    public static BigDouble multP1 = 1, bestP1 = 0;
    public static BigDouble pointsP2 = 0, p2Amount = 0;

    public static void prestigeP1Gain()
    {
        addedGens();

        BigDouble totalGens = generatorsEight[0].amount + generatorsEight[1].amount + generatorsEight[2].amount + generatorsEight[3].amount + generatorsEight[4].amount + generatorsEight[5].amount + generatorsEight[6].amount;

        productionP1 = BigDouble.Pow(pointsP0 * totalGens / 1000, 0.4);
    }

    public static void prestigeP1Function()
    {
        WriteLine("-----------------------------------------------------------");

        Write("Confirm prestige action, this is irreversible [Yes/No]: ");
        string toDoOrNotToDoThatIsTheQuestion = ReadLine()!;

        WriteLine("-----------------------------------------------------------");
        if (string.Equals(toDoOrNotToDoThatIsTheQuestion, "yes", StringComparison.OrdinalIgnoreCase) || string.Equals(toDoOrNotToDoThatIsTheQuestion, "y", StringComparison.OrdinalIgnoreCase))
        {
            multP1 += nextP1Gain();
            bestP1 = productionP1;
            preReset();
        }
        else
        {
            WriteLine("------------------");

            WriteLine("Prestige cancelled");

            WriteLine("------------------");
        }
    }

    public static BigDouble nextP1Gain()
    {
        BigDouble currentGain = productionP1;
        BigDouble gain = currentGain - bestP1;
        if (gain < 0)
        {
            gain = 0;;
        }
        
        return gain;
    }

    public static void prestigeP2Gain()
    {
        addedGens();

        productionP2 = generatorsNineTen[0].mult * generatorsNineTen[0].amount * generatorsNineTen[1].mult * generatorsNineTen[1].amount / 100;
    }

    public static void prestigeP2Function()
    {
        WriteLine("----------------------------------------------------------------------------------------------------------------");

        Write("Confirm prestige action, this is more irreversible then the other, but you can buy some cool stuff [Yes/No]: ");
        string toDoOrNotToDoThatIsTheQuestion = ReadLine()!;

        WriteLine("-----------------------------------------------------------------------------------------------------------------");
        if (string.Equals(toDoOrNotToDoThatIsTheQuestion, "yes", StringComparison.OrdinalIgnoreCase) || string.Equals(toDoOrNotToDoThatIsTheQuestion, "y", StringComparison.OrdinalIgnoreCase))
        {
            pointsP2 += productionP2;
            p2Amount += productionP2Amount;
            Thread.Sleep(100);
            pre2Reset();
        }
        else
        {
            WriteLine("------------------");

            WriteLine("Prestige cancelled");

            WriteLine("------------------");
        }
    }

    public static void pre2Reset()
    {
        addedGens();
        boughtGensCalculator();
        preReset();

        multP1 = 1;
        productionP1 = 0;
        timeThisP2 = 0;
    }

    public static void debug()
    {
        WriteLine($"MultP1 = {multP1}");
        WriteLine($"ProductionP1 = {productionP1}");
        WriteLine($"ProductionP2 = {productionP2}");
    }
}