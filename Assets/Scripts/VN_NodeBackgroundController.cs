using UnityEngine;
using Yarn.Unity;

public class VN_NodeBackgroundController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private VN_NodeBackgroundDatabase database;
    [SerializeField] private VN_BackgroundView view;

    [Header("Debug")]
    [SerializeField] private bool debugLogs = true;

    private void Reset()
    {
        dialogueRunner = FindFirstObjectByType<DialogueRunner>();
    }

    private void OnEnable()
    {
        if (dialogueRunner != null)
            dialogueRunner.onNodeStart.AddListener(HandleNodeStart);
    }

    private void OnDisable()
    {
        if (dialogueRunner != null)
            dialogueRunner.onNodeStart.RemoveListener(HandleNodeStart);
    }

    private void Start()
    {
        // No-op. We'll update backgrounds when nodes start via onNodeStart.
    }

    private void HandleNodeStart(string nodeName)
    {
        if (database == null || view == null)
            return;

        if (database.TryGetBackground(nodeName, out var sprite))
        {
            view.SetSprite(sprite);
            if (debugLogs) Debug.Log($"[VN] Background set for node '{nodeName}'", this);
        }
        else
        {
            if (debugLogs) Debug.Log($"[VN] No background mapped for node '{nodeName}'", this);
        }
    }
}