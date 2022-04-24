using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace maross3
{
    public class FourWayIntersection : MonoBehaviour
    {
        [SerializeField] private int sequenceTimer;
        [SerializeField] private TrafficLight[] signalArray;
        
        private Dictionary<int, Sequence> _corridorMap;
        private WaitForSeconds _sequenceInterval;
        
        private void Start()
        {
            _corridorMap = new Dictionary<int, Sequence>();
            _sequenceInterval = new WaitForSeconds(sequenceTimer);
            PopulateCorridorMap();

            StartCoroutine(OperateIntersection());
        }

        void ChangeSequence()
        {
            UpdateCorridorMap();

            foreach (var light in signalArray)
                light.ChangeState(_corridorMap[light.corridorNumber]);
        }
        
        private IEnumerator OperateIntersection()
        {
            // needs an exit condition
            while (true)
            {
                ChangeSequence();
                yield return _sequenceInterval;
            }
        }
        
        private void PopulateCorridorMap()
        {
            foreach (var signal in signalArray)
            {
                if (_corridorMap.ContainsKey(signal.corridorNumber)) continue;
                
                // to switch statement when implementing Sequence.Left
                if (signal.startingState == SignalState.Red)
                    _corridorMap.Add(signal.corridorNumber, Sequence.Stop);
                else if (signal.startingState == SignalState.Green)
                    _corridorMap.Add(signal.corridorNumber, Sequence.Straight);
            }
        }

        // TODO refactor this, add left sequence before straight sequence
        private void UpdateCorridorMap()
        {
            var temp = new Dictionary<int, Sequence>();
            
            foreach (var corridor in _corridorMap)
            {
                if (corridor.Value == Sequence.Straight) temp[corridor.Key] = Sequence.Stop;
                else temp[corridor.Key] = Sequence.Straight;
            }
            _corridorMap = temp;
        }
    }
}