public class BTNode : MonoBehaviour
{
    protected List<BTNode> children = new List<BTNode>();

    public void AddChild(BTNode node)
    {
        children.Add(node);
    }
    
    public virtual BTNodeState Evaluate()
    {
        return BTNodeState.Failure;
    }
}
