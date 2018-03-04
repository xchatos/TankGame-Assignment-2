using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame { 
public class CameraScript : MonoBehaviour, ICameraFollow {


        public Transform Player;

        private float _time = 0.5f;
        public float _distance =7f;
        public float _angle = 70f;

        

        private Vector3 _offset;


        public void SetAngle(float angle)
        {
            // in inspector
        }

        public void SetDistance(float distance)
        {
            // in inspector
        }

        public void SetTarget(Transform targetTransform)
        {
            // in inspector
        }



        // Update is called once per frame
        void LateUpdate () {

            float yPos = _distance * (Mathf.Cos(_angle * Mathf.Deg2Rad));
            Vector3 offset = new Vector3(-Player.transform.forward.x * _distance, yPos, -Player.transform.forward.z * _distance  );
            Vector3 _newPos = Vector3.Lerp(transform.position, Player.transform.position+offset , _time);
            transform.position = _newPos;

            transform.LookAt(Player.transform.position, Vector3.up);
        }

    }
   
}

