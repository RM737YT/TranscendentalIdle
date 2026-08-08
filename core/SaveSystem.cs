using static System.Console;
using static System.Convert;
using System.Text.Json;
using System.Text;
using static Pre;
using static Chall;
using static Upg;
using static Program;
using static Gen;
using BreakInfinity;

class Save
{
    private static void makeDir() => Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);
    private static string savePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Transcendental Idle", "saveDefault.json");
    
    public static bool saveFunction()
    {
        makeDir();

        addedGens();
        addedChalls();
        addedUpgrades();

        SaveData save = new SaveData
        {
            saveVersion = 1,

            tst = totalSpentTime.ToString(),
            tp2 = timeThisP2.ToString(),
            nas = nextAutoSave.ToString(),

            p0 = pointsP0.ToString(),
            pp0 = productionP0.ToString(),

            p1 = multP1.ToString(),
            pp1 = productionP1.ToString(),

            p2 = pointsP2.ToString(),
            pp2 = productionP2.ToString(),

            p2a = p2Amount.ToString(),
            pp2a = productionP2Amount.ToString(),

            cc = new bool[p2Challenges.Length],
            cs = new bool[p2Challenges.Length]
        };
        for (int i = 0; i < p2Challenges.Length; i++)
        {
            save.cc[i] = p2Challenges[i].challCompletion;
            save.cs[i] = p2Challenges[i].challRunningState;
        }

        save.sus = new string[p2StaticUpgrades.Length];
        save.sus[0] = timesBoughtP2SU1.ToString();
        save.sus[1] = timesBoughtP2SU2.ToString();

        save.us = new bool[p2Upgrades.Length];
        for (int i = 0; i < p2Upgrades.Length; i++)
        {
            save.us[i] = p2Upgrades[i].upgradeStateOfBuy;
        }

        save.cus = new bool[p2ChallengeUpgrades.Length];
        for (int i = 0; i < p2ChallengeUpgrades.Length; i++)
        {
            save.cus[i] = p2ChallengeUpgrades[i].upgradeStateOfBuy;
        }

        save.fus = new bool[p2FinalUpgrades.Length];        
        for (int i = 0; i < p2FinalUpgrades.Length; i++)
        {
            save.fus[i] = p2FinalUpgrades[i].upgradeStateOfBuy;
        }

        save.gbe = new string[generatorsEight.Length];
        save.gme = new string[generatorsEight.Length];
        save.gae = new string[generatorsEight.Length];
        save.gce = new string[generatorsEight.Length];
        for(int i = 0; i < generatorsEight.Length; i++)
        {
            save.gbe[i] = generatorsEight[i].bought.ToString();
            save.gme[i] = generatorsEight[i].mult.ToString();
            save.gae[i] = generatorsEight[i].amount.ToString();
            save.gce[i] = generatorsEight[i].cost.ToString();
        }

        save.gbnt = new string[generatorsNineTen.Length];
        save.gmnt = new string[generatorsNineTen.Length];
        save.gant = new string[generatorsNineTen.Length];
        save.gcnt = new string[generatorsNineTen.Length];
        for(int i = 0; i < generatorsNineTen.Length; i++)
        {
            save.gbnt[i] = generatorsNineTen[i].bought.ToString();
            save.gmnt[i] = generatorsNineTen[i].mult.ToString();
            save.gant[i] = generatorsNineTen[i].amount.ToString();
            save.gcnt[i] = generatorsNineTen[i].cost.ToString();
        }

        string json = JsonSerializer.Serialize(save, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
        string encoded = ToBase64String(Encoding.UTF8.GetBytes(json));
        File.WriteAllText(savePath, encoded);

        return true;
    }

    public static bool loadFunction()
    {
        if (!File.Exists(savePath))
        {
            saveFunction();
            return false;
        }

        string encoded = File.ReadAllText(savePath);
        string json = Encoding.UTF8.GetString(FromBase64String(encoded));
        SaveData save = JsonSerializer.Deserialize<SaveData>(json, new JsonSerializerOptions { IncludeFields = true })!;

        addedGens();
        addedChalls();
        addedUpgrades();

        totalSpentTime = BigDouble.Parse(save.tst);
        timeThisP2 = BigDouble.Parse(save.tp2);
        nextAutoSave = BigDouble.Parse(save.nas);

        pointsP0 = BigDouble.Parse(save.p0);
        productionP0 = BigDouble.Parse(save.pp0);

        multP1 = BigDouble.Parse(save.p1);
        productionP1 = BigDouble.Parse(save.pp1);

        pointsP2 = BigDouble.Parse(save.p2);
        productionP2 = BigDouble.Parse(save.pp2);

        p2Amount = BigDouble.Parse(save.p2a);
        productionP2Amount = BigDouble.Parse(save.pp2a);

        for (int i = 0; i < p2Challenges.Length; i++)
        {
            p2Challenges[i].challCompletion = save.cc[i];
            p2Challenges[i].challRunningState = save.cs[i];
        }

        timesBoughtP2SU1 = int.Parse(save.sus[0]);
        timesBoughtP2SU2 = int.Parse(save.sus[1]);

        for (int i = 0; i < p2Upgrades.Length; i++)
        {
            p2Upgrades[i].upgradeStateOfBuy = save.us[i];
        }

        for (int i = 0; i < p2ChallengeUpgrades.Length; i++)
        {
            p2ChallengeUpgrades[i].upgradeStateOfBuy = save.cus[i];
        }

        for (int i = 0; i < p2FinalUpgrades.Length; i++)
        {
            p2FinalUpgrades[i].upgradeStateOfBuy = save.fus[i];
        }

        for (int i = 0; i < generatorsEight.Length; i++)
        {
            generatorsEight[i].bought = BigDouble.Parse(save.gbe[i]);
            generatorsEight[i].mult = BigDouble.Parse(save.gme[i]);
            generatorsEight[i].amount = BigDouble.Parse(save.gae[i]);
            generatorsEight[i].cost = BigDouble.Parse(save.gce[i]);
        }

        for (int i = 0; i < generatorsNineTen.Length; i++)
        {
            generatorsNineTen[i].bought = BigDouble.Parse(save.gbnt[i]);
            generatorsNineTen[i].mult = BigDouble.Parse(save.gmnt[i]);
            generatorsNineTen[i].amount = BigDouble.Parse(save.gant[i]);
            generatorsNineTen[i].cost = BigDouble.Parse(save.gcnt[i]);
        }

        return true;
    }
}

