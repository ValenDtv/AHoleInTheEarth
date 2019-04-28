using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.CharacterController;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ViewItem3D : MonoBehaviour
{
    public GameObject GOCollector;
    private GameObjectCollector Collector;
    GameObject iw;

    private void Start()
    {
        Collector = GOCollector.GetComponent<GameObjectCollector>();
        iw = Collector.GameObjects.InventWindow;
    }

    public IEnumerator View(Item item)
    {
        //float distance = float.MaxValue;
        //Cell newCell = cell;
        //float temp;
        // x = distance <= temp ? false : true;
        print("работает4");
        vThirdPersonInput inputscr = Collector.GameObjects.Player.GetComponent<vThirdPersonInput>();
        //GameObject.Find("vThirdPersonController").GetComponent<vThirdPersonInput>();

        inputscr.disable = true;
        //distance = temp;
        //newCell = Inventary.content4[0];

        GameObject camk = null;
        Vector3 camkpos = new Vector3(0, 0);

        GameObject cam = Collector.GameObjects.CameraInv;
        //GameObject.Find("CameraInv");
        cam.GetComponent<Camera>().enabled = true;
        Vector3 campos = cam.transform.position;
        //GameObject.Find("CameraInv").transform.position;
        if (SceneManager.GetActiveScene().name == "Вторая локация")
        {
            camk = GameObject.Find("CameraKaps");
            camk.GetComponent<Camera>().enabled = true;
            camkpos = GameObject.Find("CameraKaps").transform.position;
        }

        string name = item.gameObject.name + "3D";
        GameObject obj3d = Instantiate(Resources.Load<GameObject>(name));
        Quaternion rotationY = Quaternion.AngleAxis(1, Vector3.up);
        obj3d.transform.SetPositionAndRotation(campos + Vector3.forward / 2, rotationY);



        GameObject tm = Collector.GameObjects.ThirdPersonCamera;
        //GameObject.Find("vThirdPersonCamera");
        //tm.GetComponent<CursorControl>().timing = 1.0f;
        Time.timeScale = 1.0f;
        print(obj3d);
        tm.GetComponent<CursorControl>().enabled = false;
        //canvas.GetComponentInParent<Canvas>().enabled = false;
        iw.SetActive(false);
        item.gameObject.GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(2.0f);
        //print(name);

        if (name == "Kapsula3D")
        {
            print("Это оно");


            GameObject objkaps = Instantiate(Resources.Load<GameObject>(name));
            objkaps.transform.SetPositionAndRotation(camkpos + Vector3.forward / 4, rotationY);

            camk.GetComponent<Camera>().enabled = true;
        }
        //camk.GetComponent<Camera>().enabled = false;
        Destroy(obj3d);

        inputscr.disable = false;
        //float t = new Time.deltaTime;
        tm.GetComponent<CursorControl>().enabled = true;
        //canvas.GetComponentInParent<Canvas>().enabled = true;
        iw.SetActive(true);
        item.gameObject.GetComponent<Image>().enabled = true;
        item.gameObject.SetActive(true);
        cam.GetComponent<Camera>().enabled = false;
        Time.timeScale = 0.0f;
        //print(tm);
        //cam.GetComponent<Camera>().enabled = false;
        /**/
        yield break;
    }
}
