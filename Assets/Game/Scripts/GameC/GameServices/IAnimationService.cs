using UnityEngine;

namespace Assets.Game.Scripts.GameC.GameServices
{
    public interface IAnimationService {
        AnimationClip GetAnimationClip(string key);
        void SetAnimation(Animator animator, string animationName, AnimationClip clip);
        void InitializeAnimator(Animator animator);
    }
}