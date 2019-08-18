using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerialPortChanger : MonoBehaviour
{
    public SerialController serialController;
    public Text consoleText;
    public InputField portInput;
    public GameObject canvasContainer;

    private void Start()
    {
        canvasContainer.SetActive(false);
    }

    public void ChangePort()
    {
        serialController.gameObject.SetActive(false);
        serialController.portName = portInput.text;
        serialController.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            canvasContainer.SetActive(!canvasContainer.activeSelf);
        }
    }
}
