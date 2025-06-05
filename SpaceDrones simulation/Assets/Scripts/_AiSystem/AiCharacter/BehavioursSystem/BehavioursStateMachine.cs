using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Zenject;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class BehavioursStateMachine : MonoBehaviour, ICharacterActiveSetteble
{
    [SerializeField] private BehaviourBaseGeneric[] _behaviours;
    [SerializeField] private int _defaultBehaviourIndex;

    private BehaviourBaseGeneric _currentBehaviour;

    private Dictionary<BehaviourSettingsBase, int> _associatedBehabiours = new();

    public string CurrentBehaviourName => _currentBehaviour.GetType().Name;

    public event Action behaviourChanged;

#if UNITY_EDITOR
    public BehaviourBaseGeneric[] Behaviours
    {
        get
        {
            if (Application.isPlaying)
            {
                throw new Exception("Can`t get behaviours at runtime");
            }

            return _behaviours;
        }
    }
#endif

    private void Awake()
    {
        for (int i = 0; i < _behaviours.Length; i++)
        {
            _associatedBehabiours.Add(_behaviours[i].GetSettings(), i);
        }

        ApplyBehaviour(_defaultBehaviourIndex);
    }

    private void FixedUpdate()
    {
        if (_currentBehaviour.IsCanEnter() == false)
        {
            BehaviourSettingsBase[] connectedBehaviours = _currentBehaviour.GetSettings().ConnectedBehaviours;

            if (connectedBehaviours == null || connectedBehaviours.Length == 0)
            {
                ApplyBehaviour(_defaultBehaviourIndex);
            }
            else
            {
                bool entered = false;

                foreach (BehaviourSettingsBase connectedBehaviour in connectedBehaviours)
                {
                    if (Change(_associatedBehabiours[connectedBehaviour]))
                    {
                        entered = true;
                        break;
                    }
                }

                if (!entered)
                {
                    ApplyBehaviour(_defaultBehaviourIndex);
                }
            }
        }

        _currentBehaviour.FixedTick();
    }

    private void Update()
    {
        _currentBehaviour.Tick();
    }

    private bool Change(int index)
    {
        if (_behaviours[index].IsCanEnter() == false)
        {
            return false;
        }

        ApplyBehaviour(index);

        return true;
    }

    private void ApplyBehaviour(int index)
    {
        _currentBehaviour?.Exit();

        _currentBehaviour = _behaviours[index];
        behaviourChanged?.Invoke();

        _currentBehaviour.Enter();
    }

    public void SetActive(bool state)
    {
        if (state == false)
        {
            Change(_defaultBehaviourIndex);
        }

        enabled = state;
    }

#if UNITY_EDITOR
    public void SetUpBehaviours(BehaviourBaseGeneric[] behaviours)
    {
        if (Application.isPlaying)
        {
            throw new Exception("Can`t set behaviours at runtime");
        }

        EditorUtility.SetDirty(this);
        Undo.RecordObject(this, $"Behaviours changed in: {gameObject}");
    
        _behaviours = behaviours;
        _defaultBehaviourIndex = _behaviours.Select(behaviour => behaviour.GetType()).ToList().IndexOf(typeof(IdleBehaviour));
    }
#endif
}