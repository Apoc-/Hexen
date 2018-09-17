using Assets.Scripts.Systems.FactionSystem;

namespace Assets.Scripts.Definitions.Factions
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