using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class Vector3IntExtensions
    {
        public static bool IsNullOrEmpty(this Vector3Int value)
            => value == null || value == Vector3Int.zero;
    }
}
