using System.Collections.Generic;
using UnityEngine;

namespace Runtime.SecgScripts
{
    /// <summary>
    /// Screen edge collider generator
    /// </summary>
    public class Secg : MonoBehaviour
    {
        [SerializeField] private EdgeCollider2D _topCollider;
        [SerializeField] private EdgeCollider2D _bottomCollider;

        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            GenerateEdges();   
        }

        private void GenerateEdges()
        { 
            Vector2 leftBottom = _camera.ScreenToWorldPoint(new Vector2(0, 0));
            Vector2 rightBottom = _camera.ScreenToWorldPoint(new Vector2(Screen.width, 0));
            Vector2 leftTop = _camera.ScreenToWorldPoint(new Vector2(0, Screen.height));
            Vector2 rightTop = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            
            var topEdgePoints = new List<Vector2> { leftTop, rightTop };
            var bottomEdgePoints = new List<Vector2> { leftBottom, rightBottom };
            
            _topCollider.SetPoints(topEdgePoints);
            _bottomCollider.SetPoints(bottomEdgePoints);
        }
    }
}