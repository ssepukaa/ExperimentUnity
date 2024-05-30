using Assets.Game.Scripts.GameC.GameServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AnimationPoolService : BaseGameService {
    private readonly Dictionary<string, AnimationClip> _animationClips = new Dictionary<string, AnimationClip>();
    private AnimatorOverrideController _animatorOverrideController;

    public async Task Initialize(Dictionary<string, string> animationAddresses) {
        Debug.Log("Initializing AnimationPoolService with animation addresses...");
        List<Task> loadTasks = new List<Task>();

        foreach (var animation in animationAddresses) {
            if (!string.IsNullOrEmpty(animation.Key) && !string.IsNullOrEmpty(animation.Value)) {
                Debug.Log($"Loading animation: {animation.Key} from address: {animation.Value}");
                loadTasks.Add(LoadAnimationClipAsync(animation.Key, animation.Value));
            } else {
                Debug.LogWarning($"Animation key or address is null or empty. Key: {animation.Key}, Address: {animation.Value}");
            }
        }

        await Task.WhenAll(loadTasks);
    }

    private async Task LoadAnimationClipAsync(string key, string address) {
        if (!_animationClips.ContainsKey(key)) {
            var handle = Addressables.LoadAssetAsync<AnimationClip>(address);
            await handle.Task;
            if (handle.Status == AsyncOperationStatus.Succeeded) {
                _animationClips[key] = handle.Result;
                Debug.Log($"Loaded animation clip for key: {key} from address: {address}");
            } else {
                Debug.LogWarning($"Failed to load animation clip for key: {key} from address: {address}");
            }
        }
    }

    public AnimationClip GetAnimationClip(string key) {
        if (key == null) {
            Debug.LogWarning("Requested animation key is null.");
            return null;
        }

        if (_animationClips.TryGetValue(key, out var clip)) {
            Debug.Log($"Animation clip found for key: {key}");
            return clip;
        } else {
            Debug.LogWarning($"Animation clip not found for key: {key}");
            return null;
        }
    }

    public void InitializeAnimator(Animator animator) {
        _animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = _animatorOverrideController;
    }

    public void SetAnimation(Animator animator, string animationName, AnimationClip clip) {
        if (_animatorOverrideController != null && !string.IsNullOrEmpty(animationName) && clip != null) {
            _animatorOverrideController[animationName] = clip;
        } else {
            Debug.LogWarning($"Animator Override Controller, animation name or clip is null. AnimationName: {animationName}, Clip: {clip}");
        }
    }
}
