using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScaleUp : MonoBehaviour
{
    public float scaleDuration;
    public Transform herringSpawnArea;
    public Transform herringSpawnTransform;

    public IEnumerator ScaleBoat()
    {
        float initialScale = transform.localScale.x;
        float finalScale = 1.5f;
        float scaleFactor = (finalScale - initialScale) / scaleDuration;
        while (this.transform.localScale.x < finalScale)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x + scaleFactor * Time.deltaTime, this.transform.localScale.y + scaleFactor * Time.deltaTime, this.transform.localScale.z + scaleFactor * Time.deltaTime); ;
            yield return null;
        }
        herringSpawnArea.SetParent(this.transform, true);
        herringSpawnArea.localPosition = herringSpawnTransform.localPosition;
        herringSpawnArea.gameObject.SetActive(true);
    }

}
