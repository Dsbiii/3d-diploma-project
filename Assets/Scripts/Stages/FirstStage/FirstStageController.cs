using Assets.Scripts.Menu.Panels;
using Assets.Scripts.Stages.FirstStage.Panels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Stages.FirstStage
{
    public class FirstStageController : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private List<Item> _obligatoryItems = new List<Item>();
        [SerializeField] private List<string> _sizInInventory;
        [SerializeField] private SelectedInventoryItemsObject _selectedInventoryItemsObject;
        [SerializeField] private ItemService _itemService;

        private FirstStageItemPicker _itemPicker;
        private GameState _firstStageState;
        private FirstStageModel _firstStageModel;
        private ItemsInventory _itemsInventory;
        private Inventory _inventory;
        private PreviewPanel _previewPanel;
        private InventoryItemPreviewPanel _inventoryItemPreview;
        public bool IsFromStageOpenMenu { get; private set; }

        private void Awake()
        {
            PlayerPrefs.DeleteKey("PreLoadStage");
            if (PlayerPrefs.HasKey("PreviosStage"))
            {
                string previousScene = PlayerPrefs.GetString("PreviosStage");
                if(previousScene != "Menu")
                {
                    IsFromStageOpenMenu = true;
                }
            }
            if (PlayerPrefs.HasKey("USDPStage"))
            {
                SceneManager.LoadScene("SecondStage");
            }
        }

        public void Init(PreviewPanel previewPanel, InventoryItemPreviewPanel inventoryItemPreviewPanel ,ItemsInventory itemsInventory,FirstStageItemPicker firstStageItemPicker, GameState firstStageState, FirstStageModel firstStageModel, Inventory inventory)
        {
            _previewPanel = previewPanel;
            _inventoryItemPreview = inventoryItemPreviewPanel;
            _itemsInventory = itemsInventory;
            _inventory = inventory;
            _itemPicker = firstStageItemPicker;
            _firstStageModel = firstStageModel;
            _firstStageState = firstStageState;

            ExamSystem.Instance.TakeDate();
        }

        public void Update()
        {
            if(_firstStageState.CurrentState == State.Selecting_Item)
            {
                if(Input.GetMouseButtonDown(0) && _itemPicker.TryPickItem(out Item item))
                {
                    _firstStageModel.SetCurrentItem(item);
                    _firstStageState.EnterInPreviewState();
                    item.TakedInPreview();
                    return;
                }
            }
            if(_firstStageState.CurrentState == State.Inventory)
            {
                if (Input.GetMouseButtonUp(0) && _itemPicker.TryPickInventoryItem(out InventoryItem inventoryItem))
                {
                    _firstStageModel.SetCurrentInventoryItem(inventoryItem);
                    _firstStageState.EnterInPreviewState();
                    return;
                }
            }
            if(_firstStageState.CurrentState == State.Preview)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (_inventoryItemPreview.IsOpen)
                        _inventoryItemPreview.Close();

                    if (_previewPanel.IsOpen)
                    {
                        _previewPanel.Close();
                    }
                }
            }

            //if (Input.GetKeyDown(KeyCode.G))
            //    AddScores();
        }

        public void AddScores()
        {
            int right = 0;
            int wrong = 0;
            string action = "";
            string notNecessary = "";
            bool isTakedNotNecesacy = false;
            //ExamSystem.Instance.AddExam(new Exam("СИЗ"));
            for (int i = 0; i < _obligatoryItems.Count; i++)
            {
                Debug.Log("item.Name  " + _obligatoryItems[i] + " " + _obligatoryItems[i].IsOverdue 
                    + " " + _obligatoryItems[i].IsDeffected + " " + _obligatoryItems[i].IsWrongReplace);

                if (_inventory.Items.Contains(_obligatoryItems[i]))
                {
                    if (!_obligatoryItems[i].IsDeffected && !_obligatoryItems[i].IsOverdue && !_obligatoryItems[i].IsWrongReplace)
                    {
                        //ExamSystem.Instance.AddExam("Да", _obligatoryItems[i].IdealAction,
                        //        "Правильно", _obligatoryItems[i].Name);
                        right++;
                        action += _obligatoryItems[i].Name;
                    }
                    else
                    {
                       // ExamSystem.Instance.AddExam("Нет", _obligatoryItems[i].IdealActionWithDeffect,
                       //         "Ошибка", _obligatoryItems[i].Name);
                    }
                }
                else
                {
                    wrong++;
                   // ExamSystem.Instance.AddExam("Нет", _obligatoryItems[i].IdealActionWithDeffect,
                     //           "Ошибка", _obligatoryItems[i].Name);
                }
            }

            foreach(var item in _inventory.Items)
            {
                if (!_obligatoryItems.Contains(item))
                {
                    Debug.Log("item.Name  " + item.Name + " " + item);
                    wrong++;
                    notNecessary += item.Name;
                    isTakedNotNecesacy = true;
                }
            }

            //if (isTakedNotNecesacy)
            //{
            //    ExamSystem.Instance.AddExam("Нет", "Взяты",
            //            "Ошибка", "Дополнительные предметы");
            //}
            //else
            //{
            //    ExamSystem.Instance.AddExam("Да", "Не взяты",
            //            "Правильно", "Дополнительные предметы");
            //}

            //if (_obligatoryItems.Count == right)
            //{
            //    ExamSystem.Instance.AddExam(0, 5, "2каски, 2 х/б перчаток, 2 диэлектрических перчаток, 2 комп. Инструмента, фонарь, очки, журнал ТБ, планшет с тетрадью, Парма ВАФ,CE602M, магнит, 2 газовых баллончика, комплект пломб, набор плакатов, секундомер, 2 указателя напряжения, аптечка, токоизмерительные клещи, боты, нагрузочное устройство ",
            //            action, "Комплектность");
            //}
            //else
            //{
            //    ExamSystem.Instance.AddExam(0, 0, "2каски, 2 х/б перчаток, 2 диэлектрических перчаток, 2 комп. Инструмента, фонарь, очки, журнал ТБ, планшет с тетрадью, Парма ВАФ, магнит, 2 газовых баллончика, комплект пломб, набор плакатов, секундомер, 2 указателя напряжения, аптечка, токоизмерительные клещи, боты, нагрузочное устройство ",
            //            action, "Комплектность");
            //}
        }

        public void Complite()
        {
            AddScores();

            _selectedInventoryItemsObject.SetItems(_inventory.Items);
            _selectedInventoryItemsObject.AddAllItemsToObligatory(_itemService.Items, _parent);

            foreach(var item in _inventory.Items)
            {
                item.transform.SetParent(_selectedInventoryItemsObject.transform);

            }
            SceneManager.LoadScene("SecondStage");
        }
        public void ReplaceItemInInventory()
        {
            if (_firstStageModel.CurrentItem.TryGetComponent(out DeffectedItemService deffectedItemService))
                deffectedItemService.OffDeffects();
            _inventory.AddItem(_firstStageModel.CurrentItem);
        }
        public void AddItemInInventory()
        {
            _inventory.AddItem(_firstStageModel.CurrentItem);
        }

        public void RemoveItemFromInventory()
        {
            _inventory.RemoveItem(_firstStageModel.CurrentItem);
        }

        public void ExitFromPreview()
        {
            _firstStageState.EnterInSelectingItemState();
        }
        public void ExitFromInventoryPreview()
        {
            _firstStageState.EnterInInventoryState();
        }

    }
}