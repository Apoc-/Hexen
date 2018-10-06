using Systems.FactionSystem;

namespace Definitions.Factions
{
    public class Goblins : Faction
    {
        public Goblins() : base(
            name: "Goblins",
            description: "Goblins have an ancient and war-proofed tactic: \"Make things go boom\".",
            factionName: FactionNames.Goblins,
            opponentFactionName: FactionNames.Dwarfs)
        {

        }
    }
}