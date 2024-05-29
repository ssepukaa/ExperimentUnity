using UnityEngine;
using Assets.Game.Scripts.GameC.GameServices;
using Assets.Game.Scripts.PlayerC;
using System.Collections.Generic;
using Assets.Game.Scripts.Data.Constants;

namespace Assets.Game.Scripts.Services.MovementServices {
    public class MovementService {
        private Transform _transform;
        private Animator _animator;
        private IAnimationService _animationService;
        private Vector3? _targetPosition;
        private bool _isAttacking;
        private bool _isMoving;
        private readonly PlayerModel _model;
        private Dictionary<string, int> _animationHashes;
        private int _currentAnimationHash;

        public MovementService(Transform transform, PlayerModel model, Animator animator, IAnimationService animationService) {
            _transform = transform;
            _model = model;
            _animator = animator;
            _animationService = animationService;
            _animationHashes = new Dictionary<string, int>();
            _animationService.InitializeAnimator(_animator);
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
                    _transform.position = Vector3.MoveTowards(_transform.position, _targetPosition.Value, _model.Speed * Time.deltaTime);
                    RotateTowards(direction);
                    if (!_isAttacking && !_animator.GetCurrentAnimatorStateInfo(0).IsName("amoeba_attack")) {
                        PlayMoveAnimation();
                    }
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
            PlayAnimation(ResourceConstants.AmoebaMove);
        }

        private void RotateTowards(Vector3 direction) {
            if (direction != Vector3.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _model.Speed * Time.deltaTime);
            }
        }

        public void PlayIdleAnimation() {
            PlayAnimation(ResourceConstants.AmoebaIdle);
            _isMoving = false;
        }

        public bool TryPlayAttackAnimation() {
            if (!_isAttacking) {
                _isAttacking = true;
                BlendAnimation(ResourceConstants.AmoebaAttack, 0.5f); // Время смешивания 0.5 секунд
                return true;
            }
            return false;
        }

        private void PlayAnimation(string animationName) {
            if (!_animationHashes.ContainsKey(animationName)) {
                var clip = _animationService.GetAnimationClip(animationName);
                if (clip != null) {
                    _animationHashes[animationName] = Animator.StringToHash(clip.name);
                } else {
                    Debug.LogWarning($"Animation clip not found for key: {animationName}");
                    return;
                }
            }
            int animationHash = _animationHashes[animationName];
            if (_currentAnimationHash != animationHash) {
                _animator.Play(animationHash);
                _currentAnimationHash = animationHash;
                Debug.Log($"Playing animation: {animationName}");
            }
        }

        private void BlendAnimation(string animationName, float blendDuration) {
            if (!_animationHashes.ContainsKey(animationName)) {
                var clip = _animationService.GetAnimationClip(animationName);
                if (clip != null) {
                    _animationHashes[animationName] = Animator.StringToHash(clip.name);
                } else {
                    Debug.LogWarning($"Animation clip not found for key: {animationName}");
                    return;
                }
            }
            int animationHash = _animationHashes[animationName];
            if (_currentAnimationHash != animationHash) {
                _animator.CrossFadeInFixedTime(animationHash, blendDuration);
                _currentAnimationHash = animationHash;
                Debug.Log($"Blending to animation: {animationName}");
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
