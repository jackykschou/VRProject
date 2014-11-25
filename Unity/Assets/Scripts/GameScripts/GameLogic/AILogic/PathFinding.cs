//#define ASTARDEBUG

using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Attributes;
using Assets.Scripts.Utility;
using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

/** AI for following paths.
 * This AI is the default movement script which comes with the A* Pathfinding Project.
 * It is in no way required by the rest of the system, so feel free to write your own. But I hope this script will make it easier
 * to set up movement for the characters in your game. This script is not written for high performance, so I do not recommend using it for large groups of units.
 * \n
 * \n
 * This script will try to follow a Target transform, in regular intervals, the path to that Target will be recalculated.
 * It will on FixedUpdate try to move towards the next point in the path.
 * However it will only move in the forward direction, but it will rotate around it's Y-axis
 * to make it reach the Target.
 * 
 * \section variables Quick overview of the variables
 * In the inspector in Unity, you will see a bunch of variables. You can view detailed information further down, but here's a quick overview.\n
 * The #repathRate determines how often it will search for new paths, if you have fast moving targets, you might want to set it to a lower value.\n
 * The #Target variable is where the AI will try to move, it can be a point on the ground where the player has clicked in an RTS for example.
 * Or it can be the player object in a zombie game.\n
 * The speed is self-explanatory, so is turningSpeed, however #slowdownDistance might require some explanation.
 * It is the approximate distance from the Target where the AI will start to slow down. Note that this doesn't only affect the end point of the path
 * but also any intermediate points, so be sure to set #forwardLook and #pickNextWaypointDist to a higher value than this.\n
 * #pickNextWaypointDist is simply determines within what range it will switch to Target the next waypoint in the path.\n
 * #forwardLook will try to calculate an interpolated Target point on the current segment in the path so that it has a distance of #forwardLook from the AI\n
 * Below is an image illustrating several variables as well as some internal ones, but which are relevant for understanding how it works.
 * Note that the #forwardLook range will not match up exactly with the Target point practically, even though that's the goal.
 * \shadowimage{aipath_variables.png}
 * This script has many movement fallbacks.
 * If it finds a NavmeshController, it will use that, otherwise it will look for a character controller, then for a rigidbody and if it hasn't been able to find any
 * it will use Transform.Translate which is guaranteed to always work.
 */
namespace Assets.Scripts.GameScripts.GameLogic.AILogic
{
    [RequireComponent(typeof(Seeker))]
    [AddComponentMenu("AILogic/PathFinding")]
    public class PathFinding : GameLogic
    {
        public bool PathFindEnabled { get; private set; }

        public bool TargetReachable
        {
            get
            {
                if (Target == null)
                {
                    return false;
                }
                return UtilityFunctions.LocationPathFindingReachable(transform.position, Target.position);
            }       
        }

        public bool CurrentPathReachable
        {
            get
            {
                if (path == null || path.vectorPath == null || path.vectorPath.Count == 0)
                {
                    return false;
                }
                return UtilityFunctions.LocationPathFindingReachable(Target.position, path.vectorPath[currentWaypointIndex]);
            }
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnNewTargetDiscovered)]
        public void UpdateTarget(GameObject target)
        {
            Target = target.transform;
        }

