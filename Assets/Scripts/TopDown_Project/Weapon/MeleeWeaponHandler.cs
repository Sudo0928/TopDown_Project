using UnityEngine;

namespace TopDown_Project
{
    public class MeleeWeaponHandler : WeaponHandler
    {
        [Header("Melee Attack Info")]
        public Vector2 collideBoxSize = Vector2.one;

        protected override void Start()
        {
            base.Start();
            collideBoxSize = collideBoxSize * WeaponSize;
        }

        public override void Attack()
        {
            base.Attack();

            RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x, collideBoxSize, 0, Vector2.zero, 0, target);

            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent<ResourceController>(out var resourceController))
                {
                    resourceController.ChangeHealth(-Power);
                    if (IsOnKnockBack)
                    {
                        if (hit.collider.TryGetComponent<BaseController>(out var controller))
                        {
                            controller.ApplyKnockback(transform, KnockbackPower, KnockbackTime);
                        }
                    }
                }
            }
        }

        public override void Rotate(bool isLeft)
        {
            if (isLeft)
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            else
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }
}