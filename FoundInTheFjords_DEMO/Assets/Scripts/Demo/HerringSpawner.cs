using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerringSpawner : MonoBehaviour
{
    public static HerringSpawner HS;
    public GameObject herringPrefabCarousel;
    public int numberOfHerringCarousel;
    public List<GameObject> herringListCarousel;
    public Transform carouselHerringParent;
    public GameObject herringPrefabFishing;
    public int numberOfHerringFishing;
    public List<GameObject> herringListFishing;
    public Transform fishingHerringParent;
    public bool useGravityCarousel;
    public bool useGravityFishing;


    private void Awake()
    {
        HS = this;
        useGravityCarousel = true;
        useGravityFishing = false;
}
    void Start()
    {
        for(int i = 0; i < numberOfHerringCarousel; i++)
        {
            herringListCarousel.Add(Instantiate(herringPrefabCarousel, EatingControllerDEMO.ECDemo.herringStorageAreaCarousel.position, Quaternion.identity, carouselHerringParent));
            herringListCarousel[i].SetActive(false);
            
            if (herringListCarousel[i].TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.useGravity = false;
                rigidbody.isKinematic = true;
            }
        }

        for (int i = 0; i < numberOfHerringFishing; i++)
        {
            herringListFishing.Add(Instantiate(herringPrefabFishing, EatingControllerDEMO.ECDemo.herringStorageAreaFishing.position, Quaternion.identity));
            herringListFishing[i].SetActive(false);
            herringListFishing[i].transform.SetParent(fishingHerringParent);
            if (herringListFishing[i].TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.useGravity = false;
                rigidbody.isKinematic = true;
            }
        }
    }



   
}
