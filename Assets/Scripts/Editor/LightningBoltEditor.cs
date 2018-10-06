using System;
using Systems.SpecialEffectSystem;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(LightningBoltScript))]
    public class LightningBoltEditor : UnityEditor.Editor
    {
        private Texture2D _logo;

        public override void OnInspectorGUI()
        {
            if (_logo == null)
            {
                string[] guids = AssetDatabase.FindAssets("LightningBoltLogo");
                foreach (string guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    _logo = AssetDatabase.LoadMainAssetAtPath(path) as Texture2D;
                    if (_logo != null)
                    {
                        break;
                    }
                }
            }
            if (_logo != null)
            {
                const float maxLogoWidth = 430.0f;
                EditorGUILayout.Separator();
                float w = EditorGUIUtility.currentViewWidth;
                Rect r = new Rect();
                r.width = Math.Min(w - 40.0f, maxLogoWidth);
                r.height = r.width / 2.7f;
                Rect r2 = GUILayoutUtility.GetRect(r.width, r.height);
                r.x = ((EditorGUIUtility.currentViewWidth - r.width) * 0.5f) - 4.0f;
                r.y = r2.y;
                GUI.DrawTexture(r, _logo, ScaleMode.StretchToFill);
                if (GUI.Button(r, "", new GUIStyle()))
                {
                    Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/34217?aid=1011lGnL");
                }
                EditorGUILayout.Separator();
            }

            DrawDefaultInspector();
        }
    }
}