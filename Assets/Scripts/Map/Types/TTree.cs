using OfFogAndDust.Map;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Map.Types
{
    public class TTree
    {
        public TTree()
        {
            root = new Node();
            children = new List<TTree>();
        }

        public class Node
        {
            public LocationPoint point;
            public Vector3 location;
        }

        public Node root;
        public List<TTree> children;
    }
}
