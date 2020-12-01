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
        double experienceNeeded;
        double experienceTotal;
        public double Health { get; set; }
        public double Magic { get; set; }
        public double Stamina { get; set; }

        public int Level { get; set; }

        public double Strength { get; set; }
        public double Defense { get; set; }
        public double Power { get; set; }
        public double Fortitude { get; set; }
        public double Speed { get; set; }


        public void LevelUp(Dictionary<string, LevelingCurve> levelingCurves)
        {
            Health = levelingCurves["Health"].GetExperienceForNextLevel(Level);
			Magic = levelingCurves["Magic"].GetExperienceForNextLevel(Level);
			Stamina = levelingCurves["Stamina"].GetExperienceForNextLevel(Level);
            Strength = levelingCurves["Strength"].GetExperienceForNextLevel(Level);
            Defense = levelingCurves["Defense"].GetExperienceForNextLevel(Level);
            Power = levelingCurves["Power"].GetExperienceForNextLevel(Level);
            Fortitude = levelingCurves["Fortitude"].GetExperienceForNextLevel(Level);
            Speed = levelingCurves["Speed"].GetExperienceForNextLevel(Level);
            Level++;
        }
    }
}

