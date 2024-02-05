using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SixthStage.Report
{
    public class SumPoints : MonoBehaviour
    {
        [SerializeField] private RouteEquimentPanel _point_1;
        [SerializeField] private ObjectService _point2_1;
        [SerializeField] private MEteringDevicesService _point2_2;
        [SerializeField] private AccountingPointService _point2_3;
        [SerializeField] private DataCollectorService _point3_1;
        [SerializeField] private AccountingPointService _point3_2;
        [SerializeField] private AbonentPanel _point4_1;
        [SerializeField] private Pasport _point4_2;
        [SerializeField] private UsersAndRolesPanel _point5_1;
        [SerializeField] private DataCheck _point_6;
        private int _point1;
        private int _point2;
        private int _point3;
        private int _point4;
        private int _point5;
        private int _point6;
        public void SumAllPoints()
        {
            int Points = SumPoint1() + SumPoint2() + SumPoint3() + SumPoint4() + SumPoint5() + SumPoint6();
        }
        public int SumPoint1()
        {
            try
            {
                _point1 = _point_1.GetPoints();
                return _point1;
            }
            catch
            {
                return 0;
            }
        }
        public int SumPoint2()
        {
            try
            {
                _point2 = _point2_1.GetPoints() + _point2_2.GetPoints() + _point2_3.GetPoints2();
                return _point2;
            }
            catch
            {
                return 0;
            }
        }
        public int SumPoint3()
        {
            try
            {
                _point3 = _point3_1.GetPoints() + _point3_2.GetPoints3();
                return _point3;
            }
            catch
            {
                return 0;
            }
        }
        public int SumPoint4()
        {
            try
            {
                _point4 = _point4_1.GetPoints() + _point4_2.Points();
                return _point4;
            }
            catch
            {
                return 0;
            }
        }
        public int SumPoint5()
        {
            try
            {
                _point5 = _point5_1.GetPoints();
                return _point5;
            }
            catch
            {
                return 0;
            }
        }
        public int SumPoint6()
        {
            try
            {
                _point6 = _point_6.Points();
                return _point6;
            }
            catch
            {
                return 0;
            }
        }
    }
}