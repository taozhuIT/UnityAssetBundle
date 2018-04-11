using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate : MonoBehaviour
{

    [SerializeField] private GameObject sprite1;
    [SerializeField] private GameObject sprite2;
    [SerializeField] private GameObject cube;

    void Start()
    {
        //OnSet3DObjRotation();
        OnSet2DObjRotation();
    }

    void Update()
    {
    }

    /// <summary>
    /// 设置3D物体旋转
    /// </summary>
    private void OnSet3DObjRotation()
    {
        // 通过两个点相减再归一化得到方向
        //Vector3 direction = (cube2.transform.position - cube1.transform.position).normalized;
        //// 旋转到指定方向
        //Quaternion dire = Quaternion.LookRotation(direction);
        //// 设置方向
        //cube2.transform.rotation = dire;
        //// 获取旋转角度
        //Debug.Log(dire.eulerAngles);
    }

    /// <summary>
    /// 获取一个点相对于另一个点的旋转角度（2d/3d通用）
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private void OnSet2DObjRotation()
    {
        // 3D的计算只是将Y值换成Z值

        // 方法1
        // 和方法2中的transform.InverseTransformPoint(target.position);意义一样
        Vector2 relative = new Vector2(sprite2.transform.position.x - sprite1.transform.position.x, sprite2.transform.position.y - sprite1.transform.position.y);
        // 以弧度为单位计算并返回 y/x 的反正切值   用Mathf.Atan将Y除X  如果用Mathf.Atan2则不需要自己除。但是计算方法是一样的，可以参考方法2
        float rotateAngle = Mathf.Rad2Deg * Mathf.Atan(relative.y / -relative.x);
        // 判断象限 如果是二三象限 + 90  一四象限 - 90
        if (sprite2.transform.position.x - sprite1.transform.position.x < 0)
            rotateAngle = rotateAngle + 90;
        else
            rotateAngle = rotateAngle - 90;

        // 打印按照正常逻辑从12点开始顺时针旋转的角度
        Debug.Log(rotateAngle + 180);
        // 将角度为负值是正常指向的角度
        sprite1.transform.Rotate(0f, 0f, -(rotateAngle + 180));
        cube.transform.Rotate(0f, (rotateAngle + 180), 0f);


        // 方法2   
        // transfrom相当于中心点、target相当于外面不停移动的一个点，将移动的点的坐标设置为以中心节点为中心
        //Vector3 relative = transform.InverseTransformPoint(target.position);
        //// 如果，从右边开始顺时针旋转那么x值为负值，反之为正值。如果，以左边点旋转那么将Y值放在后面比如：Mathf.Atan2(relative.x, -relative.y)方向和从右边旋转的道理一样（3D物体也可以用这种方法来回去一个相对旋转角度）
        //float angle = Mathf.Atan2(relative.y, -relative.x) * Mathf.Rad2Deg;
        //// 获取的值从-180~180之间。需要转换成0~360的话就要在获得的值上加180
        //return angle + 180;
    }
}


