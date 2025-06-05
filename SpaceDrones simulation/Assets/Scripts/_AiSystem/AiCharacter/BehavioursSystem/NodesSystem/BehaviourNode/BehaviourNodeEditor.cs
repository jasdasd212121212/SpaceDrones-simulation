using UnityEditor;
using UnityEngine;

public class BehaviourNodeEditor : NodeEditorBase
{
    private int _weigth;
    private bool _isFoldedOut;
    private Vector2 _scroll;

    private BehaviourNode _node;

    private BehaviourSettingsBase _behaviour;
    private AiPredicateBase[] _predicates;

    public BehaviourNodeEditor(NodeBase node) : base(node)
    {
        _node = node as BehaviourNode;

        _behaviour = _node.Behaviour;
        _predicates = _node.Predicates;
        _weigth = _node.Weigth;
    }

    protected override Vector2 TrueSize => new Vector2(150, 380);

#if UNITY_EDITOR
    protected override void OnDraw()
    {
        GUIStyle textStyle = new GUIStyle(GUI.skin.label);
        GUIStyle fieldStyle = new GUIStyle(GUI.skin.textField);

        textStyle.fontSize = (int)(12 * ZoomScale);

        fieldStyle.fontSize = (int)(16 * ZoomScale);
        fieldStyle.fixedHeight = 20 * ZoomScale;

        EditorStyles.objectField.fixedHeight = 20 * ZoomScale;

        EditorGUILayout.Space(25 * ZoomScale);
        _behaviour = EditorGUILayout.ObjectField(_behaviour, typeof(BehaviourSettingsBase)) as BehaviourSettingsBase;

        EditorGUILayout.Space(5 * ZoomScale);

        EditorGUILayout.LabelField("Weigth", textStyle);
        _weigth = EditorGUILayout.IntField(_weigth, fieldStyle);
        _weigth = Mathf.Clamp(_weigth, 0, int.MaxValue);

        EditorGUILayout.Space(5 * ZoomScale);

        if (_behaviour != null && _behaviour is PredicatedBehaviourSettingsBase)
        {
            EditorGUILayout.Space(5 * ZoomScale);

            _predicates = DrawArray("Predicates", ref _isFoldedOut, _predicates);
        }

        if (_weigth != _node.Weigth)
        {
            _node.SetWeigth(_weigth);
        }

        if (_behaviour != _node.Behaviour)
        {
            _node.SetBehaviour(_behaviour);
        }

        if (_predicates != _node.Predicates)
        {
            _node.SetPredicates(_predicates);
        }
    }

    private AiPredicateBase[] DrawArray(string label, ref bool isOpen, AiPredicateBase[] array)
    {
        GUIStyle foldout = new GUIStyle(GUI.skin.label);
        GUIStyle intField = new GUIStyle(GUI.skin.textField);
        GUIStyle button = new GUIStyle(GUI.skin.button);

        foldout.fontSize = (int)(11 * ZoomScale);

        intField.fixedHeight = 15 * ZoomScale;
        intField.fontSize = (int)(11 * ZoomScale);  

        button.fixedHeight = 15 * ZoomScale;
        button.fontSize = (int)(11 * ZoomScale);

        isOpen = EditorGUILayout.Foldout(isOpen, label);

        if (array == null)
        {
            return new AiPredicateBase[0];
        }

        int newSize = array.Length;

        if (isOpen)
        {
            newSize = newSize < 0 ? 0 : newSize;

            EditorGUILayout.Space(25 * ZoomScale);

            if (newSize != array.Length)
            {
                array = ResizeArray(array, newSize);
            }

            EditorStyles.objectField.fixedHeight = 15 * ZoomScale;

            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            for (var i = 0; i < newSize; i++)
            {
                array[i] = EditorGUILayout.ObjectField(array[i], typeof(AiPredicateBase)) as AiPredicateBase;
            }

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Add", button))
            {
                array = ResizeArray(array, array.Length + 1);
            }

            if (GUILayout.Button("Remove", button))
            {
                array = ResizeArray(array, array.Length - 1);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();
        }
        return array;
    }

    private static T[] ResizeArray<T>(T[] array, int size)
    {
        T[] newArray = new T[size];

        for (var i = 0; i < size; i++)
        {
            if (i < array.Length)
            {
                newArray[i] = array[i];
            }
        }

        return newArray;
    }
#endif
}