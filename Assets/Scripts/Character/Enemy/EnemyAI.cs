using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Movement.Base.Enemy
{
    public class EnemyAI : Movement
    {
        public List<Transform> Points;

        int _index = 0;
        protected override void Start()
        {
            base.Start();
        }

        IEnumerator PatrolToPoint()
        {
            yield return StartCoroutine(WaitUntilMove(Points[_index].position));
            yield return new WaitForSecondsRealtime(5);
            _index = (_index + 1) == Points.Count ? 0 : _index + 1;
            StartCoroutine(PatrolToPoint());
        }
    }
}
