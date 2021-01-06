namespace Assets.Scripts
{
    public interface IDamageable
    {
        /// <summary>
        /// 承受伤害
        /// </summary>
        /// <param name="damage"></param>
        void TakeDamage(Damage damage);

    }
}