using UnityEngine;
using UnityEditor;
using System.Text;
using System.Reflection;
using System.Collections; // <- agregado

public class HierarchyDeepReporter
{
    [MenuItem("Tools/Export Deep Scene Report")]
    static void ExportHierarchyDeep()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("=== DEEP SCENE HIERARCHY REPORT ===");
        sb.AppendLine($"Scene: {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");
        sb.AppendLine("--------------------------------------------------\n");

        foreach (GameObject go in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
        {
            PrintObject(go, sb, 0);
        }

        string path = Application.dataPath + "/HierarchyDeepReport.txt";
        System.IO.File.WriteAllText(path, sb.ToString());
        Debug.Log($"Deep hierarchy report exported to: {path}");
    }

    static void PrintObject(GameObject go, StringBuilder sb, int indent)
    {
        string prefix = new string(' ', indent * 2);
        sb.AppendLine($"{prefix}- {go.name}");

        Component[] components = go.GetComponents<Component>();
        foreach (var comp in components)
        {
            if (comp == null)
            {
                sb.AppendLine($"{prefix}  [Missing Component]");
                continue;
            }

            sb.AppendLine($"{prefix}  [Component] {comp.GetType().Name}");

            // Extrae todas las variables: públicas, serializadas y privadas
            FieldInfo[] fields = comp.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                // Muestra todo (no solo lo visible en el inspector)
                object value;
                try
                {
                    value = field.GetValue(comp);
                }
                catch
                {
                    value = "[Error reading value]";
                }

                string valStr = ValueToString(value);
                sb.AppendLine($"{prefix}    - {field.Name} ({field.FieldType.Name}): {valStr}");
            }
        }

        foreach (Transform child in go.transform)
        {
            PrintObject(child.gameObject, sb, indent + 1);
        }
    }

    static string ValueToString(object value)
    {
        if (value == null) return "null";

        // Ignorar referencias a UnityEngine.Object que no están asignadas
        if (value is UnityEngine.Object unityObj && unityObj == null)
            return "[Unassigned Unity Object]";

        if (value is GameObject go) return $"GameObject('{go.name}')";
        if (value is Component comp) return $"Component('{comp.GetType().Name}') on '{comp.gameObject.name}'";
        if (value is string s) return $"\"{s}\"";
        if (value is float f) return f.ToString("F2");
        if (value is Vector3 v3) return $"Vector3({v3.x:F2}, {v3.y:F2}, {v3.z:F2})";
        if (value is Vector2 v2) return $"Vector2({v2.x:F2}, {v2.y:F2})";
        if (value is bool b) return b.ToString();
        if (value is IList list)
        {
            StringBuilder sb = new StringBuilder("[");
            foreach (var item in list)
                sb.Append(ValueToString(item) + ", ");
            sb.Append("]");
            return sb.ToString();
        }

        // Intentar convertir el valor a string sin lanzar excepción
        try
        {
            return value.ToString();
        }
        catch
        {
            return "[Error reading value]";
        }
    }

}
