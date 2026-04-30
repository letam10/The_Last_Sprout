namespace TheLastSprout.Save
{
    public struct SaveOperationResult
    {
        public bool Success;
        public string ErrorMessage;
        public SaveValidationResult ValidationResult;

        public static SaveOperationResult Ok() => new SaveOperationResult { Success = true, ValidationResult = SaveValidationResult.Valid };
        public static SaveOperationResult Error(string msg, SaveValidationResult reason) => new SaveOperationResult { Success = false, ErrorMessage = msg, ValidationResult = reason };
    }
}
