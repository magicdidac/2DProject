using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTool : MonoBehaviour
{

    #region Variables

    [HideInInspector] private List<Vector3> positions = null;
    [HideInInspector] private Transform parentOfPoints = null;
    [HideInInspector] private Transform parentOfCoins = null;


    [SerializeField] private GameObject coinPrefab = null;
    [SerializeField] private int numberOfCoins = 2;

    #endregion


    #region Getters

    private Transform[] ObtainPoints()
    {
        Transform[] points = new Transform[parentOfPoints.childCount];

        for(int i = 0; i<parentOfPoints.childCount; i++)
        {
            points[i] = parentOfPoints.GetChild(i);
        }

        return points;
    }

    private List<Vector3> ObtainCoinPositions(Transform[] points)
    {
        float totalDistance = CalculateTotalDistance(points);
        float distanceBetween = totalDistance / (numberOfCoins - 1);

        List<Vector3> positions = new List<Vector3>();
        positions.Add(points[0].position);

        for(int i=1; i<points.Length; i++)
        {

            if (!double.IsNaN((points[i].position - positions[positions.Count - 1]).magnitude))
            {

                if ((points[i].position - positions[positions.Count - 1]).magnitude < distanceBetween)
                {
                    float newBetween = distanceBetween - (points[i].position - positions[positions.Count - 1]).magnitude;
                    if (points.Length - 1 >= i + 1)
                        positions.Add(CalculatePoint(points[i].position, points[i + 1].position, newBetween));
                }
                else
                {
                    positions.Add(CalculatePoint(positions[positions.Count - 1], points[i].position, distanceBetween));
                    i--;
                }
            }
        }

        positions.Add(points[points.Length - 1].position);

        return positions;

    }

    private Vector3 GetPoint(float total, float distance, float xTotal, float yTotal, Vector3 startPos)
    {
        float percentage = (distance * 100) / total;
        float xAmmount = (percentage * xTotal) / 100;
        float yAmmount = (percentage * yTotal) / 100;

        Vector3 resultVector = new Vector3(startPos.x + xAmmount, startPos.y + yAmmount, 0);

        return resultVector;
    }

    private Vector3 CalculatePoint(Vector3 startPos, Vector3 endPos, float distance)
    {
        float xTotal = endPos.x - startPos.x;
        float yTotal = endPos.y - startPos.y;
        float totalDistance = (endPos-startPos).magnitude;
        Vector3 fianalPoint = GetPoint(totalDistance, distance, xTotal, yTotal, startPos);
        return fianalPoint;
    }

    private float CalculateTotalDistance(Transform[] points)
    {
        Transform oldPoint = null;
        float distance = 0;

        foreach(Transform t in points)
        {
            if (oldPoint == null)
                oldPoint = t;
            else
            {
                distance += (t.position - oldPoint.position).magnitude;
                oldPoint = t;
            }
        }

        return distance;

    }

    #endregion


    #region Other

    public void Generate()
    {
        if (!Validate())
            return;

        while (parentOfCoins.childCount != 0)
        {
            GameObject.DestroyImmediate(parentOfCoins.GetChild(0).gameObject);
        }

        foreach (Vector3 v in positions)
        {
            Instantiate(coinPrefab, v, Quaternion.identity, parentOfCoins);
        }
    }

    private bool Validate()
    {
        bool isCorrect = true;

        if (coinPrefab == null)
        {
            Debug.LogError("Coin prefab is null.");
            isCorrect = false;
        }

        return isCorrect;

    }

    #endregion


    #region Gizmos

    private void OnDrawGizmos()
    {

        if (parentOfPoints == null)
        {
            parentOfPoints = new GameObject("ParentOfPoints").transform;
            parentOfPoints.SetParent(transform);
        }

        if (parentOfCoins == null)
        {
            parentOfCoins = new GameObject("ParentOfCoins").transform;
            parentOfCoins.SetParent(transform);
        }

        if (numberOfCoins < 2)
            numberOfCoins = 2;

        if (!Validate())
            return;

        Transform[] points = ObtainPoints();

        if (points.Length < 1)
        {
            Transform one = new GameObject("FirstPoint").transform;
            one.SetParent(parentOfPoints);
            one.position = transform.position + Vector3.left;

            Transform two = new GameObject("SecondPoint").transform;
            two.SetParent(parentOfPoints);
            two.position = transform.position + Vector3.right;
            return;
        }

        if(points.Length < 2)
        {
            Transform two = new GameObject("SecondPoint").transform;
            two.SetParent(parentOfPoints);
            two.position = transform.position + Vector3.right;
            return;
        }

        Transform oldPoint = null;

        Gizmos.color = Color.white;

        foreach(Transform t in points)
        {
            if (oldPoint == null)
                oldPoint = t;
            else
            {
                Gizmos.DrawLine(oldPoint.position, t.position);
                oldPoint = t;
            }
        }
        
        positions = ObtainCoinPositions(points);
        
        Gizmos.color = Color.red;

        foreach(Vector3 v in positions)
        {
            Gizmos.DrawSphere(v, .1f);
        }

    }

    #endregion

}
