using Assets.Scripts.FactionSystem;

namespace Hexen.GameData.Factions
{
    public class Humans : Faction
    {
        public Humans() : base(
            name: "Humans",
            description: "A generic faction with generic towers.",
            factionName: Assets.Scripts.FactionSystem.FactionNames.Humans)
        {
            IncreaseStanding();
        }
    }
}