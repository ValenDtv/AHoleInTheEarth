using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTextWindow : MonoBehaviour
{
    GameObject canvastxt;
    private GameObjectCollector Collector;

    Canvas x;
    Image p;
    Text t;
    string textInstruction;

    void Start()
    {
        Collector = GameObjectCollector.Collector.GetComponent<GameObjectCollector>();
        canvastxt = Collector.GameObjects.Canvastxt;
        Settext(@"W,A,S,D - движение вперед/влево/назад/вправо
E - использование предмета
P - пауза в игре
Tab - меню инвентаря
    Нажатие правой кнопки мыши по предмету открывает меню взаимодействия с ним.
    Для того, что применить предмет из ивентаря к объекту на локации, нужно сначала взять его в руки.
    В инвентаре для перетаскивание предмета на предмет позволяет
    получать их совмещение(если предметы подходят друг к другу)
    ");
    }
    void Settext(string str)
    {
        x = canvastxt.GetComponentInChildren<Canvas>();
        p = x.GetComponentInChildren<Image>();
        t = p.GetComponentInChildren<Text>();

        textInstruction =
@"W,A,S,D - движение вперед/влево/назад/вправо
E - использование предмета
P - пауза в игре
Tab - меню инвентаря
    Нажатие правой кнопки мыши по предмету открывает меню взаимодействия с ним.
    Для того, что применить предмет из ивентаря к объекту на локации, нужно сначала взять его в руки.
    В инвентаре для перетаскивание предмета на предмет позволяет
    получать их совмещение(если предметы подходят друг к другу)
    ";
        t.text = str;
        //canvastxt.GetComponent<Canvas>().enabled = true;
    }

    public void ButtonOKClick()
    {
        canvastxt.GetComponent<Canvas>().enabled = false;
        Cursor.visible = false;
    }
}
