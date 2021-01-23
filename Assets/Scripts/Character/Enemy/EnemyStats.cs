using RPG.Data.Enemy;
using RPG.Data.Player;
using RPG.Pooler.Base;
using RPG.Resources.Player;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats.Base.Enemy
{
    public class EnemyStats : BaseStats
    {
        public Transform Target;

        PlayerResources _resources;
        Transform _cam;
        Transform _ui;
        Image _healthSlider;
        void Start()
        {
            _resources = PlayerData.instance.Resources;

            _cam = _resources.PlayerMainCamera;
            _ui = PoolerData.instance.Spawn("Health", _resources.HealthUICanvas).transform;
            _healthSlider = _ui.GetChild(0).GetComponent<Image>();

            UpdateManager.instance.Enemies.Add(this);
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
            CombatText textDamage = PoolerData.instance.Spawn("Damage", _resources.DamageUICanvas).GetComponent<CombatText>();
            Vector2 screenPosition = textDamage.transform.position;
            textDamage.transform.position = screenPosition;
            textDamage.SetText(damage.ToString());
            base.TakeDamage(damage);
            _healthSlider.fillAmount = (float)CurrentHealth / Stats.MaxHealth;
        }

        public override void Die()
        {
            base.Die();
            PoolerData.instance.BackToPool("Health", _ui.gameObject);
            EnemyData.instance.Enemies.Remove(transform);
            UpdateManager.instance.Enemies.Remove(this);
            Destroy(gameObject);
        }
    }
}
