using System;
using System.Collections.Generic;
using EmblemonLib.Interfaces;

namespace EmblemonLib.Combat
{
	public class Party : List<Combatable>
	{
		public Party (List<Combatable> combatants)
		{
			AddRange (combatants);
		}

		public void RemoveDeadCombatants() {
			for (int i = 0; i < Count; i++) {
				if (!this[i].IsAlive) {
					this.RemoveAt (i);
					i--;
				}
			}
		}

		public bool IsAtLeastOneAlive() {
			return (Count > 0);
		}

	}
}

