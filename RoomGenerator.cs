using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction { up, down, left, right };
    public Direction direction;

    [Header("������Ϣ")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor, endColor;
    private GameObject endRoom;

    [Header("λ�ÿ���")]
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
            //����point
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
            //�������ⰴ�����¼��ر�����
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
        //OverlapCircle ���ʣ��ķ���
    }

    private void ChangeStartAndEndRoomColor()
    {
        //���ĵ�һ��&���һ���������ɫ
        rooms[0].GetComponent<SpriteRenderer>().color = startColor;
        endRoom = rooms[0];
        foreach (var room in rooms)
        {   //sqrMagnitude����ǻ�ȡģ�ĳ��� ��������
            //��һ�������ê����֮��ķ����ê����ȣ������������֮ǰ�Ľ����ã�һֱ�ҵ���Զ������Ǹ�����
            if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)
            {
                endRoom = room;
            }
        }
        endRoom.GetComponent<SpriteRenderer>().color = endColor;
    }

}
