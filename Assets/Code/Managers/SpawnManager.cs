using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    //public enum TypeOfState
    //{
    //    SpawnEnemy,
    //    TimedWave
    //}

    public GameManager gameManager;
    public Wave[] Wave;

    [Header("Enemies")]
    public BasicEnemyController basicEnemy;
    public float basicEnemyWeight;
    public RocketEnemySmall rocketEnemySmall;
    public float rocketEnemySmallWeight;
    public RocketEnemy rocketEnemy;
    public float rocketEnemyWeight;
    public SwarmEnemy swarmEnemy;
    public float swarmEnemyWeight;
    public KamikaziEnemy kamikaziEnemy;
    public float kamikaziEnemyWeight;

    [Header("Spawn Settings")]
    public float spawnWeight;
    public float maxSpawnWeight;
    public int timeForNextSpawn;

    [Header("Sounds")]
   

    private int DelaySpawn;
    private int currentWave;
    private int currentWaveSpawns;
    private int NumberOfKills;
    private UnityAction countKillsListener;

    //Events
    private UnityAction basicEnemyDestroyed, rocketEnemySmallDestroyed, rocketEnemyDestroyed, swarmEnemyDestroyed, kamikaziEnemyDestroyed;

    void Start()
    {

        //Handle Events
        //int obj;
        //countKillsListener = new UnityAction(CountKills);
        basicEnemyDestroyed = new UnityAction(BasicEnemyDestroyed);
        EventManager.StartListening("OnBasicEnemyDestroyed", basicEnemyDestroyed);

        rocketEnemySmallDestroyed = new UnityAction(RocketEnemySmallDestroyed);
        EventManager.StartListening("OnRocketEnemySmallDestroyed", rocketEnemySmallDestroyed);

        rocketEnemyDestroyed = new UnityAction(RocketEnemyDestroyed);
        EventManager.StartListening("OnRocketEnemyDestroyed", rocketEnemyDestroyed);

        swarmEnemyDestroyed = new UnityAction(SwarmEnemyDestroyed);
        EventManager.StartListening("OnSwarmEnemyDestroyed", swarmEnemyDestroyed);

        kamikaziEnemyDestroyed = new UnityAction(KamikaziEnemyDestroyed);
        EventManager.StartListening("OnKamikaziDestroyed", kamikaziEnemyDestroyed);


        currentWave = 0;
       
        
    }

    public void StarSpawning()
    {
        StartCoroutine(HandleSpawn());
    }

   

    IEnumerator HandleSpawn()
    {

        for (int i = 0; i < Wave.Length; i++)
        {

            currentWaveSpawns = Wave[currentWave].basicEnemySpawns + Wave[currentWave].rocketEnemy + Wave[currentWave].rocketEnemysmall + Wave[currentWave].swarmEnemy + Wave[currentWave].kamikaziEnemy;
           

            while (currentWaveSpawns > 0)
            {
                if (!basicEnemy.isAlive & (Wave[currentWave].basicEnemySpawns > 0) & (spawnWeight + basicEnemyWeight <= maxSpawnWeight))
                {

                    basicEnemy.OnSpawn();
                    spawnWeight += basicEnemyWeight;
                    yield return new WaitForSeconds(timeForNextSpawn);
                }

                if (!rocketEnemySmall.isAlive & (Wave[currentWave].rocketEnemysmall > 0) & (spawnWeight + rocketEnemySmallWeight <= maxSpawnWeight))
                {
                    rocketEnemySmall.OnSpawn();
                    spawnWeight += rocketEnemySmallWeight;
                    yield return new WaitForSeconds(timeForNextSpawn);
                }
                if (!rocketEnemy.isAlive & (Wave[currentWave].rocketEnemy > 0) & (spawnWeight + rocketEnemyWeight <= maxSpawnWeight))
                {
                    rocketEnemy.OnSpawn();
                    spawnWeight += rocketEnemyWeight;
                    yield return new WaitForSeconds(timeForNextSpawn);
                }
                if (!swarmEnemy.isAlive & (Wave[currentWave].swarmEnemy > 0) & (spawnWeight + swarmEnemyWeight <= maxSpawnWeight))
                {
                    swarmEnemy.OnSpawn();
                    spawnWeight += swarmEnemyWeight;
                    yield return new WaitForSeconds(timeForNextSpawn);
                }
                if(!kamikaziEnemy.IsAlive & (Wave[currentWave].kamikaziEnemy > 0) & (spawnWeight + kamikaziEnemyWeight <= maxSpawnWeight))
                {
                    kamikaziEnemy.OnSpawn();
                    spawnWeight += kamikaziEnemyWeight;
                    yield return new WaitForSeconds(timeForNextSpawn);
                }

                yield return new WaitForSeconds(1);
            }

            yield return new WaitForSeconds(3);
            currentWave++;
            if(currentWave == 2)
            {
                AudioManager.PlayBGM(1,.15f,true,5,10);
            }
        }
        
        gameManager.NextLevel();
    }
    
    public void BasicEnemyDestroyed()
    {
        Wave[currentWave].basicEnemySpawns--;
        spawnWeight -= basicEnemyWeight;
        currentWaveSpawns--;
    }

    public void RocketEnemySmallDestroyed()
    {
        Wave[currentWave].rocketEnemysmall--;
        spawnWeight -= rocketEnemySmallWeight;
        currentWaveSpawns--;
    }
    public void RocketEnemyDestroyed()
    {
        Wave[currentWave].rocketEnemy--;
        spawnWeight -= rocketEnemyWeight;
        currentWaveSpawns--;
    }
    public void SwarmEnemyDestroyed()
    {
        Wave[currentWave].swarmEnemy--;
        spawnWeight -= swarmEnemyWeight;
        currentWaveSpawns--;
    }
    public void KamikaziEnemyDestroyed()
    {
        Wave[currentWave].kamikaziEnemy--;
        spawnWeight -= kamikaziEnemyWeight;
        currentWaveSpawns--;
    }

}
[System.Serializable]
public class Wave
{
    //public EnemyProperities[] Enemies;
    public int basicEnemySpawns;
    public int rocketEnemysmall;
    public int rocketEnemy;
    public int swarmEnemy;
    public int kamikaziEnemy;
}

