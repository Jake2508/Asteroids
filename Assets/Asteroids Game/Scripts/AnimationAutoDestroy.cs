using UnityEngine;

public class AnimationAutoDestroy : MonoBehaviour
{
    public float destroyDelay = 0.5f;

    private void Update()
    {
        destroyDelay -= Time.deltaTime;
        if (destroyDelay < 0f)
        {
            Destroy(gameObject);
        }
    }
}
