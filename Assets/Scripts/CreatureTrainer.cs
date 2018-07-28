using System.Collections.Generic;
using UnityEngine;

public interface ICreatureTrainer
{
    void Train(CreatureList.Creature creatureOne, CreatureList.Creature creatureTwo);
}

public abstract class Trainer : ICreatureTrainer
{
    protected string name;
    public string Name
    {
        get
        {
            return name;
        }
    }
    protected int numberOfCreaturesRequired;
    protected int pairedTrainingMultiplier;
    protected float price;
    public float Price
    {
        get
        {
            return price;
        }
    }
    protected string stats;
    public string Stats
    {
        get
        {
            return stats;
        }
    }
    public System.Guid guid;
    public GameObject creatureOne;
    public GameObject creatureTwo;

    public Trainer()
    {
        name = "Default Trainer";
        numberOfCreaturesRequired = 0;
        pairedTrainingMultiplier = 2;
        price = 0.00f;
        stats = "^None";
        guid = System.Guid.NewGuid();
    }

    protected int SuccessChance()
    {
        return Random.Range(1, 101);
    }

    protected int StatsGained(CreatureList.Creature creature)
    {
        return (int)(Random.Range(1f, 10f) * (creature.stats.intelligence / 100f));
    }

    public abstract void Train(CreatureList.Creature creatureOne, CreatureList.Creature creatureTwo);
}

public class PunchingBag : Trainer
{

    public PunchingBag()
    {
        name = "Punching Bag";
        numberOfCreaturesRequired = 1;
        price = 500f;
        stats = "^ST";
    }

    public override void Train(CreatureList.Creature creatureOne, CreatureList.Creature creatureTwo)
    {
        int successChance = SuccessChance();
        int statsGained = StatsGained(creatureOne);

        if (successChance <= creatureOne.temperament)
        {
            creatureOne.stats.strength += statsGained;
        }

        if (successChance % 2 == 0)
        {
            creatureOne.temperament += 1;
        }

        creatureOne.secondaryStats.Stress += 15;
        creatureOne.secondaryStats.Fatigue += 15;
    }

}

public class PounceTrainer : Trainer
{

    public PounceTrainer()
    {
        name = "Pounce Trainer";
        numberOfCreaturesRequired = 1;
        price = 500f;
    }

    public override void Train(CreatureList.Creature creatureOne, CreatureList.Creature creatureTwo)
    {
        int successChance = SuccessChance();
        int statsGained = StatsGained(creatureOne);

        if (successChance <= creatureOne.temperament)
        {
            creatureOne.stats.dexterity += statsGained;
        }

        if (successChance % 2 == 0)
        {
            creatureOne.temperament += 1;
        }

        creatureOne.secondaryStats.Stress += 10;
        creatureOne.secondaryStats.Fatigue += 20;
    }
}

public class Puzzles : Trainer
{

    public Puzzles()
    {
        name = "Puzzles";
        numberOfCreaturesRequired = 1;
        price = 500f;
    }

    public override void Train(CreatureList.Creature creatureOne, CreatureList.Creature creatureTwo)
    {
        int successChance = SuccessChance();
        int statsGained = StatsGained(creatureOne);

        if (successChance <= creatureOne.temperament)
        {
            creatureOne.stats.intelligence += statsGained;
        }

        if (successChance % 2 == 0)
        {
            creatureOne.temperament += 1;
        }

        creatureOne.secondaryStats.Stress += 20;
        creatureOne.secondaryStats.Fatigue += 10;
    }
}

public class Track : Trainer
{

    public Track()
    {
        name = "Track";
        numberOfCreaturesRequired = 1;
        price = 500f;
    }

    public override void Train(CreatureList.Creature creatureOne, CreatureList.Creature creatureTwo)
    {
        int successChance = SuccessChance();
        int statsGained = StatsGained(creatureOne);

        if (successChance <= creatureOne.temperament)
        {
            creatureOne.stats.fitness += statsGained;
        }

        if (successChance % 2 == 0)
        {
            creatureOne.temperament += 1;
        }

        creatureOne.secondaryStats.Stress += 10;
        creatureOne.secondaryStats.Fatigue += 20;
    }
}

public class SparringRing : Trainer
{

    public SparringRing()
    {
        name = "Sparring Ring";
        numberOfCreaturesRequired = 2;
    }

