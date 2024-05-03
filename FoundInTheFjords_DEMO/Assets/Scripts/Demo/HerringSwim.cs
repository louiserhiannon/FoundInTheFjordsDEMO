using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerringSwim : MonoBehaviour
{
    private MoveToObject swim;
    public GameObject herringTargetPrefab;
    public Transform herringStorageArea;
    [SerializeField] private GameObject herringTarget;
    private Transform fishingBoat;

    private void Awake()
    {
        fishingBoat = HerringSpawner.HS.fishingHerringParent;
        swim = GetComponent<MoveToObject>();
        swim.minDistance = 0.5f;
        swim.speed = Random.Range(SpawnEatableHerring.SH.minSpeed, SpawnEatableHerring.SH.maxSpeed);
        swim.rotationSpeed = Random.Range(SpawnEatableHerring.SH.minRotateSpeed, SpawnEatableHerring.SH.maxRotateSpeed);
        Vector3 dir = new Vector3(Random.Range(0.15f,-0.15f), Random.Range(-0.3f,0.1f), -1).normalized;
        Vector3 pos = transform.localPosition + dir * 25;
        herringTarget = Instantiate(herringTargetPrefab, pos, Quaternion.identity, fishingBoat);
        swim.targetTransform = herringTarget.transform;
        herringStorageArea = SpawnEatableHerring.SH.transform;
        //herringTarget.SetActive(false);
        //this.gameObject.SetActive(false);
    }

    
    private void OnEnable()
    {
              
        swim.targetTransform = herringTarget.transform;
        swim.distance = Vector3.Distance(transform.position, swim.targetTransform.position);
        //herringTarget.SetActive(true);
        StartCoroutine(SwimFromNet());
    }

    public IEnumerator SwimFromNet()
    {
        while (swim.distance > swim.minDistance)
        {
            swim.MoveToMinimumDistance();
            //followMom.Follow();

            yield return null;
        }


        gameObject.transform.position = herringStorageArea.position;

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);

        }



    }
}
