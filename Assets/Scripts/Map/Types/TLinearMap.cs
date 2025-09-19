using OfFogAndDust.Map.Events;
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
        public List<Vector3> locations;
        public List<MapEvent> events;

        public TLinearMap() { }

        public TLinearMap(TTreeMap map)
        {
            // this completly breaks links between nodes
            void Construct(TTree tree)
            {
                locations.Add(tree.root.location);
                if (map.entrance == tree)
                {
                    events.Add(new MapEvent
                    {
                        type = MapEvent.EventTypeEnum.ENTRANCE
                    });
                }
                else
                {
                    if (map.exits.Contains(tree))
                    {
                        events.Add(new MapEvent
                        {
                            type = MapEvent.EventTypeEnum.EXIT
                        });
                    }
                    else
                    {
                        events.Add(new MapEvent
                        {
                            type = MapEvent.EventTypeEnum.DIALOG
                        });
                    }
                }
                foreach (TTree t in tree.children)
                {
                    Construct(t);
                }
            }

            events = new List<MapEvent>();
            locations = new List<Vector3>();
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

        internal int FindEntrance()
        {
            for (int i = 0; i < locations.Count; i++)
            {
                if (events[i].type == MapEvent.EventTypeEnum.ENTRANCE)
                {
                    return i;
                }
            }
            throw new Exception("Map Entrance not found");
        }

        internal List<int> FindExits()
        {
            List<int> result = new List<int>();
            for (int i = 0; i < locations.Count; i++)
            {
                if (events[i].type == MapEvent.EventTypeEnum.EXIT)
                {
                    result.Add(i);
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
