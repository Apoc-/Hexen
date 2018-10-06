using Systems.NpcSystem;
using Systems.TowerSystem;

namespace Systems.AttackSystem
{
    public abstract class DirectAttack : AbstractAttack
    {
        public override void InitAttack(Npc target, Tower source)
        {
            base.InitAttack(target, source);

            ExecuteAttack();
        }

        protected virtual void ExecuteAttack()
        {
            ApplyEffectsToTarget(Target);
        }
    }
}