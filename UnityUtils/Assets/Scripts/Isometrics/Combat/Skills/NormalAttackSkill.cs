namespace Assets.Scripts.Isometrics.Combat.Skills
{
    public class NormalAttackSkill : Skill
    {
        public override void Effect()
        {
            FilterContent();

            for (int i = 0; i < Turn.Targets.Count; i++)
            {
                var unit = Turn.Targets[i].content.GetComponent<Unit>();

                if (unit != null)
                {
                    var hp = unit.GetStat(StatEnum.HP);

                    CombatLog.Append(string.Format("{0} estava com {1} HP, foi afetado por {2} e ficou com {3}", unit, hp, -Damage, hp - Damage));
                    unit.SetStat(StatEnum.HP, -Damage);
                }
            }
        }
    }
}
