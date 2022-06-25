using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{

    public GameObject door_left, door_right, door_up, door_down;
    public bool room_left, room_right, room_up, room_down;
    public int step_to_start;
    public Text step_text;
    //bool未赋值为假

    public int door_number;
    void Start()
    {
        door_left.SetActive(room_left);
        door_right.SetActive(room_right);
        door_up.SetActive(room_up);
        door_down.SetActive(room_down);


    }

    public void UpdateRoom()
    {
        step_to_start = (int)(Mathf.Abs(transform.position.x / 18) + Mathf.Abs(transform.position.y / 9));
        //以上这步是获得每个具体数字
        step_text.text = step_to_start.ToString();
        if (room_up || room_down || room_left || room_right)
        {
            door_number++;
        }
    }

}
