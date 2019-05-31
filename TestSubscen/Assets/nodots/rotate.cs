using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;

public class rotate : MonoBehaviour
{
    public GameObject gm;
    private Rotation rotation;
    // Start is called before the first frame update
    void Start()
    {
        rotation.Value = gm.transform.rotation ;
    }

    // Update is called once per frame
    void Update()
    {
        gm.transform.rotation =math.mul(math.normalize(gm.transform.rotation), quaternion.AxisAngle(math.up(), math.radians(360) * Time.deltaTime));
    }
}
