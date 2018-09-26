using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using DigitalRuby.LightningBolt;
using UnityEngine;

namespace Assets.Scripts.Definitions.DirectAttacks
{
    public class StormCallerDirectAttack : DirectAttack
    {
        private List<Npc> hitNpcs = new List<Npc>();
        private int otherTargetsCount = 2;
        private LightningBoltBehaviour boltPrefab;
        //private List<GameObject lastBoltTarget;
        private List<LightningBoltBehaviour> bolts = new List<LightningBoltBehaviour>();

        private void Awake()
        {
            boltPrefab = Resources.Load<LightningBoltBehaviour>("Sfx/LightningBolt");
        }

        protected override void InitAttackData()
        {
            
        }

        protected override void InitAttackEffects()
        {
            AddAttackEffect(new DamageAttackEffect());
        }

        protected override void ExecuteAttack()
        {
            ApplyEffectsToTarget(Target);

            var otherTargets = GetOtherTargets();

            if (otherTargets.Count > 0)
            {
                StartCoroutine(ExecuteChainLightning(otherTargets));
            }
        }

        private List<Npc> GetOtherTargets()
        {
            var npcs = TargetingHelper.GetNpcsInRadius(Target.transform.position, 3);
            npcs.Remove(Target);
            npcs.Shuffle();

            return npcs.Take(otherTargetsCount).ToList();
        }

        private IEnumerator ExecuteChainLightning(List<Npc> otherTargets)
        {
            foreach (var target in otherTargets)
            {
                if (target != null)
                {
                    ApplyEffectsToTarget(target);
                }

                yield return new WaitForSecondsRealtime(0.25f);
            }

            Destroy(gameObject);
        }
    }
}