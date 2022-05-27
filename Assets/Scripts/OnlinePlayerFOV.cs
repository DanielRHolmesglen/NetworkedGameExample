using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlinePlayerFOV : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    CharacterMovement characterMovement;
    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        StartCoroutine("FindTargetsWithDelay", 0.2f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        if (targetsInViewRadius.Length > 0)
        {
            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask) && target != gameObject.transform)
                    {
                        visibleTargets.Add(target);
                    }
                }
            }
            PickTarget();
        }
        else { characterMovement.target = null; }
    }
    void PickTarget()
    {
        for (int i = 0; i < visibleTargets.Count; i++)
        {
            if (visibleTargets[i].tag == "Player")
            {
                characterMovement.target = visibleTargets[i];
                return;
            }
            if (i > 0 && Vector3.Distance(transform.position, visibleTargets[i].position) < Vector3.Distance(transform.position, visibleTargets[i - 1].position))
            {
                characterMovement.target = visibleTargets[i];
            }
            if (i == 0)
            {
                characterMovement.target = visibleTargets[i];
            }
        }
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}

