using System.Linq;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [SerializeField, Min(3)] private int lineSegments = 60;
    [SerializeField, Min(1)] private float timeOfTheFlight = 3;

    public void ShowTrajectoryLine(Vector3 startPoint, Vector3 startVelocity, LineRenderer lineRenderer)
    {
        float timeStep = timeOfTheFlight / lineSegments;

        Vector3[] lineRendererPoints = CalculateTrajectory(startPoint, startVelocity, timeStep, lineRenderer);
        lineRenderer.positionCount = lineRendererPoints.Count();
        lineRenderer.SetPositions(lineRendererPoints);
    }

    private Vector3[] CalculateTrajectory(Vector3 startPoint, Vector3 startVelocity, float timeStep, LineRenderer lineRenderer)
    {
        Vector3[] lineRendererPoints = new Vector3[lineSegments];

        lineRendererPoints[0] = startPoint;

        for (int i = 1; i < lineSegments; i++)
        {
            float timeOffset = timeStep * i;

            Vector3 progressWithNoGravityApplied = startVelocity * timeOffset;
            Vector3 gravityOffset = Vector3.up * -0.5f * Physics.gravity.y * timeOffset * timeOffset;
            Vector3 newPos = startPoint + progressWithNoGravityApplied - gravityOffset;
            RaycastHit hit;

            if (Physics.Raycast(lineRendererPoints[i - 1], newPos - lineRendererPoints[i - 1], out hit, (newPos - lineRendererPoints[i - 1]).magnitude))
            {
                lineRendererPoints[i] = hit.point;
                return lineRendererPoints;
            }

            lineRendererPoints[i] = newPos;
        }

        return lineRendererPoints;
    }
}
