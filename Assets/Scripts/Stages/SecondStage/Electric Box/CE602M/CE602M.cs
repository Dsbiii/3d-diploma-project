using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M
{
    public class CE602M : MonoBehaviour
    {
        [SerializeField] private MagnitePoint[] _magnitePoints;
        [SerializeField] private CablePort[] _cableComponents;


        public void OffActivePoints()
        {
            foreach(var item in _magnitePoints)
            {
                item.UnmagniteMovebleObject();
            }
        }

        public void DisplayIndicatorsOnMagnitePoints(MagnitePointsTypes magnitePointsTypes)
        {
            foreach(var magnitePoint in _magnitePoints)
            {
                foreach(var point in magnitePoint.MagnitePointsTypes)
                {
                    if (point.MagnitePointsTypes == magnitePointsTypes && !magnitePoint.IsMagnited)
                    {
                        magnitePoint.DisplayIndicate();
                    }
                }
            }
        }

        public void HideIndicatorsOnMagnitePoints()
        {
            foreach (var magnitePoint in _magnitePoints)
            {
                magnitePoint.HideIndicate();
            }
        }

        public bool TryConnect(MagnitePoint magnitePoint,MagnitePointsTypes magnitePointsTypes)
        {
            foreach(var port in _cableComponents)
            {
                foreach(var point in magnitePoint.MagnitePointsTypes)
                {
                    if (magnitePointsTypes == port.MagnitePointsTypes && point.MagnitePointsTypes == port.MagnitePointsTypes && port.IsEmpty)
                    {
                        if(magnitePointsTypes == MagnitePointsTypes.Pliers)
                        {
                            if (magnitePoint.TryMagnite())
                            {
                                if (magnitePoint.MagnitePointsTypes.Length > 1)
                                {
                                    magnitePoint.SetCablePort(port);
                                    port.Connect(point.Object.transform);
                                    magnitePoint.Magnited(magnitePointsTypes, port.MagnitePointsTypes);
                                }
                                else
                                {
                                    magnitePoint.SetCablePort(port);
                                    port.Connect(magnitePoint.PointObject.transform);
                                    magnitePoint.Magnited(port.MagnitePointsColor);
                                }
                                return true;
                            }
                        }
                        else
                        {
                            if (magnitePoint.MagnitePointsTypes.Length > 1)
                            {
                                magnitePoint.SetCablePort(port);
                                port.Connect(point.Object.transform);
                                magnitePoint.Magnited(magnitePointsTypes, port.MagnitePointsTypes);
                            }
                            else
                            {
                                magnitePoint.SetCablePort(port);
                                port.Connect(magnitePoint.PointObject.transform);
                                magnitePoint.Magnited(port.MagnitePointsColor);
                            }
                            return true;
                        }
  
                    }
                }
            }
            return false;
        }

    }
}