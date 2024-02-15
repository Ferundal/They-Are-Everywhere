using UnityEngine;

[ExecuteInEditMode]
public class TesmGenMesh : MonoBehaviour
{
    private void OnEnable()
    {
        Component[] components = GetComponents<Component>();
        
        foreach (var component in components)
        {
            // Проверяем, является ли текущий компонент скриптом
            if (component != this && component.GetType() != typeof(Transform))
            {
                // Если не скрипт, удаляем компонент
                DestroyImmediate(component);
            }
        }
        
        Mesh mesh = new Mesh();

        // Задаем вершины меша
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(1, 1, 0),
            new Vector3(0, 1, 0)
        };

        // Задаем треугольники (индексы вершин для формирования полигонов)
        int[] triangles = new int[]
        {
            0, 1, 2,
            2, 3, 0
        };
        
        // Присваиваем созданные данные мешу
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        // Создаем объект с компонентами MeshFilter и MeshRenderer
        GameObject meshObject = gameObject;
        MeshFilter meshFilter = meshObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = meshObject.AddComponent<MeshRenderer>();

        // Устанавливаем меш для MeshFilter
        meshFilter.mesh = mesh;
    }
}
