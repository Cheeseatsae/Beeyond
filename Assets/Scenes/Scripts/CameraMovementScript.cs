﻿using System.Collections; 
using System.Collections.Generic;
using Harry;
using UnityEngine;

namespace Roo
{

    public class CameraMovementScript : MonoBehaviour
    {
        public GameObject FollowTarget; // object to track 
        [Range(0.1f, 5f)] public float cameraSpeed = 2; // lerp speed 

        private Vector3 TargetPosition;
        public float cameraClampMinX;
        public float[] cameraClampMaxX;
        public static float cameraClampMinY = 22f;
        public float cameraClampMaxYExploring = 22f;
        public float cameraClampMaxYStruggle = 8f;
        public int openGate = 0;
        public static GameObject liveCamera;

        public GameObject Fence;
        public GameObject CameraClampDeviation;

        void Awake()
        {
            liveCamera = this.gameObject;
        }
        

        // Start is called before the first frame update 
        void Start()
        {
            HiveInteractable.PollenCollected += IncreaseClamp;
        }

        void FixedUpdate()
        {
            TargetPosition = new Vector3(FollowTarget.transform.position.x, FollowTarget.transform.position.y,
                transform.position.z);
            transform.position =
                Vector3.Lerp(transform.position, TargetPosition,
                    cameraSpeed * Time.deltaTime); // lerp to target poosition 

            transform.position =
                new Vector3(Mathf.Clamp(transform.position.x, cameraClampMinX, cameraClampMaxX[openGate]),
                    Mathf.Clamp(transform.position.y, cameraClampMinY, CalculateClamp()), transform.position.z); // clamp camera boundaries 
        }

        public void IncreaseClamp(int count)
        {
            if (openGate < cameraClampMaxX.Length - 1)
            {
                openGate++;
            }
        }

        private void OnDestroy()
        {
            HiveInteractable.PollenCollected -= IncreaseClamp;
        }

        private float CalculateClamp()
        {
            if (transform.position.x < CameraClampDeviation.transform.position.x) return cameraClampMaxYExploring;
            if (transform.position.x > Fence.transform.position.x) return cameraClampMaxYStruggle;

            float totalDistance = Fence.transform.position.x - CameraClampDeviation.transform.position.x;
            float totalHeight = cameraClampMaxYExploring - cameraClampMaxYStruggle;
            float percentageDistance = (Fence.transform.position.x - transform.position.x) / totalDistance;

            float cameraClampMaxY = cameraClampMaxYStruggle + (totalHeight * percentageDistance);

            return cameraClampMaxY;
        }
    }
}