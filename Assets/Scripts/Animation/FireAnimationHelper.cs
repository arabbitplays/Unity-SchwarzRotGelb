using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAnimationHelper : MonoBehaviour
{
    private HypeManager hypeManager;

    public void SetHypeManager(HypeManager hypeManager) {
        this.hypeManager = hypeManager;
    }

    public void EndFire() {
        hypeManager.DeactivateFire(gameObject);
    }
}
