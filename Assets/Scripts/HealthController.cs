using UnityEngine;

public class HealthController : MonoBehaviour
{
    public Renderer rend;
    public float currentHealth;
    public float maxHealth = 5f;

    private float _disolveMultipler = 1;
    private float _disolve = 0f;
    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth > 0) return;
        
        _disolve += Time.deltaTime;
        rend.material.SetFloat("_DissolveValue", _disolve);
        if (_disolve > 1)
            Destroy(gameObject);
    }

    public void Damage()
    {
        currentHealth--;
    }
}

