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
        private int maxTargets = 3;
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
            //GameObject
            //lastTarget = Target;
            ApplyEffectsToTarget(Target);
            hitNpcs.Add(Target);

            StartCoroutine(ExecuteChainLightning());
        }

        private IEnumerator ExecuteChainLightning()
        {
            while (hitNpcs.Count < maxTargets)
            {
                var npcs = TargetingHelper.GetNpcsInRadius(Target.transform.position, 3);
                var possibleTargets = npcs
                    .Where(npc => !hitNpcs.Contains(npc))
                    .ToList();

                if (possibleTargets.Count == 0) break;

                var target = possibleTargets[MathHelper.RandomInt(0, npcs.Count)];
                ApplyEffectsToTarget(target);
                //lastBoltTarget = target;
                hitNpcs.Add(target);

                /*var bolt = Instantiate(boltPrefab);
                bolt.StartObject = lastBoltTarget.gameObject;
                bolt.EndObject = target.gameObject;
                bolts.Add(bolt);*/
                
                yield return new WaitForSecondsRealtime(0.25f);
            }

            //yield return new WaitForSecondsRealtime(1f);

            //bolts.ForEach(Destroy);
            Destroy(gameObject);
        }
    }
}