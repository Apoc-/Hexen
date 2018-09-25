using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.ProjectileSystem;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class BuildersShackProjectileAttack : DwarfsProjectileAttack
    {
        protected override void InitAttackEffects()
        {
            base.InitAttackEffects();

            AddAttackEffect(new DamageAttackEffect());
        }
    }
}