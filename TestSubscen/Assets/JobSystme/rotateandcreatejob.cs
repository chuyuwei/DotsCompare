using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine.Jobs;
using Unity.Burst;


public class rotateandcreatejob : MonoBehaviour
{
    public GameObject prefab;
    public int countx;
    public int county;

    TransformAccessArray m_TransformsAccessArray;
    protected Transform[] m_Transforms;

    RotationUpdateJob m_job;

    JobHandle m_RotationUpdateJobHandle;


    // Start is called before the first frame update
    void Start()
    {
        m_Transforms = new Transform[countx * county];
        for (int x = 0; x < countx; x++)
            for (int y = 0; y < county; y++)
            {
                var instance = Instantiate(prefab);
                LocalToWorld location;
                location.Value = instance.transform.localToWorldMatrix;
                instance.transform.position = math.transform(location.Value,
                        new float3(x * 1.3F, noise.cnoise(new float2(x, y) * 0.21F) * 2, y * 1.3F));
                m_Transforms[x * county + y] = instance.transform;
            }
        m_TransformsAccessArray = new TransformAccessArray(m_Transforms);
    }

    //[BurstCompile]
    struct RotationUpdateJob : IJobParallelForTransform
    {
        [ReadOnly]
        public float deltaTime;
        public void Execute(int index, TransformAccess transform)
        {
            transform.rotation = math.mul(math.normalize(transform.rotation), quaternion.AxisAngle(math.up(), math.radians(360) * deltaTime));
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_job = new RotationUpdateJob()
        {
            deltaTime = Time.deltaTime
        };

        m_RotationUpdateJobHandle = m_job.Schedule(m_TransformsAccessArray);
    }

    private void LateUpdate()
    {
        m_RotationUpdateJobHandle.Complete();
    }

    private void OnDestroy()
    {
        m_TransformsAccessArray.Dispose();
    }
}
