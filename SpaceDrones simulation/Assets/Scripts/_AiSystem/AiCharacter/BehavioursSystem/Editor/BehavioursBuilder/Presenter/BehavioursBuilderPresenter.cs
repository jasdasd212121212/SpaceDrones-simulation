using UnityEngine;

public class BehavioursBuilderPresenter
{
    private BehavioursBuilderModel _graphBuilder;
    private BehavioursProvidersBuilder _providersBuilder;

    public BehavioursBuilderPresenter(BehavioursBuilderModel graphBuilder, BehavioursProvidersBuilder providersBuilder)
    {
        _graphBuilder = graphBuilder;
        _providersBuilder = providersBuilder;
    }

    public void Build(BehavioursBuilderPresenterData data)
    {
        _graphBuilder.Build(data.GraphName);

        if (data.DoBuildProviders)
        {
            _providersBuilder.Build(data.GraphName, data.BehavioursStateMachine);
        }

        Debug.Log("Behaviours builded succwsfully!!!");
    }
}