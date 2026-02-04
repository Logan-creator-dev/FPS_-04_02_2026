using System;
using UnityEngine;


public class LauncherController : MonoBehaviour
{
    [SerializeField] private float _ammoPower; 
    
    [SerializeField] private GameObject _ammoPrefab;
    [SerializeField] private Transform _spawnPoint;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject instantiate = Instantiate(_ammoPrefab, _spawnPoint.position, Quaternion.identity);
            Rigidbody rb = instantiate.GetComponent<Rigidbody>();
            rb.AddForce(_spawnPoint.forward * _ammoPower);
        }
    }
}