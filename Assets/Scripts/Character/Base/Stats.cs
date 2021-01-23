using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Stats.Base
{
    [System.Serializable]
    public struct Stats
    {
        public BaseValue BaseHealth;
        public BaseValue BaseDamage;
        public BaseValue BaseDef;
        public BaseValue Str;
        public BaseValue Agi;
        public BaseValue Luck;
        public int MaxHealth { 
            get { return BaseHealth.GetValue() + (Str.GetValue() * 10); }
        }
        public int Damage {
            get { return BaseDamage.GetValue() + Str.GetValue(); }
        }
        public int Evasion {
            get { return Agi.GetValue() * 10 / 100; }
        }
        public float AttackSpeed {
            get { return 1f + (Agi.GetValue() * 0.01f); }
        }
    }

    [System.Serializable]
    public class BaseValue {
        [SerializeField]
        private int Value;
        private readonly List<int> Modifiers = new List<int>();
        public int GetValue() {
            int sum = Modifiers.Sum();
            return Value + sum;
        }
        public void SetValue(int val) {
            Value = val;
        }
        public void AddModifier(int data) {
            if (data != 0)
                Modifiers.Add(data);
        }
        public void RemoveModifier(int data) {
            if(data != 0)
                Modifiers.Remove(data);
        }
    }
}
