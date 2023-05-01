using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OfFogAndDust.Map
{
    public class Map
    {
        public List<Vector3> locations = new List<Vector3>();
        public Tree entrance;
        public List<Tree> exits;
        public Tree mapTree;

        #region Tree
        public class Tree
        {
            public Tree()
            {
                root = new Node();
                children = new List<Tree>();
            }

            public class Node
            {
                public GameObject relatedGameObject;
                public Vector3 location;
            }

            public Node root;
            public List<Tree> children;
        }
        #endregion

        #region Map Generation
        public Tree Construct(MapManager.MapGenerationSettings settings) 
        {
            Queue<Tree> queue = new Queue<Tree>();
            int remainingNodes = settings.maxNodeNumber;

            Tree result = new Tree();
            result.root.location = new Vector3();
            locations.Add(result.root.location);
            remainingNodes--;
            queue.Enqueue(result);

            while (queue.Count > 0)
            {
                Tree currentTree = queue.Dequeue();
                int childrenNumber = (int)Mathf.Floor(UnityEngine.Random.Range(1, Mathf.Min(remainingNodes, settings.maxNodePerRoot)));

                for (int i = 0; i < childrenNumber; i++)
                {
                    Tree newTree = new Tree();
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
                    if ((l - newPosition).magnitude < 50)
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
            float angle = UnityEngine.Random.value * 160f + 10f; // between 10 and 170
            float radAngle = angle * 2 * Mathf.PI / 360f;
            float distance = UnityEngine.Random.value * maxDist + distOffset;
            return new Vector3(distance * Mathf.Sin(radAngle), distance * Mathf.Cos(radAngle), 0f);
        }

        #endregion

        #region Find
        internal Vector3 FindOnFunction(Func<Vector3, Vector3, bool> func)
        {
            if (locations.Count == 0) return this.mapTree.root.location;

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
            entrance = this.mapTree;
        }

        private List<Tree> FindLeaves()
        {
            Queue<Tree> queue = new Queue<Tree>();
            List<Tree> result = new List<Tree>();
            queue.Enqueue(this.mapTree);

            while (queue.Count > 0)
            {
                Tree currentTree = queue.Dequeue();
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
            List<Tree> leaves = FindLeaves();
            System.Random random = new System.Random();
            exits = leaves.OrderBy(x => random.Next()).Take(exitNumber).ToList();
        }
        #endregion

        #region Apply Function to Tree
        // _locations must be empty before the call
        internal void ApplyTreeFunction(Func<Vector3, Vector3> func, Tree tree)
        {
            tree.root.location = func(tree.root.location);
            locations.Add(tree.root.location);

            foreach (Tree t in tree.children)
            {
                ApplyTreeFunction(func, t);
            }
        }

        #endregion

        //private void GeneratePath(GameObject currentPoint) 
        //{
        //    List<GameObject> closestPoints = FindClosestPoints(currentPoint);
        //    foreach (GameObject point in closestPoints)
        //    {
        //        GeneratePathAux(currentPoint, point);
        //    }
        //} 

        //#region Point Find
        //
        //    return result;
        //}

        //private List<List<GameObject>> FindAllNeighbors(float distance) 
        //{
        //    List<List<GameObject>> result = new List<List<GameObject>>();
        //    foreach (GameObject l in _locations)
        //    {
        //        result.Add(FindNeighbors(l, distance));
        //    }
        //    return result;
        //}

        //private List<GameObject>? FindNeighbors(GameObject currentPoint, float distance)
        //{
        //    if (_locations.Count == 0) return null;

        //    List<GameObject> result = new List<GameObject>();
        //    foreach (GameObject l in _locations)
        //    {
        //        if (l.transform.position != currentPoint.transform.position && (l.transform.position - currentPoint.transform.position).magnitude < distance)
        //        {
        //            result.Add(l);
        //        }
        //    }
        //    return result;
        //} 

        //private List<GameObject>? FindClosestPoints(GameObject currentPoint)
        //{
        //    if (_locations.Count == 0) return null;

        //    List<GameObject> result = new List<GameObject>();
        //    if (_locations.Count < 3)
        //    {
        //        result.Add(_locations[0]);
        //        return result;
        //    }
        //    result.Add(_locations[0]);
        //    result.Add(_locations[1]);
        //    result.Add(_locations[2]);

        //    foreach (GameObject l in _locations)
        //    {
        //        if (l.transform.position != currentPoint.transform.position)
        //        {
        //            for (int i = 0; i < result.Count; i++)
        //            {
        //                if ((l.transform.position - currentPoint.transform.position).magnitude < (result[i].transform.position - currentPoint.transform.position).magnitude)
        //                {
        //                    result[i] = l;
        //                    break;
        //                }
        //            }
        //        }


        //    }
        //    return result;
        //}
        //#endregion
    }
}

