namespace Assets.Scripts.Constants
{
    public class LayerConstants
    {
        public static class LayerNames
        {
            public const string StaticObstacle = "StaticObstacle";
            public const string Projectile = "Projectile";
            public const string DamageArea = "DamageArea";
            public const string Enemy = "Enemy";
            public const string PlayerCharacter= "PlayerCharacter";
            public const string Destroyable = "Health";
            public const string DestroyableObstacle = "DestroyableObstacle";
            public const string InvisibleWall = "InvisibleWall";
            public const string SpawnArea = "SpawnArea";
            public const string PlayerInteractiveArea = "PlayerInteractiveArea";
            public const string PlayerPickUp = "PlayerPickUp";
        }

        public static class LayerMask
        {
            public static int StaticObstacle
            {
                get { return UnityEngine.LayerMask.GetMask(LayerNames.StaticObstacle); }
            }
            public static int Projectile
            {
                get { return UnityEngine.LayerMask.GetMask(LayerNames.Projectile); }
            }
            public static int DamageArea
            {
                get { return UnityEngine.LayerMask.GetMask(LayerNames.DamageArea); }
            }
            public static int Enemy
            {
                get { return UnityEngine.LayerMask.GetMask(LayerNames.Enemy); }
            }
            public static int PlayerCharacter
            {
                get { return UnityEngine.LayerMask.GetMask(LayerNames.PlayerCharacter); }
            }
            public static int Destroyable
            {
                get { return UnityEngine.LayerMask.GetMask(LayerNames.Destroyable, LayerNames.PlayerCharacter, LayerNames.Enemy, LayerNames.DestroyableObstacle); }
            }
            public static int DestroyableObstacle
            {
                get { return UnityEngine.LayerMask.NameToLayer(LayerNames.DestroyableObstacle); }
            }
            public static int Obstacle
            {
                get { return UnityEngine.LayerMask.GetMask(LayerNames.StaticObstacle, LayerNames.DestroyableObstacle, LayerNames.InvisibleWall); }
            }
            public static int Character
            {
                get { return UnityEngine.LayerMask.GetMask(LayerNames.PlayerCharacter, LayerNames.Enemy); }
            }

            public static int InvisibleWall
            {
                get { return UnityEngine.LayerMask.GetMask(LayerNames.InvisibleWall); }
            }

            public static int SpawnArea
            {
                get { return UnityEngine.LayerMask.GetMask(LayerNames.SpawnArea); }
            }

            public static int PlayerInteractiveArea
            {
                get { return UnityEngine.LayerMask.GetMask(LayerNames.PlayerInteractiveArea); }
            }

            public static int PlayerPickUp
            {
                get { return UnityEngine.LayerMask.NameToLayer(LayerNames.PlayerPickUp); }
            }
        }
    }
}
