using ModestTree;
using UnityEngine;

namespace Zenject
{
    public class ZenAutoInjecter : MonoBehaviour
    {
        [SerializeField]
        ContainerSources _containerSource = ContainerSources.SearchHierarchy;

        bool _hasInjected;

        public ContainerSources ContainerSource
        {
            get { return _containerSource; }
            set { _containerSource = value; }
        }

        // Make sure they don't cause injection to happen twice
        [Inject]
        public void Construct()
        {
            if (!_hasInjected)
            {
                Destroy(this);
            }
        }

        public void Awake()
        {
            _hasInjected = true;

            try
            {
                LookupContainer().InjectGameObject(gameObject);
            }
            catch { }
        }

        DiContainer LookupContainer()
        {
            if (_containerSource == ContainerSources.ProjectContext)
            {
                return ProjectContext.Instance.Container;
            }

            if (_containerSource == ContainerSources.SceneContext)
            {
                return GetContainerForCurrentScene();
            }

            Assert.IsEqual(_containerSource, ContainerSources.SearchHierarchy);

            var parentContext = transform.GetComponentInParent<Context>();

            if (parentContext != null)
            {
                return parentContext.Container;
            }

            return GetContainerForCurrentScene();
        }

        DiContainer GetContainerForCurrentScene()
        {
            return ProjectContext.Instance.Container.Resolve<SceneContextRegistry>()
                .GetContainerForScene(gameObject.scene);
        }

        public enum ContainerSources
        {
            SceneContext,
            ProjectContext,
            SearchHierarchy
        }
    }
}
