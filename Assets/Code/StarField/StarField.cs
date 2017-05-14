using UnityEngine;
using System.Collections;

public class StarField : MonoBehaviour {

    private Transform tx;
    private ParticleSystem.Particle[] points;

    public int starMax = 100;
    public float starSize = 1;
    public float starDistance = 10;

    void Start()
    {
        tx = this.transform;
    }

    void Update()
    {
        if (points == null) CreateStarts();

       
    }


    private void CreateStarts()
    {
        points = new ParticleSystem.Particle[starMax];

        for(int i = 0; i<starMax; i++)
        {
            points[i].position = Random.insideUnitSphere * starDistance + tx.position;
            points[i].startColor = new Color(1, 1, 1, 1);
            points[i].startSize = starSize;
        }
        GetComponent<ParticleSystem>().SetParticles(points, points.Length);
    }

}
