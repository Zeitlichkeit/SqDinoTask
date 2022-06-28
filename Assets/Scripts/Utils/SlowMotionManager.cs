using System.Collections;
using UnityEngine;

namespace Utils
{
    public class SlowMotionManager : MonoBehaviour
    {
        public static SlowMotionManager Instance => _instance;
        public float timeScale;

        [SerializeField] private float defaultTimeScale;
        [SerializeField] private float defaultFixedDeltaTime;

        private bool _slowMotionActive;
        private float _slowMotionTimer;
        private float _slowMotionTime;
        private Coroutine _slowMotionCoroutine;

        private static SlowMotionManager _instance;

        void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this);

                defaultTimeScale = Time.timeScale;
                defaultFixedDeltaTime = Time.fixedDeltaTime;
            }
        }

        public void DefaultScale()
        {
            if (_slowMotionCoroutine != null)
            {
                _slowMotionActive = false;
            }

            Time.timeScale = defaultTimeScale;
            Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
        }
        
        public void ActivateSlowMotionFor(float seconds = 0.0f)
        {
            _slowMotionTimer = 0.0f;
            _slowMotionTime = seconds;
            
            if (!_slowMotionActive)
            {
                _slowMotionCoroutine = StartCoroutine(SlowMotionCoroutine());
            }
        }

        private IEnumerator SlowMotionCoroutine()
        {
            _slowMotionActive = true;
            Time.timeScale = timeScale;
            Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
            while (_slowMotionTimer < _slowMotionTime && _slowMotionActive)
            {
                _slowMotionTimer += Time.deltaTime;
                yield return null;
            }

            Time.timeScale = defaultTimeScale;
            Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
            _slowMotionActive = false;
        }
    }
}