using System;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
   private float _timer;
   // private void Start()
   // {
   //    Invoke(nameof(DestroyMe), 3f);
   // }
   //
   // private void DestroyMe()
   // {
   //    Destroy(gameObject); 
   // }

   private void Update()
   {
      _timer += Time.deltaTime;
      if (_timer >= 3f)
      {
         Destroy(gameObject);
      }
         
   }
   private void OnTriggerEnter(Collider other)
   {
      TargetController target = other.GetComponent<TargetController>();
      if (target != null)
      {
         target.TakeDamage(1);
         Destroy(gameObject);
      }
   }
}