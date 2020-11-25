using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // 如果距離攝影機一定距離就刪除
        float tz = transform.position.z;
        float cz = Camera.main.transform.position.z;

        if (Mathf.Abs(tz - cz) < 5.5f)
            Destroy(gameObject);
    }
}
