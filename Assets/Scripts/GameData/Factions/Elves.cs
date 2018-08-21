using Assets.Scripts.FactionSystem;

namespace Hexen.GameData.Factions
{
    public class Elves : Faction
    {
        public Elves() : base(
            name: "Elves", 
            description: "Elves are a noble race with the ability to use elemental magic.", 
            factionName: Assets.Scripts.FactionSystem.FactionNames.Elves)
        {

        }
    }
}