        public Vector3 GetMoveDirection()
        {
            return CalculateVelocity(GetFeetPosition()).normalized;
        }

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            seeker = GetComponent<Seeker>();
            tr = transform;
            controller = GetComponent<CharacterController>();
            navController = GetComponent<NavmeshController>();
            rvoController = GetComponent<RVOController>();
            rigid = rigidbody;
        }

        protected override void Initialize()
        {
            base.Initialize();

            if (rvoController != null) rvoController.enableRotation = false;

            EnablePathFind();
        }

        protected override void Deinitialize()
        {
            // Abort calculation of path
            if (seeker != null && !seeker.IsDone()) seeker.GetCurrentPath().Error();

            // Release current path
            if (path != null) path.Release(this);
            path = null;

            //Make sure we receive callbacks when paths complete
            seeker.pathCallback -= OnPathComplete;

            DisablePathFind();
        }

        public void EnablePathFind()
        {
            PathFindEnabled = true;

            startHasRun = true;

            lastRepath = -9999;
            canSearchAgain = true;

            lastFoundWaypointPosition = GetFeetPosition();

            if (startHasRun)
            {
                //Make sure we receive callbacks when paths complete
                seeker.pathCallback += OnPathComplete;
            }
        }

        public void DisablePathFind()
        {
            PathFindEnabled = false;

            StopCoroutine(RepeatTrySearchPath());

            // Abort calculation of path
            if (seeker != null && !seeker.IsDone()) seeker.GetCurrentPath().Error();

            // Release current path
            if (path != null) path.Release(this);
            path = null;

            //Make sure we receive callbacks when paths complete
            seeker.pathCallback -= OnPathComplete;
        }

        /** Determines how often it will search for new paths. 
     * If you have fast moving targets or AIs, you might want to set it to a lower value.
     * The value is in seconds between path requests.
     */
        public float repathRate = 0.7f;

        /** Target to move towards.
     * The AI will try to follow/move towards this Target.
     * It can be a point on the ground where the player has clicked in an RTS for example, or it can be the player object in a zombie game.
     */
        public Transform Target;

        /** Enables or disables searching for paths.
     * Setting this to false does not stop any active path requests from being calculated or stop it from continuing to follow the current path.
     * \see #canMove
     */
        public bool canSearch = true;

        /** Enables or disables movement.
      * \see #canSearch */
        public bool canMove = true;

        /** Maximum velocity.
     * This is the maximum speed in world units per second.
     */
        public float speed = 3;

        /** Rotation speed.
     * Rotation is calculated using Quaternion.SLerp. This variable represents the damping, the higher, the faster it will be able to rotate.
     */
        public float turningSpeed = 5;

        /** Distance from the Target point where the AI will start to slow down.
     * Note that this doesn't only affect the end point of the path
     * but also any intermediate points, so be sure to set #forwardLook and #pickNextWaypointDist to a higher value than this
     */
        public float slowdownDistance = 0.6F;

        /** Determines within what range it will switch to Target the next waypoint in the path */
        public float pickNextWaypointDist = 2;

        /** Target point is Interpolated on the current segment in the path so that it has a distance of #forwardLook from the AI.
      * See the detailed description of AIPath for an illustrative image */
        public float forwardLook = 1;

        /** Distance to the end point to consider the end of path to be reached.
     * When this has been reached, the AI will not move anymore until the Target changes and OnTargetReached will be called.
     */
        public float endReachedDistance = 0.2F;

        /** Do a closest point on path check when receiving path callback.
     * Usually the AI has moved a bit between requesting the path, and getting it back, and there is usually a small gap between the AI
     * and the closest node.
     * If this option is enabled, it will simulate, when the path callback is received, movement between the closest node and the current
     * AI position. This helps to reduce the moments when the AI just get a new path back, and thinks it ought to move backwards to the start of the new path
     * even though it really should just proceed forward.
     */
        public bool closestOnPathCheck = true;

        protected float minMoveScale = 0.05F;

        /** Cached Seeker component */
        protected Seeker seeker;

        /** Cached Transform component */
        protected Transform tr;

        /** Time when the last path request was sent */
        private float lastRepath = -9999;

        /** Current path which is followed */
        protected Path path;

        /** Cached CharacterController component */
        protected CharacterController controller;

        /** Cached NavmeshController component */
        protected NavmeshController navController;

        protected RVOController rvoController;

        /** Cached Rigidbody component */
        protected Rigidbody rigid;

        /** Current index in the path which is current Target */
        protected int currentWaypointIndex = 0;

        /** Holds if the end-of-path is reached
     * \see TargetReached */
        protected bool targetReached = false;

        /** Only when the previous path has been returned should be search for a new path */
        protected bool canSearchAgain = true;

        protected Vector3 lastFoundWaypointPosition;
        protected float lastFoundWaypointTime = -9999;

        /** Returns if the end-of-path has been reached
     * \see targetReached */
        public bool TargetReached
        {
            get
            {
                return targetReached;
            }
        }

        /** Holds if the Start function has been run.
     * Used to test if coroutines should be started in OnEnable to prevent calculating paths
     * in the awake stage (or rather before start on frame 0).
     */
        private bool startHasRun = false;

        /** Tries to search for a path every #repathRate seconds.
      * \see TrySearchPath
      */
        protected IEnumerator RepeatTrySearchPath()
        {
            while (true)
            {
                float v = TrySearchPath();
                yield return new WaitForSeconds(v);
            }
        }

        /** Tries to search for a path.
     * Will search for a new path if there was a sufficient time since the last repath and both
     * #canSearchAgain and #canSearch are true and there is a Target.
     * 
     * \returns The time to wait until calling this function again (based on #repathRate) 
     */
        public float TrySearchPath()
        {
            if (Time.time - lastRepath >= repathRate && canSearchAgain && canSearch && Target != null && PathFindEnabled)
            {
                SearchPath();
                return repathRate;
            }
            else
            {
                //StartCoroutine (WaitForRepath ());
                float v = repathRate - (Time.time - lastRepath);
                return v < 0 ? 0 : v;
            }
        }

        /** Requests a path to the Target */
        public virtual void SearchPath()
        {

            if (Target == null) throw new System.InvalidOperationException("Target is null");

            lastRepath = Time.time;
            //This is where we should search to
            Vector3 targetPosition = Target.position;

            canSearchAgain = false;

            //Alternative way of requesting the path
            //ABPath p = ABPath.Construct (GetFeetPosition(),targetPoint,null);
            //seeker.StartPath (p);

            //We should search from the current position
            seeker.StartPath(GetFeetPosition(), targetPosition);
        }

        public virtual void OnTargetReached()
        {
            //End of path has been reached
            //If you want custom logic for when the AI has reached it's destination
            //add it here
            //You can also create a new script which inherits from this one
            //and override the function in that script
        }

        /** Called when a requested path has finished calculation.
      * A path is first requested by #SearchPath, it is then calculated, probably in the same or the next frame.
      * Finally it is returned to the seeker which forwards it to this function.\n
      */
        public virtual void OnPathComplete(Path _p)
        {
            ABPath p = _p as ABPath;
            if (p == null) throw new System.Exception("This function only handles ABPaths, do not use special path types");

            canSearchAgain = true;

            //Claim the new path
            p.Claim(this);

            // Path couldn't be calculated of some reason.
            // More info in p.errorLog (debug string)
            if (p.error)
            {
                p.Release(this);
                return;
            }

            //Release the previous path
            if (path != null) path.Release(this);

            //Replace the old path
            path = p;

            //Reset some variables
            currentWaypointIndex = 0;
            targetReached = false;

            //The next row can be used to find out if the path could be found or not
            //If it couldn't (error == true), then a message has probably been logged to the console
            //however it can also be got using p.errorLog
            //if (p.error)

            if (closestOnPathCheck)
            {
                Vector3 p1 = Time.time - lastFoundWaypointTime < 0.3f ? lastFoundWaypointPosition : p.originalStartPoint;
                Vector3 p2 = GetFeetPosition();
                Vector3 dir = p2 - p1;
                float magn = dir.magnitude;
                dir /= magn;
                int steps = (int)(magn / pickNextWaypointDist);

#if ASTARDEBUG
			Debug.DrawLine (p1,p2,Color.red,1);
#endif

                for (int i = 0; i <= steps; i++)
                {
                    CalculateVelocity(p1);
                    p1 += dir;
                }

            }
        }

        public virtual Vector3 GetFeetPosition()
        {
            return transform.position;
        }

        /** Point to where the AI is heading.
      * Filled in by #CalculateVelocity */
        protected Vector3 targetPoint;
        /** Relative direction to where the AI is heading.
     * Filled in by #CalculateVelocity */
        protected Vector3 targetDirection;

        protected float XZSqrMagnitude(Vector3 a, Vector3 b)
        {
            float dx = b.x - a.x;
            float dz = b.z - a.z;
            return dx * dx + dz * dz;
        }

        /** Calculates desired velocity.
     * Finds the Target path segment and returns the forward direction, scaled with speed.
     * A whole bunch of restrictions on the velocity is applied to make sure it doesn't overshoot, does not look too far ahead,
     * and slows down when close to the Target.
     * /see speed
     * /see endReachedDistance
     * /see slowdownDistance
     * /see CalculateTargetPoint
     * /see targetPoint
     * /see targetDirection
     * /see currentWaypointIndex
     */
        protected Vector3 CalculateVelocity(Vector3 currentPosition)
        {
            if (path == null || path.vectorPath == null || path.vectorPath.Count == 0) return Vector3.zero;

            List<Vector3> vPath = path.vectorPath;

            if (vPath.Count == 1)
            {
                vPath.Insert(0, currentPosition);
            }

            if (currentWaypointIndex >= vPath.Count) { currentWaypointIndex = vPath.Count - 1; }

            if (currentWaypointIndex <= 1) currentWaypointIndex = 1;

            while (true)
            {
                if (currentWaypointIndex < vPath.Count - 1)
                {
                    //There is a "next path segment"
                    float dist = XZSqrMagnitude(vPath[currentWaypointIndex], currentPosition);
                    //Mathfx.DistancePointSegmentStrict (vPath[currentWaypointIndex+1],vPath[currentWaypointIndex+2],currentPosition);
                    if (dist < pickNextWaypointDist * pickNextWaypointDist)
                    {
                        lastFoundWaypointPosition = currentPosition;
                        lastFoundWaypointTime = Time.time;
                        currentWaypointIndex++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            Vector3 dir = vPath[currentWaypointIndex] - vPath[currentWaypointIndex - 1];

            return dir;
        }

        /** Rotates in the specified direction.
     * Rotates around the Y-axis.
     * \see turningSpeed
     */
        protected virtual void RotateTowards(Vector3 dir)
        {

            if (dir == Vector3.zero) return;

            Quaternion rot = tr.rotation;
            Quaternion toTarget = Quaternion.LookRotation(dir);

            rot = Quaternion.Slerp(rot, toTarget, turningSpeed * Time.fixedDeltaTime);
            Vector3 euler = rot.eulerAngles;
            euler.z = 0;
            euler.x = 0;
            rot = Quaternion.Euler(euler);

            tr.rotation = rot;
        }

        /** Calculates Target point from the current line segment.
     * \param p Current position
     * \param a Line segment start
     * \param b Line segment end
     * The returned point will lie somewhere on the line segment.
     * \see #forwardLook
     * \todo This function uses .magnitude quite a lot, can it be optimized?
     */
        protected Vector3 CalculateTargetPoint(Vector3 p, Vector3 a, Vector3 b)
        {
            a.z = p.z;
            b.z = p.z;

            float magn = (a - b).magnitude;
            if (magn == 0) return a;

            float closest = AstarMath.Clamp01(AstarMath.NearestPointFactor(a, b, p));
            Vector3 point = (b - a) * closest + a;
            float distance = (point - p).magnitude;

            float lookAhead = Mathf.Clamp(forwardLook - distance, 0.0F, forwardLook);

            float offset = lookAhead / magn;
            offset = Mathf.Clamp(offset + closest, 0.0F, 1.0F);
            return (b - a) * offset + a;
        }
    }
}