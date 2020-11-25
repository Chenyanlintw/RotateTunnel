using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public Light lightRef;         // 複製燈光的參考對象
    public int currZ = 1;          // 記錄目前產生的位置
    public List<Light> lightList;  // 燈光清單

  
    void Start()
    {
        // 初始化清單
        lightList = new List<Light>();

        // 開場先產生 10 盞燈光
        for (int i = 0; i < 10; i++)
        {
            CreateLight();
        }
    }


    void Update()
    {
        // 取得位於攝影機之後的燈光
        List<Light> passedLights = new List<Light>();
        foreach (Light l in lightList)
        {
            if (l.transform.position.z < Camera.main.transform.position.z)
            {
                passedLights.Add(l);
            }
        }

        // 刪除超過攝影機的燈光、並再產生
        foreach (Light l in passedLights)
        {
            DeleteLight(l);
            CreateLight();
        }
    }

    // 產生
    void CreateLight()
    {
        Light l = Instantiate(lightRef);
        float z = currZ * 25f;
        l.transform.position = new Vector3(0, 0, z);
        lightList.Add(l);

        currZ++;
    }

    // 刪除
    void DeleteLight(Light l)
    {
        lightList.Remove(l);
        Destroy(l.gameObject);
    }
}
