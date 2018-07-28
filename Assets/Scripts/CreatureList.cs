using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureList : MonoBehaviour {


    public List<Creature> creatures;

    [System.Serializable]
    public class Creature
    {
        public string GivenName { get; set; }             //the creature's given name
        public string species;          
        public Stats stats;
        public int temperament;                      //0-100, where 100 means completely agreeable and 0 means completely rebellious    //75 base
        public int lifeExpectancy;                   //in days                                                                          //1100 base
        public string bio;
        public SecondaryStats secondaryStats;
        public Guid guid;

        public Creature()
        {
            GivenName = "Default_Name";
            species = "Ratcher";
            stats = new Stats();
            temperament = 0;
            lifeExpectancy = 0;
            secondaryStats = new SecondaryStats();
            guid = Guid.NewGuid();
        }

        public void Sleep(NecessityManager.IRester bed)
        {
            bed.Rest(this);
        }

        public void Eat(NecessityManager.IEatable food)
        {
            food.Eat(this);
        }

        public void Play(NecessityManager.IEntertainable toy)
        {
            toy.Play(this);
        }
    }

    [System.Serializable]
    public class Stats
    {
        public int strength;
        public int dexterity;
        public int intelligence;
        public int fitness;

        public Stats()
        {

            strength = 10;                                                                                                              //100 base
            dexterity = 10;                                                                                                             //100 base
            intelligence = 10;                                                                                                          //100 base
            fitness = 10;                                                                                                               //100 base
        }
    }

    public class SecondaryStats
    {
        private int stress;
        public int Stress
        {
            get
            {
                return stress;
            }
            set
            {
                if (value <= 0)
                {
                    stress = 0;
                }
                else if (value >= 100)
                {
                    stress = 100;
                }
                else
                {
                    stress = value;
                }
            }
        }
        private int fatigue;
        public int Fatigue
        {
            get
            {
                return fatigue;
            }
            set
            {
                if(value <= 0)
                {
                    fatigue = 0;
                }
                else if(value >= 100)
                {
                    fatigue = 100;
                }
                else
                {
                    fatigue = value;
                }
            }
        }
        private int hunger;
        public int Hunger
        {
            get
            {
                return hunger;
            }
            set
            {
                if (value <= 0)
                {
                    hunger = 0;
                }
                else if (value >= 100)
                {
                    hunger = 100;
                }
                else
                {
                    hunger = value;
                }
            }
        }

        public SecondaryStats()
        {
            stress = 0;
            fatigue = 0;
            hunger = 0;
        }
    }
    /// <summary>
    /// Every creature gets a 50 point positive bonus total, with each point over the base equalling one point for every stat aside from lifeExpectancy
    /// for which every 10 days of lifeExpectancy is worth one "point". Increments of 5 (or 50 for lifeExpectancy) are best.
    /// </summary>
    public class Ratcher : Creature
    {

        public Ratcher()
        {
            GivenName = "Ratcher";
            species = "Ratcher";
            stats.strength = 90;
            stats.dexterity = 110;
            stats.intelligence = 135;
            stats.fitness = 110;
            temperament = 85;
            lifeExpectancy = 1100;      //Just over three years
            bio = "Ratchers are intelligent and inherently magical creatures, with limited power over pyromancy. It is said that they're sapient and can even learn to speak.";
        }

    }

    public class IceRatcher : Creature
    {

        public IceRatcher() : base()
        {
            GivenName = "Ice Ratcher";
            species = "Ice Ratcher";
            stats.strength = 80;
            stats.dexterity = 110;
            stats.intelligence = 135;
            stats.fitness = 120;
            temperament = 85;
            lifeExpectancy = 1100;      //Just over three years
            bio = "These rat-like birds are hardier than their cousins and have limited power over cryomancy. Like all types of Ratchers, Ice Ratchers are extremely intelligent and learn very quickly.";
        }
        
    }

    public class WindRatcher : Creature
    {

        public WindRatcher()
        {
            GivenName = "Wind Ratcher";
            species = "Wind Ratcher";
            stats.strength = 80;
            stats.dexterity = 120;
            stats.intelligence = 135;
            stats.fitness = 110;
            temperament = 85;
            lifeExpectancy = 1100;
            bio = "Wind Ratchers are the most nimble of the ratchers and, as the name suggests, practice aeromancy. They are not picky eaters, though they prefer carrion.";
        }

    }

    public class VoidRatcher : Creature
    {

        public VoidRatcher()
        {
            GivenName = "Void Ratcher";
            species = "Void Ratcher";
            stats.strength = 100;
            stats.dexterity = 110;
            stats.intelligence = 155;
            stats.fitness = 100;
            temperament = 70;
            lifeExpectancy = 1000;
            bio = "Void Ratchers are the rarest of their kind and are exceptional in many regards, foremost their intelligence and short temper. They tend to live shorter lives than their cousins.";
        }

    }

    public class Burdek : Creature
    {

        public Burdek()
        {

            GivenName = "Burdek";
            species = "Burdek";
            stats.strength = 100;
            stats.dexterity = 40;
            stats.intelligence = 100;
            stats.fitness = 85;
            temperament = 100;
            lifeExpectancy = 2100;
            bio = "Really? A Burdek?";
        }

    }

    public class Murderdeathatron : Creature
    {

        public Murderdeathatron()
        {
            GivenName = "Murderdeathatron";
            species = "Murderdeathatron";
            stats.strength = 140;
            stats.dexterity = 90;
            stats.intelligence = 90;
            stats.fitness = 120;
            temperament = 65;
            lifeExpectancy = 1300;
            bio = "They may look cute, but Murderdeathatrons got their name for a reason. They're exceptionally strong and are known to fly into fits of rage at the drop of a hat.";
        }

    }

    public class CoolMurderdeathatron : Creature
    {

        public CoolMurderdeathatron()
        {
            GivenName = "Cool Murderdeathatron";
            species = "Cool Murderdeathatron";
            stats.strength = 160;
            stats.dexterity = 100;
            stats.intelligence = 100;
            stats.fitness = 130;
            temperament = 55;
            lifeExpectancy = 1100;
            bio = "You often find yourself wishing you were as cool as this Murderdeathatron.";
        }

    }

    public class ToxicBoi : Creature
    {

        public ToxicBoi()
        {
            GivenName = "Toxic Boi";
            species = "Toxic Boi";
            stats.strength = 100;
            stats.dexterity = 140;
            stats.intelligence = 100;
            stats.fitness = 90;
            temperament = 95;
            lifeExpectancy = 1100;
            bio = "Despite a frightening appearance and a fearsome reputation, Toxic Bois actually make very loyal companions. They are, however, exceptionally venomous and occasionally poison their playmates.";
        }

    }

    public class HermitSquid : Creature
    {

        public HermitSquid()
        {
            GivenName = "Hermit Squid";
            species = "Hermit Squid";
            stats.strength = 105;
            stats.dexterity = 120;
            stats.intelligence = 100;
            stats.fitness = 140;
            temperament = 80;
            lifeExpectancy = 900;
            bio = "Hermit Squids' shells are actually a form of exoskeleton and provide the stability needed for them to live on the land. They are much happier in the water, however.";
        }

    }

    public class Krocosilisk : Creature
    {

        public Krocosilisk()
        {
            GivenName = "Krocosilisk";
            species = "Krocosilisk";            
            stats.strength = 150;
            stats.dexterity = 130;
            stats.intelligence = 80;
            stats.fitness = 115;
            temperament = 50;
            lifeExpectancy = 1300;
            bio = "Foul-tempered and unwilling to learn, Krocosilisks are challenging creatures to train. Despite this, they are still popular choices among more experienced trainers due to their natural prowess.";
        }

    }

    public class SpinyAnteater : Creature
    {

        public SpinyAnteater()
        {
            GivenName = "Spiny Anteater";
            species = "Spiny Anteater";         
            stats.strength = 80;
            stats.dexterity = 85;
            stats.intelligence = 120;
            stats.fitness = 130;
            temperament = 80;
            lifeExpectancy = 1600;
            bio = "The Spiny Anteater is known for both its exoskeleton spine and its long life. While they're not the most impressive creature at the beginning of their lives, the patient trainer is sure to reap great rewards.";
        }
    }

    public class Laretyme : Creature
    {


        public Laretyme()
        {
            GivenName = "Laretyme";
            species = "Laretyme";
            stats.strength = 110;
            stats.dexterity = 120;
            stats.intelligence = 85;
            stats.fitness = 105;
            temperament = 85;
            lifeExpectancy = 1500;
            bio = "Laretymes are good natured and mostly docile, so they are often kept as household pets by weird people. This has led to many house fires, as the Laretyme's claws are incendiary through either some trick of biology or magic.";
        }
    }

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        creatures = new List<Creature>
        {
            new Ratcher(),
            new IceRatcher(),
            new WindRatcher(),
            new VoidRatcher(),
            new Burdek(),
            new Murderdeathatron(),
            new CoolMurderdeathatron(),
            new ToxicBoi(),
            new HermitSquid(),
            new Krocosilisk(),
            new SpinyAnteater(),
            new Laretyme()
        };


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
