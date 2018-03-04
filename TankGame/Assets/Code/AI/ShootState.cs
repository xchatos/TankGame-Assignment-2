using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.AI { 

    public class ShootState : AIStateBase {

        public float SqrShootingDistance
        {
            get { return Owner.ShootingDistance * Owner.ShootingDistance; }
        }

        public float SqrDetectEnemyDistance
        {
            get { return Owner.DetectEnemyDistance * Owner.DetectEnemyDistance; }
        }

        public ShootState(EnemyUnit owner)
			: base( owner, AIStateType.ShootState )
		{
            AddTransition(AIStateType.Patrol);
            AddTransition(AIStateType.FollowTarget);
        }

        public override void Update()
        {
            Debug.Log(Owner.CurrentState);
            if (!ChangeState())
            {
                Owner.Mover.Move(Owner.transform.forward);
                Owner.Mover.Turn(Owner.Target.transform.position);
                Owner.Weapon.Shoot();
            }
        }


        private bool ChangeState()
        {
            // 1. Are we out of the shooting range?
            // && Are we still in detection distance?
            // If yes, go to follow target state.

            Vector3 toPlayerVector =
                Owner.transform.position - Owner.Target.transform.position;
            float sqrDistanceToPlayer = toPlayerVector.sqrMagnitude;


            if (sqrDistanceToPlayer > SqrShootingDistance && sqrDistanceToPlayer < SqrDetectEnemyDistance)
            {
                return Owner.PerformTransition(AIStateType.FollowTarget);
            }



            // 2. Did the player get away?
            // If yes, go to patrol state.
            // || Is the target active?
            // if no, go to patrol state

            if (sqrDistanceToPlayer > SqrDetectEnemyDistance || !Owner.Target.isActiveAndEnabled)
            {
                Owner.Target = null;
                return Owner.PerformTransition(AIStateType.Patrol);
            }

            // Otherwise return false.
            return false;
        }


    }



}