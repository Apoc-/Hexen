using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Systems.SfxSystem
{
    class SFXManager : MonoBehaviour
    {
        private const string sfxPath = "Sfx";
        private List<GameObject> ongoingEffects = new List<GameObject>();
        private GameObject origin;
        private bool destroyWithOrigin = false;

        private GameObject LoadEffect(string name)
        {
            return Resources.Load(System.IO.Path.Combine(sfxPath, name)) as GameObject;
        }

        public void PlaySpecialEffect(string effectName, GameObject origin, bool destroyWithOrigin = false)
        {
            this.origin = origin;
            var position = origin.transform.position;
            var rotation = origin.transform.rotation;

            PlaySpecialEffect(effectName, position, rotation);
        }

        public void PlaySpecialEffect(string effectName, Vector3 position, Quaternion rotation)
        {
            GameObject effectPrefab = LoadEffect(effectName);
            if (effectPrefab == null)
            {
                throw new NullReferenceException("SFX '" + effectName + "' not found");
            }

            GameObject containerPrefab = new GameObject("effectContainer");

            var container = Instantiate(containerPrefab, position, rotation, this.transform);
            var go = Instantiate(effectPrefab, container.transform);
            ongoingEffects.Add(go);
            GameObject.Destroy(containerPrefab);
        }

        private void Update()
        {
            ongoingEffects.ForEach(go =>
            {
                bool destroy = false;

                var particles = go.GetComponentInChildren<ParticleSystem>();
                if (particles != null)
                {
                    destroy = particles.isStopped;
                }

                var sounds = go.GetComponentInChildren<AudioSource>();
                if (sounds != null)
                {
                    destroy = !sounds.isPlaying;
                }

                if (destroyWithOrigin)
                {
                    destroy = origin == null;
                }

                if (destroy)
                {
                    Destroy(go.transform.parent.gameObject);
                    ongoingEffects.Remove(go);
                }
            });
        }
    }
}
