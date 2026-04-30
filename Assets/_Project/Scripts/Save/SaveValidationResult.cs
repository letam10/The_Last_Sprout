namespace TheLastSprout.Save
{
    public enum SaveValidationResult
    {
        Valid,
        Corrupted,
        VersionMismatch,
        FileNotFound
    }
}
