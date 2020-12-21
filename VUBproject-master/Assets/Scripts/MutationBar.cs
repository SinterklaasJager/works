using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutationBar : MonoBehaviour
{
    public GameManager gameManager;
    float mutationPoints;
    float maxMutationPoints;
    Slider slider;
    public Image Fill;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;

    }
    void Update()
    {

        if (slider.value >= 3)
        {
            Fill.color = new Color(238, 130, 238, 50);
        }
        else if (slider.value >= 6)
        {
            Fill.color = new Color(238, 130, 238, 100);
        }
    }
    public float GetMutationPoints(float mutationPoints)
    {

        return mutationPoints = this.mutationPoints;
    }

    public void SetMutationPoints(float mutationPoints)
    {
        this.mutationPoints = mutationPoints;
    }
    public void updateslider(int amount)
    {
        slider.value += amount;
        if (slider.value >= 15)
        {
            InvokeRepeating("Deplete", 0.0f, 1.0f);
        }

    }

    void Deplete()
    {

        slider.value--;
        if (slider.value == 0)
        {
            gameManager.stopmutation();

            CancelInvoke("Deplete");
        }


    }




}
