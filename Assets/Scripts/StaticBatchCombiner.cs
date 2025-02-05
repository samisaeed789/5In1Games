using UnityEngine;

public class StaticBatchCombiner : MonoBehaviour
{
    [SerializeField]GameObject[] allGameObjects;//= GameObject.FindObjectsOfType<GameObject>();
    void Start()
    {
        // Find all GameObjects in the scene

        // Create a list to hold all static GameObjects
        var staticObjects = new System.Collections.Generic.List<GameObject>();

        // Iterate through all GameObjects and check if they are static
        foreach (GameObject go in allGameObjects)
        {
            if (go.isStatic)
            {
                staticObjects.Add(go);
            }
        }

        // Combine static GameObjects into a single batch
        if (staticObjects.Count > 0)
        {
            // Create a new parent GameObject to hold the combined static objects
            GameObject combinedStaticParent = new GameObject("Combined_Static_Objects");
            combinedStaticParent.isStatic = true;

            // Reparent all static GameObjects to the new parent
            foreach (GameObject staticObject in staticObjects)
            {
                staticObject.transform.SetParent(combinedStaticParent.transform, true);
            }

            // Force Unity to combine the static objects into a single batch
            StaticBatchingUtility.Combine(combinedStaticParent);
        }
        else
        {
            Debug.LogWarning("No static GameObjects found in the scene.");
        }
    }
}