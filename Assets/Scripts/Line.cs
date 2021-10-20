using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform p2 = null;
    [SerializeField] LineRenderer line = null;
    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0,transform.localPosition);
        line.SetPosition(1,p2.localPosition);
    }
}
