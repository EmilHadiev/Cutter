using UnityEngine;

public class ObstacleStaticCut : ObstacleCut
{
    [SerializeField] private GameObject _nonStaticPrefab;

    private void Start()
    {
        CreateClone(_nonStaticPrefab);
    }
}