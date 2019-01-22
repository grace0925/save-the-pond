
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint duckie;
    public TurretBlueprint crazyDuckie;
    public TurretBlueprint caterpillar;


    BuildManager buildManager;

    void Start() {
        buildManager = BuildManager.instance;
    }

    public void SelectDuckie() {
        Debug.Log("Duckie selected");
        // if duckie is selected (user triggers onclick event), selectTurretToBuild() sets turretToBuild as DuckiePrefab
        // so Node.cs can call GetTurretToBuild() from BuildManager.cs and instantiate the duckie prefab
        buildManager.SelectTurretToBuild(duckie);
    }

    public void SelectCrazyDuckie() {
        Debug.Log("Crazy Duckie selected");
        buildManager.SelectTurretToBuild(crazyDuckie);
    }

    public void SelectCaterpillar() {
        Debug.Log("Caterpillar selected");
        buildManager.SelectTurretToBuild(caterpillar);
    }
}
