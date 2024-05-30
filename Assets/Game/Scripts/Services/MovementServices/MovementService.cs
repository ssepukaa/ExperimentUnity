using System.Collections.Generic;
using UnityEngine;
using Assets.Game.Scripts.Bases.Interfaces;
using System.Threading.Tasks;

namespace Assets.Game.Scripts.Services.MovementServices {
    public class MovementService {
        private Transform _transform;
        private Animator _animator;
        private AnimationPoolService _animationService;
        private Vector3? _targetPosition;
        private bool _isAttacking;
        private bool _isMoving;
        private readonly IMovable _movable;
        private Dictionary<string, AnimationClip> _animations;
        private int _currentAnimationHash;

        public MovementService(Transform transform, IMovable movable, Animator animator, AnimationPoolService animationService) {
            _transform = transform;
            _movable = movable;
            _animator = animator;
            _animationService = animationService;
            _animations = new Dictionary<string, AnimationClip>();
        }

        public async Task Initialize() {
            await LoadAnimations();
            if (_animator != null) {
                _animationService.InitializeAnimator(_animator);
            } else {
                Debug.LogWarning("Animator is null in MovementService.");
            }
        }

        private async Task LoadAnimations() {
            _animations["Idle"] = _animationService.GetAnimationClip("Idle");
            _animations["Move"] = _animationService.GetAnimationClip("Move");
            _animations["Attack"] = _animationService.GetAnimationClip("Attack");

            foreach (var animation in _animations) {
                if (animation.Value == null) {
                    Debug.LogWarning($"Animation clip for {animation.Key} is not loaded.");
                } else {
                    Debug.Log($"Animation clip for {animation.Key} is loaded successfully.");
                }
            }
        }

        public void SetTarget(Vector3 targetPosition) {
            _targetPosition = targetPosition;
            if (!_isMoving && !_isAttacking) {
                PlayMoveAnimation();
                _isMoving = true;
            }
            Debug.Log("Target set to: " + targetPosition);
        }

        public void UpdateState() {
            if (_targetPosition.HasValue) {
                Vector3 direction = (_targetPosition.Value - _transform.position).normalized;
                if (Vector3.Distance(_transform.position, _targetPosition.Value) > 0.1f) {
                    _transform.position = Vector3.MoveTowards(_transform.position, _targetPosition.Value, _movable.Speed * Time.deltaTime);
                    RotateTowards(direction);
                } else {
                    _targetPosition = null;
                    if (!_isAttacking) {
                        PlayIdleAnimation();
                        _isMoving = false;
                    }
                    Debug.Log("Reached target, switching to idle animation.");
                }
            } else if (!_isAttacking) {
                PlayIdleAnimation();
            }
        }

        private void PlayMoveAnimation() {
            if (_animations.TryGetValue("Move", out var moveClip)) {
                PlayAnimation(moveClip);
            } else {
                Debug.LogWarning("Move animation clip not found.");
            }
        }

        private void RotateTowards(Vector3 direction) {
            if (direction != Vector3.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _movable.Speed * Time.deltaTime);
            }
        }

        public void PlayIdleAnimation() {
            if (_animations.TryGetValue("Idle", out var idleClip)) {
                PlayAnimation(idleClip);
            } else {
                Debug.LogWarning("Idle animation clip not found.");
            }
            _isMoving = false;
        }

        public bool TryPlayAttackAnimation() {
            Debug.Log($"_isAttacking =={_isAttacking}");
            if (!_isAttacking) {
                _isAttacking = true;
                if (_animations.TryGetValue("Attack", out var attackClip)) {
                    BlendAnimation(attackClip, 0.5f); // Время смешивания 0.5 секунд
                    return true;
                } else {
                    Debug.LogWarning("Attack animation clip not found.");
                }
            }
            return false;
        }

        private void PlayAnimation(AnimationClip clip) {
            if (clip == null) {
                Debug.LogWarning($"Clip is null, cannot play animation.");
                return;
            }
            int animationHash = Animator.StringToHash(clip.name);
            if (_currentAnimationHash != animationHash) {
                _animator.Play(animationHash);
                _currentAnimationHash = animationHash;
                Debug.Log($"Playing animation: {clip.name}");
            }
        }

        private void BlendAnimation(AnimationClip clip, float blendDuration) {
            if (clip == null) {
                Debug.LogWarning($"Clip is null, cannot blend animation.");
                return;
            }
            int animationHash = Animator.StringToHash(clip.name);
            if (_currentAnimationHash != animationHash) {
                _animator.CrossFadeInFixedTime(animationHash, blendDuration);
                _currentAnimationHash = animationHash;
                Debug.Log($"Blending to animation: {clip.name}");
            }
        }

        public void StopAttackAnimation() {
            _isAttacking = false;
            if (_targetPosition.HasValue) {
                PlayMoveAnimation();
                _isMoving = true;
            } else {
                PlayIdleAnimation();
            }
        }
    }
}
