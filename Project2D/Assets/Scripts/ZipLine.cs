using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class ZipLine : MonoBehaviour
{
    [SerializeField] public BoxCollider2D col = null;
    [SerializeField] public SpriteRenderer spr = null;
    [SerializeField] public Transform startPoint = null;
    [SerializeField] public Transform endPoint = null;
    [Range(0, 100)] [SerializeField] public float percentage = 0;
    [SerializeField] public int floorDiference = 0;


    private void OnDrawGizmos()
    {
        float lenght = (endPoint.position - startPoint.position).magnitude;
        spr.size = new Vector2(lenght, spr.size.y);
        col.size = new Vector2(lenght - .1f, spr.size.y);
        col.offset = new Vector2(lenght/2, 0);
        startPoint.position = new Vector3(startPoint.position.x, startPoint.position.y, 0);
        endPoint.position = new Vector3(endPoint.position.x, endPoint.position.y, 0);
        spr.transform.position = new Vector3(startPoint.position.x, startPoint.position.y, 0);
        Vector3 direction = endPoint.position - startPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spr.transform.eulerAngles = Vector3.forward * angle;
        

        float total = endPoint.position.x - startPoint.position.x;
        float totalVertical = endPoint.position.y - startPoint.position.y;
        float currentvertical = (totalVertical * percentage) / 100;
        Vector2 position = new Vector2((total * percentage) / 100, (totalVertical * percentage) / 100);
        position = new Vector2(startPoint.position.x + position.x, startPoint.position.y + position.y);
        Gizmos.DrawLine(position - (Vector2.right*.5f), position + (Vector2.right*.5f));
        Gizmos.DrawLine(position - (Vector2.up*.5f), position + (Vector2.up*.5f));

    }

}
