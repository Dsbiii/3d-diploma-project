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
        [SerializeField] private CheckNetworkHierarchy _checkNetworkHierarchy;
        private int _point1;
        private int _point2;
        private int _point3;
        private int _point4;
        private int _point5;
        private int _point6;
        public int Allpoints;
        public bool CriticalError;
        public void SumAllPoints()
        {
            int Points = SumPoint1() + SumPoint2() + SumPoint3() + SumPoint4() + SumPoint5() + SumPoint6();
            if(Points > 30)
                _checkNetworkHierarchy.ClickUpdate();
        }
        public int SumPoint1()
        {
            try
            {
                _point1 = _point_1.GetPoints();
                if(_point1 == 0)
                    CriticalError = true;
                return _point1;
            }
            catch
            {
                CriticalError = true;
                return 0;
            }
        }
        public int SumPoint2()
        {
            try
            {
                _point2 = _point2_1.GetPoints() + _point2_2.GetPoints() + _point2_3.GetPoints2();
                Debug.Log("Point2 " + _point2 + " = " + _point2_1.GetPoints() + " " + _point2_2.GetPoints() + " " + _point2_3.GetPoints2());
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
                Debug.Log("Point3 " + _point3 + " = " + _point3_1.GetPoints() + " " + _point3_2.GetPoints3());
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
                Debug.Log("Point4 " + _point4 + " = " + _point4_1.GetPoints() + " " + _point4_2.Points());
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
                Debug.Log("Point5 " + _point5);
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
                Debug.Log("Point6 " + _point6);
                return _point6;
            }
            catch
            {
                Debug.Log("Cath");
                return 0;
            }
        }
    }
}