using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Systems.UiSystem
{
    public enum Cursors
    {
        Standard,
        ReplaceTower,
        None
    }

    public class CursorHandler : MonoBehaviour
    {
        private Cursors currentCursor = Cursors.Standard;
        private GameObject currentCursorGameObject;

        [SerializeField] private GameObject replaceTowerCursor;

        private readonly Dictionary<Cursors, GameObject> cursorDictionary = new Dictionary<Cursors, GameObject>();

        private void Start()
        {
            cursorDictionary.Add(Cursors.ReplaceTower, replaceTowerCursor);
        }

        private void Update()
        {
            currentCursorGameObject = replaceTowerCursor;
            

            if (currentCursor != Cursors.None && currentCursor != Cursors.Standard)
            {
                var pos = Input.mousePosition + new Vector3(32f, 16f, 0f);
                currentCursorGameObject.transform.position = pos;
            }
        }

        public void SwitchCursor(Cursors cursor)
        {
            if (currentCursor == cursor) return;

            DisableCursors();
            currentCursor = cursor;
            
            if (cursor == Cursors.None)
            {
                Cursor.visible = false;
                return;
            }

            if (cursor == Cursors.Standard)
            {
                Cursor.visible = true;
                return;
            }

            EnableCursor(cursor);
        }

        private void EnableCursor(Cursors cursor)
        {
            if (!cursorDictionary.ContainsKey(cursor)) return;

            currentCursorGameObject = cursorDictionary[cursor];
            currentCursorGameObject.SetActive(true);
        }

        public void DisableCursors()
        {
            currentCursor = Cursors.None;
            currentCursorGameObject = null;

            foreach (var go in cursorDictionary.Values)
            {
                go.SetActive(false);
            }
        }
    }
}