using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecessityManager : MonoBehaviour {

    /// <summary>
    /// Necessities are as follows:
    /// 
    /// Housing
    /// Food
    /// Sanitation
    /// Entertainment
    /// 
    /// </summary>

    public interface IRester
    {
        void Rest(CreatureList.Creature creature);
    }
    public interface IEatable
    {
        void Eat(CreatureList.Creature creature);
    }
    public interface ICleaner
    {
        void Clean();
    }
    public interface IEntertainable
    {
        void Play(CreatureList.Creature creature);
    }
    
    public class Pillow : IRester
    {
        private int rest = 35;

        public void Rest(CreatureList.Creature creature)
        {
            creature.secondaryStats.Fatigue -= rest;
        }
    }

    public class Kibble : IEatable
    {
        private int nutrition = 35;

        public void Eat(CreatureList.Creature creature)
        {
            creature.secondaryStats.Hunger -= nutrition;
        }
    }

    public class ChewToy : IEntertainable
    {
        private int entertainment = 35;

        public void Play(CreatureList.Creature creature)
        {
            creature.secondaryStats.Stress -= entertainment;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Sleep(IRester bed, CreatureList.Creature creature)
    {
        bed.Rest(creature);
    }

    public void Eat(IEatable food, CreatureList.Creature creature)
    {
        food.Eat(creature);
    }

    public void Clean(ICleaner sanitizer, CreatureList.Creature creature)
    {
        sanitizer.Clean();
    }

    public void Play(IEntertainable toy, CreatureList.Creature creature)
    {
        toy.Play(creature);
    }
}
