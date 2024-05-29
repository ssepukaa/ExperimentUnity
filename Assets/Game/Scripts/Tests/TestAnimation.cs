using UnityEngine;

public class TestAnimation : MonoBehaviour {
    public Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            animator.Play("amoeba_idle");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            animator.Play("amoeba_move");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            animator.Play("amoeba_attack");
        }
    }
}