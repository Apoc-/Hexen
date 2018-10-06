using Systems.FactionSystem;

namespace Definitions.Factions
{
    public class Elves : Faction
    {
        public Elves() : base(
            name: "Elves", 
            description: "Elves are a noble race with the ability to use elemental magic.", 
            factionName: FactionNames.Elves,
            opponentFactionName: FactionNames.Orcs)
        {

        }
    }
}