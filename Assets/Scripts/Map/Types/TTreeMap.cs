using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OfFogAndDust.Map.Types
{
    /* Tree map acts as a transition between 2 locations
    * It is designed to go from point A to point B
    * Thus, a tree data structure is used, entrance being the
    * root, maptree being the link between everything else. */
    [Serializable]
    public class TTreeMap
    {
        public List<Vector3> locations = new List<Vector3>();
        public TTree entrance;
        public List<TTree> exits;
        public TTree mapTree;

        public TTreeMap() { }

        #region Map Generation
        public TTree Construct(MapManager.MapGenerationSettings settings) 
        {
            Queue<TTree> queue = new Queue<TTree>();
            int remainingNodes = settings.maxNodeNumber;

            TTree result = new TTree();
            result.root.location = new Vector3();
            locations.Add(result.root.location);
            remainingNodes--;
            queue.Enqueue(result);

            while (queue.Count > 0)
            {
                TTree currentTree = queue.Dequeue();
                int childrenNumber = (int)Mathf.Floor(UnityEngine.Random.Range(1, Mathf.Min(remainingNodes, settings.maxNodePerRoot)));

                for (int i = 0; i < childrenNumber; i++)
                {
                    TTree newTree = new TTree();
                    Vector3 newLocation = GenerateLocation(currentTree.root.location);
                    newTree.root.location = newLocation;
                    locations.Add(newLocation);
                    currentTree.children.Add(newTree);
                    remainingNodes--;
                    queue.Enqueue(newTree);
                }
            }
            return result;
        }

        private Vector3 GenerateLocation(Vector3 currentPointLocation)
        {
            bool newPositionValid = false;
            Vector3 newPosition = currentPointLocation - GetNewPointLocation(200f, 100f);
            while (!newPositionValid)
            {
                foreach (Vector3 l in locations)
                {
                    if ((l - newPosition).magnitude < 80)
                    {
                        newPositionValid = false;
                        newPosition = currentPointLocation - GetNewPointLocation(200f, 100f);
                        break;
                    }
                    newPositionValid = true;
                }
            }
            return newPosition;
        }

        private Vector3 GetNewPointLocation(float maxDist, float distOffset)
        {
            float angle = Random.value * 160f + 10f; // between 10 and 170
            float radAngle = angle * 2 * Mathf.PI / 360f;
            float distance = Random.value * maxDist + distOffset;
            return new Vector3(distance * Mathf.Sin(radAngle), distance * Mathf.Cos(radAngle), 0f);
        }

        #endregion

        #region Find
        internal Vector3 FindOnFunction(Func<Vector3, Vector3, bool> func)
        {
            if (locations.Count == 0) return mapTree.root.location;

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

        public void FindEntrance()
        {
            entrance = mapTree;
        }

        private List<TTree> FindLeaves()
        {
            Queue<TTree> queue = new Queue<TTree>();
            List<TTree> result = new List<TTree>();
            queue.Enqueue(mapTree);

            while (queue.Count > 0)
            {
                TTree currentTree = queue.Dequeue();
                if (currentTree.children.Count == 0)
                    result.Add(currentTree);

                for (int i = 0; i < currentTree.children.Count; i++)
                {
                    queue.Enqueue(currentTree.children[i]);
                }
            }
            return result;
        }

        public void FindExits(int exitNumber)
        {
            List<TTree> leaves = FindLeaves();
            System.Random random = new System.Random();
            exits = leaves.OrderBy(x => random.Next()).Take(exitNumber).ToList();
        }
        #endregion

        #region Apply Function to Tree
        // _locations must be empty before the call
        internal void ApplyTreeFunction(Func<Vector3, Vector3> func, TTree tree)
        {
            tree.root.location = func(tree.root.location);
            locations.Add(tree.root.location);

            foreach (TTree t in tree.children)
            {
                ApplyTreeFunction(func, t);
            }
        }

        #endregion

        #region From TLinearMap
        public TTreeMap(TLinearMap linearMap)
        {
            TTree Construct(int node)
            {
                TTree result = new TTree();
                result.root.location = linearMap.locations[node];
                foreach (int i in linearMap.nodes[node])
                {
                    TTree newTree = Construct(i);
                    result.children.Add(newTree);
                    if (linearMap.exits.Contains(i))
                    {
                        exits.Add(newTree);
                    }
                }
                return result;
            }

            mapTree = Construct(0);
            entrance = mapTree;
        }
        #endregion
    }
}

