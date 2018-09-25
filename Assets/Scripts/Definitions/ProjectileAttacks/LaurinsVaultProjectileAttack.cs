using System.Collections.Generic;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;

namespace Assets.Scripts.Definitions.Projectiles
{
    public class LaurinsVaultProjectileAttack : DwarfsProjectileAttack
    {
        protected override void InitAttackEffects()
        {
            base.InitAttackEffects();

            AddAttackEffect(new LaurinsVaultAttackEffect());
            AddAttackEffect(new EnableRandomTowerAttackEffect(0.05f));
        }
    }
}