using UnityEditor;
using UnityEngine;

public class BehavioursBuilderView : EditorWindow
{
    private BehavioursBuilderPresenter _presenter;

    private BehavioursStateMachine _stateMachine;
   
    private string _graphName;
    private bool _doBuildProviders;

    private bool _initialized;

    public void Initialize(BehavioursBuilderPresenter presenter)
    {
        if (_initialized)
        {
            return;
        }

        _presenter = presenter;

        _initialized = true;
    }

    private void OnGUI()
    {
        _graphName = EditorGUILayout.TextField("Graph name", _graphName);
        _doBuildProviders = EditorGUILayout.Toggle("Do build providers", _doBuildProviders);
            
        EditorGUILayout.Space(15);

        if (_doBuildProviders)
        {
            _stateMachine = EditorGUILayout.ObjectField("Behaviours state machine", _stateMachine, typeof(BehavioursStateMachine)) as BehavioursStateMachine;
        }

        EditorGUILayout.Space(25);

        DrawButton();
    }

    private void DrawButton()
    {
        if (CanBuild())
        {
            if (GUILayout.Button("Build"))
            {
                _presenter.Build(new BehavioursBuilderPresenterData(_graphName, _doBuildProviders, _stateMachine));
            }
        }
        else
        {
            EditorGUILayout.LabelField("Can`t build, wrong settings!");
        }
    }

    private bool CanBuild()
    {
        if (_doBuildProviders)
        {
            return (!string.IsNullOrEmpty(_graphName)) && _stateMachine != null;
        }
        else
        {
            return !string.IsNullOrEmpty(_graphName);
        }
    }
}