    public override void Train(CreatureList.Creature creatureOne, CreatureList.Creature creatureTwo)
    {
        int successChanceOne = SuccessChance();
        int statsGainedSTOne = StatsGained(creatureOne);
        int statsGainedDXOne = StatsGained(creatureOne);

        int successChanceTwo = SuccessChance();
        int statsGainedSTTwo = StatsGained(creatureTwo);
        int statsGainedDXTwo = StatsGained(creatureTwo);

        if (successChanceOne <= creatureOne.temperament && successChanceTwo <= creatureTwo.temperament)
        {
            creatureOne.stats.strength += statsGainedSTOne;
            creatureOne.stats.dexterity += statsGainedDXOne;

            creatureTwo.stats.strength += statsGainedSTTwo;
            creatureTwo.stats.dexterity += statsGainedDXTwo;
        }
        else if (successChanceOne <= creatureOne.temperament)
        {
            creatureOne.stats.strength += statsGainedSTOne / 2;
            creatureOne.stats.dexterity += statsGainedDXOne / 2;
        }
        else if (successChanceTwo <= creatureTwo.temperament)
        {
            creatureTwo.stats.strength += statsGainedSTTwo / 2;
            creatureTwo.stats.dexterity += statsGainedDXTwo / 2;
        }

        if (successChanceOne % 2 == 0)
        {
            creatureOne.temperament += 1;
        }
        if (successChanceTwo % 2 == 0)
        {
            creatureTwo.temperament += 1;
        }

        creatureOne.secondaryStats.Stress += 25;
        creatureOne.secondaryStats.Fatigue += 25;

        creatureTwo.secondaryStats.Stress += 25;
        creatureTwo.secondaryStats.Fatigue += 25;

    }
}

public class TacticalGames : Trainer
{

    public TacticalGames()
    {
        name = "Tactical Games";
        numberOfCreaturesRequired = 2;
    }

    public override void Train(CreatureList.Creature creatureOne, CreatureList.Creature creatureTwo)
    {
        int successChanceOne = SuccessChance();
        int statsGainedOne = StatsGained(creatureOne) * pairedTrainingMultiplier;
        int statsLostOne = StatsGained(creatureOne) / pairedTrainingMultiplier;

        int successChanceTwo = SuccessChance();
        int statsGainedTwo = StatsGained(creatureTwo) * pairedTrainingMultiplier;
        int statsLostTwo = StatsGained(creatureTwo) / pairedTrainingMultiplier;

        if (successChanceOne <= creatureOne.temperament && successChanceTwo <= creatureTwo.temperament)
        {
            creatureOne.stats.intelligence += statsGainedOne;
            creatureOne.stats.fitness -= statsLostOne;

            creatureTwo.stats.intelligence += statsGainedTwo;
            creatureTwo.stats.fitness -= statsLostTwo;
        }
        else if (successChanceOne <= creatureOne.temperament)
        {
            creatureOne.stats.intelligence += statsGainedOne / pairedTrainingMultiplier;
            creatureOne.stats.fitness -= statsLostOne / pairedTrainingMultiplier;
        }
        else if (successChanceTwo <= creatureTwo.temperament)
        {
            creatureTwo.stats.intelligence += statsGainedTwo / pairedTrainingMultiplier;
            creatureTwo.stats.fitness -= statsLostTwo / pairedTrainingMultiplier;
        }

        if (successChanceOne % 2 == 0)
        {
            creatureOne.temperament += 1;
        }
        if (successChanceTwo % 2 == 0)
        {
            creatureTwo.temperament += 1;
        }

        creatureOne.secondaryStats.Stress += 30;
        creatureOne.secondaryStats.Fatigue += 15;

        creatureTwo.secondaryStats.Stress += 30;
        creatureTwo.secondaryStats.Fatigue += 15;

    }
}

public class CompetitiveSwim : Trainer
{

    public CompetitiveSwim()
    {
        name = "Competitive Swim";
        numberOfCreaturesRequired = 2;
    }

