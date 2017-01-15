using System;
using System.Collections.Generic;

namespace EmblemonLib.Combat
{
	/// <summary>
	/// Stats for a character.
	/// Health
	/// Magic
	/// 
	/// Level
	/// 
	/// Strength (Physical Offense)
	/// Defense (Physical Defense)
	/// Power (Magic Offense)
	/// Fortitude (Magice Defense)
	/// Speed
	/// 
	/// </summary>
	public class CharacterStats
    {
        public const int STATNUMBER = 8;

        int health;
		int magic;
        int experienceNeeded;
        int experienceTotal;

        int level;

		int strength;
		int defense;
		int power;
		int fortitude;
		int speed;

		public void LoadStats(int health, int magic, int level, int strength, int defense,
			int power, int fortitude, int speed) {

			this.health = health;
			this.magic = magic;

			this.level = level;

			this.strength = strength;
			this.defense = defense;
			this.power = power;
			this.fortitude = fortitude;
			this.speed = speed;
        }

        public void LevelUp(Dictionary<string, LevelingCurve> levelingCurves)
        {
            health = (int)levelingCurves["Health"].GetExperienceForNextLevel(level);
            magic = (int)levelingCurves["Magic"].GetExperienceForNextLevel(level);
            strength = (int)levelingCurves["Strength"].GetExperienceForNextLevel(level);
            defense = (int)levelingCurves["Defense"].GetExperienceForNextLevel(level);
            power = (int)levelingCurves["Power"].GetExperienceForNextLevel(level);
            fortitude = (int)levelingCurves["Fortitude"].GetExperienceForNextLevel(level);
            speed = (int)levelingCurves["Speed"].GetExperienceForNextLevel(level);
            level++;
        }

		public int Health {
			get { return health; }
		}
		public int Magic {
			get { return magic; }
		}

		public int Level {
			get { return level; }
		}

		public int Strength {
			get { return strength; }
		}
		public int Defense {
			get { return defense; }
		}
		public int Power {
			get { return power; }
		}
		public int Fortitude {
			get { return fortitude; }
		}
		public int Speed {
			get { return speed; }
		}
	}
}

