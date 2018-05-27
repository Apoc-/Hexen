using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexen
{
    public class Player : Entity
    {
        public int Gold { get; set; }

        public List<Tower> BuildableTowers { get; set; }
    }
}
