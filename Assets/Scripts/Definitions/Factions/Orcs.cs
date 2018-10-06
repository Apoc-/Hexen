using Systems.FactionSystem;

namespace Definitions.Factions
{
    public class Orcs : Faction
    {
        public Orcs() : base(
            name: "Orcs",
            description: "Orcs are a tribal faction, deeply rooted in tradition and war.",
            factionName: FactionNames.Orcs,
            opponentFactionName: FactionNames.Elves)
        {

        }
    }
}