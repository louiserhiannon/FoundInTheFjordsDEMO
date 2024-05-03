using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestActivateIdentitySwap : MonoBehaviour
{
    public Transform activeSnorkeler;
    private Camera head;
    public int count = 0;
    private int maxCount = 6;
    //public bool snorkelerActive = false;
    [SerializeField] private bool counterActive = true;
    public TMP_Text counter;
    //public IdentitySwap identitySwap;
    private float currentAngle;

    // Start is called before the first frame update
    void Start()
    {
        head = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = head.transform.localEulerAngles.z;
        Debug.Log(currentAngle);
        activeSnorkeler.eulerAngles = new Vector3(activeSnorkeler.eulerAngles.x, activeSnorkeler.eulerAngles.y, -head.transform.eulerAngles.z);
        if (count < maxCount)
        {
            if (counterActive)
            {
                if ((head.transform.localEulerAngles.z > 30 && head.transform.localEulerAngles.z < 90) || (head.transform.localEulerAngles.z > 270 && head.transform.localEulerAngles.z < 330))
                {
                    count++;
                    counterActive = false;
                }
            }
            else if (head.transform.localEulerAngles.z < 2 || head.transform.localEulerAngles.z > 358)
            {
                counterActive = true;
            }

        }

        counter.text = count.ToString();
    }
}
