using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        private LivingObject _livingObject;
        private Animator _animator;
        private GeneratorHpSlider generatorHpSlider;

        private void Awake()
        {
            _livingObject = new LivingObject();
            _animator = GetComponent<Animator>();
            generatorHpSlider = GetComponentInChildren<GeneratorHpSlider>();
        }

        private void Update()
        {

        }


        public void TakeDamage(Damage damage)
        {
            _livingObject.TakeDamage(damage);
            generatorHpSlider.HpSlider.value -= damage.DamageValue;
            if (_livingObject.CurrentHealthPoint < 0)
            {
                _animator.SetTrigger("Death");
                return;
            }
            _animator.SetTrigger("BeHit");
        }

    }
}