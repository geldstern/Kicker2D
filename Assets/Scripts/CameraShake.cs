using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour
{
  
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.position = new Vector3(x, y, -10);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.position = originalPosition;
    }
   
}
