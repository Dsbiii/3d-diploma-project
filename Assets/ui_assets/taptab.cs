using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class taptab : MonoBehaviour
{
    EventSystem system;

    void Start ()
    {
        system = EventSystem.current;
    }
 
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = null;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
                if (next == null)
                    next = system.lastSelectedGameObject.GetComponent<Selectable>();
            }
            else
            {
                next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
                if (next == null)
                    next = system.firstSelectedGameObject.GetComponent<Selectable>();
            }
 
            if (next != null)
            {
 
                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null) inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret
 
                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }
        }
    }

// ============================= LISTER <L> ============================== 
/* 
    public  List<InputField> fields;
    int fieldIndexer;

    private void Update() {
            if (Input.GetKeyDown(KeyCode.Tab)) {
                
                 if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    {
                    fieldIndexer--;
                    }
                else 
                    {
                    fieldIndexer++;
                    }    

                    fields[fieldIndexer].Select();
                }
            }
*/
// ============================= LISTER END ============================== 

// =========================== SELECTABLE ================================= 
/*
        public Selectable[] UISelectables;
        private EventSystem eventSystem;

        private void Start() {
            eventSystem = FindObjectOfType<EventSystem>();
        }

        private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            for (int i = 0; i < UISelectables.Length; i++) {
                if(UISelectables[i].gameObject == eventSystem.currentSelectedGameObject) {
                UISelectables[(i+1) % UISelectables.Length].Select();
                break;
                }
                }
            }
        }
 */
// =========================== SELCTABLE END ============================= 

}
