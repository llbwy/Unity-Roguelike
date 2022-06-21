using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction { up, down, left, right };
    public Direction direction;

    [Header("房间信息")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor, endColor;
    private GameObject endRoom;

    [Header("位置控制")]
    public Transform generratorPoint;
    public float xOffset, yOffset;
    public LayerMask roomLayer;

    public List<GameObject> rooms = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generratorPoint.position, Quaternion.identity));
            //更改point
            ChangePointPos();
        }
        ChangeStartAndEndRoomColor();





    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //按下任意按键重新加载本场景
        }
    }


    public void ChangePointPos()
    {
        do
        {
            direction = (Direction)Random.Range(0, 4);
            switch (direction)
            {
                case Direction.up:
                    generratorPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.down:
                    generratorPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.left:
                    generratorPoint.position += new Vector3(-xOffset, 0, 0);
                    break;
                case Direction.right:
                    generratorPoint.position += new Vector3(xOffset, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(generratorPoint.position, 0.2f, roomLayer));
        //OverlapCircle 检测剩余的方向
    }

    private void ChangeStartAndEndRoomColor()
    {
        //更改第一个&最后一个房间的颜色
        rooms[0].GetComponent<SpriteRenderer>().color = startColor;
        endRoom = rooms[0];
        foreach (var room in rooms)
        {   //sqrMagnitude这个是获取模的长度 过程如下
            //第一个房间的锚点与之后的房间的锚点相比，距离如果大于之前的将重置，一直找到最远距离的那个房间
            if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)
            {
                endRoom = room;
            }
        }
        endRoom.GetComponent<SpriteRenderer>().color = endColor;
    }

}
