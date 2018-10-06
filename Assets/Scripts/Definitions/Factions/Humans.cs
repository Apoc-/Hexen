using Systems.FactionSystem;

namespace Definitions.Factions
{
    public class Humans : Faction
    {
        public Humans() : base(
            name: "Humans",
            description: "A generic faction with generic towers.",
            factionName: FactionNames.Humans)
        {
            IncreaseStanding();
        }
    }
}