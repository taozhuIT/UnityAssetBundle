using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2d碰撞
/// </summary>
public class FightMainView : MonoBehaviour
{
    [SerializeField] private GameObject target;
	void Start ()
    {
    }
	
	void Update ()
    {
        // 画线代码
        for (int i = 1; i < 60; ++i)
        {
            // 老方法先得到旋转
            //Quaternion right = Quaternion.identity;
            //Quaternion left = Quaternion.identity;

            // 每次得到旋转角度后将子节点旋转
            Transform childObj = transform.Find("GameObject");

            if (i < 30)
            {
                childObj.rotation = transform.rotation * Quaternion.AngleAxis(i, transform.up);
                // 老方法
                //right = transform.rotation * Quaternion.AngleAxis(i, transform.up);
            }
            else
            {
                childObj.rotation = transform.rotation * Quaternion.AngleAxis(i - 30, -transform.up);
                // 老方法
                //left = transform.rotation * Quaternion.AngleAxis(i - 30, -transform.up);
            }

            // 再得到子节点方向*距离
            Vector3 n = childObj.position + (childObj.forward * 10);

            // 老方法，拿自身Z轴方向*距离，再*得到的旋转 这样算出来的位置不对
            //Vector3 n = transform.position + (transform.forward * 10);
            //Vector3 leftPoint = left * n;
            //Vector3 rightPoint = right * n;

            Debug.DrawLine(transform.position, n, Color.red);
        }


        // 判断目标是否在自己指定扇形范围内，并且距离是否达到指定距离
        float distance = Vector3.Distance(target.transform.position, transform.position);
        Vector3 targetDir = target.transform.position - transform.position;
        Vector3 forward = transform.forward;
        float angle = Vector3.Angle(targetDir, forward);
        if (angle < 30.0f && distance <= 10f)
        {
            Debug.Log("可以攻击  distance:" + distance);
            //攻击代码
        }
    }

    private void OnCollisionEnter2D(Collision2D coll_)
    {
        Debug.Log("Collision   " + coll_.transform.name);
    }

    private void OnTriggerEnter2D(Collider2D coll_)
    {
        Debug.Log("Trigger   " + coll_.transform.name);
    }
}
