using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent((typeof(NavMeshAgent)))]
public class SpiderOnClick : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public Transform point3;

    private NavMeshAgent agent;
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Заполнение массива точек
        waypoints = new Transform[] { point1, point2, point3 };

        // Начать движение к первой точке
        MoveToNextWaypoint();
    }

    void Update()
    {
        // Проверка, достиг ли агент текущей точки
        if (agent.remainingDistance < 0.1f && !agent.pathPending)
        {
            // Переход к следующей точке
            MoveToNextWaypoint();
        }
    }

    void MoveToNextWaypoint()
    {
        // Установка следующей точки назначения
        agent.destination = waypoints[currentWaypointIndex].position;

        // Увеличение индекса текущей точки
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}
