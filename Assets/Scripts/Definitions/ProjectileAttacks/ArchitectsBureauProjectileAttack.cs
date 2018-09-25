using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class ArchitectsBureauProjectileAttack : DwarfsProjectileAttack
    {
        protected override void InitAttackEffects()
        {
            base.InitAttackEffects();

            AddAttackEffect(new DamageAttackEffect());
        }
    }
}