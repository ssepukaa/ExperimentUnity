using System.Collections.Generic;
using Assets.Game.Scripts.Data.Constants;
using Assets.Game.Scripts.GameC.GameServices;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AnimationPoolService : BaseGameService, IAnimationService {
    private readonly Dictionary<string, AnimationClip> _animationClips;

    public AnimationPoolService() {
        _animationClips = new Dictionary<string, AnimationClip>();
        Initialize();
    }

    private void Initialize() {
        LoadAnimationClip(ResourceConstants.AmoebaIdle);
        LoadAnimationClip(ResourceConstants.AmoebaMove);
        LoadAnimationClip(ResourceConstants.AmoebaAttack);
    }

    private void LoadAnimationClip(string key) {
        Addressables.LoadAssetAsync<AnimationClip>(key).Completed += handle => {
            if (handle.Status == AsyncOperationStatus.Succeeded) {
                if (!_animationClips.ContainsKey(key)) {
                    _animationClips[key] = handle.Result;
                    Debug.Log($"Loaded animation clip for key: {key}");
                }
            } else {
                Debug.LogError($"Failed to load animation clip for key: {key}");
            }
        };
    }

    public AnimationClip GetAnimationClip(string key) {
        if (_animationClips.TryGetValue(key, out var clip)) {
            Debug.Log($"Animation clip found for key: {key}");
            return clip;
        } else {
            Debug.LogWarning($"Animation clip not found for key: {key}");
            return null;
        }
    }

    public void InitializeAnimator(Animator animator) {
        var overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        foreach (var clip in _animationClips) {
            overrideController[clip.Key] = clip.Value;
        }
    }

    public void SetAnimation(Animator animator, string animationName, AnimationClip clip) {
        var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
        if (overrideController != null) {
            overrideController[animationName] = clip;
        } else {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            overrideController[animationName] = clip;
            animator.runtimeAnimatorController = overrideController;
        }
    }
}
