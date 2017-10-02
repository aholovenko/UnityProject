using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHere : MonoBehaviour {

    //Стандартна функція, яка викличеться,
    //коли поточний об’єкт зіштовхнеться із іншим
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Намагаємося отримати компонент кролика
        HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
        //Впасти міг не тільки кролик
        if (rabbit != null)
        {
            //Повідомляємо рівень, про смерть кролика
            LevelController.current.onRabbitDeath(rabbit);         
        }
    }
}
