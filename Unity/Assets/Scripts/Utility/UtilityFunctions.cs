using System;
using Assets.Scripts.Constants;
using Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class UtilityFunctions
    {
        public static bool LocationPathFindingReachable(Vector3 from, Vector3 to)
        {
            return PathUtilities.IsPathPossible((GraphNode)AstarPath.active.GetNearest(from, NNConstraint.Default), (GraphNode)AstarPath.active.GetNearest(to, NNConstraint.Default));
        }

        public static bool RollChance(float chance)
        {
            if (chance >= 1.0f)
            {
                return true;
            }
            if (chance <= 0f)
            {
                return false;
            }

            return UnityEngine.Random.value < chance;
        }

        public static Vector2 GetDirection(this GameObject obj, GameObject target)
        {
            return (target.transform.position - obj.transform.position).normalized;
        }

        public static Vector2 GetDirection(Vector3 from, Vector3 to)
        {
            return (to - from).normalized;
        }

        public static Vector2 GetFacingDirectionVector(FacingDirection facingDirection)
        {
            switch (facingDirection)
            {
                case FacingDirection.Up:
                    return Vector2.up;
                case FacingDirection.Down:
                    return -Vector2.up;
                case FacingDirection.Left:
                    return -Vector2.right;
                default:
                    return Vector2.right;
            }
        }

        public static FacingDirection GetFacingDirection(this Vector2 v)
        {
            v = v.normalized;
            if (v == Vector2.zero)
            {
                return FacingDirection.Down;
            }
            if (Mathf.Approximately(Vector2.Angle(Vector2.right, v), 45f))
            {
                return FacingDirection.Right;
            }
            if (Mathf.Approximately(Vector2.Angle(-Vector2.right, v), 45f))
            {
                return FacingDirection.Left;
            }
            if (Vector2.Angle(Vector2.up, v) < 45f)
            {
                return FacingDirection.Up;
            }
            if (Vector2.Angle(Vector2.right, v) < 45f)
            {
                return FacingDirection.Right;
            }
            if (Vector2.Angle(-Vector2.up, v) < 45f)
            {
                return FacingDirection.Down;
            }
            if (Vector2.Angle(-Vector2.right, v) < 45f)
            {
                return FacingDirection.Left;
            }
            throw new Exception("Unreachable Code");
        }
    }
}
