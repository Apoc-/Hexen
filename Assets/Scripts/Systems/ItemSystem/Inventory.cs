﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.ItemSystem
{
    public class Inventory : MonoBehaviour
    {
        private int _size;
        public List<Item> Items { get; private set; } = new List<Item>();

        public void InitInventory(int size)
        {
            _size = size;
            Items = new List<Item>();
        }

        public bool AddItem(Item item)
        {
            if (Items.Count > _size) throw new Exception("Inventory overflow.");
            if (Items.Count == _size) return false;

            Items.Add(item);
            item.transform.parent = transform;

            return true;
        }

        public void RemoveItem(Item item)
        {
            if (!Items.Remove(item)) throw new Exception("Item not found in Inventory.");

            Destroy(item.gameObject);
        }

        public bool MoveItem(Item item, Inventory newInventory)
        {
            if (!Items.Contains(item)) return false;

            if (newInventory.AddItem(item))
            {
                Items.Remove(item);
                item.transform.parent = newInventory.transform;
                return true;
            }

            return false;
        }
    }
}