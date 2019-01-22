using UnityEngine.EventSystems;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Visual")]
    public Color hoverColor;
    private Color startColor;
    private Renderer rend;
    public Vector3 positionOffset;

    [Header("Unity Setup Fields")]
    public GameObject turret;

    BuildManager buildManager;

    void Start() {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    void OnMouseEnter() { // called once when mouse enters the collider(hover over the node)
        //check if an UI element is in the way
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        if (!buildManager.CanBuild) { // if no turrets no selected, disable hovering colours, if CanBuild returns true
            return;
        }

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor; // set color as hoverColor when mouse hovers over the node
        }
        else { // not enough money colour
            rend.material.color = Color.red;
        }
    }

    void OnMouseExit() {
        rend.material.color = startColor; // set node back to start colour when mouse exits the node
    }

    void OnMouseDown() { // called when mouse clicks
        if (!buildManager.CanBuild) {
            return;
        }
  
        if (turret != null) { // if a turret is already built
            buildManager.SelectNode(this);
            return;
        }

        buildManager.BuildTurret(this); // build turret on this node

    }

    public Vector3 GetbuildPosition() {
        return transform.position + positionOffset;
    }
}
