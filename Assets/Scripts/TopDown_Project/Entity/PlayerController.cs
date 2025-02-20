using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace TopDown_Project
{
    public class PlayerController : BaseController
    {
        private Camera camera;
        private GameManager gameManager;

        internal void Init(GameManager gameManager)
        {
            this.gameManager = gameManager;
            camera = Camera.main;
        }

        protected override void HandleAction()
        {

        }

        private void OnMove(InputValue inputValue)
        {
            movementDirection = inputValue.Get<Vector2>();
            movementDirection = movementDirection.normalized;
        }

        private void OnLook(InputValue inputValue)
        {
            Vector2 mousePosition = inputValue.Get<Vector2>();
            Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
            lookDirection = (worldPos - (Vector2)transform.position);

            if (lookDirection.magnitude < 0.9f)
            {
                lookDirection = Vector2.zero;
            }
            else
            {
                lookDirection = lookDirection.normalized;
            }
        }

        private void OnFire(InputValue inputValue)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            isAttacking = inputValue.isPressed;
        }

        public override void Death()
        {
            base.Death();
            gameManager.GameOver();
        }
    }
}