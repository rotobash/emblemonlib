using System;

namespace EmblemonLib.Combat
{
	public enum FunctionType { Linear, Polynomial, Exponential, Logarithmic }

	public class LevelingCurve
	{
		FunctionType function;
		double power, xSkew, ySkew, xOffset, yOffset;

		/// <summary>
		/// Creates a new level curve where X represents the level and Y represents the experience required to acheive that level
		/// </summary>
		/// <param name="power">If function is polynomial, then this is the power of which to raise X by.
		/// If the function is logarithmic or exponential, then this is the base of those functions.
		/// E.G. If function is polynomial and power = 2, then the result is X^(2)
		/// If function is exponential and power = e, then the result is e^(X)
		/// If function is logarithmic and power = e, then the result is ln(x)</param>
		/// <param name="xSkew">X skew.</param>
		/// <param name="ySkew">Y skew.</param>
		/// <param name="xOffset">X offset.</param>
		/// <param name="yOffset">Y offset.</param>
		public LevelingCurve (FunctionType function, double power, double xSkew, double ySkew, double xOffset, double yOffset)
		{
			this.function = function;
			this.power = power;
			this.xSkew = xSkew;
			this.ySkew = ySkew;
			this.xOffset = xOffset;
			this.yOffset = yOffset;
		}

        public FunctionType Function
        {
            get
            {
                return function;
            }
        }

        public double Power
        {
            get
            {
                return power;
            }
        }

        public double XOffset
        {
            get
            {
                return xOffset;
            }
        }

        public double XSkew
        {
            get
            {
                return xSkew;
            }
        }

        public double YOffset
        {
            get
            {
                return yOffset;
            }
        }

        public double YSkew
        {
            get
            {
                return ySkew;
            }
        }

        /// <summary>
        /// Given a level, calculate the 
        /// </summary>
        /// <returns>The experience for next level.</returns>
        /// <param name="level">Level.</param>
        public double GetExperienceForNextLevel(int level) 
		{
			double functionAtX;
            if (ySkew == 0)
                return yOffset;

			switch(function) 
			{
				case FunctionType.Linear:
					functionAtX = ( (xSkew/ySkew) * level + yOffset);
					break;
				case FunctionType.Logarithmic:
					functionAtX = Math.Log ((xSkew / ySkew) * level, power) + yOffset;
					break;
				case FunctionType.Polynomial:
					functionAtX = Math.Pow((xSkew / ySkew) * level, power) + yOffset;
					break;
				case FunctionType.Exponential:
					functionAtX = Math.Pow(power, (xSkew / ySkew) * level) + yOffset;
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

