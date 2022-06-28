using System;
using Unity.VisualScripting;
using UnityEngine;
using Utils;
using Weapons;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public Transform hitPlane;
        public Vector3 defaultPlanePosition;
        public GameObject weaponObject;
        public bool AbleToShoot { get; set; }

        private IWeapon _weapon;
        private Quaternion _targetRotation;
        private Transform _planeTarget;

        private void Start()
        {
            _weapon = weaponObject.GetComponent<IWeapon>();
        }
        
        void Update () 
        {
            if (_planeTarget != null)
            {
                var direction = _planeTarget.position - transform.position;
                direction.y = 0.0f;
                _targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, Time.deltaTime * 10);
            }
            
            if (Input.touchCount > 0) {
 
                if (Input.GetTouch (0).phase == TouchPhase.Began) 
                {
                    if (AbleToShoot)
                    {
                        Shoot(Input.GetTouch(0).position);
                    }
                }
            }

            #if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                if (AbleToShoot)
                {
                    Shoot(Input.mousePosition);
                }
            }
            #endif
        }
        
        void Shoot(Vector2 screenPosition)
        {
            if (_planeTarget != null)
            {
                hitPlane.position = _planeTarget.position;
            }
            else
            {
                hitPlane.localPosition = defaultPlanePosition;
            }

            var touchPos = Camera.main.ScreenPointToRay(screenPosition);
            var weaponPosition = _weapon.GetShootPosition();
            
            Plane planeToIntersect = new Plane(-this.transform.forward, hitPlane.position);
            Vector3 hitPoint = Vector3.zero;
            if (planeToIntersect.Raycast(touchPos, out var distance)){
                hitPoint = touchPos.GetPoint(distance);
            }
            
            var dir = hitPoint - weaponPosition;
            dir.Normalize();
            
            SlowMotionManager.Instance.DefaultScale();
            _weapon.Shoot(dir);
        }

        public void SetHitPlanePosition(Transform target)
        {
            _planeTarget = target;
            if (_planeTarget != null)
            {
                hitPlane.position = _planeTarget.position;
            }
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(hitPlane.position, 0.2f);
        }
        #endif
    }
}