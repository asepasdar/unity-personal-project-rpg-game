using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Animation.Base
{
    public enum IAnimationType
    {
        Default,
        GreatSword,
        SwordShield,
        Bow
    }
    [System.Serializable]
    public struct SetClip {
        public AnimationClip Idle;
        public AnimationClip Walk;
        public AnimationClip Run;
        public AnimationClip Attack;
    }

    [System.Serializable]
    public struct AnimationSet {
        public IAnimationType Type;
        public SetClip Clips;
    }

    [System.Serializable]
    public struct ParticleSet {
        public IAnimationType Type;
        public ParticleSystem Particle;
        public float DelayPercent;
    }
    public class AnimationData : MonoBehaviour
    {
        public static AnimationData instance;
        #region Singleton
        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("More than one Animation instance");
                return;
            }
            instance = this;
        }
        #endregion
        public List<AnimationSet> Animations;
    }
}
