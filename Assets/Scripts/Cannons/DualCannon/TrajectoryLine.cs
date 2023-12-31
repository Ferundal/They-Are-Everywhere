using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [SerializeField, Min(3)] private int lineSegments = 60;
    [SerializeField, Min(1)] private float timeOfTheFlight = 3;

    public void ShowTrajectoryLine(Vector3 startPoint, Vector3 startVelocity, LineRenderer lineRenderer)
    {
        float timeStep = timeOfTheFlight / lineSegments;

        List<Vector3> lineRendererPoints = CalculateTrajectory(startPoint, startVelocity, timeStep, lineRenderer);
        lineRenderer.positionCount = lineRendererPoints.Count();
        lineRenderer.SetPositions(lineRendererPoints.ToArray());
    }

    private List<Vector3> CalculateTrajectory(Vector3 startPoint, Vector3 startVelocity, float timeStep, LineRenderer lineRenderer)
    {
        List<Vector3> lineRendererPoints = new List<Vector3>();

        lineRendererPoints.Add(startPoint);

        for (int i = 1; i < lineSegments; i++)
        {
            float timeOffset = timeStep * i;

            Vector3 progressWithNoGravityApplied = startVelocity * timeOffset;
            Vector3 gravityOffset = Vector3.up * -0.5f * Physics.gravity.y * timeOffset * timeOffset;
            Vector3 newPos = startPoint + progressWithNoGravityApplied - gravityOffset;
            RaycastHit hit;

            if (Physics.Raycast(lineRendererPoints[i - 1], newPos - lineRendererPoints[i - 1], out hit, (newPos - lineRendererPoints[i - 1]).magnitude))
            {
                if (!hit.transform.CompareTag("CannonBall"))
                {
                    lineRendererPoints.Add(hit.point);
                    break;
                }
            }

            lineRendererPoints.Add(newPos);
        }

        return lineRendererPoints;
    }
}
