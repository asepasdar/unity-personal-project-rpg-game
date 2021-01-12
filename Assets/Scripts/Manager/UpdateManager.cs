using RPG.Movement.Base;
using RPG.Stats.Base.Enemy;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    [Header("These Scripts will run Update")]
    public List<EnemyStats> Enemies;
    public List<Outline> Outlines;

    [Header("These Scripts will run FixedUpdated")]
    public List<Movement> Movements;

    #region Singleton
    public static UpdateManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one insance of PlayerData");
            return;
        }
        instance = this;
    }
    #endregion
    private void Update()
    {
        foreach (var _enemy in Enemies) {
            _enemy.UpdateMe();
        }
        foreach (var _outline in Outlines) {
            _outline.UpdateMe();
        }
    }

    private void FixedUpdate()
    {
        foreach (var _movementUpdate in Movements) {
            _movementUpdate.FixedUpdateMe();
        }
    }
}
