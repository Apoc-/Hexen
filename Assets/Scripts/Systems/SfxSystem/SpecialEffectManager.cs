﻿using System;
using System.Collections.Generic;
using System.Linq;
using DigitalRuby.LightningBolt;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Systems.SfxSystem
{
    class SpecialEffectManager : MonoBehaviour
    {
        private const string SfxPath = "Sfx";
        private List<SpecialEffectData> runningSpecialEffects = new List<SpecialEffectData>();

        public void Update()
        {
            var finishedEffects = new List<SpecialEffectData>();

            runningSpecialEffects.ForEach(effect =>
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

            runningSpecialEffects = runningSpecialEffects.Except(finishedEffects).ToList();
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

        public void PlayLightningEffect()
        {

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

            effectGameObject.transform.parent = effectContainer.transform;
            effectGameObject.transform.localPosition = Vector3.zero;

            runningSpecialEffects.Add(effectData);

            return effectGameObject;
        }

        private GameObject LoadEffect(string prefabName)
        {
            return Resources.Load(System.IO.Path.Combine(SfxPath, prefabName)) as GameObject;
        }
    }


}
