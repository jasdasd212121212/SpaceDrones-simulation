using UnityEditor;

public class BehavioursBuilderEntryPoint : EditorWindow
{
    [MenuItem("Window/BehaviourTreeEditor/Build")]
    public static void OpenWindow()
    {
        BehavioursBuilderGraphsInteractor graphInteractor = new(new GraphsPresistanceModel());

        BehavioursBuilderModel graphsBuilder = new(graphInteractor);
        BehavioursProvidersBuilder providersBuilder = new(graphInteractor);

        GetWindow<BehavioursBuilderView>().Initialize(new(graphsBuilder, providersBuilder));
    }
}