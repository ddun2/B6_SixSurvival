// 피격 인터페이스
using System.Collections;

public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}

// 타겟이 시야각 내부에 있는지 판별 인터페이스
public interface ITargetInFOV
{
    public bool IsTargetInFieldOfView();
}

// 피격 시 메쉬 색 변경 인터페이스
public interface IDamageFlash
{
    public IEnumerator DamageFlash();
}