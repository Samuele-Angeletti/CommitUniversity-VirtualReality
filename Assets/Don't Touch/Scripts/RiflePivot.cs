using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RiflePivot : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform shootPivot;
    Vector3 _target;
    RaycastHit[] _hits;
    Camera _mainCam;
    Vector3 _screenCenter;
    private void Awake()
    {
        _hits = new RaycastHit[30];
        _mainCam = Camera.main;
        _screenCenter = new Vector3(Screen.width /2, Screen.height /2, shootPivot.position.z + 1000);
    }

    //private void Update()
    //{
    //    if (Physics.RaycastNonAlloc(shootPivot.position, _screenCenter, _hits, 1000) > 0)
    //    {
    //        var first = _hits.FirstOrDefault(h => h.collider != null);
    //        if (first.collider != null)
    //        {
    //            _target = first.collider.;
    //        }
    //    }
    //}

    internal void Shoot()
    {
        var projectile = Instantiate(projectilePrefab, shootPivot.position, Quaternion.identity);
        projectile.Initialize(_screenCenter);

    }
}
