using System;
using Unity.Mathematics;
using UnityEngine;

namespace Utils
{
    public class RotateToCamera : MonoBehaviour
    {
        private void Update()
        {
            Vector3 direction = this.transform.position - Camera.main.transform.position;
            transform.rotation = Quaternion.LookRotation(direction.normalized);
        }
    }
}