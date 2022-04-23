using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Cover
{
    public enum CoverPointType{
        LeftLean,
        RightLean,
        NormalLean,
        None
    }
    public sealed class CoverInitSystem : IEcsInitSystem
    {
        private static Dictionary<Transform, string> _freePoints = new Dictionary<Transform, string>();
        private static Dictionary<Transform, string> _occupiedPoints = new Dictionary<Transform, string>();
        
        private EcsFilter<CoverPointModel> _coverFilter;
        public void Init()
        {
            foreach (var i in _coverFilter)
            {
                ref var point = ref _coverFilter.Get1(i);
                _freePoints.Add(point.Point,point.Tag);
            }
        }

        private static void SwapItem(Dictionary<Transform, string> dic1, Dictionary<Transform, string> dic2, Transform t)
        {
            if (!dic1.ContainsKey(t))
            {
                return;
            }

            var value = dic1[t];
            dic2.Add(t,value);
            dic1.Remove(t);
        }
        
        public static void BookCoverPoint(Transform t)
        {
            SwapItem(_freePoints,_occupiedPoints,t);
        }

        public static void GiveUpPoint(Transform t)
        {
            SwapItem(_occupiedPoints,_freePoints,t);
        }

        public static bool IsPointUnOccupied(Transform t)
        {
            return !_occupiedPoints.ContainsKey(t);
        }

        /// <summary>
        /// Must be casted after Book and GiveUP functions
        /// </summary>
        public static CoverPointType GetTypeOfPointCover(Transform t)
        {
            var val = _occupiedPoints[t];
            return val switch
            {
                "CoverPoint" => CoverPointType.NormalLean,
                "CoverPointLeft" => CoverPointType.LeftLean,
                "CoverPointRight" => CoverPointType.RightLean, 
                _ => CoverPointType.None
            };
        }
    }
}