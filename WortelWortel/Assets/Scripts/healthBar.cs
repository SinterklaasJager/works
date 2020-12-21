using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar : MonoBehaviour
{

        private HealthSystem healthSystem;
        private GameObject pfHealthBar;

        public void Setup(HealthSystem healthSystem,GameObject pfHealthBar) {
            this.healthSystem = healthSystem;
            this.pfHealthBar = pfHealthBar;

            healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
            UpdateHealthBar();
        }

        private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e) {
            UpdateHealthBar();
        }


        
        private void UpdateHealthBar() {
            pfHealthBar.transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1,1);
        }
    
}
