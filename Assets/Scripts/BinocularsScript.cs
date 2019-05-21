using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Invector.CharacterController;

public class BinocularsScript : MonoBehaviour
{
    private bool isUsed = false;
    public GameObject Camera;
    private Camera camera;
    public GameObject BinocularsView;
    public Image Numeral;
    private GameObjectCollector Collector;
    private Camera MainCamera;
    private vThirdPersonInput tpi;


    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.GetComponent<Camera>();
        Collector = GameObjectCollector.Collector.GetComponent<GameObjectCollector>();
        MainCamera = Collector.GameObjects.ThirdPersonCamera.GetComponent<Camera>();
        tpi = Collector.GameObjects.Player.GetComponent<vThirdPersonInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isUsed)
            if (Input.GetKeyUp(KeyCode.Mouse0))
                ChangeView();
    }

    private void ChangeView()
    {
        MainCamera.enabled = !MainCamera.enabled;
        camera.enabled = !camera.enabled;
        BinocularsView.SetActive(!BinocularsView.activeSelf);
        Numeral.enabled = !Numeral.enabled;
        tpi.disable = !tpi.disable;
        isUsed = !isUsed;
    }

    public void Action()
    {
        ChangeView();
    }
}
