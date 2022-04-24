using System;
using System.Collections;
using UnityEngine;

namespace maross3
{
    public class TrafficLight : MonoBehaviour
    {
        // corridors are lights that share the same path
        public int corridorNumber;
        public SignalState startingState;

        private Light _redLight;
        private Light _amberLight;
        private Light _greenLight;

        void Awake()
        {
            PopulateLights();
        }
        
        public void ChangeState(Sequence seq)
        {

            switch (seq)
            {
                case Sequence.Straight:
                {
                    StartCoroutine(SlowToStop(SignalState.Green));
                    break;
                }
                case Sequence.Left:
                {
                    // unimplemented
                    StartCoroutine(SlowToStop(SignalState.Green));
                    break;
                }
                case Sequence.Stop:
                {
                    StartCoroutine(SlowToStop(SignalState.Red));
                    break;
                }
            }
        }

        private void IlluminateLight(SignalState sigState)
        {
            _greenLight.enabled = false;
            _amberLight.enabled = false;
            _redLight.enabled = false;

            switch (sigState)
            {
                case SignalState.Red:
                {
                    _redLight.enabled = true;
                    break;
                }
                case SignalState.Amber:
                {
                    _amberLight.enabled = true;
                    break;
                }
                case SignalState.Green:
                {
                    _greenLight.enabled = true;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(sigState), sigState, "Signal state does not exist");
            }
        }
        
        private void PopulateLights()
        {
            var lights = GetComponentsInChildren<Light>();
            foreach (var trafficLight in lights)
            {
                switch (trafficLight.gameObject.name)
                {
                    case "Red":
                    {
                        _redLight = trafficLight;
                        break;
                    }
                    case "Amber":
                    {
                        _amberLight = trafficLight;
                        break;
                    }
                    case "Green":
                    {
                        _greenLight = trafficLight;
                        break;
                    }
                }
            }
        }

        // TODO refactor/define wait variables. Calculate better, implement Sequence.Left
        private IEnumerator SlowToStop(SignalState sigState)
        {
            if (sigState == SignalState.Red) IlluminateLight(SignalState.Amber);
            yield return new WaitForSeconds(1.5f); // amber delay needs to be 1 / 10 mph

            if (sigState == SignalState.Red)
                IlluminateLight(sigState);

            yield return new WaitForSeconds(0.3f);
            IlluminateLight(sigState);
        }
    }
}