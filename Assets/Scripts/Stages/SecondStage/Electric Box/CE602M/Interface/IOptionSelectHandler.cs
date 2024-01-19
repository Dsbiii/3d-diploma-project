using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box.CE602M.Interface
{
    public interface IOptionSelectHandler
    {
        void HandleSelect();
    }

    public interface IOptionsWindow
    {
        void Select();
        void Up();
        void Down();
    }
}