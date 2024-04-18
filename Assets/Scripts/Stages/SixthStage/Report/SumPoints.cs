using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public int Point1;
        public int Point2;
        public int Point3;
        public int Point4;
        public int Point5;
        public int Point6;
        public int Allpoints;
        public bool CriticalError;
        public List<string> ReportPoint1;
        public List<string> ReportPoint2;
        public List<string> ReportPoint3;
        public List<string> ReportPoint4;
        public List<string> ReportPoint5;
        public List<string> ReportPoint6;
        public List<string> ReportPoint1Current;
        public List<string> ReportPoint2Current;
        public List<string> ReportPoint3Current;
        public List<string> ReportPoint4Current;
        public List<string> ReportPoint5Current;
        public List<string> ReportPoint6Current;

        public bool IsHaveIndications = false;

        public void SumAllPoints()
        {
            int Point = SumPoint1() + SumPoint2() + SumPoint3() + SumPoint4() + SumPoint5() + SumPoint6();
            bool Point4IsTrue = true;
            for (int i = 0; i < 3 ; i++)
            {
                if (ReportPoint4Current[i] == "Неправильно")
                {
                    Point4IsTrue = false;
                    break;
                }
            }
            if (Point4IsTrue && SumPoint2() == 12 && SumPoint1() == 8 && SumPoint6() == 6)
            {
                IsHaveIndications = true;
                _checkNetworkHierarchy.ClickUpdate();
            }
        }
        public int SumPoint1()
        {
            try
            {
                ReportPoint1Current.Clear();
                int point1 = _point_1.GetPoints();
                Debug.Log(point1 + " point1" + string.Join(" ", _point_1.ReportInfo));
                
                if (point1 == 0)
                    CriticalError = true;
                Debug.Log("_point_1.ReportInfo.Length" + _point_1.ReportInfo.Length);
                Debug.Log("ReportPoint1" + ReportPoint1.Count);
                if(ReportPoint1.Count < 1)
                {
                    Point1 = point1;
                    for (int i = 0; i < _point_1.ReportInfo.Length; i++)
                    {
                        if (_point_1.ReportInfo[i] == 0)
                        {
                            ReportPoint1.Add("Неправильно");
                        }
                        else
                        {
                            ReportPoint1.Add("Правильно");
                        }

                    }
                }
                for(int i = 0; i < _point_1.ReportInfo.Length; i++)
                {
                    if (_point_1.ReportInfo[i] == 0)
                    {
                        ReportPoint1Current.Add("Неправильно");
                    }
                    else
                    {
                        ReportPoint1Current.Add("Правильно");
                    }

                }
                return point1;
            }
            catch
            {
                Debug.Log("Check");
                CriticalError = true;
                return 0;
            }
        }
        public int SumPoint2()
        {
            try
            {
                ReportPoint2Current.Clear();
                int point2 = _point2_1.GetPoints() + _point2_2.GetPoints() + _point2_3.GetPoints2();
                Debug.Log("Point2 " + point2 + " = " + _point2_1.GetPoints() + " " + _point2_2.GetPoints() + " " + _point2_3.GetPoints2());
                if(ReportPoint2.Count < 1)
                {
                    Point2 = point2;
                    for (int i = 0; i < _point2_1.Report.Length; i++)
                    {
                        if (_point2_1.Report[i] == 1)
                            ReportPoint2.Add("Правильно");
                        else
                            ReportPoint2.Add("Неправильно");
                    }
                    for (int i = 0; i < _point2_2.Report.Length; i++)
                    {
                        if (_point2_2.Report[i] == 1)
                            ReportPoint2.Add("Правильно");
                        else
                            ReportPoint2.Add("Неправильно");
                        Debug.Log(ReportPoint2[i + 8] + " " + _point2_2.Report[i]);
                    }
                    for (int i = 0; i < _point2_3.Report.Length; i++)
                    {
                        if (_point2_3.Report[i] == 1)
                            ReportPoint2.Add("Правильно");
                        else
                            ReportPoint2.Add("Неправильно");
                        Debug.Log(ReportPoint2[i + 10] + " " + _point2_3.Report[i]);
                    }
                }
                for (int i = 0; i < _point2_1.Report.Length; i++)
                {
                    if (_point2_1.Report[i] == 1)
                        ReportPoint2Current.Add("Правильно");
                    else
                        ReportPoint2Current.Add("Неправильно");
                }
                for (int i = 0; i < _point2_2.Report.Length; i++)
                {
                    if (_point2_2.Report[i] == 1)
                        ReportPoint2Current.Add("Правильно");
                    else
                        ReportPoint2Current.Add("Неправильно");
                    Debug.Log(ReportPoint2[i + 8] + " " + _point2_2.Report[i]);
                }
                for (int i = 0; i < _point2_3.Report.Length; i++)
                {
                    if (_point2_3.Report[i] == 1)
                        ReportPoint2Current.Add("Правильно");
                    else
                        ReportPoint2Current.Add("Неправильно");
                    Debug.Log(ReportPoint2[i + 10] + " " + _point2_3.Report[i]);
                }
                Debug.Log(string.Join(", ", ReportPoint2));
                return point2;
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
                ReportPoint3.Clear();
                int point3 = _point3_1.GetPoints() + _point3_2.GetPoints3();
                Debug.Log("Point3 " + point3 + " = " + _point3_1.GetPoints() + " " + _point3_2.GetPoints3());
                if(ReportPoint3.Count < 1)
                {
                    Point3 = point3;
                    for (int i = 0; i < _point3_1.Report.Length; i++)
                    {
                        if (_point3_1.Report[i] == 1)
                            ReportPoint3.Add("Правильно");
                        else
                            ReportPoint3.Add("Неправильно");
                    }
                    for (int i = 0; i < _point3_2.ReportSchedule.Length; i++)
                    {
                        if (_point3_2.ReportSchedule[i] == 1)
                            ReportPoint3.Add("Правильно");
                        else
                            ReportPoint3.Add("Неправильно");
                    }
                }
                for (int i = 0; i < _point3_1.Report.Length; i++)
                {
                    if (_point3_1.Report[i] == 1)
                        ReportPoint3Current.Add("Правильно");
                    else
                        ReportPoint3Current.Add("Неправильно");
                }
                for (int i = 0; i < _point3_2.ReportSchedule.Length; i++)
                {
                    if (_point3_2.ReportSchedule[i] == 1)
                        ReportPoint3Current.Add("Правильно");
                    else
                        ReportPoint3Current.Add("Неправильно");
                }
                Debug.Log(string.Join(",", ReportPoint3));
                return point3;
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
                ReportPoint4Current.Clear();
                int point4 = _point4_1.GetPoints() + _point4_2.Points();
                Debug.Log("Point4 " + point4 + " = " + _point4_1.GetPoints() + " " + _point4_2.Points());
                if(ReportPoint4.Count < 1) 
                {
                    Point4 = point4;
                    for (int i = 0; i < _point4_1.Report.Length; i++)
                    {
                        if (_point4_1.Report[i] == 1)
                        {
                            ReportPoint4.Add("Правильно");
                        }
                        else
                        {
                            ReportPoint4.Add("Неправильно");
                        }
                    }
                    for (int i = 0; i < _point4_2.Report.Length; i++)
                    {
                        if (_point4_2.Report[i] == 1)
                        {
                            ReportPoint4.Add("Правильно");
                        }
                        else
                        {
                            ReportPoint4.Add("Неправильно");
                        }
                    }
                }
                for (int i = 0; i < _point4_1.Report.Length; i++)
                {
                    if (_point4_1.Report[i] == 1)
                    {
                        ReportPoint4Current.Add("Правильно");
                    }
                    else
                    {
                        ReportPoint4Current.Add("Неправильно");
                    }
                }
                for (int i = 0; i < _point4_2.Report.Length; i++)
                {
                    if (_point4_2.Report[i] == 1)
                    {
                        ReportPoint4Current.Add("Правильно");
                    }
                    else
                    {
                        ReportPoint4Current.Add("Неправильно");
                    }
                }
                Debug.Log(string.Join(",", ReportPoint4));
                return point4;
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
                ReportPoint5Current.Clear();
                int point5 = _point5_1.GetPoints();
                Debug.Log("Point5 " + point5);
                if(ReportPoint5.Count < 1)
                {
                    Point5 = point5;
                    for (int i = 0; i < _point5_1.Report.Length; i++)
                    {
                        if (_point5_1.Report[i] == 1)
                            ReportPoint5.Add("Правильно");
                        else
                            ReportPoint5.Add("Неправильно");
                    }
                }
                for(int i = 0; i < _point5_1.Report.Length; i++)
                {
                    if (_point5_1.Report[i] == 1)
                        ReportPoint5Current.Add("Правильно");
                    else
                        ReportPoint5Current.Add("Неправильно");
                }
                Debug.Log(string.Join(",", ReportPoint5));
                return point5;
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
                ReportPoint6Current.Clear();
                int point6 = _point_6.Points();
                Debug.Log("Point6 " + point6);
                if(ReportPoint6.Count < 1)
                {
                    Point6 = point6;
                    for (int i = 0; i < _point_6.Report.Length; i++)
                    {
                        if (_point_6.Report[i] == 1)
                            ReportPoint6.Add("Правильно");
                        else
                            ReportPoint6.Add("Неправильно");
                    }
                }
                for (int i = 0; i < _point_6.Report.Length; i++)
                {
                    if (_point_6.Report[i] == 1)
                        ReportPoint6Current.Add("Правильно");
                    else
                        ReportPoint6Current.Add("Неправильно");
                }
                return point6;
            }
            catch
            {
                Debug.Log("Cath");
                return 0;
            }
        }
    }
}