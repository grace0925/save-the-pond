
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance; // buildmanager inside the buildmanager, stores a reference to itself

    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public UpgradeUI upgradeUI;

    public GameObject DuckiePrefab;
    public GameObject CrazyDuckiePrefab;
    public GameObject CaterpillarPrefab;

    public GameObject BoomEffect;


    void Awake() {
        // make sure there is only one instance
        if (instance != null) {
            Debug.Log("More than one BuildManager, not good bruh.");
        }
        instance = this; // this build manager is going to be put into the instance variable, which can be accessed from everywhere
    }

    public bool CanBuild { get { return turretToBuild != null; } }// only allow getting but not setting a variable, a property
    public bool HasMoney { get { return Stats.Money >= turretToBuild.cost; } }

    public void BuildTurret(Node node) {

        if (Stats.Money < turretToBuild.cost) {
            return;
        }

        Stats.Money -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetbuildPosition(), Quaternion.identity); // Quaternion.identity does not rotate the turret built at all
        node.turret = turret; // set gameobject turret from node as the turret that we just instantiated
        GameObject effect = (GameObject)Instantiate(BoomEffect, turret.transform.position, turret.transform.rotation);
        Destroy(effect, 4f);
    }


    public void SelectNode(Node node) {
        selectedNode = node;
        turretToBuild = null; // when we enable on of turret or node, we diasble the other

        upgradeUI.setNode(node);
    }

    public void SelectTurretToBuild(TurretBlueprint turret) {
        turretToBuild = turret;
        selectedNode = null;
    }
}