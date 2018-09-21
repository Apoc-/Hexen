using Assets.Scripts.Systems.FactionSystem;

namespace Assets.Scripts.Definitions.Factions
{
    public class Dwarfs : Faction
    {
        public Dwarfs() : base(
            name: "Dwarves",
            description: "Dwarfs might be master builders, but first and foremost they are greedy.",
            factionName: FactionNames.Dwarfs,
            opponentFactionName: FactionNames.Goblins)
        {

        }
    }
}