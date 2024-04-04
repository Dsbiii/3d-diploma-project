using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Stages
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private bool _isPribor;
        [SerializeField] private Act _act;
        [SerializeField] private string _takeInInventoryAction = "-положить в сумку";
        [SerializeField] private string _takeInPreviewAction = "Взять-осмотреть";
        [SerializeField] private string _replaceAction = "-заменить";

        [SerializeField] private string _name;
        [SerializeField] private string _idealAction;
        [SerializeField] private string _idealActionWithDeffect;
        [SerializeField] private bool _isUnpicable;
        [SerializeField] private int _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private ItemTypes _itemType;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] public bool isDemo = false;

        public string Action { get; private set; }
        private DatedItemService _datedItemService;
        private DeffectedItemService _deffectedItemService;
        private bool _isReplaced;
        private bool _isRightActions;
        private bool _isDeffectedorOverdue;

        public int CountAnimationPlayed { get; set; }

        public bool IsPribor => _isPribor;
        public bool IsDressed { get; private set; }
        public bool IsUnpicable => _isUnpicable;
        public ItemTypes ItemType => _itemType;
        public bool IsReplaced => _isReplaced;
        public bool IsDeffected
        {
            get
            {
                if (_deffectedItemService != null)
                    return _deffectedItemService.IsDeffected;
                return false;
            }
        }

        public bool IsDeffectedGloves
        {
            get
            {
                if (_deffectedItemService != null)
                    return _deffectedItemService.IsDeffectedGloves;
                return false;
            }
        }

        public bool IsOverdue
        {
            get
            {
                if(_datedItemService != null)
                    return _datedItemService.IsOverdue;
                return false;
            }
        }
        public Sprite Icon => _icon;
        public int ID => _id;
        public string IdealAction => _idealAction;
        public string Name => _name;
        public bool IsRightActions => _isRightActions;
        public string IdealActionWithDeffect
        {
            get
            {
                if (_isDeffectedorOverdue)
                {
                    return _idealActionWithDeffect;
                }
                else
                {
                    return _idealAction;
                }
            }
        }
        public bool IsWrongReplace { get; private set; } = false;

        private void Awake()
        {
            if (TryGetComponent(out DeffectedItemService deffectedItemService))
            {
                _deffectedItemService = deffectedItemService;
            }
            if (TryGetComponent(out DatedItemService datedItemService))
            {
                _datedItemService = datedItemService;
            }
        }

        public void TryDeffectItem()
        {
            Debug.Log("Deffect");
            if (_deffectedItemService != null)
                _deffectedItemService.DisplayDeffectsWithRandomValue();
        }

        private void Start()
        {
            if (_deffectedItemService != null && _deffectedItemService.IsDeffected)
            {
                _isDeffectedorOverdue = true;
                _idealAction = _idealActionWithDeffect;
            }
            if (_datedItemService != null && _datedItemService.IsOverdue)
            {
                _isDeffectedorOverdue = true;
                _idealAction = _idealActionWithDeffect;
            }
            if (ItemType == ItemTypes.CE602M)
            {
                _act.Instrumental._CE602MDate = _datedItemService.Date;
            }
            else if (ItemType == ItemTypes.Parma_VAF)
            {
                _act.Instrumental._ParmaDate = _datedItemService.Date;
            }
            else if (ItemType == ItemTypes.Clamp_Meters)
            {
                _act.Instrumental._ClampsDate = _datedItemService.Date;
            }
            else if (ItemType == ItemTypes.Stopwatch)
            {
                _act.Instrumental._TimerDate = _datedItemService.Date;
            }
        }

        public void Refresh(bool notifyToReplace = true)
        {
            bool isWasRightDate = false;
            bool isWasNotDeffected = false;
            if(_deffectedItemService != null)
            {
                if (!_deffectedItemService.IsDeffected)
                    isWasNotDeffected = true;
                _deffectedItemService.OffDeffects();
                if(!_isReplaced)
                    AddAction(_replaceAction);
                _isReplaced = true;
            }
            if(_datedItemService != null)
            {
                if (!_datedItemService.IsOverdue)
                    isWasRightDate = true;
                _datedItemService.Replace();
                if (!_isReplaced)
                    AddAction(_replaceAction);
                _isReplaced = true;
            }

            if (isWasRightDate && isWasNotDeffected)
            {
                //FindObjectOfType<ResultReplaceNotify>().NotiftWrongReplace();
                IsWrongReplace = true;
            }
            else
            {
                if(notifyToReplace)
                    FindObjectOfType<ResultReplaceNotify>().NotifyRightReplace();
            }
        }

        public void SetID(int id)
        {
            _id = id;
        }

        public void AddAction(string action)
        {
            Action += action;
        }

        public void SetAction(string action)
        {
            Action = action;
        }

        public void ResetAction()
        {
            Action = "";
        }

        public void DressItem()
        {
            IsDressed = true;
        }

        public virtual void ActionInPreview()
        {
        }

        public void TakeInInventory()
        {
            if (!_isReplaced && _isDeffectedorOverdue)
            {
                _isRightActions = false;
            }
            if (ItemType == ItemTypes.CE602M)
            {
                _act.Instrumental._CE602MDate = _datedItemService.Date;
            }
            else if (ItemType == ItemTypes.Parma_VAF)
            {
                _act.Instrumental._ParmaDate = _datedItemService.Date;
            }
            else if (ItemType == ItemTypes.Clamp_Meters)
            {
                _act.Instrumental._ClampsDate = _datedItemService.Date;
            }
            else if (ItemType == ItemTypes.Stopwatch)
            {
                _act.Instrumental._TimerDate = _datedItemService.Date;
            }
            AddAction(_takeInInventoryAction);
        }

        public void TakedInPreview()
        {
            AddAction(_takeInPreviewAction);
        }

        public void UseParticleSystem()
        {
            if (CountAnimationPlayed % 2 != 0)
            {
                CountAnimationPlayed++;
                return;
            }
            CountAnimationPlayed++;
            if (_particleSystem == null)
                return;
            if (IsDeffected && IsDeffectedGloves)
            {
                _particleSystem.gameObject.SetActive(true);
                _particleSystem.Play();
            }
        }

        public void EndedAnimation()
        {
            if (_particleSystem == null)
                return;

            _particleSystem.gameObject.SetActive(false);
        }

    }
}