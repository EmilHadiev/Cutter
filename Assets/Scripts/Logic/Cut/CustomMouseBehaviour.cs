using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace DynamicMeshCutter
{
    [RequireComponent(typeof(LineRenderer))]
    public class CustomMouseBehaviour : CutterBehaviour, ICutMouseBehaviour
    {
        private const int LeftMouseButton = 0;

        private CutLineView _lineView;
        private Vector3 _from;
        private Vector3 _to;
        private bool _isDragging;
        private Camera _mainCamera;
        private ICutPartContainer _cutPartContainer;
        private IEnergy _playerEnergy;

        public event Action CutStarted;
        public event Action CutEnded;

        private bool _isCanCut;

        protected override void Awake()
        {
            base.Awake();
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            _mainCamera = Camera.main;
            _lineView = new CutLineView(lineRenderer);
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetMouseButtonDown(LeftMouseButton))
            {
                StartCut();
            }

            if (_isDragging)
            {
                _to = GetMouseWorldPosition();
                _lineView.UpdateEndMousePosition(_to);
                _lineView.VisualizeLine(true);
            }
            else
            {
                _lineView.VisualizeLine(false);
            }

            if (Input.GetMouseButtonUp(LeftMouseButton) && _isDragging)
            {
                EndCut();
            }
        }

        private void StartCut()
        {
            if (_playerEnergy.TrySpendEnergy() == false)
            {
                _isCanCut = false;
                return;
            }

            _isCanCut = true;

            _isDragging = true;
            CutStarted?.Invoke();
            _from = GetMouseWorldPosition();
            _lineView.SetStartLinePosition(GetMouseWorldPosition());
        }

        private void EndCut()
        {
            if (_isCanCut == false)
                return;

            _isDragging = false;
            Cut();
            CutEnded?.Invoke();
        }

        [Inject]
        private void Constructor(ICutPartContainer cutPartContainer, IPlayer player)
        {
            _cutPartContainer = cutPartContainer;
            _playerEnergy = player.Energy;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = _mainCamera.nearClipPlane + 0.05f;
            return _mainCamera.ScreenToWorldPoint(mousePos);
        }

        private void Cut()
        {
            Plane plane = new Plane(_from, _to, _mainCamera.transform.position);
            Vector3 planeNormal = plane.normal;

            // Оптимизация: кэшируем корневые объекты
            GameObject[] roots = SceneManager.GetActiveScene().GetRootGameObjects();
            int rootCount = roots.Length;

            for (int i = 0; i < rootCount; i++)
            {
                GameObject root = roots[i];

                if (root.activeInHierarchy == false)
                    continue;

                // Используем GetComponentsInChildren с параметром для избежания аллокаций
                MeshTarget[] targets = root.GetComponentsInChildren<MeshTarget>(false);

                for (int j = 0; j < targets.Length; j++)
                {
                    Cut(targets[j], _to, planeNormal, null, OnCreated);
                }
            }
        }

        private void OnCreated(Info info, MeshCreationData cData)
        {
            foreach (var createdObject in cData.CreatedObjects)
            {
                _cutPartContainer.Add(createdObject);
            }

            MeshCreation.TranslateCreatedObjects(info, cData.CreatedObjects, cData.CreatedTargets, Separation);
        }

        public void SetLineColor(Color color) => _lineView.SetColor(color);
    }
}