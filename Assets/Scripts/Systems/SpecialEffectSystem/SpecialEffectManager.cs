using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Systems.SpecialEffectSystem
{
    class SpecialEffectManager : MonoBehaviour
    {
        private const string SfxPath = "Sfx";
        private List<SpecialEffectData> _runningSpecialEffects = new List<SpecialEffectData>();

        public void Update()
        {
            var finishedEffects = new List<SpecialEffectData>();

            _runningSpecialEffects.ForEach(effect =>
            {
                if (IsFinished(effect))
                {
                    finishedEffects.Add(effect);
                }
                else
                {
                    if (effect.FollowsOrigin)
                    {
                        FollowOrigin(effect);
                    }
                }
            });

            _runningSpecialEffects = _runningSpecialEffects.Except(finishedEffects).ToList();
            finishedEffects.ForEach(effect =>
            {
                Destroy(effect.EffectContainer);
            });
        }

        private void FollowOrigin(SpecialEffectData effect)
        {
            if (effect.Origin == null) return;

            var pos = effect.Origin.transform.position + effect.Offset;
            effect.EffectContainer.transform.position = pos;
        }

        private bool IsFinished(SpecialEffectData effect)
        {
            if (effect.DiesWithOrigin && effect.Origin == null) return true;
            if (effect.Duration <= 0) return false;

            effect.TimeActive += Time.deltaTime;
            return effect.TimeActive > effect.Duration;
        }

        public void PlayParticleEffect(ParticleEffectData effectData)
        {
            InstantiateEffect(effectData);
        }

        public void PlayTextEffect(TextEffectData effectData)
        {
            var textEffectGameObject = InstantiateEffect(effectData);
            var textMesh = textEffectGameObject.GetComponent<TextMeshPro>();

            textMesh.text = effectData.Text;
            textMesh.fontSize = effectData.Size;
            textMesh.color = effectData.Color;
        }

        public void PlayTrailEffect(TrailEffectData effectData)
        {
            var trail = InstantiateEffect(effectData);
            trail.GetComponent<TrailRenderer>().emitting = true;
        }

        public void PlayLightningEffect(LightningEffectData effectData)
        {
            effectData.Origin = new GameObject("OriginObject");
            effectData.Target = new GameObject("TargetObject");

            var lightning = InstantiateEffect(effectData);
            var lightningBolt = lightning.GetComponent<LightningBoltScript>();

            effectData.Origin.transform.SetParent(lightning.transform);
            effectData.Target.transform.SetParent(lightning.transform);

            lightningBolt.StartPosition = effectData.Start;
            lightningBolt.StartObject = null;
            lightningBolt.EndPosition = effectData.End;
            lightningBolt.EndObject = null;
        }

        private GameObject InstantiateEffect(SpecialEffectData effectData)
        {
            var effectPrefab = LoadEffect(effectData.EffectPrefabName);
            var effectGameObject = Instantiate(effectPrefab);
            var effectContainer = new GameObject();

            effectContainer.transform.parent = transform;
            effectContainer.transform.position = effectData.Origin.transform.position + effectData.Offset;
            effectContainer.name = effectData.EffectPrefabName + "Container";
            effectData.EffectContainer = effectContainer;

            effectGameObject.transform.SetParent(effectContainer.transform);
            effectGameObject.transform.localPosition = Vector3.zero;

            _runningSpecialEffects.Add(effectData);

            return effectGameObject;
        }

        private GameObject LoadEffect(string prefabName)
        {
            return Resources.Load(System.IO.Path.Combine(SfxPath, prefabName)) as GameObject;
        }
    }


}
