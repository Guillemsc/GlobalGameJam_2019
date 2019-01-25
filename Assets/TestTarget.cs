using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget : MonoBehaviour
{
    public Cinemachine.CinemachineTargetGroup group;

    // Start is called before the first frame update
    void Start()
    {
        group.AddTarget(this.transform, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
