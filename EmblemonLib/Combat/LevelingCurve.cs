using System;

namespace EmblemonLib.Combat
{
	public enum FunctionType { Linear, Polynomial, Exponential, Logarithmic }

	public class LevelingCurve
	{

        /// <summary>
        /// Creates a new level curve where X represents the level and Y represents the experience required to acheive that level
        /// </summary>
        public LevelingCurve ()
		{
		}

        public FunctionType Function { get; set; }

		/// <summary>
		/// If function is polynomial, then this is the power of which to raise X by.
		/// If the function is logarithmic or exponential, then this is the base of those functions.
		/// E.G. If function is polynomial and power = 2, then the result is X^(2)
		/// If function is exponential and power = e, then the result is e^(X)
		/// If function is logarithmic and power = e, then the result is ln(x)
		/// </summary>
		public double Power { get; set; }

		public double XOffset { get; set; }

		public double XSkew { get; set; }

		public double YOffset { get; set; }

		public double YSkew { get; set; }

		/// <summary>
		/// Given a level, calculate the 
		/// </summary>
		/// <returns>The experience for next level.</returns>
		/// <param name="level">Level.</param>
		public double GetExperienceForNextLevel(int level) 
		{
			double functionAtX;
            if (YSkew == 0)
                return YOffset;

			switch(Function) 
			{
				case FunctionType.Linear:
					functionAtX = ( (XSkew/YSkew) * level + YOffset);
					break;
				case FunctionType.Logarithmic:
					functionAtX = Math.Log ((XSkew / YSkew) * level, Power) + YOffset;
					break;
				case FunctionType.Polynomial:
					functionAtX = Math.Pow((XSkew / YSkew) * level, Power) + YOffset;
					break;
				case FunctionType.Exponential:
					functionAtX = Math.Pow(Power, (XSkew / YSkew) * level) + YOffset;
					break;
				default:
					functionAtX = 0;
					break;
			}
			return functionAtX;
		}

        public string Parse()
        {
            return "";
        }
	}
}

