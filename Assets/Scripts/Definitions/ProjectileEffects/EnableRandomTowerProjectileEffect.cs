using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.TowerSystem;

namespace Assets.Scripts.Definitions.ProjectileEffects
{
    public class EnableRandomTowerProjectileEffect : ProjectileEffect
    {
        public EnableRandomTowerProjectileEffect(float triggerChance) : base(triggerChance)
        {

        }

        protected override void ApplyEffect(Tower source, Npc target)
        {
            source.Owner.AddRandomBuildableTower();
        }
    }
}