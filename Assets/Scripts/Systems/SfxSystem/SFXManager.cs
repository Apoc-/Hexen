using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Systems.SfxSystem
{
    class SFXManager : MonoBehaviour
    {
        private const string sfxPath = "Sfx";
        private List<SpecialEffect> ongoingEffects = new List<SpecialEffect>();
        private List<FloatingTextBehaviour> ongoingTextEffects = new List<FloatingTextBehaviour>();
        private List<Animator> ongoingAnimations = new List<Animator>();
        private List<TrailEffect> attachedTrailEffects = new List<TrailEffect>(); 
        private bool destroyWithOrigin = false;

        private GameObject LoadEffect(string name)
        {
            return Resources.Load(System.IO.Path.Combine(sfxPath, name)) as GameObject;
        }

        public void PlaySpecialEffect(SpecialEffect effectData)
        {
            PlaySpecialEffect(effectData, Vector3.zero);
        }

        public void PlaySpecialEffect(SpecialEffect effectData, Vector3 offset)
        {
            GameObject effectPrefab = LoadEffect(effectData.EffectPrefabName);
            if (effectPrefab == null)
            {
                throw new NullReferenceException("SFX '" + effectData.EffectPrefabName + "' not found");
            }

            GameObject containerPrefab = new GameObject("effectContainer");

            var container = Instantiate(containerPrefab);

            container.transform.position = effectData.Origin.transform.position + offset;
            container.transform.rotation = effectData.Origin.transform.rotation;
            container.transform.parent = transform;

            var go = Instantiate(effectPrefab, container.transform);

            effectData.EffectContainer = go;
            
            ongoingEffects.Add(effectData);

            GameObject.Destroy(containerPrefab);
        }

        public void PlayTextEffect(string text, Vector3 position, float size, float duration, Color color)
        {
            FloatingTextBehaviour prefab = Resources.Load<FloatingTextBehaviour>(sfxPath + "/BasicFloatingTextEffect");

            if (prefab == null) return;

            GameObject container = new GameObject("effectContainer");
            container.transform.parent = this.transform;
            container.transform.localPosition = Vector3.zero;
            container.transform.position = position;
            
            var floatingText = Instantiate(prefab, container.transform);

            floatingText.Container = container;
            floatingText.Duration = duration;
            floatingText.SetSize(size);
            floatingText.SetText(text);
            floatingText.SetColor(color);
            floatingText.IsPlaying = true;

            ongoingTextEffects.Add(floatingText);
        }

        public void AttachTrail(string trailPrefabName, GameObject origin)
        {
            TrailRenderer prefab = Resources.Load<TrailRenderer>(sfxPath + "/" + trailPrefabName);

            if (prefab == null) return;

            GameObject container = new GameObject("effectContainer");
            container.transform.parent = this.transform;
            container.transform.localPosition = Vector3.zero;
            container.transform.position = origin.transform.position;

            var trail = Instantiate(prefab, container.transform);

            trail.emitting = true;

            attachedTrailEffects.Add(new TrailEffect(origin, container, trail));
        }
        
        private void Update()
        {
            attachedTrailEffects.RemoveAll(HandleTrailEffectUpdate);
            ongoingTextEffects.RemoveAll(HandleTextEffectDestruction);

            ongoingEffects.RemoveAll(specialEffect =>
            {
                var destroy = HandleSpecialEffectDestruction(specialEffect);

                return destroy;
            });
        }

        private bool HandleTrailEffectUpdate(TrailEffect trailEffect)
        {
            if (trailEffect.Trail == null)
            {
                Destroy(trailEffect.Container);
                return true; 
            }

            if (trailEffect.Origin == null) return false;

            var newPos = trailEffect.Origin.transform.position;
            trailEffect.Container.transform.position = newPos;

            return false;
        }

        private void UpdateEffectPosition(SpecialEffect specialEffect)
        {
            if (specialEffect.Origin == null) return;

            var newPos = specialEffect.Origin.transform.position;

            specialEffect.EffectContainer.transform.position = newPos;
        }

        private bool HandleSpecialEffectDestruction(SpecialEffect specialEffect)
        {
            specialEffect.TimeActive += Time.deltaTime;

            if (specialEffect.TimeActive >= specialEffect.Duration && specialEffect.Duration > -1)
            {
                Destroy(specialEffect.EffectContainer.transform.parent.gameObject);
                return true;
            }

            if (specialEffect.Origin == null && specialEffect.DiesWithOrigin)
            {
                Destroy(specialEffect.EffectContainer.transform.parent.gameObject);
                return true;
            }

            if (specialEffect.FollowsOrigin)
            {
                UpdateEffectPosition(specialEffect);
            }

            return false;
        }

        private bool HandleTextEffectDestruction(FloatingTextBehaviour textEffect)
        {
            if (!textEffect.IsPlaying)
            {
                Destroy(textEffect.Container);
                return true;
            }

            return false;
        }
        
    }
}
