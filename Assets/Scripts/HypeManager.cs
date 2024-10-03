using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypeManager : MonoBehaviour
{
    [SerializeField] private List<Animator> fireAnimations;

    private void Start() {
        for (int i = 0; i < fireAnimations.Count; i++) {
            fireAnimations[i].SetFloat("Cycle Offset", Random.Range(0f, 1f));
            fireAnimations[i].transform.position = new Vector3(-2 + i * 0.4f, -1.7f, 0f);
            fireAnimations[i].gameObject.SetActive(false);
            fireAnimations[i].gameObject.GetComponent<FireAnimationHelper>().SetHypeManager(this);
        }
    }

    public void StartFire() {
        foreach (Animator animator in fireAnimations) {
            animator.gameObject.SetActive(true);
            animator.Play("StartFire");
        }
    }

    public void EndFire() {
        foreach (Animator animator in fireAnimations) {
            animator.Play("EndFire");
        }
    }

    public void DeactivateFire(GameObject fireObj) {
        fireObj.SetActive(false);
    }
}
