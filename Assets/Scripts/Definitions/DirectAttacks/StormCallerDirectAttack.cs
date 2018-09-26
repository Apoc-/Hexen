﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Definitions.ProjectileEffects;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.ProjectileSystem;
using Assets.Scripts.Systems.SfxSystem;
using DigitalRuby.LightningBolt;
using UnityEngine;

namespace Assets.Scripts.Definitions.DirectAttacks
{
    public class StormCallerDirectAttack : DirectAttack
    {
        private List<Npc> hitNpcs = new List<Npc>();
        private int otherTargetsCount = 2;
        private Vector3 lastPosition;

        protected override void InitAttackData()
        {
            
        }

        protected override void InitAttackEffects()
        {
            AddAttackEffect(new DamageAttackEffect());
        }

        protected override void ExecuteAttack()
        {
            var towerPosition = Source.gameObject.transform.position;

            ApplyEffectsToTarget(Target);
            PlayBoltEffect(towerPosition, Target.gameObject.transform.position);

            var randomTargets = GetRandomTargets();
            var sortedTargets = SortTargetsByDistance(towerPosition, randomTargets);

            if (sortedTargets.Count > 0)
            {
                StartCoroutine(ExecuteChainLightning(sortedTargets));
            }
        }

        private List<Npc> GetRandomTargets()
        {
            var npcs = TargetingHelper.GetNpcsInRadius(Target.transform.position, 3);
            npcs.Remove(Target);
            npcs.Shuffle();

            return npcs.Take(otherTargetsCount).ToList();
        }

        private List<Npc> SortTargetsByDistance(Vector3 origin, List<Npc> targets)
        {
            return targets
                .OrderBy(target => Vector3.Distance(origin, target.gameObject.transform.position))
                .ToList();
        }

        private IEnumerator ExecuteChainLightning(List<Npc> otherTargets)
        {
            var otherTargetPositions = otherTargets
                .Select(target => target.gameObject.transform.position)
                .ToList();

            for (var index = 0; index < otherTargets.Count; index++)
            {
                var target = otherTargets[index];
                var targetPosition = otherTargetPositions[index];

                if (target != null)
                {
                    ApplyEffectsToTarget(target);
                }

                PlayBoltEffect(lastPosition, targetPosition);
                lastPosition = targetPosition;
                
                yield return new WaitForSecondsRealtime(0.05f);
            }

            Destroy(gameObject);
        }

        private void PlayBoltEffect(Vector3 start, Vector3 end)
        {
            var duration = 0.1f;
            var offset = new Vector3(0, 0.15f, 0);

            var bolt = new LightningEffectData("LightningBolt", start+offset, end+offset, duration);
            GameManager.Instance.SpecialEffectManager.PlayLightningEffect(bolt);
        }
    }
}