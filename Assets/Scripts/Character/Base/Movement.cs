using UnityEngine;
using UnityEngine.AI;
using RPG.Animation.Base;
using System.Collections;

namespace RPG.Movement.Base
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(BaseAnimation))]
    public class Movement : MonoBehaviour
    {
        NavMeshAgent agent;
        BaseAnimation animator;
        const float smoothTime = .1f;
        protected virtual void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<BaseAnimation>();
        }

        protected virtual void FixedUpdate() {
            float speedPercent = agent.velocity.magnitude / agent.speed;
            animator.MovementAnimation(speedPercent, smoothTime, Time.deltaTime);
        }

        protected void MoveToPoint(Vector3 point)
        {
            agent.stoppingDistance = 0f;
            agent.SetDestination(point);
        }

        protected void MoveToObject(Vector3 point, float radius) {
            agent.SetDestination(point);
            agent.stoppingDistance = radius * .5f;
        }

        protected IEnumerator WaitUntilMove(Vector3 point) {
            bool _onSpot = false;
            yield return new WaitUntil(() => {
                while (!_onSpot)
                {
                    agent.SetDestination(point);
                    if (!agent.pathPending)
                    {
                        if (agent.remainingDistance <= agent.stoppingDistance)
                        {
                            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) _onSpot = true;
                        }
                    }
                    return false;
                }
                return true;
            });
        }
    }
}
