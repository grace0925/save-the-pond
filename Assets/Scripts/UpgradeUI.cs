using System.Collections;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    private Node node;

    public void setNode(Node _node) {
        this.node = _node;
        transform.position = node.GetbuildPosition();
    }
}
