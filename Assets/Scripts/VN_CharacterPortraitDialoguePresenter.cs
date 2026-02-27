using UnityEngine;
using Yarn.Unity;

public sealed class VN_CharacterPortraitDialoguePresenter : DialoguePresenterBase
{
    [Header("References")]
    [SerializeField] private VN_CharacterDatabaseSO characterDatabase;
    [SerializeField] private VN_CharacterPortraitPresenter portraitPresenter;

    [Header("Debug")]
    [SerializeField] private bool debugLogs = false;

    public override YarnTask OnDialogueStartedAsync()
    {
        // Optional: clear portrait at start
        // portraitPresenter?.Hide();
        return YarnTask.CompletedTask;
    }

    public override YarnTask OnDialogueCompleteAsync()
    {
        // Optional: hide portrait at end
        // portraitPresenter?.Hide();
        return YarnTask.CompletedTask;
    }

    public override YarnTask RunLineAsync(LocalizedLine line, LineCancellationToken token)
    {
        ApplySpeakerFromLine(line);
        // React instantly; don't block dialogue.
        return YarnTask.CompletedTask;
    }

    private void ApplySpeakerFromLine(LocalizedLine line)
    {
        if (portraitPresenter == null || characterDatabase == null)
        {
            if (debugLogs) Debug.LogWarning("[VN Portrait Presenter] Missing references.", this);
            return;
        }

        var speaker = line.CharacterName;

        if (string.IsNullOrWhiteSpace(speaker))
        {
            portraitPresenter.Hide();
            return;
        }

        var def = characterDatabase.GetById(speaker);

        if (def != null)
        {
            portraitPresenter.Show(def);
        }
        else
        {
            if (debugLogs) Debug.LogWarning($"[VN Portrait Presenter] No character found for id '{speaker}'. Hiding.", this);
            portraitPresenter.Hide();
        }
    }
}