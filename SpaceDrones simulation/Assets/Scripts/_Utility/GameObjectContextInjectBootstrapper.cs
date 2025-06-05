using Zenject;
using UnityEngine;

[RequireComponent(typeof(GameObjectContext))]
public class GameObjectContextInjectBootstrapper : MonoBehaviour
{
    private void OnValidate()
    {
        if (GetComponent<ZenAutoInjecter>() == null)
        {
            gameObject.AddComponent<ZenAutoInjecter>();
        }
    }

    private void Awake()
    {
        GetComponent<GameObjectContext>().Run();
    }
}