class SaveCustom
{
    private static void makeDir() => Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);
    private static string savePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Transcendental Idle", $"{saveName}.json");
    
    public static bool saveFunctionCustom(string saveName)
    {
        makeDir();

        addedGens();
        addedChalls();
        addedUpgrades();

        SaveData save = new SaveData
        {
            saveVersion = 1,

            tst = totalSpentTime.ToString(),
            tp2 = timeThisP2.ToString(),
            nas = nextAutoSave.ToString(),

            p0 = pointsP0.ToString(),
            pp0 = productionP0.ToString(),

            p1 = multP1.ToString(),
            pp1 = productionP1.ToString(),

            p2 = pointsP2.ToString(),
            pp2 = productionP2.ToString(),

            p2a = p2Amount.ToString(),
            pp2a = productionP2Amount.ToString(),

            cc = new bool[p2Challenges.Length],
            cs = new bool[p2Challenges.Length]
        };
        for (int i = 0; i < p2Challenges.Length; i++)
        {
            save.cc[i] = p2Challenges[i].challCompletion;
            save.cs[i] = p2Challenges[i].challRunningState;
        }

        save.sus = new string[p2StaticUpgrades.Length];
        save.sus[0] = timesBoughtP2SU1.ToString();
        save.sus[1] = timesBoughtP2SU2.ToString();

        save.us = new bool[p2Upgrades.Length];
        for (int i = 0; i < p2Upgrades.Length; i++)
        {
            save.us[i] = p2Upgrades[i].upgradeStateOfBuy;
        }

        save.cus = new bool[p2ChallengeUpgrades.Length];
        for (int i = 0; i < p2ChallengeUpgrades.Length; i++)
        {
            save.cus[i] = p2ChallengeUpgrades[i].upgradeStateOfBuy;
        }

        save.fus = new bool[p2FinalUpgrades.Length];        
        for (int i = 0; i < p2FinalUpgrades.Length; i++)
        {
            save.fus[i] = p2FinalUpgrades[i].upgradeStateOfBuy;
        }

        save.gbe = new string[generatorsEight.Length];
        save.gme = new string[generatorsEight.Length];
        save.gae = new string[generatorsEight.Length];
        save.gce = new string[generatorsEight.Length];
        for(int i = 0; i < generatorsEight.Length; i++)
        {
            save.gbe[i] = generatorsEight[i].bought.ToString();
            save.gme[i] = generatorsEight[i].mult.ToString();
            save.gae[i] = generatorsEight[i].amount.ToString();
            save.gce[i] = generatorsEight[i].cost.ToString();
        }

        save.gbnt = new string[generatorsNineTen.Length];
        save.gmnt = new string[generatorsNineTen.Length];
        save.gant = new string[generatorsNineTen.Length];
        save.gcnt = new string[generatorsNineTen.Length];
        for(int i = 0; i < generatorsNineTen.Length; i++)
        {
            save.gbnt[i] = generatorsNineTen[i].bought.ToString();
            save.gmnt[i] = generatorsNineTen[i].mult.ToString();
            save.gant[i] = generatorsNineTen[i].amount.ToString();
            save.gcnt[i] = generatorsNineTen[i].cost.ToString();
        }

        string json = JsonSerializer.Serialize(save, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
        string encoded = ToBase64String(Encoding.UTF8.GetBytes(json));
        File.WriteAllText(savePath, encoded);

        return true;
    }

    public static bool loadFunctionCustom(string saveName)
    {
        string encoded = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Transcendental Idle", $"{saveName}.json"));
        string json = Encoding.UTF8.GetString(FromBase64String(encoded));
        SaveData save = JsonSerializer.Deserialize<SaveData>(json, new JsonSerializerOptions { IncludeFields = true })!;

        totalSpentTime = BigDouble.Parse(save.tst);
        timeThisP2 = BigDouble.Parse(save.tp2);
        nextAutoSave = BigDouble.Parse(save.nas);

        pointsP0 = BigDouble.Parse(save.p0);
        productionP0 = BigDouble.Parse(save.pp0);

        multP1 = BigDouble.Parse(save.p1);
        productionP1 = BigDouble.Parse(save.pp1);

        pointsP2 = BigDouble.Parse(save.p2);
        productionP2 = BigDouble.Parse(save.pp2);

        p2Amount = BigDouble.Parse(save.p2a);
        productionP2Amount = BigDouble.Parse(save.pp2a);

        for (int i = 0; i < p2Challenges.Length; i++)
        {
            p2Challenges[i].challCompletion = save.cc[i];
            p2Challenges[i].challRunningState = save.cs[i];
        }

        timesBoughtP2SU1 = int.Parse(save.sus[0]);
        timesBoughtP2SU2 = int.Parse(save.sus[1]);

        for (int i = 0; i < p2Upgrades.Length; i++)
        {
            p2Upgrades[i].upgradeStateOfBuy = save.us[i];
        }

        for (int i = 0; i < p2ChallengeUpgrades.Length; i++)
        {
            p2ChallengeUpgrades[i].upgradeStateOfBuy = save.cus[i];
        }

        for (int i = 0; i < p2FinalUpgrades.Length; i++)
        {
            p2FinalUpgrades[i].upgradeStateOfBuy = save.fus[i];
        }

        for (int i = 0; i < generatorsEight.Length; i++)
        {
            generatorsEight[i].bought = BigDouble.Parse(save.gbe[i]);
            generatorsEight[i].mult = BigDouble.Parse(save.gme[i]);
            generatorsEight[i].amount = BigDouble.Parse(save.gae[i]);
            generatorsEight[i].cost = BigDouble.Parse(save.gce[i]);
        }

        for (int i = 0; i < generatorsNineTen.Length; i++)
        {
            generatorsNineTen[i].bought = BigDouble.Parse(save.gbnt[i]);
            generatorsNineTen[i].mult = BigDouble.Parse(save.gmnt[i]);
            generatorsNineTen[i].amount = BigDouble.Parse(save.gant[i]);
            generatorsNineTen[i].cost = BigDouble.Parse(save.gcnt[i]);
        }

        return true;
    }
}

