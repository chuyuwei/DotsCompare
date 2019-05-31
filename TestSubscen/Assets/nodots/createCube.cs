using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Transforms;

public class createCube : MonoBehaviour
{
    public int countx;
    public int county;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < countx; x++)
            for (int y = 0; y < county; y++)
            {
                var instance=Instantiate(prefab);
                LocalToWorld location;
                location.Value= instance.transform.localToWorldMatrix;
                instance.transform.position= math.transform(location.Value,
                        new float3(x * 1.3F, noise.cnoise(new float2(x, y) * 0.21F) * 2, y * 1.3F));
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
