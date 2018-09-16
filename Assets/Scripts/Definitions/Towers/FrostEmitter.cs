using System.Linq;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Towers
{
    class FrostEmitter : AuraTower
    {
        public override void InitTower()
        {
            Name = "Frost Emitter";
            Description = "A tower that slows nearby creeps by 25% and deals continous damage to them.";
            GoldCost = 125;

            //ProjectileType = typeof(VoidProjectile);
            //ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Arrow");

            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Elves/Emitter");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/Banner");
            AuraEffects.Add(new AuraEffect(
                attributeEffect: new AttributeEffect(-0.25f, AttributeName.MovementSpeed, AttributeEffectType.PercentMul, this), 
                affectsTowers: false, 
                affectsNpcs: true));

            WeaponHeight = 1;

            Faction = FactionNames.Elves;
            Rarity = Rarities.Rare;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.AuraRange, 1.75f, 0.0f));
            AddAttribute(new Attribute(AttributeName.AuraDamage, 0.5f));
            AddAttribute(new Attribute(AttributeName.AuraTicksPerSecond, 4.0f));
        }
    }
}
