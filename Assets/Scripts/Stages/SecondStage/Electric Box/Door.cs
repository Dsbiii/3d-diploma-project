using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages.SecondStage.Electric_Box
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Animator _door;

        public void Open()
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            _door.SetBool("Open", true);
            _door.enabled = true;
        }

        public void Close()
        {
            _door.enabled = false;
            _door.SetBool("Open", false);
            transform.rotation = new Quaternion(0,0,0,0);
        }
    }
}