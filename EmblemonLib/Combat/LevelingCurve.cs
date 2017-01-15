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

		/// <summary>
		/// Given a level, calculate the 
		/// </summary>
		/// <returns>The experience for next level.</returns>
		/// <param name="level">Level.</param>
		public double GetExperienceForNextLevel(int level) 
		{
			double functionAtX;
			switch(function) 
			{
				case FunctionType.Linear:
					functionAtX = (xSkew * (level + xOffset));
					break;
				case FunctionType.Logarithmic:
					functionAtX = Math.Log (xSkew * (level + xOffset), power);
					break;
				case FunctionType.Polynomial:
					functionAtX = Math.Pow(xSkew * (level + xOffset), power);
					break;
				case FunctionType.Exponential:
					functionAtX = Math.Pow(power, xSkew * (level + xOffset));
					break;
				default:
					functionAtX = 0;
					break;
			}
			return ySkew * (functionAtX + yOffset);
		}
	}
}