    public override void Train(CreatureList.Creature creatureOne, CreatureList.Creature creatureTwo)
    {
        int successChanceOne = SuccessChance();
        int statsGainedOne = StatsGained(creatureOne);

        int successChanceTwo = SuccessChance();
        int statsGainedTwo = StatsGained(creatureTwo);

        if (successChanceOne < successChanceTwo)
        {
            statsGainedOne *= pairedTrainingMultiplier;
        }
        else if (successChanceTwo < successChanceOne)
        {
            statsGainedTwo *= pairedTrainingMultiplier;
        }
        else
        {
            statsGainedOne *= pairedTrainingMultiplier;
            statsGainedTwo *= pairedTrainingMultiplier;
        }

        if (successChanceOne <= creatureOne.temperament && successChanceTwo <= creatureTwo.temperament)
        {
            creatureOne.stats.fitness += statsGainedOne;
            creatureTwo.stats.fitness += statsGainedTwo;
        }
        else if (successChanceOne <= creatureOne.temperament)
        {
            creatureOne.stats.fitness += statsGainedOne;
        }
        else if (successChanceTwo <= creatureTwo.temperament)
        {
            creatureTwo.stats.fitness += statsGainedTwo;
        }

        if (successChanceOne % 2 == 0)
        {
            creatureOne.temperament += 1;
        }
        if (successChanceTwo % 2 == 0)
        {
            creatureTwo.temperament += 1;
        }

        creatureOne.secondaryStats.Stress += 15;
        creatureOne.secondaryStats.Fatigue += 30;

        creatureTwo.secondaryStats.Stress += 15;
        creatureTwo.secondaryStats.Fatigue += 30;
    }
}

public class TugOfWar : Trainer
{

    public TugOfWar()
    {
        name = "Tug of War";
        numberOfCreaturesRequired = 2;
    }

    public override void Train(CreatureList.Creature creatureOne, CreatureList.Creature creatureTwo)
    {
        int successChanceOne = SuccessChance();
        int statsGainedOne = StatsGained(creatureOne);

        int successChanceTwo = SuccessChance();
        int statsGainedTwo = StatsGained(creatureTwo);

        if (successChanceOne < successChanceTwo)
        {
            statsGainedOne *= pairedTrainingMultiplier;
        }
        else if (successChanceTwo < successChanceOne)
        {
            statsGainedTwo *= pairedTrainingMultiplier;
        }
        else
        {
            statsGainedOne *= pairedTrainingMultiplier;
            statsGainedTwo *= pairedTrainingMultiplier;
        }

        if (successChanceOne <= creatureOne.temperament && successChanceTwo <= creatureTwo.temperament)
        {
            creatureOne.stats.strength += statsGainedOne;
            creatureTwo.stats.strength += statsGainedTwo;
        }
        else if (successChanceOne <= creatureOne.temperament)
        {
            creatureOne.stats.strength += statsGainedOne;
        }
        else if (successChanceTwo <= creatureTwo.temperament)
        {
            creatureTwo.stats.strength += statsGainedTwo;
        }

        if (successChanceOne % 2 == 0)
        {
            creatureOne.temperament += 1;
        }
        if (successChanceTwo % 2 == 0)
        {
            creatureTwo.temperament += 1;
        }

        creatureOne.secondaryStats.Stress += 20;
        creatureOne.secondaryStats.Fatigue += 25;

        creatureTwo.secondaryStats.Stress += 20;
        creatureTwo.secondaryStats.Fatigue += 25;
    }
}

public class CreatureTrainer : MonoBehaviour {
    /// <summary>
    /// Outline of which training does what:
    /// 
    /// Basic Training:
    /// PunchingBag = ^Strength
    /// Pounce = ^Dexterity
    /// Puzzles = ^Intelligence
    /// Run = ^Fitness
    /// 
    /// Paired Training:
    /// Sparring Ring = ^ST, ^DX for both creatures, chance of injury
    /// Tactical Games = ^^IQ for both creatures, vFitness
    /// Competitive Swim = ^^FT for one creature, ^FT for other
    /// Tug-of-War = ^^ST for one creature, ^ST for other
    /// Catch = ^^DX for both creatures, vIQ
    /// 
    /// </summary>

    public List<Trainer> trainingEquipment;
    public Trainer activeEquipment;

    public string equipmentName;

    // Use this for initialization
    void Start()
    {
        trainingEquipment = new List<Trainer>
        {
            new PunchingBag(),
            new PounceTrainer(),
            new Puzzles(),
            new Track(),
            new SparringRing(),
            new TacticalGames(),
            new CompetitiveSwim(),
            new TugOfWar()
        };

        for(int i = 0; i < trainingEquipment.Count; i++)
        {
            if(equipmentName == trainingEquipment[i].Name)
            {
                activeEquipment = trainingEquipment[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    


}
