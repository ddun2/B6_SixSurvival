// �ǰ� �������̽�
using System.Collections;

public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}

// Ÿ���� �þ߰� ���ο� �ִ��� �Ǻ� �������̽�
public interface ITargetInFOV
{
    public bool IsTargetInFieldOfView();
}

// �ǰ� �� �޽� �� ���� �������̽�
public interface IDamageFlash
{
    public IEnumerator DamageFlash();
}