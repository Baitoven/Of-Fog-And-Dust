using Pathfinding;
using UnityEngine;

namespace OfFogAndDust.Combat
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Seeker seeker;

        [SerializeField] private float speed;
        [SerializeField] private float nextWaypointDistance;

        [SerializeField] internal Transform destination;
        private Path path;
        private int currentWaypoint;
        private bool reachedEndOfPath = false;

        // TO REMOVE LATER
        private void Start()
        {
            InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
        }

        private void UpdatePath()
        {
            seeker.StartPath(gameObject.transform.position, destination.position, OnPathComplete);
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error) 
            { 
                path = p;
                currentWaypoint = 0;
            }
        }

        private void FixedUpdate()
        {
            if (path == null)
            {
                return;
            }

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector3 direction = (path.vectorPath[currentWaypoint] - gameObject.transform.position).normalized;
            Vector3 force = direction * speed * Time.fixedDeltaTime;

            gameObject.transform.position += force;

            float distance = Vector2.Distance(gameObject.transform.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }
    }
}

