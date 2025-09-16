using System.Collections.Generic;
using UnityEngine;

namespace OfFogAndDust.Map.Types
{
    /* Cloud Map acts as a transition between multiple locations
     * Navigation in here is not designed in any way
     * It is just a bunch of points scattered into 2D
     * space. */
    [SerializeField]
    internal class TCloudMap
    {
        int entrance;
        List<int> exits;
        public List<Vector3> locations;

        public TCloudMap() { }

        public TCloudMap(TTreeMap map)
        {

        }
    }
}
