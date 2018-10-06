using Systems.AttackSystem;
using Systems.GameSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;

namespace Definitions.AttackEffects
{
    public class EnableRandomTowerAttackEffect : AttackEffect
    {
        public EnableRandomTowerAttackEffect(float triggerChance) : base(triggerChance)
        {

        }

        protected override void ApplyEffect(Tower source, Npc target)
        {
            GameManager.Instance.TowerBuildManager.AddRandomBuildableTower();
        }
    }
}