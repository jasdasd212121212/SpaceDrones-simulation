using UnityEngine; 

public class AiCharacter : MonoBehaviour 
{
    private bool _initialized;

    public AiComponentsContainer AiComponentsContainer { get; private set; }

    public void Initialize(AiComponentsContainer aiComponentsContainer)
    {
        if (_initialized)
        {
            return;
        }

        AiComponentsContainer = aiComponentsContainer;

        _initialized = true;
    }
}