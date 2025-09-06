using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldElement : MonoBehaviour
{
    public Transform owner;//当前元素属于哪个角色
    public float height = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (owner != null)
        {
            transform.position = owner.position + Vector3.up * height;
        }
        if (Camera.main != null)
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}
