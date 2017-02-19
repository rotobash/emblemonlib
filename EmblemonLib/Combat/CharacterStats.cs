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
        public const int STATNUMBER = 9;

        double health;
        double magic;
        double stamina;
        double experienceNeeded;
        double experienceTotal;

        int level;

        double strength;
        double defense;
        double power;
        double fortitude;
        double speed;

		public void LoadStats(int health, int magic, int stamina, int level, int strength, int defense,
			int power, int fortitude, int speed) {

			this.health = health;
			this.magic = magic;
			this.stamina = stamina;

			this.level = level;

			this.strength = strength;
			this.defense = defense;
			this.power = power;
			this.fortitude = fortitude;
			this.speed = speed;
        }

        public void LevelUp(Dictionary<string, LevelingCurve> levelingCurves)
        {
            health = levelingCurves["Health"].GetExperienceForNextLevel(level);
			magic = levelingCurves["Magic"].GetExperienceForNextLevel(level);
			stamina = levelingCurves["Stamina"].GetExperienceForNextLevel(level);
            strength = levelingCurves["Strength"].GetExperienceForNextLevel(level);
            defense = levelingCurves["Defense"].GetExperienceForNextLevel(level);
            power = levelingCurves["Power"].GetExperienceForNextLevel(level);
            fortitude = levelingCurves["Fortitude"].GetExperienceForNextLevel(level);
            speed = levelingCurves["Speed"].GetExperienceForNextLevel(level);
            level++;
        }

		public double Health {
			get { return health; }
		}
		public double Magic {
			get { return magic; }
		}
		public double Stamina {
			get { return stamina; }
		}

		public int Level {
			get { return level; }
		}

		public double Strength {
			get { return strength; }
		}
		public double Defense {
			get { return defense; }
		}
		public double Power {
			get { return power; }
		}
		public double Fortitude {
			get { return fortitude; }
		}
		public double Speed {
			get { return speed; }
		}
	}
}

