using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Systems.UiSystem.Core
{
    public enum Cursors
    {
        Standard,
        ReplaceTower,
        None
    }

    public class CursorHandler : MonoBehaviour
    {
        private Cursors _currentCursor = Cursors.Standard;
        private GameObject _currentCursorGameObject;

        [FormerlySerializedAs("replaceTowerCursor")] [SerializeField] private GameObject _replaceTowerCursor;

        private readonly Dictionary<Cursors, GameObject> _cursorDictionary = new Dictionary<Cursors, GameObject>();

        private void Start()
        {
            _cursorDictionary.Add(Cursors.ReplaceTower, _replaceTowerCursor);
        }

        private void Update()
        {
            _currentCursorGameObject = _replaceTowerCursor;
            

            if (_currentCursor != Cursors.None && _currentCursor != Cursors.Standard)
            {
                var pos = Input.mousePosition + new Vector3(32f, 16f, 0f);
                _currentCursorGameObject.transform.position = pos;
            }
        }

        public void SwitchCursor(Cursors cursor)
        {
            if (_currentCursor == cursor) return;

            DisableCursors();
            _currentCursor = cursor;
            
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
            if (!_cursorDictionary.ContainsKey(cursor)) return;

            _currentCursorGameObject = _cursorDictionary[cursor];
            _currentCursorGameObject.SetActive(true);
        }

        public void DisableCursors()
        {
            _currentCursor = Cursors.None;
            _currentCursorGameObject = null;

            foreach (var go in _cursorDictionary.Values)
            {
                go.SetActive(false);
            }
        }
    }
}