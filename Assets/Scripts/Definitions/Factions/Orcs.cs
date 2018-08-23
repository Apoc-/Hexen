using Assets.Scripts.FactionSystem;

namespace Hexen.GameData.Factions
{
    public class Orcs : Faction
    {
        public Orcs() : base(
            name: "Orcs",
            description: "Orcs are a tribal faction, deeply rooted in tradition and war.",
            factionName: Assets.Scripts.FactionSystem.FactionNames.Orcs)
        {

        }
    }
}