using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HeartManager;

public class HealthBar : MonoBehaviour
{
    List<HeartManager> hearts = new List<HeartManager>();
    public GameObject heartPrefab;
    public Health health;

    private void Start()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {
        ClearHearts();

        float maxHealthRemainder = health.GetMaxHealth() % 2;
        int heartsToMake = (int) (health.GetMaxHealth() / 2 + maxHealthRemainder);

        for(int i = 0; i < heartsToMake; i++)
        {
            CreateHeart();
        }

        for(int i = 0; i < hearts.Count; i++)
        {
            int heartState = (int) Mathf.Clamp(health.GetHealth() - (i * 2), 0, 2);
            hearts[i].SetHeartState((HeartStatus)heartState);
        }
    }

    public void CreateHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HeartManager heartComponent = newHeart.GetComponent<HeartManager>();
        heartComponent.SetHeartState(HeartStatus.EMPTY);
        hearts.Add(heartComponent);
    }

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        hearts = new List<HeartManager>();
    }
}
