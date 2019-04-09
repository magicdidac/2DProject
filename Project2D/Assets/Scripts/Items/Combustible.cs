using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combustible : MonoBehaviour
{

    [SerializeField] public CombustibleModel _combustibleModel;

    private float referenceHeight;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
        _combustibleModel = Instantiate(_combustibleModel);
        referenceHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, (referenceHeight + _combustibleModel.amplitude * Mathf.Sin(t * _combustibleModel.speed)));
        t += _combustibleModel.speed * Time.deltaTime;
    }
}