class SaveData
{
    public int saveVersion; // declaration of the variable for version of the save REMEMBER TO UPDATE FOR EVERY MAJOR COMMIT

    public string tst; // totalSpentTime current time
    public string tp2; // timeThisP2 current time
    public string nas; // nextAutoSave current time

    public string p0; // current p0 amount
    public string pp0; // p0 production

    public string p1; // current p1 amount
    public string pp1; // p1 production

    public string p2; // current p2 amount
    public string pp2; // p2 production

    public string p2a; // current p2 prestige amount
    public string pp2a; // p2 prestige production

    public bool[] cc; // challenge completed or not
    public bool[] cs; // challenge currently running or not

    public string[] sus; // times bought static upgrade

    public bool[] us; // upgrade bought or not
    public bool[] cus; // challenge upgrade bought or not
    public bool[] fus; // final upgrade bought or not

    public string[] gbe; // generator bought for g1-8
    public string[] gme; // generator multiplier for g1-8
    public string[] gae; // generator amount for g1-8
    public string[] gce; // generator cost for g1-8

    public string[] gbnt; // generator bought for g9-10
    public string[] gmnt; // generator multiplier for g9-10
    public string[] gant; // generator amount for g9-10
    public string[] gcnt; // generator cost for g9-10
}

class Debug
{
    public static void debug()
    {
        Save.loadFunction();
    }
}