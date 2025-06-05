public struct BehavioursBuilderPresenterData
{
    public string GraphName { get; private set; }
    public bool DoBuildProviders { get; private set; }
    public BehavioursStateMachine BehavioursStateMachine { get; private set; }

    public BehavioursBuilderPresenterData(string graphName, bool doBuildProviders, BehavioursStateMachine behavioursStateMachine)
    {
        GraphName = graphName;
        DoBuildProviders = doBuildProviders;
        BehavioursStateMachine = behavioursStateMachine;
    }
}