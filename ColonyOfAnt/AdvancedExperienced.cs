using System.Collections.Generic;
using static ColonyOfAnt.Utility;

namespace ColonyOfAnt
{
    public class AdvancedExperienced : Advanced
    {
        // <продвинутый> ВОИН(здоровье=6, защита=2, урон=4) может атаковать 2 цели за раз и наносит 1 укус.
        // <продвинутый бригадир> РАБОЧИЙ(здоровье=6, защита=2) может брать 2 ресурса: 'веточка или камушек' за раз; все рабочие могут брать +1 ресурс.
        // <продвинутый опытный> РАБОЧИЙ(здоровье=6, защита=2) может брать 2 ресурса: 'росинка или росинка' за раз; может брать любой тип ресурса.
        // <легендарный худой> ВОИН(здоровье=10, защита=6, урон=4) может атаковать 3 цели за раз и наносит 1 укус; весь урон перенаправляется на союзников.
        // <обычный настойчивый> ВОИН(здоровье=1, защита=0, урон=1) может атаковать 1 цель за раз и наносит 1 укус; всегда наносит укус, даже если был убит.


        public AdvancedExperienced(string UnitClass, Colony colony) : base(UnitClass, colony)
        {
            IHaveModifier = true;
            myModifier = new List<string>() {"опытный"};
            Backpack = new List<BackpackResource>(4);
            for (int i = 0; i < 4; i++)
            {
                Backpack[i].AddElement(0, existingResource[i]);
            }
        }
    }
}