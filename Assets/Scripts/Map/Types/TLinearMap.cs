using System;
using System.Collections.Generic;
using UnityEngine;

namespace OfFogAndDust.Map.Types
{
    /* Linear Map is a linearized type for TTreeMap,
     * enabling saves on a non linear data structure. 
     * This is the main dataframe that should be used */
    [SerializeField]
    public class TLinearMap
    {
        public int entrance;
        public List<int> exits;
        public List<Vector3> locations;
        public Dictionary<int, List<int>> nodes;

        public TLinearMap() { }

        public TLinearMap(TTreeMap map)
        {
            // this completly breaks links between nodes
            void Construct(TTree tree)
            {
                locations.Add(tree.root.location);
                if (map.exits.Contains(tree))
                {
                    exits.Add(locations.Count);
                }
                foreach (TTree t in tree.children)
                {
                    Construct(t);
                }
            }

            entrance = 0;
            exits = new List<int>();
            locations = new List<Vector3>();
            nodes = new Dictionary<int, List<int>>();
            Construct(map.entrance);
        }

        #region Find
        internal Vector3 FindOnFunction(Func<Vector3, Vector3, bool> func)
        {
            if (locations.Count == 0) throw new ArgumentException();
            if (locations.Count == 1) return locations[0];

            Vector3 result = locations[0];

            foreach (Vector3 l in locations)
            {
                if (func(l, result))
                {
                    result = l;
                }
            }
            return result;
        }
        #endregion

        #region Apply Function to Map
        internal void ApplyFunction(Func<Vector3, Vector3> func, List<Vector3> llocations)
        {
            locations = new List<Vector3>();
            foreach (Vector3 location in llocations)
            {
                locations.Add(func(location));
            }
        }

        #endregion
    }
}
