using RPG.Data.Enemy;
using RPG.Data.Player;
using RPG.Resources.Player;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats.Base.Enemy
{
    public class EnemyStats : BaseStats
    {
        public Transform Target;
        public Outline Outline;
        public Movement.Base.Movement Movement;

        PlayerResources _resources;
        UpdateManager _updateManager;
        Transform _cam;
        Transform _ui;
        Image _healthSlider;
        void Start()
        {
            _updateManager = UpdateManager.instance;
            _resources = PlayerData.instance.Resources;

            _cam = _resources.PlayerMainCamera;
            _ui = Instantiate(_resources.HealthBarPrefab, _resources.HealthUICanvas).transform;
            _healthSlider = _ui.GetChild(0).GetComponent<Image>();

            _updateManager.Outlines.Add(Outline);
            _updateManager.Enemies.Add(this);
            _updateManager.Movements.Add(Movement);
        }

        public void UpdateMe()
        {
            if (_ui != null)
            {
                _ui.position = Target.position;
                _ui.forward = -_cam.forward;
            }
        }
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            float percent = (float)CurrentHealth / Stats.MaxHealth;
            _healthSlider.fillAmount = percent;
        }

        public override void Die()
        {
            base.Die();
            Destroy(_ui.gameObject);
            EnemyData.instance.Enemies.Remove(transform);
            _updateManager.Enemies.Remove(this);
            _updateManager.Outlines.Remove(Outline);
            _updateManager.Movements.Remove(Movement);
            Destroy(gameObject);
        }
    }
}
