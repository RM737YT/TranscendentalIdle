using System.Numerics;
using static System.Console;
using static System.Math;
using static Gen;

class Pre
{
    public static double productionP1 = 0;
    public static double multP1 = 1;
    public static double bestP1 = 0;

    public static void prestigeP1Gain()
    {
        addedGens();

        double totalGens = generators[0].amount + generators[1].amount + generators[2].amount + generators[3].amount + generators[4].amount + generators[5].amount + generators[6].amount;

        productionP1 = Pow(pointsP0 * totalGens / 1000.0, 0.16);
    }

    public static void prestigeP1Function()
    {
        Write("Confirm prestige action [Yes/No]: ");
        string toDoOrNotToDoThatIsTheQuestion = ReadLine()!;
        if (string.Equals(toDoOrNotToDoThatIsTheQuestion, "yes", StringComparison.OrdinalIgnoreCase) || string.Equals(toDoOrNotToDoThatIsTheQuestion, "y", StringComparison.OrdinalIgnoreCase))
        {
            multP1 += nextP1Gain();
            bestP1 = productionP1;
            pointsP0 = 0;
            preReset();
        }
        else
        {
            WriteLine("Prestige cancelled");
        }
    }

    public static double nextP1Gain()
    {
        double currentGain = productionP1;
        double gain = currentGain - bestP1;
        if (gain < 0)
        {
            gain = 0;;
        }
        
        return gain;
    }

    public static void debug()
    {
        WriteLine($"MultP1 = {multP1}");
        WriteLine($"ProductionP1 = {productionP1}");
    }
}