using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *通过计算两个矩形在x轴或y轴的投影是否均有重合判断重叠矩形，利用两两对比迭代求出全部重叠矩形
 */

//创建矩形结构
[System.Serializable]
public struct Rectangle
{
    public Vector2 v1;
    public Vector2 v2;
    public Vector2 v3;
    public Vector2 v4;
}

public class T02 : MonoBehaviour
{
    public Rectangle[] rectangles;
    public int rectCount;    //矩形个数
    public int rect_over_Sum;    //重叠矩形个数

    public int pointRange;  //矩形的点坐标给个随机范围，0 —— vaule
    //初始化
    void Init()
    {
        rect_over_Sum = 0;
        rectangles = new Rectangle[rectCount];

        for (int i = 0; i < rectangles.Length; i++) //按设定矩形数量创建矩形
        {
            rectangles[i] = CreateRectangles();
        }
    }

    Rectangle CreateRectangles() //依据pointRange创建随机坐标，创建矩形
    {
        Rectangle rectangle = new Rectangle();
        rectangle.v1 = new Vector2(Random.Range(0, pointRange), Random.Range(0, pointRange));
        rectangle.v3 = new Vector2(Random.Range(0, pointRange), Random.Range(0, pointRange));
        rectangle.v2 = new Vector2((rectangle.v1.x - rectangle.v1.y + rectangle.v3.x + rectangle.v3.y) / 2,
            (rectangle.v1.x + rectangle.v1.y - rectangle.v3.x + rectangle.v3.y) / 2);
        rectangle.v4 = new Vector2((rectangle.v1.x + rectangle.v1.y + rectangle.v3.x - rectangle.v3.y) / 2, 
            (-rectangle.v1.x + rectangle.v1.y + rectangle.v3.x + rectangle.v3.y) / 2);
        return rectangle;
    }

    private void Start()
    {
        Init();
        //分别计算投影大小，判断重叠矩形个数 
        for (int i = 0; i < rectCount; i++)
        {
            for(int j = i+1;j <rectCount;j++)
            {
                Vector2 rect01PX = CalProjectionInterval(rectangles[i].v1.x, rectangles[i].v2.x, rectangles[i].v3.x, rectangles[i].v4.x);
                Vector2 rect02PX = CalProjectionInterval(rectangles[j].v1.x, rectangles[j].v2.x, rectangles[j].v3.x, rectangles[j].v4.x);
                Vector2 rect01PY = CalProjectionInterval(rectangles[i].v1.y, rectangles[i].v2.y, rectangles[i].v3.y, rectangles[i].v4.y);
                Vector2 rect02PY = CalProjectionInterval(rectangles[j].v1.y, rectangles[j].v2.y, rectangles[j].v3.y, rectangles[j].v4.y);

                if (isRectangleOver(rect01PX,rect02PX,rect01PY,rect02PY))
                {
                    rect_over_Sum++;
                    break;
                }
            }
        }
        Debug.Log("共有" + rectCount +"个矩形，有" + rect_over_Sum+"个重叠矩形。");
    }

    //计算投影大小，返回值vector2（轴向最小值，轴向最大值）表示一个区间
    Vector2 CalProjectionInterval(float f1,float f2,float f3,float f4)
    {
        float max = f1;
        float min = 0;
        if (f2 > max)
        {
            max = f2;
            min = f1;
        }
        else
            min = f2;

        if (f3 > max)
            max = f3;
        else
            if(min > f3)
            {
                min = f3;
            }

        if (f4 > max)
            max = f4;
        else
            if (min > f4)
            {
                min = f4;
            }

        return new Vector2(min, max);
    }

    //判断两矩形投影是否重叠
    public bool isRectangleOver(Vector2 rect01X,Vector2 rect02X, Vector2 rect01Y, Vector2 rect02Y)
    {
        bool x_over = !(rect01X.y <= rect02X.x || rect02X.y <= rect01X.x);
        bool y_over = !(rect01Y.y <= rect02Y.x || rect02Y.y <= rect01Y.x);
        return x_over && y_over;
    }
}
