using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace DynamicMeshCutter
{
    public class CustomMouseBehaviour : CutterBehaviour
    {
        private LineRenderer _lineRenderer;
        private Vector3 _from;
        private Vector3 _to;
        private bool _isDragging;
        private Camera _mainCamera;
        private ICutPartContainer _cutPartContainer;
        private static readonly Plane[] _tempPlane = new Plane[1];

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
                _isDragging = true;
                _from = GetMouseWorldPosition();
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
                Cut();
                _isDragging = false;
            }
        }

        [Inject]
        private void Constructor(ICutPartContainer cutPartContainer)
        {
            _cutPartContainer = cutPartContainer;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = _mainCamera.nearClipPlane + 0.05f;
            return _mainCamera.ScreenToWorldPoint(mousePos);
        }
        private void Cut()
        {
            _tempPlane[0] = new Plane(_from, _to, _mainCamera.transform.position);
            Vector3 planeNormal = _tempPlane[0].normal;

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
                Debug.Log($"Created new part: {createdObject.name}");
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
    }
}
