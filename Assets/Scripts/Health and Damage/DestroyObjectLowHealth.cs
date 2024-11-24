using UnityEngine;

namespace SS
{
    [RequireComponent(typeof(HealthSystem))]
    public class DestroyObjectLowHealth : MonoBehaviour
    {
        [SerializeField]
        private float minHealth = 0f;

        HealthSystem healthSystem;

        private void Start()
        {
            healthSystem = GetComponent<HealthSystem>();
            if (healthSystem == null)
                Debug.LogError("Could not find the health system!");
            healthSystem.onHealthChanged += HealthSystem_onHealthChanged;

        }

        private void OnDestroy()
        {
            healthSystem.onHealthChanged -= HealthSystem_onHealthChanged;
        }

        private void HealthSystem_onHealthChanged(object sender, HealthSystem.OnHealthAmountChangedEventArgs args)
        {
            if (args.healthAmount <= minHealth)
                Destroy(transform.gameObject);
        }
    }
}
