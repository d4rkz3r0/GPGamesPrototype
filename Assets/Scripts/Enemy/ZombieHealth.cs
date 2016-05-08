using UnityEngine;
using System.Collections;

public class ZombieHealth : MonoBehaviour
{
    public int maxHP = 5;
    public int enemyHP;
    public float HPRegenRate = 0.1f;
    public float HPRegenTimeInterval = 1.0f;
     
    private float regenTimer = 0.0f;
    private float fHPRegenAmount;
    private int iHPRegenAmount;

    //External
    public GameObject healthBar;

	void Start ()
    {
	    //Init
	    enemyHP = maxHP;
	    fHPRegenAmount = maxHP * HPRegenRate;
	    iHPRegenAmount = Mathf.RoundToInt(fHPRegenAmount);
        ScaleHealthBar(enemyHP / maxHP);
    }
	
	void Update ()
    {
	    if (!amDead())
	    {
            RegenHP();
        }
	    else
	    {
            Destroy(gameObject);
	    }
    }

    void ScaleHealthBar(float health)
    {
        healthBar.transform.localScale = new Vector3(health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    public void restoreHP(int amountToHeal)
    {
        enemyHP += amountToHeal;
    }

    public void takeDamage(int damageTaken)
    {
        enemyHP -= damageTaken;
    }

    public bool amDead()
    {
        return enemyHP <= 0;
    }

    private void RegenHP()
    {
        if (enemyHP <= maxHP)
        {
            regenTimer += Time.deltaTime;
            if (regenTimer >= HPRegenTimeInterval)
            {
                enemyHP += iHPRegenAmount;
                regenTimer = 0.0f;
            }
        }
    }

    private void fullRestore()
    {
        enemyHP = maxHP;
    }
}
