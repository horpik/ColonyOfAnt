using System.Collections.Generic;
using System.Linq;

namespace ColonyOfAnt
{
    public class AdvancedBrigadier : Advanced
    {
        // <продвинутый> ВОИН(здоровье=6, защита=2, урон=4) может атаковать 2 цели за раз и наносит 1 укус.
        // <продвинутый бригадир> РАБОЧИЙ(здоровье=6, защита=2) может брать 2 ресурса: 'веточка или камушек' за раз; все рабочие могут брать +1 ресурс.
        // <продвинутый опытный> РАБОЧИЙ(здоровье=6, защита=2) может брать 2 ресурса: 'росинка или росинка' за раз; может брать любой тип ресурса.
        // <легендарный худой> ВОИН(здоровье=10, защита=6, урон=4) может атаковать 3 цели за раз и наносит 1 укус; весь урон перенаправляется на союзников.
        // <обычный настойчивый> ВОИН(здоровье=1, защита=0, урон=1) может атаковать 1 цель за раз и наносит 1 укус; всегда наносит укус, даже если был убит.


        public AdvancedBrigadier(string UnitClass, Colony colony) : base(UnitClass, colony)
        {
            IHaveModifier = true;
            myModifier = new List<string>(){"бригадир"};
        }
        public override void ModifierAction(Heap heap, List<Ant> ants)
        {
            foreach (var ant in ants.Where(ant => ant.myClass == "рабочий"))
            {
                ant.TakeResource(heap, 1);
            }
        }
    }
}