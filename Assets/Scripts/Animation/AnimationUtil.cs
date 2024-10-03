using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUtil
{
    public delegate float EasingFunction(float t);

    private AnimationUtil() {}

    public static IEnumerator AnimateTranslation(Transform transform, Vector3 endPosition, float animationTime, EasingFunction easingFunction) {
        Vector3 startPosition = transform.position;

        float frameCount = Mathf.Round(animationTime / Time.fixedDeltaTime);
        float delta = 1f / frameCount;
        for (int i = 0; i < frameCount; i++) {
            transform.position = Vector3.Lerp(startPosition, endPosition, easingFunction(i * delta));
            yield return new WaitForFixedUpdate();
        }
        transform.position = endPosition;
    }

    public static IEnumerator AnimateRotation(Transform transform, Quaternion endRotation, float animationTime, EasingFunction easingFunction) {
        Quaternion startRotation = transform.rotation;

        float frameCount = Mathf.Round(animationTime / Time.fixedDeltaTime);
        float delta = 1f / frameCount;
        for (int i = 0; i < frameCount; i++) {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, easingFunction(delta * i));
            yield return new WaitForFixedUpdate();
        }
        transform.rotation = endRotation;
    }

}
