using UnityEngine;

namespace TopDown_Project
{
    public class RangeWeaponHandler : WeaponHandler
    {
        private ProjectileManager projectileManager;

        [Header("Ranged Attack Data")]
        [SerializeField] private Transform projectileSpawnPosition;

        [SerializeField] private int bulletIndex;
        public int BulletIndex { get { return bulletIndex; } }

        [SerializeField] private float bulletSize = 1f;
        public float BulletSize { get { return bulletSize; } }

        [SerializeField] private float duration;
        public float Duration { get { return duration; } }

        [SerializeField] private float spread;
        public float Spread { get { return spread; } }

        [SerializeField] private int numberofProjectilesPerShot;
        public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }

        [SerializeField] private float multipleProjectilesAngle;
        public float MultipleProjectilesAngle { get { return multipleProjectilesAngle; } }

        [SerializeField] private Color projectileColor;
        public Color ProjectileColor { get { return projectileColor; } }

        private StatHandler statHandler;

        protected override void Start()
        {
            base.Start();
            projectileManager = ProjectileManager.Instance;
            statHandler = GetComponentInParent<StatHandler>();
        }

        public override void Attack()
        {
            base.Attack();

            float projectilesAngleSpace = multipleProjectilesAngle;
            int numberOfProjectilesPerShot = numberofProjectilesPerShot + (int)statHandler.GetStat(StatType.ProjectileCount);

            float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace;

            for (int i = 0; i < numberOfProjectilesPerShot; i++)
            {
                float angle = minAngle + projectilesAngleSpace * 1;
                float randomSpread = Random.Range(-spread, spread);
                angle += randomSpread;
                CreateProjectile(Controller.LookDirection, angle);
            }
        }

        private void CreateProjectile(Vector2 _lookDirection, float angle)
        {
            projectileManager.ShootBullet(
                this, projectileSpawnPosition.position,
                RotateVector2(_lookDirection, angle));
        }

        private static Vector2 RotateVector2(Vector2 v, float degree)
        {
            return Quaternion.Euler(0, 0, degree) * v;
        }
    }
}