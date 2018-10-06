using Systems.AttackSystem;
using Systems.NpcSystem;
using Systems.TowerSystem;

namespace Definitions.AttackEffects
{
    public class CoinsAttackEffect : AttackEffect
    {
        public CoinsAttackEffect(float triggerChance) : base(triggerChance)
        {

        }

        protected override void ApplyEffect(Tower source, Npc target)
        {
            source.PlayParticleEffectAboveTower("GotSomeCoinEffect", 3);
            source.Owner.IncreaseGold(1);
        }
    }
}