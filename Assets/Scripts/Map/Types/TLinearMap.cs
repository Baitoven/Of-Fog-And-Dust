using Assets.Scripts.Map.Types;
using System.Collections.Generic;
using UnityEngine;

namespace OfFogAndDust.Map.Types
{
    [SerializeField]
    public class TLinearMap
    {
        int entrance;
        List<int> exits;
        public List<Vector3> locations;
        public Dictionary<int, List<int>> nodes;

        public TLinearMap() { }

        public TLinearMap(Map map)
        {
            void AddNode(int from, int to)
            {
                if (nodes.ContainsKey(from))
                {
                    nodes[from].Add(to);
                }
                else
                {
                    nodes.Add(from, new List<int>());
                }
            }

            void Construct(int node, TTree tree)
            {
                int newNode = locations.Count;
                AddNode(node, newNode);
                locations.Add(tree.root.location);
                if (tree.children.Count == 0)
                {
                    exits.Add(newNode);
                }
                foreach (TTree t in tree.children)
                {
                    Construct(newNode, t);
                }
            }

            entrance = 0;
            exits = new List<int>();
            locations = new List<Vector3>();
            nodes = new Dictionary<int, List<int>>();
            Construct(0, map.entrance);
        }
    }
}
