using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CodeEnigma : MonoBehaviour
{
    [SerializeField] string solution;
    [SerializeField] UnityEvent onSolutionCompleted;
    [SerializeField] UnityEvent onSolutionFailed;
    [SerializeField] Material basicMat;
    [SerializeField] List<MeshRenderer> renderers;
    [SerializeField] List<SphereCollider> colliders;
    string _currentSolution;
    public bool SolutionCompleted;
    private void Awake()
    {
        _currentSolution = string.Empty;
    }

    private void OnEnable()
    {
        if (SolutionCompleted)
        {
            gameObject.SetActive(false);
        }
        else
            ResetValues();
    }
    public void AddToSolution(string text)
    {
        _currentSolution += text;
        if (_currentSolution.Length == solution.Length)
        {
            TestSolution();
        }
    }

    private void TestSolution()
    {
        if (SolutionCompleted)
            return;

        if (solution == _currentSolution)
        {
            SolutionCompleted = true;
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }
            onSolutionCompleted.Invoke();
        }
        else
        {
            onSolutionFailed.Invoke();
            foreach (var collider in colliders)
            {
                collider.enabled = true;
            }
        }
        ResetValues();
    }

    private void ResetValues()
    {
        _currentSolution = string.Empty;
        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].material = basicMat;
        }
        
    }

    private void OnDisable()
    {
        ResetValues();
    }
}
