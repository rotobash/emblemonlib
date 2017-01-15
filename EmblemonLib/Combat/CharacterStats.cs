using System;

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
		int health;
		int magic;

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

		public void LoadStats() {

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

