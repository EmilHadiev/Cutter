using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace DynamicMeshCutter
{
    public class CustomMouseBehaviour : CutterBehaviour, ICutMouseBehaviour
    {
        private LineRenderer _lineRenderer;
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
            _lineRenderer = GetComponent<LineRenderer>();
            _mainCamera = Camera.main;
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetMouseButtonDown(0))
            {
                StartCut();
            }

            if (_isDragging)
            {
                _to = GetMouseWorldPosition();
                VisualizeLine(true);
            }
            else
            {
                VisualizeLine(false);
            }

            if (Input.GetMouseButtonUp(0) && _isDragging)
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

        private void VisualizeLine(bool value)
        {
            if (_lineRenderer == null) return;

            _lineRenderer.enabled = value;

            if (value)
            {
                _lineRenderer.positionCount = 2;
                _lineRenderer.SetPosition(0, _from);
                _lineRenderer.SetPosition(1, _to);
            }
        }

        public void SetLineColor(Color color)
        {
            _lineRenderer.endColor = color;
            _lineRenderer.startColor = new Color(1.0f - color.r, 1.0f - color.g, 1.0f - color.b);
        }
    }
}