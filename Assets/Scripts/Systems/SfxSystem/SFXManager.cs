using Hexen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Hexen
{
    class SFXManager : MonoBehaviour
    {
        private const string sfxPath = "Sfx";
        private List<GameObject> ongoingEffects = new List<GameObject>();

        private GameObject LoadEffect(string name)
        {
            return Resources.Load(System.IO.Path.Combine(sfxPath, name)) as GameObject;
        }

        public void PlaySpecialEffect(MonoBehaviour origin, string effectName)
        {
            GameObject effectPrefab = LoadEffect(effectName);
            if (effectPrefab == null)
            {
                throw new NullReferenceException("SFX '" + effectName + "' not found");
            }

            GameObject containerPrefab = new GameObject("effectContainer");

            var container = Instantiate(containerPrefab, origin.transform.position, origin.transform.rotation, this.transform);
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

                if (destroy)
                {
                    Destroy(go.transform.parent.gameObject);
                    ongoingEffects.Remove(go);
                }
            });
        }
    }
}
